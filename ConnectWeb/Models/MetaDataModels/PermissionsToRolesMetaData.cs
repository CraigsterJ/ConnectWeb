using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
namespace ConnectWeb.Models.DataModels
{
    public partial class PermissionsToRolesMetaDataType
    {
        [Required(ErrorMessage = "Please select a permission.")]
        public int CurrentPermissionId { get; set; }

        [Required(ErrorMessage = "At least one role id is required.")]
        public List<string> CurrentRoleIds { get; set; }
    }
}
