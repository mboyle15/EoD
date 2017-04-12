using System;
using System.Collections.Generic;
using System.Linq;

namespace EngineeringOnDisplay2017.Models
{
    public class ElectricalRepository : IElectricalRepository
    {
        private AppDbContext _appDbContext;

        public ElectricalRepository(AppDbContext appDbContext)
        {

            //store the Context for the database.  Note Context is the glue to the database.  All calls for database go through AppDbContext for this case.  
            _appDbContext = appDbContext;


            //hard set the building to the EIB.  Will have to look how to program for this in the future. 
            CurrentBuilding = _appDbContext.BuildingRecords.FirstOrDefault(b => b.Acronym == "EIB");
        }

        //property to contain which building is currently being looked at
        public BuildingRecord CurrentBuilding { get; set; }
  

        //get a Electrical record that matches the passed in electrical record id.
        public ElectricalRecord GetElectricalRecordById(int eletricalRecordId)
        {
            return _appDbContext.EletricalRecords.FirstOrDefault(e => e.ElectricalRecordId == eletricalRecordId);
        }

        //get all records in the eletrical table with or without specifing a building.
        public IEnumerable<ElectricalRecord> GetElectricalRecords()
        {
            //if no building specified
            if(CurrentBuilding == null)
            {
                //return the entire table of electrical records
                return _appDbContext.EletricalRecords.ToList();
            }

            //else return list of electrical records with currentbuilding specified
            return _appDbContext.EletricalRecords.Where(e => e.BuildingRecord == CurrentBuilding).ToList();
        }

        //get all electrial records in timeframe with or without concer for the current building
        public IEnumerable<ElectricalRecord> GetElectricalRecords(DateTime start, DateTime end)
        {
            //return a list of eletrical records within or equal to the timeframe without concern for current building.
            if(CurrentBuilding == null)
            {
                return _appDbContext.EletricalRecords.Where(e => e.RecordedDateTime >= start && e.RecordedDateTime <= end).ToList();
            }

            //else return a list of eletrical records within or equal to the timeframe with concern for current building.
            return _appDbContext.EletricalRecords.Where(e => e.RecordedDateTime >= start && e.RecordedDateTime <= end).ToList();
        }
    }
}
