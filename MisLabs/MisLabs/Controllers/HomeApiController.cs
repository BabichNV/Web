using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using JsonRepository;
using MisLabs.Models;
using Newtonsoft.Json;

namespace MisLabs.Controllers
{
    public class HomeApiController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public IEnumerable<Account> GetAccounts()
        {
            JsonMethods rep = new JsonMethods();
            return rep.GetAccounts();
        }
        [HttpGet]
        public IEnumerable<Account> GetAccountsBetweenDates(string ldate, string rdate)
        {
            JsonMethods rep = new JsonMethods();
            var lms = ldate.Split('.');
            var rms = rdate.Split('.');
            return rep.GetAccountsBetweenDates(new DateTime(Convert.ToInt32(lms[2]), Convert.ToInt32(lms[1]), Convert.ToInt32(lms[0])),
                new DateTime(Convert.ToInt32(rms[2]), Convert.ToInt32(rms[1]), Convert.ToInt32(rms[0])));
        }

        [HttpGet]
        public IEnumerable<Account> GetLastMonthAccounts()
        {
            JsonRepository.JsonMethods rep = new JsonMethods();
            var ldate = DateTime.Now.AddMonths(-1);
            return rep.GetAccountsBetweenDates(ldate, DateTime.Now);
        }
        [HttpGet]
        public Account GetAccount(string id)
        {
            JsonMethods rep = new JsonMethods();
            return rep.GetAccount(Guid.Parse(id));
        }
        // POST api/<controller>
        [HttpPost]
        public void Create([FromBody]Account account)
        {
            string s = JsonConvert.SerializeObject(account);
            JsonMethods rep = new JsonMethods();
            rep.AddAccount(account);
        }

        [HttpPut]
        public void Update([FromBody]Account account)
        {
            JsonMethods rep = new JsonMethods();
            rep.UpdateAccount(account);
        }

        [HttpDelete]
        public void Delete(string id)
        {
            JsonMethods rep = new JsonMethods();
            rep.RemoveAccount(Guid.Parse(id));
        }
    }
}