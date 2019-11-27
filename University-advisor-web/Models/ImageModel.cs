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
        public ImageModel(){ }
        public ImageModel(int iconId, string imageAddress, string title, string authorCredit = "") {
            ImageId = iconId;
            ImageAddress = imageAddress;
            Title = title;
            AuthorCredit = authorCredit;
        }
        public List<ImageModel> GetAllIcons() 
        {
            var sqlSelectIcons = SqlDriver.Fetch("SELECT * FROM icons");
            var iconsList = new List<ImageModel>();
            foreach (var icon in sqlSelectIcons)
            {
                ImageId = Convert.ToInt32(icon["iconId"].ToString());
                ImageAddress = icon["iconAddress"].ToString();
                Title = icon["title"].ToString();
                AuthorCredit = icon["credit"].ToString();
                var newIcon = new ImageModel(ImageId, ImageAddress, Title, AuthorCredit);
                iconsList.Add(newIcon);
            }
            return iconsList;
        } 

    }
}
