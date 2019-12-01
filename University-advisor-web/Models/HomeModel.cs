using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class HomeModel
    {
        public MapModel Map { get; set; } 
        public UserModel User { get; set; }

        public HomeModel() { }
        public HomeModel(UserModel user)
        {
            User = user;
        }
    }
}
