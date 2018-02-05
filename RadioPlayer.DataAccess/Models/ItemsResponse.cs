using System.Collections.Generic;
using RadioPlayer.DataAccess.Entities;

namespace RadioPlayer.DataAccess.Models
{
    public class ContactsResponse : BaseResponse
    {
        public List<StationEntity> Contacts { get; set; }
    }
}