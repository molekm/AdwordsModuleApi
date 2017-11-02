using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace AdwordsModuleApi.Controllers.Api
{
    [Route("api/hello")]
    public class HelloController : ApiController
    {
        [HttpGet]
        public IHttpActionResult SayHello()
        {
            Person person = new Person
            {
                name = "Stefan",
                address = "Vesterled 47",
                age = 35
            };

            //var json = new JavaScriptSerializer().Serialize(person);

            return Ok(person);
        }
    }

    public class Person
    {
        public string name;
        public string address;
        public int age;
    }
}
