using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    public static class ConstraintFactory
    {
        public static Constraints GetConstraint(string schema, string name, string table, string type)
        {
            switch (type)
            {
                case "P":
                    return new PrimaryKey(schema, name, table);

                case "F":
                    return new ForeignKey(schema, name, table);

                case "K":
                    return new Check(schema, name, table);

                default:
                    return null;
            }
        }
    }
}
