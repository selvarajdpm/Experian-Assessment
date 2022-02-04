using System.Collections.Generic;
using System.Threading.Tasks;

namespace Experian.API.Helpers.RestClient
{
    /// <summary>
    /// Contract to take care of Http operations
    /// </summary>
    public interface IRestClientHelper
    {
        /// <summary>
        /// Http Get action
        /// </summary>
        /// <typeparam name="T">Type to which the response will be deserialized</typeparam>
        /// <param name="requestUri">Request Uri</param>
        /// <returns>Typed response</returns>
        Task<T> GetAsync<T>(string requestUri) where T : class;        
    }
}
