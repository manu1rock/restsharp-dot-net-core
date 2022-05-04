using System;
using System.Collections.Generic;
using System.Text;

namespace API.Automation.Models.Response
{
    public class CreateUserRes
    {
        public string name { get; set; }
        public string job { get; set; }
        public string id { get; set; }
        public DateTime createdAt { get; set; }
    }
}
