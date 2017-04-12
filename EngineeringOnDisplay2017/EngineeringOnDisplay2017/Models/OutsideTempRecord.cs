using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineeringOnDisplay2017.Models
{
    public class OutsideTempRecord
    {
        [Column(Order = 1)]
        public int OutsideTempRecordId { get; set; } //primary key for record

        [Column(Order = 2)]
        public DateTime RecordedDateTime { get; set; }  //time record was created

        [Column(Order = 3)]
        public float Temperature { get; set; } //Temperature in degrees F
        
        //public int BuildingId { get; set; } //Foriegn key for the Building record Id
        [Column(Order = 4)]
        public BuildingRecord BuildingRecord { get; set; }  //reference to that building record.
    }
}
