using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
namespace ConnectWeb.Models.DataModels
{
    public partial class UserMetaDataType
    {
        [Display(Name ="User Name", Description = "Signon id for user")]
        [Required(ErrorMessage = "User name is required.")]
        [StringLength(100, ErrorMessage = "User name can not exceed 100 characters.")]
        public string UserName { get; set; }

        [Display(Name = "Full Name", Description ="Users real name")]
        [StringLength(250, ErrorMessage = "Full name can not exceed 250 characters.")]
        public string FullName { get; set; }
    }
}
