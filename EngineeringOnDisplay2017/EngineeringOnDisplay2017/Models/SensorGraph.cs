using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.UI;
//using System.Web.Script.Serialization;

namespace EngineeringOnDisplay2017.Models
{
    public class SensorGraph
    {
        public string Type { get; set; } //the type of graph ie electricity, water, naturalgas, or outsidetemperature.  default replace graph area with error.

        public string Title { get; set; } //the title the graph

        public string YAxisLable { get; set; } //the y axis units to display left of the y axis

        public string IconUrl { get; set; } //the Url for the icon for the type of graph

        public string BackgroundColor { get; set; } //the color of the line in graph. example "rgba(255, 255, 0, 1) for bright yellow.

        public string BorderColor { get; set; } //the color of the fill area under line of graph. "rgba(255, 255, 0, .5) for less bright yellow

        public IEnumerable<String> XAxisValues { get; set; } //time lables for the graph from the DateTimeRecorded field, see how this looks in the file.

        public IEnumerable<float> YAxisValues { get; set; } //the list of values for the y axis.  the sensor recordings.  



        public static void GetSensorGraphFromSensorRecords(IEnumerable<ISensor> sensorRecords, int windowSize)
        {
            SensorGraph graph = new SensorGraph();
            int count = sensorRecords.Count();
            ISensor first = sensorRecords.First();
            ISensor last = sensorRecords.Last();
            List<string> xAxisValues = new List<string>();
            List<float> YAxisValues = new List<float>();
            TimeSpan timeDifference = last.RecordedDateTime.Subtract(first.RecordedDateTime);
            //string dateTimeFormat = "";


            //determine what type of graph of graph and title
            if(first is ElectricalRecord)
            {
                graph.Type = "Electrical";
                graph.Title = "Electrical Demand for " + first.Building.Acronym;
                graph.YAxisLable = "kiloWatts";
                
            }
            else if(first is WaterRecord)
            {
                graph.Type = "Water";
                graph.Title = "Water Demand for " + first.Building.Acronym;
                graph.YAxisLable = "Gallons per Hour";
            }
            else if (first is NaturalGasRecord)
            {
                graph.Type = "NaturalGas";
                graph.Title = "Natural Gas Demand for " + first.Building.Acronym;
                graph.YAxisLable = "100 cubic feet (ccf) per Hour";
            }
            else if (first is OutsideTempRecord)
            {
                graph.Type = "OutsideTemperature";
                graph.Title = "Outside Temperature for " + first.Building.Acronym;
                graph.YAxisLable = "Degrees Fahrenheit";
            }
            else
            {
                graph.Title = "Error: Unknown graph type";
            }
            
            //check what time of date to return to the browser




        }



    }

}
