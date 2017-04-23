using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringOnDisplay2017.Models
{
    public class SensorRepository: ISensorRepository
    {
        private AppDbContext _appDbContext;

        /**
         * Construct a instance with a default building set to the EIB.
         * @param   appDbContext    the connection to the database.
         **/
        public SensorRepository(AppDbContext appDbContext)
        {
            //store the Context for the database.  Note Context is the glue to the database.  All calls for database go through AppDbContext for this case.  
            _appDbContext = appDbContext;

            //hard set the building to the EIB.  Will have to look how to program for this in the future. 
            Building = _appDbContext.BuildingRecords.FirstOrDefault(record => record.Acronym == "EIB");
        }
        /**
         * Get the ISensor record with a matching Id.
         * @param   sensorType  Enum for the sensors.  Electrical, NaturalGas, Water and OutsideTemperature
         * @param   recordId    Unique id of the record for selected sensorType.
         * @return              ISenor with matching the recordId
         **/
        public ISensor GetSensorRecordById(SensorType sensorType, int recordId)
        {
            //use a private method below to switch through the sensor types and return an IEnumerable collection to db.
            //will return an empty IEnumerable if sensorType is not in switch
            var records = getSenorRecordCollectionFromDb(sensorType);


            //query the collection
            var query = records.Where(r => r.Id == recordId)
                            .FirstOrDefault();

            //return the results of query
            return query;
        }

        /**
         * Get all sensor records for selected sensor type.
         * @param   sensorType  Enum for the sensors.  Electrical, NaturalGas, Water and OutsideTemperature
         * @return              IEnumerable collection of all the sensor records for the sensorType
         **/
        public IEnumerable<ISensor> GetSensorRecords(SensorType sensorType)
        {
            //get the sensor collection
            var records = getSenorRecordCollectionFromDb(sensorType);

            //query the collection
            var query = records.Where(r => r.Building == Building) //select the building.  default 'EIB'
                                .OrderBy(r => r.RecordedDateTime) //order results from filter by time inserted
                                .ToList();  //run query and output a list of sensor records 
   
            //return results
            return query;
        }

        /**
         * Get all sensor records for selected sensor type between start and end DateTimes.
         * @param   sensorType  Enum for the sensors.  Electrical, NaturalGas, Water and OutsideTemperature
         * @param   start       DateTime for start of the time frame (greater than or equal)
         * @param   end         DateTime for end of the time frame (less than or equal)
         * @return              IEnumerable collection of all the sensor records for the sensorType
         **/
        public IEnumerable<ISensor> GetSensorRecords(SensorType sensorType, DateTime start, DateTime end)
        {
            //get the sensor collection
            var records = getSenorRecordCollectionFromDb(sensorType);

            //query the collection
            var query = records.Where(r => r.Building == Building &&   // filter building
                                           r.RecordedDateTime >= start &&   //filter start
                                           r.RecordedDateTime <= end)   //filter end
                                .OrderBy(r => r.RecordedDateTime)  //order results from filter by time inserted
                                .ToList();  //run query and output a list of sensor records 

            //return results
            return query;
        }

        /**
         * query testbed 
         * @param   none    
         * @return          Graphdata with a set of points
         **/
         public Graph QueryTests()
        {
            var records = getSenorRecordCollectionFromDb(SensorType.Electrical);

            //query with a projection into a new graph points collection
            var queryResults_1 = records.Where(r => r.Id == 1)
                                      .Select(r => new GraphPoint()
                                      {
                                          X_Value = r.RecordedDateTime,
                                          Y_Value = r.Amount
                                      });

            var queryResults_2 =
                from record in records
                where record.Id == 1
                select new GraphPoint()
                {
                    X_Value = record.RecordedDateTime,
                    Y_Value = record.Amount
                };
            
            
            
            //return a new graph data with points set
            return new Graph() { Points = queryResults_1 };
        }
    
        public string TestConsole()
        {   
            //get a collection of electrical records to play with
            var records = getSenorRecordCollectionFromDb(SensorType.Electrical);

            //query for max record in database
            var query = records.OrderByDescending(r => r.RecordedDateTime)
                                .Take(1)
                                .Select(r => new GraphPoint
                                {
                                    X_Value=r.RecordedDateTime,
                                    Y_Value=r.Amount
                                });

            var output = "Max point for Electrical Usage Graph\n";

            foreach(var point in query)
            {
                //output += $"X_Value = {point.X_Value} : Y_Value = {point.Y_Value}\n";
            }

            output += "\n****************************************************************\n";
            output += "Max for each day test\n";

            //query max for each day
            var query2 = 
                        records
                            .OrderByDescending(r => r.RecordedDateTime)
                            .GroupBy(r => r.RecordedDateTime.Day)
                            .ToList(); //forces a query

            foreach(var group in query2)
            {
               var dayMax = 
                            group
                                .FirstOrDefault();

                output += $"Max Usage for {dayMax.RecordedDateTime.ToString()} is {dayMax.Amount} kiloWattHours\n"; 

            }
            output += "\n****************************************************************\n";
            output += "Max Usage each hour per day\n";

            var days =
                    records
                        .OrderBy(r => r.RecordedDateTime)
                        .GroupBy(r => r.RecordedDateTime.Date)
                        .ToList(); // force query to run

            var firstItem = days.FirstOrDefault().FirstOrDefault();             

            var previousDate = firstItem.RecordedDateTime.AddMinutes(1);
            var previousAmount = firstItem.Amount + 1;
            
            
            foreach (var day in days)
            {
                var hours =
                        from hour in day
                        group hour by hour.RecordedDateTime.Hour;

                foreach (var quarterHour in hours)
                {
                    var max = quarterHour.LastOrDefault();
                    var demand = (previousAmount - max.Amount) / previousDate.Subtract(max.RecordedDateTime).TotalHours;
                    previousDate = max.RecordedDateTime;
                    previousAmount = max.Amount;

                    output += $"Max demand per day / hour for {max.RecordedDateTime.ToString("MMM dd htt")} is {demand}kW\n";
                }

            }

            output += "\n****************************************************************\n";



            return output;
        }

        /**
         * Get the IEnumerable collection from the database corrosponding to the Enum SensorType
         * @param   sensorType  Enum for what type of sensor records to return
         * @return              The collection of all records for the table selected.  Returns empty collection if no sensorType is found.
         **/
        private IEnumerable<ISensor> getSenorRecordCollectionFromDb(SensorType sensorType)
        {
            switch (sensorType)
            {
                case SensorType.Electrical:
                    return _appDbContext.ElectricalRecords;
                case SensorType.NaturalGas:
                    return _appDbContext.NaturalGasRecords;
                case SensorType.OutsideTemperature:
                    return _appDbContext.OutsideTempRecords;
                case SensorType.Water:
                    return _appDbContext.WaterRecords;
            }
            //return an empty 
            return Enumerable.Empty<ISensor>();
        }
        //stores the building record to use in queries.  Currently build to only handel one building at a time.   
        public BuildingRecord Building { get; set; }
    }
}
