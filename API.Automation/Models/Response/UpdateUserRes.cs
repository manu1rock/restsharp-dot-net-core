using System;
using System.Collections.Generic;
using System.Text;

namespace API.Automation.Models.Response
{
    public class UpdateUserRes
    {
        public string name { get; set; }
        public string job { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
