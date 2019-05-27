using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorI18n.Models
{
    public class BlazorI18nConfiguration
    {
        public string DefaultLocal { get; set; }
        public string CurrentLocal { get; set; }
        public bool ForceReloadLocal { get; set; }
    }
}
