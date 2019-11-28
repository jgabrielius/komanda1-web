using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class ImageModel
    {
        public int ImageId { get; set; }
        public string ImageAddress { get; set; }
        public string Title { get; set; }
        public string AuthorCredit { get; set; }
        public ImageModel()
        { 

        }
        public List<ImageModel> GetAllIcons() 
        {
            var sqlSelectIcons = SqlDriver.Fetch("SELECT * FROM icons");
            var iconsList = new List<ImageModel>();
            foreach (var icon in sqlSelectIcons)
            {
                var imageId = Convert.ToInt32(icon["iconId"].ToString());
                var imageAddress = icon["iconAddress"].ToString();
                var title = icon["title"].ToString();
                var authorCredit = icon["credit"].ToString();
                var newIcon = new ImageModel { 
                    ImageId = imageId, 
                    ImageAddress = imageAddress,
                    Title = title,
                    AuthorCredit = authorCredit,
                };
                iconsList.Add(newIcon);
            }
            return iconsList;
        } 

    }
}
