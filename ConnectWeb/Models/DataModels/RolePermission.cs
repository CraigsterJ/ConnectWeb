using System;
using System.Collections.Generic;

namespace ConnectWeb.Models.DataModels
{
    public partial class RolePermission
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public Permission Permission { get; set; }
        public Role Role { get; set; }
    }
}
