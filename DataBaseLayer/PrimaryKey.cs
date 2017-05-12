using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    public class PrimaryKey : Constraints
    {
        private readonly string _schema;
        private readonly string _name;
        private string _table;

        public PrimaryKey(string schema, string name, string table)
        {
            _schema = schema;
            _name = name;
            _table = table;
        }

        public override string GenerateDDL()
        {
            return Index.GenerateDDL(_schema, _name);
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
