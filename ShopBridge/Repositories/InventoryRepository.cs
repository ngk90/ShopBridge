using AutoMapper;
using ShopBridge.DTO;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Repositories
{
    public class InventoryRepository: IInventoryRepository
    {
        private readonly InventoryDBContext inventoryDBContext;
        private readonly IMapper mapper;

        public InventoryRepository(InventoryDBContext _inventoryDBContext, IMapper _mapper)
        {
            inventoryDBContext = _inventoryDBContext;
            mapper = _mapper;
        }

        public async Task<int> Add(InventoryDto inventory)
        {
            if (inventory != null)
            {
                var inventoryModel = mapper.Map<Inventory>(inventory);
                await inventoryDBContext.AddAsync(inventoryModel);

                return await inventoryDBContext.SaveChangesAsync();

            }
            return 0;
        }

        public async Task<int> Delete(int id)
        {
            var model = await inventoryDBContext.Inventories.FindAsync(id);
            if (model != null)
            {
                inventoryDBContext.Remove(model);
                return await inventoryDBContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Edit(InventoryDto inventory)
        {
            if (inventory != null)
            {
                var inventoryModel = mapper.Map<Inventory>(inventory);
                inventoryDBContext.Entry(inventoryModel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                return await inventoryDBContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<InventoryDto>> GetAll()
        {
            var invetries =  inventoryDBContext.Inventories.ToList();

            return mapper.Map<List<InventoryDto>>(invetries);            
        }
    }
}
