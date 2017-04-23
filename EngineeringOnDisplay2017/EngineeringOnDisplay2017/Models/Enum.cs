using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EngineeringOnDisplay2017.Models
{
    public enum SensorType
    {
        Electrical,
        NaturalGas,
        Water,
        OutsideTemperature
    }

    public enum SensorData
    {
       Amount,
       Change
    }

    public enum GraphScale
    {
        All,
        Hour,
        Day,
        Week,
        Month,
        Year
    }
}
