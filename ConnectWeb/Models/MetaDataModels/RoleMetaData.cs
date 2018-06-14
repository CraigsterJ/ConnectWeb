using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
namespace ConnectWeb.Models.DataModels
{
    public partial class RoleMetaDataType
    {
        [Required(ErrorMessage = "Role name is required.")]
        [StringLength(100, ErrorMessage = "Name can not exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "Description can not exceed 250 characters.")]
        public string Description { get; set; }
    }
}
