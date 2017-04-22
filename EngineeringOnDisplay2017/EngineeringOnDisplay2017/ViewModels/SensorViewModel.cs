using System.Collections.Generic;
using EngineeringOnDisplay2017.Models;

namespace EngineeringOnDisplay2017.ViewModels
{
    public class SensorViewModel
    {
        public IEnumerable<ISensor> SensorRecords { get; set; }
    }
}
