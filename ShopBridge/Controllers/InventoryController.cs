using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.DTO;
using ShopBridge.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository inventoryRepository;
        public InventoryController(IInventoryRepository _inventoryRepository)
        {
            inventoryRepository = _inventoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await inventoryRepository.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]InventoryDto inventoryDto)
        {
            if (ModelState.IsValid)
            {
                var result = await inventoryRepository.Add(inventoryDto);
                if (result == 1)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Error while adding new resource");
                }
            }            
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int  id)
        {
            if (id>0)
            {
                var result = await inventoryRepository.Delete(id);
                if (result == 1)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Error while removing resource");
                }
            }
            return BadRequest("No Data found!");
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] InventoryDto inventoryDto)
        {
            if (ModelState.IsValid)
            {
                var result = await inventoryRepository.Edit(inventoryDto);
                if (result == 1)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Error while editing resource");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
