using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Moq;
using MotorDB.Core.Interfaces;
using MotorDB.Core.Models;
using MotorDB.UI.api;
using NUnit.Framework;
using Newtonsoft.Json;


namespace MotorDB.UI.Tests.ApiControllerTests
{
    [TestFixture]
    public class PolicyControllerTests
    {
        private MockList _mocks;
        private Mock<IPolicyRepository> _mockPolicyRepository;

        [SetUp]
        public void Setup()
        {
            _mocks = new MockList();
            _mockPolicyRepository = _mocks.Add(new Mock<IPolicyRepository>());
        }

        [TearDown]
        public void Teardown()
        {
            _mocks.VerifyAll();
            _mocks = null;
        }

        private PolicyController GetControllerForTests()
        {
            return new PolicyController(_mockPolicyRepository.Object).SetupControllerConfigs();
        }

        [Test]
        public void Can_Instantiate_Web_Api_Controller()
        {
            Assert.DoesNotThrow(() => GetControllerForTests());
        }

        [Test]
        public void When_Call_Get_Method_Then_Expected_Status_Code_Ok()
        {
            var controller = GetControllerForTests();
            var policyResult = controller.Get();
            Assert.That(policyResult, Is.Not.Null);
        }

        [Test]
        public void When_Call_Get_Then_Expected_Data_Is_Returned()
        {
            IList<Policy> mockDatabase = GetMockDatabase();
            _mockPolicyRepository.Setup(m => m.Get())
                                 .Returns(mockDatabase);
            var controller = GetControllerForTests();

            var response = controller.Get();

            var returnedListOfPolicy =
                response as OkNegotiatedContentResult<IList<Policy>>;
            Assert.That(returnedListOfPolicy, Is.Not.Null);
            Assert.That(returnedListOfPolicy.Content.Count, Is.EqualTo(mockDatabase.Count()));
        }

        [Test]
        public void When_Call_Get_With_Identifier_Then_Get_Expected_Data()
        {
            IList<Policy> mockDatabase = GetMockDatabase();
            _mockPolicyRepository.Setup(m => m.GetPolicyFor(It.IsAny<int>()))
                                 .Returns(mockDatabase.ToList().FirstOrDefault(m => m.Identifier == 1));
            var controller = GetControllerForTests();

            var response = controller.Get(1);

            var returnedPolicy = response as OkNegotiatedContentResult<Policy>;

            Assert.That(returnedPolicy.Content.Identifier, Is.EqualTo(1));
        }

        [Test]
        public void When_Call_Get_With_Invalid_Identifier_Then_Get_No_Data()
        {
            var controller = GetControllerForTests();

            var response = controller.Get(9999);
            Assert.That(response, Is.TypeOf<NotFoundResult>());
        }

        private IList<Policy> GetMockDatabase()
        {
            var policyDatabaseToReturn = new List<Policy>();

            var policy = new Policy
            {
                Identifier = 1,
                PolicyholderName = "Policyholder 1",
                PolicyNumber = "1001",
                PolicyPeriods = new List<PolicyPeriod>
                        {
                            new PolicyPeriod
                                {
                                    StartDate = DateTime.Parse("2011-02-01"),
                                    EndDate = DateTime.Parse("2012-01-31")
                                },
                            new PolicyPeriod
                                {
                                    StartDate = DateTime.Parse("2012-02-01"),
                                    EndDate = DateTime.Parse("2013-01-31")
                                },
                            new PolicyPeriod
                                {
                                    StartDate = DateTime.Parse("2013-02-01"),
                                    EndDate = DateTime.Parse("2014-01-31")
                                }
                        }
            };
            policyDatabaseToReturn.Add(policy);

            policy = new Policy
            {
                Identifier = 2,
                PolicyholderName = "Policyholder 2",
                PolicyNumber = "1002",
                PolicyPeriods = new List<PolicyPeriod>
                        {
                            new PolicyPeriod
                                {
                                    StartDate = DateTime.Parse("2012-05-01"),
                                    EndDate = DateTime.Parse("2013-04-30")
                                },
                            new PolicyPeriod
                                {
                                    StartDate = DateTime.Parse("2013-05-01"),
                                    EndDate = DateTime.Parse("2014-04-30")
                                }
                        }
            };
            policyDatabaseToReturn.Add(policy);

            policy = new Policy()
            {
                Identifier = 3,
                PolicyholderName = "Policyholder 3",
                PolicyNumber = "1003",
                PolicyPeriods = new List<PolicyPeriod>
                        {
                            new PolicyPeriod
                                {
                                    StartDate = DateTime.Parse("2013-06-01"),
                                    EndDate = DateTime.Parse("2014-05-31")
                                }
                        }
            };
            policyDatabaseToReturn.Add(policy);
            return policyDatabaseToReturn;
        }
    }
}
