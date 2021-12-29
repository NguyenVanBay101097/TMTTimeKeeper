using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TMTTimeKeeper.Interface;
using TMTTimeKeeper.Models;
using TMTTimeKeeper.Models.ApiRequestModels;

namespace TMTTimeKeeper.Services
{
    public class TimeKeeperService :BaseService, ITimeKeeperService
    {
        private readonly ITendalRequestService _tdentalRequestService;
        public TimeKeeperService(IServiceProvider provider, ITendalRequestService tendalRequestService):base(provider)
        {
            _tdentalRequestService = tendalRequestService;
        }
        public async Task<IEnumerable<TimeKeeperDisplay>> GetAll()
        {
            var val = new TimeKeeperPaged();
            var res = await _tdentalRequestService.GetAsync<PagedResult2<TimeKeeperDisplay>>("api/TimeAttendanceMachines", val);
            if (res != null)
                return res.Items;
            else
                return null;
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

        public async Task<TimeKeeperDisplay> Create(TimeKeeperSave val)
        {
            var res = await _tdentalRequestService.PostRequest<TimeKeeperDisplay>("api/TimeAttendanceMachines", val);
            return res;
        }

        public async Task SyncData(ReadTimeGLogDataReq val)
        {
            var czkHelper = GetService<ICzkemHelper>();

            //kiểm tra kết nối
            var device = await GetById(val.DeviceId);
            czkHelper.Connect(device.IPAddress,device.TCPPort);
            //lấy all chấm công
            var attendances = czkHelper.ReadTimeGLogData(int.Parse(device.TCPPort), val.DateFrom.ToString("yyyy-MM-dd HH:mm:ss"), val.DateTo.ToString("yyyy-MM-dd HH:mm:ss")).Data;
            //lấy list chấm công thành công
            var logReq = new GetAttendanceSyncReq()
            {
                DateFrom = val.DateFrom,
                DateTo = val.DateTo,
                MachineId = val.DeviceId,
                State = "success"
            };
            var successLog = await _tdentalRequestService.GetAttendanceSyncLog(logReq);
            //loại bỏ list chấm công đã từng đồng bộ thành công
            attendances = attendances.Where(x=> !successLog.Any(z=> z.AttendanceTime == x.Date)).ToList();
            //gọi api đồng bộ
            await _tdentalRequestService.PostRequest<object>("api/TimeAttendanceMachines/SyncData", new AttenDanceSyncDataReq() { MachineId = device.Id, Data = attendances});

        }
    }
}
