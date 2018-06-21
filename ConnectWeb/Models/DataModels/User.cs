using System;
using System.Collections.Generic;

namespace ConnectWeb.Models.DataModels
{
    public partial class User
    {
        public int Id { get; set; }
        public Guid? UserUniqueId { get; set; }
        public int ApplicationId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool? Deleted { get; set; }
    }
}
