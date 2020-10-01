using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BnhsQrCode.Abstracts;
using BnhsQrCode.Model;
using BnhsQrCode.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;

namespace BnhsQrCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanHistoryController : ControllerBase
    {
        private readonly IScanHistoryService _scanHistoryService;

        public ScanHistoryController(IScanHistoryService scanHistoryService)
        {
            _scanHistoryService = scanHistoryService;
        }
        // GET: api/ScanHistory
        [HttpGet]
        public IActionResult Get()
        {
            var histories = _scanHistoryService.ScanHistory.ToList();
            return Ok(histories);
        }

        // GET: api/ScanHistory/AllTime/5
        [HttpGet]
        [Route("AllTime/{id}")]
        public IActionResult AllTime(int id)
        {
            var allScanHistory = _scanHistoryService.ScanHistory.Where(x => x.UserProfileId == id).ToList();
            return Ok(allScanHistory);
        }

        // GET: api/ScanHistory/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var history = _scanHistoryService.ScanHistory.Single(x => x.Id == id);
            return Ok(history);
        }

        // POST: api/ScanHistory
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ScanHistory scanHistory)
        {
            try
            {
                _scanHistoryService.BeginTransaction();
                await _scanHistoryService.Save(scanHistory);
                await _scanHistoryService.Commit();
            }
            catch
            {
                // log exception here
                await _scanHistoryService.Rollback();
                return Ok(false);
            }
            finally
            {
                _scanHistoryService.CloseTransaction();
            }
            return Ok(true);
        }

        // PUT: api/ScanHistory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ScanHistory scanHistory)
        {
            var historyModel = await _scanHistoryService.ScanHistory.SingleAsync(x => x.Id == scanHistory.Id);
            try
            {
                _scanHistoryService.BeginTransaction();

                historyModel.UserProfileId = scanHistory.UserProfileId;
                historyModel.BodyTemp = scanHistory.BodyTemp;
                historyModel.TimeStatus = scanHistory.TimeStatus;
                historyModel.CreatedByDateTime = historyModel.CreatedByDateTime;

                await _scanHistoryService.Save(historyModel);
                await _scanHistoryService.Commit();
            }
            catch
            {
                // log exception here
                await _scanHistoryService.Rollback();
                return Ok(false);
            }
            finally
            {
                _scanHistoryService.CloseTransaction();
            }
            return Ok(historyModel);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _scanHistoryService.BeginTransaction();

                var history = _scanHistoryService.ScanHistory.Single(x => x.Id == id);

                await _scanHistoryService.Delete(history);
                await _scanHistoryService.Commit();
            }
            catch
            {
                // log exception here
                await _scanHistoryService.Rollback();
                return Ok(false);
            }
            finally
            {
                _scanHistoryService.CloseTransaction();
            }
            return Ok(true);
        }
    }
}
