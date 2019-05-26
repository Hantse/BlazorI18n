using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorI18n.Services
{
    public interface II18n
    {
        /// <summary>
        /// Get a value from key base on current local set in I18nService
        /// </summary>
        /// <param name="key"></param>
        /// <returns>
        /// Value if found or key passed in parameter
        /// </returns>
        Task<string> GetValue(string key);


        /// <summary>
        /// Change current local, fetch value and update UI
        /// </summary>
        /// <param name="local">Local must be exist in LocalsUri or available over OnBaseUri</param>
        Task ChangeLocal(string local);

        /// <summary>
        /// Event trig when UI must be update when local change
        /// </summary>
        event Action OnLocalUpdateOrChange;
    }
}
