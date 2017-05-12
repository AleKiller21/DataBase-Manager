using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    public class ForeignKey : Constraints
    {
        public ForeignKey(string schema, string name, string table)
        {
            throw new NotImplementedException();
        }

        public override string GenerateDDL()
        {
            throw new NotImplementedException();
        }

        public override string GenerateDropDDL()
        {
            throw new NotImplementedException();
        }

        public override string GenerateGenerateCreateTemplate()
        {
            throw new NotImplementedException();
        }

        public override string GenerateAlterTemplate()
        {
            throw new NotImplementedException();
        }
    }
}
