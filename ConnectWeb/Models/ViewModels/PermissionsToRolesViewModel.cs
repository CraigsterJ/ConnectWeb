using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace ConnectWeb.Models.DataModels
{
    [ModelMetadataTypeAttribute(typeof(PermissionsToRolesMetaDataType))]
    public class PermissionsToRolesViewModel
    {
        public int ApplicationId { get; set; }
        public int CurrentPermissionId { get; set; }
        public List<string> CurrentRoleIds { get; set; }
        public List<Permission> PossiblePermissions { get; set; }
        public List<Role> PossibleRoles { get; set; }
        public List<RolePermission> ExistingRolePermission { get; set; }
        public PermissionsToRolesViewModel()
        {
            PossiblePermissions = new List<Permission>();
            PossibleRoles = new List<Role>();
            ExistingRolePermission = new List<RolePermission>();
        }
    }
}