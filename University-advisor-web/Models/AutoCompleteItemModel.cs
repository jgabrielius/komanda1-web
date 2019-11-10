using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class AutoCompleteItemModel
    {
        public string Name { get; set; }
        public string AspAction { get; set; }
        public int ItemId { get; set; }
        public AutoCompleteItemModel(string name, string aspAction, int id) 
        {
            Name = name;
            AspAction = aspAction;
            ItemId = id;
        }
    }
}
