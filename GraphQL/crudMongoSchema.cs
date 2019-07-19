using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudMongoDB.GraphQL
{
    public class crudMongoSchema: Schema
    {
        public crudMongoSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<crudMongoQuery>();
        }
    }
}
