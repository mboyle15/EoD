using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineeringOnDisplay2017.Models
{
    public class WaterRecord
    {
        [Column(Order = 1)]
        public int WaterRecordId { get; set; } //primary key for record

        [Column(Order = 2)]
        public DateTime RecordedDateTime { get; set; }  //time record was created

        [Column(Order = 3)]
        public float Usage { get; set; } //Water usage in Gallons

        [Column(Order = 4)]
        public BuildingRecord BuildingRecord { get; set; }  //reference to that building record.
    }
}
