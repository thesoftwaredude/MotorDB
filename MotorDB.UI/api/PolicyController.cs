using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MotorDB.Core.Data;
using MotorDB.Core.Interfaces;
using MotorDB.Core.Models;

namespace MotorDB.UI.api
{
    public class PolicyController : BaseApiController
    {
        private readonly Lazy<IPolicyRepository> _policyRepository = new Lazy<IPolicyRepository>(() => new PolicyRepository());

        public PolicyController()
        {

        }

        public PolicyController(IPolicyRepository policyRepository)
        {
            _policyRepository = new Lazy<IPolicyRepository>(() => policyRepository);    
        }

        public HttpResponseMessage Get()
        {
            var policyDataToReturn = _policyRepository.Value.Get();
            var response = Request.CreateResponse(HttpStatusCode.OK, policyDataToReturn);
            return response;
        }


        public HttpResponseMessage Get(int id)
        {
            var policyToReturn = _policyRepository.Value.GetPolicyFor(id);
            if ( policyToReturn == null )
                return Request.CreateResponse(HttpStatusCode.NotFound);
            var response = Request.CreateResponse(HttpStatusCode.OK, policyToReturn);
            return response;

        }

        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}