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
        * @return              GraphPoints is a class that takes the parameter points and splits into to independent arrays.  
        **/
        public GraphPoints GetGraphPoints(SensorType sensorType)
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

            return convertEnumerableToGraphPoints(results.Take(20));
        }

        /**
         * Primary Get method for getting a graph model. Chooses a query based on parameters.
         * @param   sensor      Enum for the sensor type (Electrical, NaturalGas, Water, OutsideTemperature)
         * @param   start       DateTime for the beginin of the time frame. (Greater than or equal)
         * @param   end         DateTime for the end of the time frame. (Less than or equal)
         * @param   sensorData  Enum for type of data returned (amount "runing total" or change "how much amount changes in time period")
         * @param   pointScale  Enum for scaling the amount of points returned. (All, Hour, Day, Week, Month or Year) 
         * @return              GraphPoints is a class that takes the parameter points and splits into to independent arrays.  
         **/
        public GraphPoints GetGraphPoints(DateTime end, int numTicks, SensorType sensorType, SensorData sensorData, GraphScale graphScale)
        {

            //incase numTicks was not set then set to default
            if (numTicks <= 0)
            {
                numTicks = DEFAULT_NUM_TICKS;
            }

            //holds the results for the context
            var points = Enumerable.Empty<GraphPoint>();

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
                            points = getGraphAmountAll(end, numTicks, context);
                            break;
                        case GraphScale.Hour:
                            points = getGraphAmountHour(end, numTicks, context);
                            break;
                        case GraphScale.Day:
                            points = getGraphAmountDay(end, numTicks, context);
                            break;
                        case GraphScale.Month:
                            points = getGraphAmountMonth(end, numTicks, context);
                            break;
                        case GraphScale.Year:
                            points = getGraphAmountYear(end, numTicks, context);
                            break;
                    }
                }
                //check for sensor is set to change
                else if(sensorData == SensorData.Change)
                {
                    //switch on different scales
                    switch (graphScale)
                    {
                        case GraphScale.All:
                            points = getGraphChangeAll(end, numTicks, context);
                            break;

                        case GraphScale.Hour:
                            points = getGraphChangeHour(end, numTicks, context);
                            break;

                        case GraphScale.Day:
                            points = getGraphChangeDay(end, numTicks, context);
                            break;                            
                        case GraphScale.Month:
                            points = getGraphChangeMonth(end, numTicks, context);
                            break;
                        case GraphScale.Year:
                            points = getGraphChangeYear(end, numTicks, context);
                            break;
                    }

                }
            }
            //return a converted IEnumerable to a GraphPoint object
            return convertEnumerableToGraphPoints(points);
        }

        /**
         * Gets a list of graph points for the query of all the amount data. 
         * @param   start           DateTime for the beginin of the time frame. (Greater than or equal)
         * @param   end             DateTime for the end of the time frame. (Less than or equal)
         * @param   sensorRecords   IEnumerable<ISensor> list this method will query against.
         * @return                  IEnumerable<GraphPoint> list of points for the graph
         **/
        private IEnumerable<GraphPoint> getGraphAmountAll(DateTime end, int numTicks, IEnumerable<ISensor> sensorRecords)
        {

            //incase numTicks was not set or 
          

            var start = end.AddMinutes(-15*numTicks);

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
        * Gets a list of graph points for change of all the amount data. 
        * @param   start           DateTime for the beginin of the time frame. (Greater than or equal)
        * @param   end             DateTime for the end of the time frame. (Less than or equal)
        * @param   sensorRecords   IEnumerable<ISensor> list this method will query against.
        * @return                  IEnumerable<GraphPoint> list of points for the graph
        **/
        private IEnumerable<GraphPoint> getGraphChangeAll(DateTime end, int numTicks, IEnumerable<ISensor> sensorRecords)
        {

            //incase numTicks was not set or 


            var start = end.AddMinutes(-15 * (numTicks + 1)); //go back one more time then nessicary so you can trim off the dud first record

            var previousTime = DateTime.Now;
            var previousAmount = float.MinValue;
            var result = new List<GraphPoint>();

            var query =
                from record in sensorRecords
                where record.RecordedDateTime >= start && record.RecordedDateTime <= end && record.Building == Building
                orderby record.RecordedDateTime
                select record;


            

            foreach (var line in query)
            {
                result.Add(new GraphPoint()
                {
                    X_Value = line.RecordedDateTime,
                    Y_Value = (float)((line.Amount - previousAmount) / line.RecordedDateTime.Subtract(previousTime).TotalHours)
                });

                previousTime = line.RecordedDateTime;
                previousAmount = line.Amount;
            }

            //skip the first result because it will be bad.  fake previous data
            return result.Skip(1);
        }

        /**
      * Gets a list of max hour amounts converted into graphPoint. 
      * @param   start           DateTime for the beginin of the time frame. (Greater than or equal)
      * @param   end             DateTime for the end of the time frame. (Less than or equal)
      * @param   sensorRecords   IEnumerable<ISensor> list this method will query against.
      * @return                  IEnumerable<GraphPoint> list of points for the graph
      **/
        private IEnumerable<GraphPoint> getGraphAmountHour(DateTime end, int numTicks, IEnumerable<ISensor> sensorRecords)
        {
            //add calculate the start time based on number of ticks
            var start = end.AddHours(-numTicks);

            //get a grouping of days ordered by RecordedDateTime.  
            var days =
                from record in sensorRecords
                where record.RecordedDateTime >= start && record.RecordedDateTime <= end && record.Building == Building
                orderby record.RecordedDateTime
                group record by record.RecordedDateTime.Date;

            //create a list to hold results
            var result = new List<GraphPoint>();


            //dive into each day grouping
            foreach (var day in days)
            {   
                //group each hour in the day
                var hourGrp =
                    from hours in day
                    group hours by hours.RecordedDateTime.Hour; 


                //dive into each hour grouping
                foreach(var fifteenMin in hourGrp)
                {
                    //get the max record each hour, assumed it is the last
                    var maxFifteenMin = fifteenMin.LastOrDefault();

                    //create a new graph point with info and add to results list
                    result.Add(new GraphPoint()
                    {
                        X_Value = maxFifteenMin.RecordedDateTime,
                        Y_Value = maxFifteenMin.Amount

                    });
                }
            }
            return result;
        }

        /**
        * Gets a list of max hour amounts converted into graphPoint. 
        * @param   start           DateTime for the beginin of the time frame. (Greater than or equal)
        * @param   end             DateTime for the end of the time frame. (Less than or equal)
        * @param   sensorRecords   IEnumerable<ISensor> list this method will query against.
        * @return                  IEnumerable<GraphPoint> list of points for the graph
        **/
        private IEnumerable<GraphPoint> getGraphChangeHour(DateTime end, int numTicks, IEnumerable<ISensor> sensorRecords)
        {
            //add calculate the start time based on number of ticks
            var start = end.AddHours(-(numTicks + 1));

            var previousTime = DateTime.Now;
            var previousAmount = float.MinValue;
            var result = new List<GraphPoint>();

            //get a grouping of days ordered by RecordedDateTime.  
            var days =
                from record in sensorRecords
                where record.RecordedDateTime >= start && record.RecordedDateTime <= end && record.Building == Building
                orderby record.RecordedDateTime
                group record by record.RecordedDateTime.Date;

            //dive into each day grouping
            foreach (var day in days)
            {
                //group each hour in the day
                var hourGrp =
                    from hours in day
                    group hours by hours.RecordedDateTime.Hour;


                //dive into each hour grouping
                foreach (var fifteenMin in hourGrp)
                {
                    //get the max record each hour, assumed it is the last
                    var maxFifteenMin = fifteenMin.LastOrDefault();

                    //create a new graph point with info and add to results list
                    result.Add(new GraphPoint()
                    {
                        X_Value = maxFifteenMin.RecordedDateTime,
                        Y_Value = (float)((maxFifteenMin.Amount - previousAmount) / maxFifteenMin.RecordedDateTime.Subtract(previousTime).TotalHours)

                    });
                    previousTime = maxFifteenMin.RecordedDateTime;
                    previousAmount = maxFifteenMin.Amount;
                }
            }
            //skip the first result because it will be bad.  fake previous data
            return result.Skip(1);
        }

        /**
         * Gets a list of graph points for the query the Amount data per day. 
         * @param   start           DateTime for the beginin of the time frame. (Greater than or equal)
         * @param   end             DateTime for the end of the time frame. (Less than or equal)
         * @param   sensorRecords   IEnumerable<ISensor> list this method will query against.
         * @return                  IEnumerable<GraphPoint> list of points for the graph
         **/
        private IEnumerable<GraphPoint> getGraphAmountDay(DateTime end, int numTicks, IEnumerable<ISensor> sensorRecords)
        {

            var start = end.AddDays(-numTicks);

            var results =
                from record in sensorRecords
                where record.RecordedDateTime >= start && record.RecordedDateTime <= end && record.Building == Building
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
       * Gets a list of graph points for the query the Amount data per day. 
       * @param   start           DateTime for the beginin of the time frame. (Greater than or equal)
       * @param   end             DateTime for the end of the time frame. (Less than or equal)
       * @param   sensorRecords   IEnumerable<ISensor> list this method will query against.
       * @return                  IEnumerable<GraphPoint> list of points for the graph
       **/
        private IEnumerable<GraphPoint> getGraphChangeDay(DateTime end, int numTicks, IEnumerable<ISensor> sensorRecords)
        {

            var start = end.AddDays(-(numTicks + 1));

            var previousTime = DateTime.Now;
            var previousAmount = float.MinValue;
            var result = new List<GraphPoint>();

            var dayGrp =
                from record in sensorRecords
                where record.RecordedDateTime >= start && record.RecordedDateTime <= end && record.Building == Building
                orderby record.RecordedDateTime
                group record by record.RecordedDateTime.Date;

            foreach (var day in dayGrp)
            {
                var dayMax = day.LastOrDefault();

                result.Add(new GraphPoint()
                {
                    X_Value = dayMax.RecordedDateTime,
                    Y_Value = (float)((dayMax.Amount - previousAmount) / dayMax.RecordedDateTime.Subtract(previousTime).TotalHours)
                });

                previousTime = dayMax.RecordedDateTime;
                previousAmount = dayMax.Amount;
            };

            //skip the first result because it will be bad.  fake previous data
            return result.Skip(1);
        }

        /**
        * Gets a list of max record per month converted graphPoint. 
        * @param   start           DateTime for the beginin of the time frame. (Greater than or equal)
        * @param   end             DateTime for the end of the time frame. (Less than or equal)
        * @param   sensorRecords   IEnumerable<ISensor> list this method will query against.
        * @return                  IEnumerable<GraphPoint> list of points for the graph
        **/
        private IEnumerable<GraphPoint> getGraphAmountMonth(DateTime end, int numTicks, IEnumerable<ISensor> sensorRecords)
        {
            //add calculate the start time based on number of ticks
            var start = end.AddMonths(-numTicks);

            //get a grouping of days ordered by RecordedDateTime.  
            var years =
                from record in sensorRecords
                where record.RecordedDateTime >= start && record.RecordedDateTime <= end && record.Building == Building
                orderby record.RecordedDateTime
                group record by record.RecordedDateTime.Year;

            //create a list to hold results
            var result = new List<GraphPoint>();


            //dive into each day grouping
            foreach (var year in years)
            {
                //group each hour in the day
                var MonthGrp =
                    from months in year
                    group months by months.RecordedDateTime.Month;


                //dive into each hour grouping
                foreach (var month in MonthGrp)
                {
                    //get the max record each hour, assumed it is the last
                    var maxRecordMonth = month.LastOrDefault();

                    //create a new graph point with info and add to results list
                    result.Add(new GraphPoint()
                    {
                        X_Value = maxRecordMonth.RecordedDateTime,
                        Y_Value = maxRecordMonth.Amount

                    });
                }

            }
            return result;
        }

        /**
        * Gets a list of max record per month converted graphPoint. 
        * @param   start           DateTime for the beginin of the time frame. (Greater than or equal)
        * @param   end             DateTime for the end of the time frame. (Less than or equal)
        * @param   sensorRecords   IEnumerable<ISensor> list this method will query against.
        * @return                  IEnumerable<GraphPoint> list of points for the graph
        **/
        private IEnumerable<GraphPoint> getGraphChangeMonth(DateTime end, int numTicks, IEnumerable<ISensor> sensorRecords)
        {
            //add calculate the start time based on number of ticks
           var start = end.AddMonths(-(numTicks + 1));

            var previousTime = DateTime.Now;
            var previousAmount = float.MinValue;
            var result = new List<GraphPoint>();


            //get a grouping of days ordered by RecordedDateTime.  
            var years =
                from record in sensorRecords
                where record.RecordedDateTime >= start && record.RecordedDateTime <= end && record.Building == Building
                orderby record.RecordedDateTime
                group record by record.RecordedDateTime.Year;


            //dive into each day grouping
            foreach (var year in years)
            {
                //group each hour in the day
                var MonthGrp =
                    from months in year
                    group months by months.RecordedDateTime.Month;


                //dive into each hour grouping
                foreach (var month in MonthGrp)
                {
                    //get the max record each hour, assumed it is the last
                    var maxRecordMonth = month.LastOrDefault();

                    //create a new graph point with info and add to results list
                    result.Add(new GraphPoint()
                    {
                        X_Value = maxRecordMonth.RecordedDateTime,
                        Y_Value = (float)((maxRecordMonth.Amount - previousAmount) / maxRecordMonth.RecordedDateTime.Subtract(previousTime).TotalHours)

                    });

                    previousTime = maxRecordMonth.RecordedDateTime;
                    previousAmount = maxRecordMonth.Amount;
                }
            }
            //skip the first result because it will be bad.  fake previous data
            return result.Skip(1);
        }

        /**
        * Gets a list of max recors per year converted to graphPoint 
        * @param   start           DateTime for the beginin of the time frame. (Greater than or equal)
        * @param   end             DateTime for the end of the time frame. (Less than or equal)
        * @param   sensorRecords   IEnumerable<ISensor> list this method will query against.
        * @return                  IEnumerable<GraphPoint> list of points for the graph
        **/
        private IEnumerable<GraphPoint> getGraphAmountYear(DateTime end, int numTicks, IEnumerable<ISensor> sensorRecords)
        {

            var start = end.AddYears(-numTicks);

            var results =
                from record in sensorRecords
                where record.RecordedDateTime >= start && record.RecordedDateTime <= end && record.Building == Building
                orderby record.RecordedDateTime
                group record by record.RecordedDateTime.Year into yearGroup
                select new GraphPoint()
                {
                    X_Value = yearGroup.LastOrDefault().RecordedDateTime,
                    Y_Value = yearGroup.LastOrDefault().Amount
                };

            return results;
        }

        /**
        * Gets a list of max recors per year converted to graphPoint 
        * @param   start           DateTime for the beginin of the time frame. (Greater than or equal)
        * @param   end             DateTime for the end of the time frame. (Less than or equal)
        * @param   sensorRecords   IEnumerable<ISensor> list this method will query against.
        * @return                  IEnumerable<GraphPoint> list of points for the graph
        **/
        private IEnumerable<GraphPoint> getGraphChangeYear(DateTime end, int numTicks, IEnumerable<ISensor> sensorRecords)
        {
            //add calculate the start time based on number of ticks
            var start = end.AddYears(-(numTicks + 1));

            var previousTime = DateTime.Now;
            var previousAmount = float.MinValue;
            var result = new List<GraphPoint>();

            var yearGrp =
                from record in sensorRecords
                where record.RecordedDateTime >= start && record.RecordedDateTime <= end && record.Building == Building
                orderby record.RecordedDateTime
                group record by record.RecordedDateTime.Year;

            //dive into each hour grouping
            foreach (var year in yearGrp)
            {
                //get the max record each hour, assumed it is the last
                var maxRecordYear = year.LastOrDefault();

                //create a new graph point with info and add to results list
                result.Add(new GraphPoint()
                {
                    X_Value = maxRecordYear.RecordedDateTime,
                    Y_Value = (float)((maxRecordYear.Amount - previousAmount) / maxRecordYear.RecordedDateTime.Subtract(previousTime).TotalHours)

                });
                previousTime = maxRecordYear.RecordedDateTime;
                previousAmount = maxRecordYear.Amount;
            }
            //skip the first result because it will be bad.  fake previous data
            return result.Skip(1);
        }

        /**
         * Converts a IEnumerable<GraphPoints> into a single class with arrays for both xAxis and yAxis. Needed to make Ajax easier.
         * @param   points  IEnumerable<GraphPoint> A list of points generated by a query method. 
         * @return          GraphPoints is a class that takes the parameter points and splits into to independent arrays.  
         **/
        private GraphPoints convertEnumerableToGraphPoints(IEnumerable<GraphPoint> points)
        {
            //check if IEnumerable is empty. 
            if (!points.Any())
            {
                //if empty then return an empty 
                return new GraphPoints();
            }
            //list of x and y points to put into the GraphPoints.  Splits up each axis.
            var xPoints = new List<string>();
            var yPoints = new List<float>();


            //loop through the IEnumerable to do the separation.
            foreach (var point in points)
            {
                xPoints.Add(point.X_Value.ToString("yyyy-MM-dd HH:mm"));
                yPoints.Add(point.Y_Value);
            }

            //return a filled graph points class.  
            return new GraphPoints() { XAxis = xPoints, YAxis = yPoints };
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

        public const int DEFAULT_NUM_TICKS = 20;


    }
}
