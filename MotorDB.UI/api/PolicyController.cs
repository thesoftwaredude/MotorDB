using System.Net;
using System.Net.Http;
using System.Web.Http;
using MotorDB.Core.Interfaces;

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

        //[Route("policys")]
        public HttpResponseMessage Get()
        {
            var policyDataToReturn = _policyRepository.Get();
            var response = Request.CreateResponse(HttpStatusCode.OK, policyDataToReturn);
            return response;
        }

        //[Route("policys/{id:int}")]
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