using System;
using System.Collections.Generic;

namespace ConnectWeb.Models.DataModels
{
    public partial class Application
    {
        public Application()
        {
            Permission = new HashSet<Permission>();
            Role = new HashSet<Role>();
        }

        public int ApplicationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Deleted { get; set; }

        public ICollection<Permission> Permission { get; set; }
        public ICollection<Role> Role { get; set; }
    }
}
