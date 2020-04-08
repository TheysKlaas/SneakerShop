using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Types.Input
{
    public class SupplierInputType : InputObjectGraphType
    {
        public SupplierInputType()
        {
            Name = "supplierInput";
            Field<NonNullGraphType<IdGraphType>>("SupplierId");
            Field<NonNullGraphType<StringGraphType>>("CompanyName");
        }
    }
}
