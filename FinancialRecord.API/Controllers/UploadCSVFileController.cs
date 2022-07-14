using FinancialRecord.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancialRecord.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadCSVFileController : ControllerBase
    {
        private readonly IUploadFile uploadFile;
        
        public UploadCSVFileController(IUploadFile uploadFile)
        {
            this.uploadFile = uploadFile;
        }

        /// <summary>
        /// Upload the csv file, parse it and save it to database
        /// </summary>
        /// <returns></returns>
        /// TODO : restrict to upload only csv files
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFile()
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();
            
            var response = await this.uploadFile.UploadCSVFile(file);
            return Ok(response);
        }
    }
}
