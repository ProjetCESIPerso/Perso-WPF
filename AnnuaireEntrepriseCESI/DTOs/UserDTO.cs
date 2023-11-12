using AnnuaireEntrepriseCESI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnuaireEntrepriseCESI.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhone { get; set; }
        public Service Service { get; set; }
        public Site Site { get; set; }
        public int ServiceId { get; set; }
        public int SiteId { get; set; }

    }
}
