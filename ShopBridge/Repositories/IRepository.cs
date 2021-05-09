using ShopBridge.DTO;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Repositories
{
    public interface IInventoryRepository
    {
        Task<int> Add(InventoryDto inventory);

        Task<int> Delete(int id);

        Task<int> Edit(InventoryDto inventory);

        Task<IEnumerable<InventoryDto>> GetAll();
    }
}
