using System.Collections.Generic;
using System.Threading.Tasks;
using TMTTimeKeeper.Models.ApiRequestModels;

namespace TMTTimeKeeper.Interface
{
    public interface ITendalRequestService
    {
        Task<TResult> GetAsync<TResult>(string url);
        Task<TResult> GetAsync<TResult>(string url, object obj = null);
        Task<string> GetStringAsync(string url);
        Task<TResult> PostRequest<TResult>(string apiUrl, object postObject);
        Task PutRequest<T>(string apiUrl, object putObject);
        Task DeleteRequest(string apiUrl);
        Task<IEnumerable<TimeAttendanceSyncLogDisplay>> GetAttendanceSyncLog(GetAttendanceSyncReq val);
    }
}
