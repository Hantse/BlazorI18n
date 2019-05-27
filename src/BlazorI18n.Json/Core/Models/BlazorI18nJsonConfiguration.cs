using BlazorI18n.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorI18n.Core.Models
{
    public class BlazorI18nJsonConfiguration : BlazorI18nConfiguration
    {
        public Dictionary<string, string> LocalsUri { get; set; }
        public string OnBaseUri { get; set; }
    }
}
