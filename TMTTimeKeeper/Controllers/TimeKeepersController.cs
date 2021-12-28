using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMTTimeKeeper.Helpers;
using TMTTimeKeeper.Interface;
using TMTTimeKeeper.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TMTTimeKeeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeKeepersController : ControllerBase
    {
        private readonly ICzkemHelper _czkemHelper = new CzkemHelper();
        private readonly ITimeKeeperService _timeKeeperService;
        public TimeKeepersController(ICzkemHelper czkemHelper, ITimeKeeperService timeKeeperService)
        {
            _czkemHelper = czkemHelper;
            _timeKeeperService = timeKeeperService;
        }
        // GET: api/<TimeKeepersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _timeKeeperService.GetAll();
            return Ok(res);
        }

        // GET api/<TimeKeepersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var res = await _timeKeeperService.GetById(id);
            return Ok(res);
        }

        // POST api/<TimeKeepersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TimeKeeperSave val)
        {
            //kiểm tra kết nối
            var connect = _czkemHelper.Connect(val.IPAddress, val.TCPPort);
            if (!connect)
                throw new Exception("Kết nối máy chấm công thất bại");
            return Ok();
        }

        // PUT api/<TimeKeepersController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] TimeKeeperSave val)
        {
            await _timeKeeperService.Update(id, val);
        }

        // DELETE api/<TimeKeepersController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _timeKeeperService.Delete(id);
        }
        
        [HttpPost("[action]")]
        public IActionResult Connect([FromBody] TimeKeeperConnectReq val)
        {
            var res = _czkemHelper.Connect(val.IPAddress,val.TCPPort);
            return Ok(new {Result = res});
        }

        [HttpPost("[action]")]
        public IActionResult ReadTimeGLogData([FromBody] ReadTimeGLogDataReq val)
        {
            var fromDate = val.DateFrom.ToString("yyyy-MM-dd HH:mm:ss");
            var toDate = val.DateTo.ToString("yyyy-MM-dd HH:mm:ss");
            var connect = _czkemHelper.Connect(val.IPAddress, val.TCPPort);
            if (!connect)
                throw new Exception("Kết nối máy chấm công thất bại");
            var result = _czkemHelper.ReadTimeGLogData(int.Parse(val.TCPPort),fromDate,toDate);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public IActionResult SyncData([FromBody] ReadTimeGLogDataReq val)
        {
           
            return Ok();
        }
    }
}
