using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringOnDisplay2017.Models
{
    public class WaterRecord
    {
        public int WaterRecordId { get; set; } //primary key for record
        public DateTime RecordedDateTime { get; set; }  //time record was created
        public float Usage { get; set; } //Water usage in Gallons
        public BuildingRecord BuildingRecord { get; set; }  //reference to that building record.
    }
}
