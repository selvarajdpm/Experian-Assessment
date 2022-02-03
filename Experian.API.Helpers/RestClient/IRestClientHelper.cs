using System.Collections.Generic;
using System.Threading.Tasks;

namespace Experian.API.Helpers.RestClient
{
    public interface IRestClientHelper
    {
        Task<T> GetAsync<T>(string requestUri) where T : class;        
    }
}
