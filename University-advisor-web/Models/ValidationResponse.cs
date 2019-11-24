using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class ValidationResponse
    {
        public bool Successful { get; private set; }
        public string Information { get; private set; }
        public void SetInformation(int rate)
        {
            if (rate > 40)
            {
                Information = "Uploaded document is valid. Match is: " + rate + "%";
                Successful = true;
            }
            else if (rate == -1)
            {
                Information = "Something is not right with Vision API. Try again later";
                Successful = false;
            }
            else
            {
                Information = "Uploaded document is invalid. Match is only: " + rate + "%";
                Successful = false;
            }

        }
    }
}
