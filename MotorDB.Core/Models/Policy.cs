using System.Collections.Generic;

namespace MotorDB.Core.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Policy
    {
        /// <summary>
        /// 
        /// </summary>
        public int Identifier { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PolicyNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PolicyholderName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IList<PolicyPeriod> PolicyPeriods { get; set; }
    }
}
