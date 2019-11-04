using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Interfaces
{
    public interface IPasswordHasher
    {
        public string CreateMD5(string input);
    }
}
