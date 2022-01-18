using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMTTimeKeeper.Models;

namespace TMTTimeKeeper.Interface
{
    public interface ITimeKeeperService
    {
        Task<IEnumerable<TimeKeeperDisplay>> GetAll();
        Task<TimeKeeperDisplay> GetById(Guid id);
        Task<TimeKeeperDisplay> Create(TimeKeeperSave val);
        Task Update(Guid id, TimeKeeperSave val);
        Task Delete(Guid id);
        Task SyncData(ReadTimeGLogDataReq val);

    }
}
