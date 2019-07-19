using crudMongoDB.models;
using GraphQL.Types;

namespace crudMongoDB.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.Id);
            Field(x => x.Nome);
        }
    }
}
