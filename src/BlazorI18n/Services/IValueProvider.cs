using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorI18n.Services
{
    public interface IValueProvider
    {
        Task<Dictionary<string, string>> FetchValues(string local);
    }
}
