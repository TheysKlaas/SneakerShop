using GraphQL.Types;
using SneakerShop.API.GraphQL.Types;
using SneakerShop.API.GraphQL.Types.NoSQLTypes;
using SneakerShop.NoSQLModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.NoSQL
{
    public class NoSQLQuery : ObjectGraphType
    {
        public NoSQLQuery(IOrderNoSQLRepo orderRepo)
        {
            try
            {
                Field<ListGraphType<NoSQLOrderType>>(
                  "orders",
                  resolve: context => orderRepo.GetAll()
                );
            }
            catch (Exception ex) { throw ex; }
        }

    }
}
