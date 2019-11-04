using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace University_advisor_web.Interfaces
{
    public interface IRegistrationService
    {
        public List<SelectListItem> GetAllUniversities();
    }
}