//using AdventureWorksModel;
using DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace RestApi.Controllers
{
    [RoutePrefix("contacts")]
    public class ContactsController : ApiController
    {
        private IData GetData()
        {
            //return new Data();
            return new LiteDBData(ConfigurationManager.AppSettings["DbPath"]);
            return new MockData();
        }

        // GET contacts
        // GET contacts/5 or contacts/call-list
        public JsonResult<string> Get()
        {
            var idArg = RequestContext.RouteData.Values["id"];
            // all contacts
            if (idArg == null)
            {
                var test = RequestContext.RouteData;
                var result = GetData().GetAllContacts();
                var json = JsonConvert.SerializeObject(result);
                return Json(json);
            }
            else
            {
                // call-list
                if (idArg.ToString() == "call-list")
                {
                    var result = GetData().GetCallList();
                    var json = JsonConvert.SerializeObject(result);
                    return Json(json);
                }
                // specific contact
                else if (int.TryParse(idArg.ToString(), out int id))
                {
                    var result = GetData().GetContact(id);
                    if (result != null)
                    {
                        var json = JsonConvert.SerializeObject(result);
                        return Json(json);
                    }
                }

                throw new HttpException(404, "not found");
            }
        }



        // POST contacts/5
        public void Post(int id, [FromBody] Contact contact)
        {
            GetData().Update(contact);
        }

        // PUT contacts/5
        public void Put(int id, [FromBody] Contact contact)
        {
            GetData().Create(contact);
        }

        // DELETE contacts/5
        public void Delete(int id)
        {
            GetData().Delete(id);
        }
    }
}
