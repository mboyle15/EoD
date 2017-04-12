using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngineeringOnDisplay2017.Models;

namespace EngineeringOnDisplay2017.Models
{
    public class ElectricalRepository : IElectricalRepository
    {
        private AppDbContext _appDbContext;
        private BuildingRecord _buildingRecord;


        public ElectricalRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }

        public BuildingRecord CurrentBuilding
        {
            get
            {
                return _buildingRecord;
            }
            set
            {
                _buildingRecord = value;
            }
        }

        public void ClearBuilding()
        {
            _buildingRecord = null;
        }

        public ElectricalRecord GetElectricalRecordById(int recordId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ElectricalRecord> GetElectricalRecords()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ElectricalRecord> GetElectricalRecords(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
