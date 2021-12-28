using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMTTimeKeeper.Models;

namespace TMTTimeKeeper.Interface
{
    public interface ITimeKeeperService
    {
        Task<IEnumerable<TimeKeeperDisplay>> GetAll();
    }
}
