using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorDB.Core.Models
{
    public class Policy
    {
        public int Identifier { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyholderName { get; set; }
        public IList<PolicyPeriod> PolicyPeriods { get; set; }
    }
}
