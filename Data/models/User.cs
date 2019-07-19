using System;
using System.ComponentModel.DataAnnotations;

namespace crudMongoDB.models
{
    public class User
    {

        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; }

        

    }
}
