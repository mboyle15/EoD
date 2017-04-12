using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineeringOnDisplay2017.Models
{
    public class NaturalGasRecord
    {
        [Column(Order = 1)]
        public int NaturalGasRecordId { get; set; } //primary key for record

        [Column(Order = 2)]
        public DateTime RecordedDateTime { get; set; }  //time record was created

        [Column(Order = 3)]
        public float Usage { get; set; } //Natural Gas usage in ccf or 100 cubic feet of natural gas

        [Column(Order = 4)]
        public BuildingRecord BuildingRecord { get; set; }  //reference to that building record.
    }
}
