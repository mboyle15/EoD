using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EngineeringOnDisplay2017.Models
{
    public class GraphRepository : IGraphRepository
    {
        //local copy of context
        private AppDbContext _appDbContext;

        /**
         * Constructor: gets a copy of the Context from dependancy injection.
         **/
        public GraphRepository(AppDbContext context)
        {
            _appDbContext = context;


            //assume the queried building is the EIB.  Can be set later if user wants a different building
            Building = context.BuildingRecords.Where(b => b.Acronym == "EIB").FirstOrDefault();
        }

       /**
       * Get all graph points in table for given sensor. According to the building, default EIB
       * @param   sensor      Enum for the sensor type (Electrical, NaturalGas, Water, OutsideTemperature)
       * @return              GraphPoint model to return a matched properties of time and value.
       **/
        public IEnumerable<GraphPoint> GetGraphPoints(SensorType sensorType)
        {
            var context = getSenorRecordCollectionFromDb(sensorType);

            var results =
                from record in context
                where record.Building == Building
                orderby record.RecordedDateTime
                select new GraphPoint()
                {
                    X_Value = record.RecordedDateTime,
                    Y_Value = record.Amount
                };

            return results.Take(20);
        }

        /**
         * Primary Get method for getting a graph model. Chooses a query based on parameters.
         * @param   sensor      Enum for the sensor type (Electrical, NaturalGas, Water, OutsideTemperature)
         * @param   start       DateTime for the beginin of the time frame. (Greater than or equal)
         * @param   end         DateTime for the end of the time frame. (Less than or equal)
         * @param   sensorData  Enum for type of data returned (amount "runing total" or change "how much amount changes in time period")
         * @param   pointScale  Enum for scaling the amount of points returned. (All, Hour, Day, Week, Month or Year) 
         * @return              GraphPoint model to return a matched properties of time and value.
         **/
        public IEnumerable<GraphPoint> GetGraphPoints(DateTime start, DateTime end, SensorType sensorType, SensorData sensorData, GraphScale graphScale)
        {
            //get the context for the sensorType
            var context = getSenorRecordCollectionFromDb(sensorType);

            //check if a sensor was found
            if (context.Any())
            {
                //check if sensor data was set to amount
                if (sensorData == SensorData.Amount)
                {
                    //switch on different scales
                    switch(graphScale)
                    {
                        case GraphScale.All:
                            return Enumerable.Empty<GraphPoint>();
                        case GraphScale.Hour:
                            return Enumerable.Empty<GraphPoint>();
                        case GraphScale.Day:
                            return Enumerable.Empty<GraphPoint>();
                        case GraphScale.Week:
                            return Enumerable.Empty<GraphPoint>();
                        case GraphScale.Month:
                            return Enumerable.Empty<GraphPoint>();
                        case GraphScale.Year:
                            return Enumerable.Empty<GraphPoint>();
                    }
                }
                //check for sensor is set to change
                else if(sensorData == SensorData.Change)
                {
                    //switch on different scales
                    switch (graphScale)
                    {
                        case GraphScale.All:
                            return Enumerable.Empty<GraphPoint>();
                        case GraphScale.Hour:
                            return Enumerable.Empty<GraphPoint>();
                        case GraphScale.Day:
                            return Enumerable.Empty<GraphPoint>();
                        case GraphScale.Week:
                            return Enumerable.Empty<GraphPoint>();
                        case GraphScale.Month:
                            return Enumerable.Empty<GraphPoint>();
                        case GraphScale.Year:
                            return Enumerable.Empty<GraphPoint>();
                    }

                }
            }
            //return an empty graph if context failed to load or query not currently supported
            return Enumerable.Empty<GraphPoint>();

        }

        /**
         * Gets a list of graph points for the query of all the amount data. 
         * @param   start           DateTime for the beginin of the time frame. (Greater than or equal)
         * @param   end             DateTime for the end of the time frame. (Less than or equal)
         * @param   sensorRecords   IEnumerable<ISensor> list this method will query against.
         * @return                  IEnumerable<GraphPoint> list of points for the graph
         **/
        private IEnumerable<GraphPoint> getGraphAmountAll(DateTime start, DateTime end, IEnumerable<ISensor> sensorRecords)
        {
            var query =
                from record in sensorRecords
                where record.RecordedDateTime >= start && record.RecordedDateTime <= end && record.Building == Building
                orderby record.RecordedDateTime
                select new GraphPoint()
                {
                    X_Value = record.RecordedDateTime,
                    Y_Value = record.Amount
                };

            return query;
        }

        /**
        * Gets a list of graph points for the query the Amount data per hour. 
        * @param   start           DateTime for the beginin of the time frame. (Greater than or equal)
        * @param   end             DateTime for the end of the time frame. (Less than or equal)
        * @param   sensorRecords   IEnumerable<ISensor> list this method will query against.
        * @return                  IEnumerable<GraphPoint> list of points for the graph
        **/
        private IEnumerable<GraphPoint> getGraphAmountDay(DateTime start, DateTime end, IEnumerable<ISensor> sensorRecords)
        {
            //var query =
            //        sensorRecords
            //            .Where(r => r.RecordedDateTime >= start && r.RecordedDateTime <= end)
            //            .OrderBy(r => r.RecordedDateTime)
            //            .GroupBy(r => r.RecordedDateTime.Date);

            //var results = new List<GraphPoint>();

            //foreach(var day in query)
            //{
            //    var dailyMax = day.LastOrDefault();

            //    results.Add(new GraphPoint()
            //    {
            //        X_Value = dailyMax.RecordedDateTime,
            //        Y_Value = dailyMax.Amount
            //    });
            //}




            var results =
                from record in sensorRecords
                where record.RecordedDateTime >= start && record.RecordedDateTime <= end
                orderby record.RecordedDateTime
                group record by record.RecordedDateTime.Date into dayGroup
                select new GraphPoint()
                {
                    X_Value = dayGroup.LastOrDefault().RecordedDateTime,
                    Y_Value = dayGroup.LastOrDefault().Amount
                };




            return results;
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
        
        /**
         * Property for the building. Constructor assumes it will be set to EIB. Needs to be changed for different building 
         **/
        public BuildingRecord Building { get; set; }

    }
}
