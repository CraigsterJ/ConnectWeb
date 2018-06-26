using System;
using System.Collections.Generic;

namespace ConnectWeb.Models.DataModels
{
    public class PermissionsToRolesViewModel
    {
        public int ApplicationId { get; set; }
        public int CurrentPermissionId { get; set; }
        public List<string> CurrentRoleIds { get; set; }
        public List<Permission> PossiblePermissions { get; set; }
        public List<Role> PossibleRoles { get; set; }
        public List<RolePermissions> ExistingRolePermissions { get; set; }
        public PermissionsToRolesViewModel()
        {
            PossiblePermissions = new List<Permission>();
            PossibleRoles = new List<Role>();
            ExistingRolePermissions = new List<RolePermissions>();
        }
    }
}