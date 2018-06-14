using System;
using System.Collections.Generic;

namespace ConnectWeb.Models
{
    public partial class Application
    {
        public int Id { get; set; }
        public Guid ApplicationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Deleted { get; set; }
    }
}
