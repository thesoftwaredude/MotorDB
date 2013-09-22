using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotorDB.Core.Interfaces;
using MotorDB.Core.Models;

namespace MotorDB.Core.Data
{
    public class PolicyRepository : IPolicyRepository
    {
        private static List<Policy> _database;

        public PolicyRepository()
        {
            _database = new List<Policy>();
            var policy = new Policy
                {
                    Identifier = 1,
                    PolicyholderName = "Policyholder 1",
                    PolicyNumber = "1001",
                    PolicyPeriods = new List<PolicyPeriod>()
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
            _database.Add(policy);

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
            _database.Add(policy);

            policy = new Policy
            {
                Identifier = 1,
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
            _database.Add(policy);
        }

        public IList<Policy> Get()
        {
            return _database;
        }

        public Policy GetPolicyFor(int identifier)
        {
            return _database.FirstOrDefault(f => f.Identifier == identifier);
        }
    }
}
