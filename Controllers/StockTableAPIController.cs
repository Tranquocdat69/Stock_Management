using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stock_Management.Models;
using Stock_Management.Repository;

namespace Stock_Management.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StockTableAPIController : ControllerBase
    {
        private readonly IStockTableRepository _stockTableRepository;

        public StockTableAPIController(IStockTableRepository stockTableRepository)
        {
            this._stockTableRepository = stockTableRepository;
        }

        [HttpGet("get_all_data")]
        public async Task<IActionResult> GetAllData()
        {
            List<TbBangHienThi> result = await _stockTableRepository.GetAllDataFromStockTable();
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Insert(TbBangHienThi data)
        {
            bool checkAdd = await _stockTableRepository.AddNewData(data);
            if (checkAdd)
            {
                return Ok();
            }
            return BadRequest("Add failed");
        }

        [HttpPut("update/{oldCode}")]
        public async Task<IActionResult> Update(string oldCode, TbBangHienThi data)
        {
            bool checkUpdate = await _stockTableRepository.UpdateData(oldCode, data);
            if (checkUpdate)
            {
                return Ok();
            }
            return BadRequest("Update failed");
        }

        [HttpDelete("delete/{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            bool checkDelete = await _stockTableRepository.DeleteData(code);
            if (checkDelete)
            {
                return Ok();
            }
            return BadRequest("Update failed");
        }

    }
}