using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using TMTTimeKeeper.Interface;
using TMTTimeKeeper.Models;

namespace TMTTimeKeeper.Services
{
    public class TimeKeeperService : ITimeKeeperService
    {
        private readonly ITendalRequestService _tdentalRequestService;
        public TimeKeeperService(ITendalRequestService tendalRequestService)
        {
            _tdentalRequestService = tendalRequestService;
        }
        public async Task<IEnumerable<TimeKeeperDisplay>> GetAll()
        {
            var val = new TimeKeeperPaged();
            var res = await _tdentalRequestService.GetAsync<PagedResult2<TimeKeeperDisplay>>("api/TimeAttendanceMachines", val);
            return res.Items;
        }

        public async Task<TimeKeeperDisplay> GetById(Guid id)
        {
            var res = await _tdentalRequestService.GetAsync<TimeKeeperDisplay>("api/TimeAttendanceMachines/"+id);
            return res;
        }

        public async Task Update(Guid id, TimeKeeperSave val)
        {
            await _tdentalRequestService.PutRequest<TimeKeeperDisplay>("api/TimeAttendanceMachines/"+id, val);
        }

        public async Task Delete(Guid id)
        {
            await _tdentalRequestService.DeleteRequest("api/TimeAttendanceMachines/" + id);
        }
    }
}
