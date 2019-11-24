using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_advisor_web.Constants;

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
                Information = Messages.uploadedDocumentIsValid + rate + "%";
                Successful = true;
            }
            else if (rate == -1)
            {
                Information = Messages.visionApiError;
                Successful = false;
            }
            else
            {
                Information = Messages.uploadedDocumentIsInvalid + rate + "%";
                Successful = false;
            }

        }
    }
}
