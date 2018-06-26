using System;
using System.Collections.Generic;

namespace ConnectWeb.Models.DataModels
{
    public partial class Role
    {
        public Role()
        {
            RolePermission = new HashSet<RolePermission>();
        }

        public int RoleId { get; set; }
        public int ApplicationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Deleted { get; set; }

        public Application Application { get; set; }
        public ICollection<RolePermission> RolePermission { get; set; }
    }
}
