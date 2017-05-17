using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    public abstract class Constraints
    {
        public abstract string GenerateDDL();
        public abstract string GenerateDropDDL();
        public abstract string GenerateAlterTemplate();
    }
}
