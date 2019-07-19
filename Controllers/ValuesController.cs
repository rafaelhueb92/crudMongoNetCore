using System;
using System.Collections.Generic;
using crudMongoDB.Data.Context;
using crudMongoDB.models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace crudMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        cxMongoDB dbContext = new cxMongoDB();
        
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            try
            {
                List<User> listaUsuarios = dbContext.User.FindSync(m => true).ToList();
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
          
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(Guid id)
        {
            var usuario = dbContext.User.FindSync(m => m.Id == id).FirstOrDefault();
            return usuario;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] User user)
        {
            dbContext.User.InsertOne(user);
        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromBody] User user)
        {
            dbContext.User.ReplaceOne(m => m.Id == user.Id, user);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            dbContext.User.DeleteOne(m => m.Id == id);
        }
    }
}
