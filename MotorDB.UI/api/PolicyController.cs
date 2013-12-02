﻿using System.Net;
using System.Net.Http;
using System.Web.Http;
using MotorDB.Core.Interfaces;
using MotorDB.Core.Models;

namespace MotorDB.UI.api
{
    [Route("api/policys")]
    public class PolicyController : BaseApiController
    {
        private readonly IPolicyRepository _policyRepository;

                  
        public PolicyController(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;    
        }

        /// <summary>
        /// Get a list of all policies
        /// </summary>
        /// <returns>List of <see cref="Policy"/></returns>
        public HttpResponseMessage Get()
        {
            var policyDataToReturn = _policyRepository.Get();
            var response = Request.CreateResponse(HttpStatusCode.OK, policyDataToReturn);
            return response;
        }

        /// <summary>
        /// Get an individual policy based on the policy identifier
        /// </summary>
        /// <param name="id">Required Policy Identifier</param>
        /// <returns>Returns an <see cref="Policy"/> object</returns>
        public HttpResponseMessage Get(int id)
        {
            var policyToReturn = _policyRepository.GetPolicyFor(id);
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