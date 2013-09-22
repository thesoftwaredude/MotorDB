using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotorDB.Core.Models;

namespace MotorDB.Core.Interfaces
{
    public interface IPolicyRepository
    {
        IList<Policy> Get();
        Policy GetPolicyFor(int identifier);
    }
}
