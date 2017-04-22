using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringOnDisplay2017.Models
{
    public class BuildingRepository: IBuildingRepository
    {
        //local copy of context passed in by dependency injection
        private AppDbContext _appDbContext;

        //simple constructor that stores the local reference to appDbContext passed in though dependency injection
        //  and does a hard code set of 
        public BuildingRepository(AppDbContext appDbContext)
        {
            //store the Context for the database.  Note Context is the glue to the database.  All calls for database go through AppDbContext for this case.  
            _appDbContext = appDbContext;
        }


        //return all records in Building table
        public IEnumerable<BuildingRecord> GetBuildingRecords()
        {
            //return the entire table of records
            return _appDbContext.BuildingRecords.ToList();
          
        }

        //get a record that matches the passed in record id.
        public BuildingRecord GetBuildingRecordById(int recordId)
        {
            return _appDbContext.BuildingRecords.FirstOrDefault(record => record.BuildingRecordId == recordId);
        }

        //set a single new Building into the database
        public void SetBuildingRecord(BuildingRecord newBuilding)
        {
            //ToDo: need to make errorchecking here..

            _appDbContext.BuildingRecords.Add(newBuilding);
            _appDbContext.SaveChanges();
        }

        //set a list of new buildings into the database
        public void SetBuildingRecord(IEnumerable<BuildingRecord> newBuildings)
        {
            //Todo: need ot make sure this is valid data before adding
  
            _appDbContext.BuildingRecords.AddRange(newBuildings);
            _appDbContext.SaveChanges();

        }

    }
}
