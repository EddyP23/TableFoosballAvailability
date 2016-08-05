using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.FreeFoosballInspector
{
    public interface IFreeFoosballInspector
    {
        bool HasChangedToFree();

        bool HasChangedToOccupied();
        
    }
}
