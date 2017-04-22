using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringOnDisplay2017.Models
{
    public interface ISensor
    {
        int Id { get; set; }
        DateTime RecordedDateTime { get; set; }
        float Amount { get; set; }
        float Change { get; set; }
        BuildingRecord Building { get; set; }
    }
}
