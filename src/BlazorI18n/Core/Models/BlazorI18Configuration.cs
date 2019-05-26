using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorI18n.Core.Models
{
    public class BlazorI18Configuration
    {
        public Dictionary<string, string> LocalsUri { get; set; }
        public string DefaultLocal { get; set; }
        public string CurrentLocal { get; set; }
        public bool ForceReloadLocal { get; set; }
        public string OnBaseUri { get; set; }
    }
}
