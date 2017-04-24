using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringOnDisplay2017.Models
{
    public interface IGraphRepository
    {


        GraphPoints GetGraphPoints(SensorType sensorType);


        GraphPoints GetGraphPoints(DateTime start, DateTime end, SensorType sensorType, SensorData sensorData, GraphScale graphScale);












        BuildingRecord Building { get; set; }

    }
}
