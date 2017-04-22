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

    public enum GraphType
    {
       Usage,
       Demand
    }
}
