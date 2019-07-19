using crudMongoDB.Data.Context;
using crudMongoDB.GraphQL.Types;
using crudMongoDB.models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using System.Text;
using System.Threading.Tasks;

namespace crudMongoDB.GraphQL
{
    public class crudMongoQuery: ObjectGraphType
    {
        public crudMongoQuery()
        {
            Field<ListGraphType<UserType>>("users",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<IdGraphType>
                    {
                        Name = "id"
                    },
                    new QueryArgument<DateGraphType>
                    {
                        Name = "Nome"
                    }
                }),
                resolve: context =>
                {

                    cxMongoDB dbContext = new cxMongoDB();
                    List<User> listaUsuarios = dbContext.User.FindSync(m => true).ToList();

                    return listaUsuarios.ToList();
                }
            );

        }
    }
}
