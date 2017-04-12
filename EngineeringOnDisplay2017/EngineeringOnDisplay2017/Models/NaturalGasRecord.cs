using System;

namespace EngineeringOnDisplay2017.Models
{
    public class NaturalGasRecord
    {
        public int Id { get; set; } //primary key for record
        public DateTime RecordedDateTime { get; set; }  //time record was created
        public float Usage { get; set; } //Natural Gas usage in ccf or 100 cubic feet of natural gas
        //public int BuildingId { get; set; } //Foriegn key for the Building record Id
        public BuildingRecord BuildingRecord { get; set; }  //reference to that building record.
    }
}
