using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringOnDisplay2017.Models
{
    public interface ISensorRepository
    {
        BuildingRecord Building { get; set; }

        IEnumerable<ISensor> GetSensorRecords(SensorType sensorType);

        IEnumerable<ISensor> GetSensorRecords(SensorType sensorType, DateTime start, DateTime end);

        ISensor GetSensorRecordById(SensorType sensorType, int RecordId);

        GraphData QueryTests();

        string TestConsole();
    }
}
 