using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SnowmireMVC.BusinessLayer;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Web.Http.Results;
using SnowmireMVC.Models;
using MyAccount.Infrastructure;

namespace SnowmireMVC.Controllers
{
    public class ValuesController : ApiController
    {
        private SchoolContext db = new SchoolContext();
        // GET api/values
        [CustomAuthorize("SuperAdmin")]
        public HttpResponseMessage Get()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            List<Student> asd = new List<Student>();
            foreach(var item in db.Students)
            {
                asd.Add(item);
            }
            var jsonString = serializer.Serialize(asd);

            var res = Request.CreateResponse(HttpStatusCode.OK);

            res.Content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

            return res;
        }

        // GET api/values/5
        [CustomAuthorize("SuperAdmin")]
        public HttpResponseMessage Get(int id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var myobject = db.Students.FirstOrDefault(x => x.ID == id);
            Student student = new Student()
            {
                ID = myobject.ID,
                Level = myobject.Level,
                Name = myobject.Name,
                Title = myobject.Title
            };
            var jsonString = serializer.Serialize(student);

            var res = Request.CreateResponse(HttpStatusCode.OK);

            res.Content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

            return res;
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
