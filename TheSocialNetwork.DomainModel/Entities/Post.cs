using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Interfaces.Repositories;

namespace TheSocialNetwork.DomainModel.Entities
{
    public class Post : EntityBase
    {
        public Profile Sender { get; set; }
        public Profile Recipient { get; set; }

        public MarketPlace MarketPlace { get; set; }

        public DateTime PublishDateTime { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public string PhotoAdvertisement { get; set; }

        public Post()
        {
            PublishDateTime = DateTime.Now;
        }
    }
}
