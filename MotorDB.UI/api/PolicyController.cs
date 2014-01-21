using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
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
        /// <returns>Returns a list of <see cref="Policy" /> objects</returns>
        [ResponseType(typeof(Policy))]
        public IHttpActionResult Get()
        {
            var policyDataToReturn = _policyRepository.Get();
            return Ok(policyDataToReturn);
            //var response = Request.CreateResponse(HttpStatusCode.OK, policyDataToReturn);
            //return response;
        }

        /// <summary>
        /// Get an individual policy based on the policy identifier
        /// </summary>
        /// <param name="id">Required Policy Identifier</param>
        /// <returns>Returns a <see cref="Policy"/> object</returns>
        [ResponseType(typeof(Policy))]
        public IHttpActionResult Get(int id)
        {
            var policyToReturn = _policyRepository.GetPolicyFor(id);
            if (policyToReturn == null)
                return NotFound();
            return Ok(policyToReturn);
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
        public void Post([FromBody]Policy policy)
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