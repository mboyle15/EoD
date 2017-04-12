using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringOnDisplay2017.Models
{
    public class WaterRecord
    {
        public int Id { get; set; } //primary key for record
        public DateTime RecordedDateTime { get; set; }  //time record was created
        public float Usage { get; set; } //Water usage in Gallons
        //public int BuildingId { get; set; } //Foriegn key for the Building record Id
        public BuildingRecord BuildingRecord { get; set; }  //reference to that building record.
    }
}
