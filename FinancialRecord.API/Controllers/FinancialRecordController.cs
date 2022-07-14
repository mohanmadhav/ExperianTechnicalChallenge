using FinancialRecord.API.Interfaces;
using FinancialRecord.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialRecord.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialRecordController : ControllerBase
    {
        #region Global Declaration
        private readonly IFinancialRecord record;
        #endregion

        #region Constructor
        public FinancialRecordController(IFinancialRecord record)
        {
            this.record = record;
        }
        #endregion

        /// <summary>
        /// Read all the financial records with seearch string firstname and surname
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("FinancialRecord")]
        public async Task<IActionResult> ReadRecord(FinancialRecordRequest request)
        {
            var response = await this.record.ReadRecord(request);
            return Ok(response);
        }
    }
}
