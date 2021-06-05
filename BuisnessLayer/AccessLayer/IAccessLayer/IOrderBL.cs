using BuisnessLayer.DBModels;
using BuisnessLayer.Models;
using OrderAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    public interface IOrderBL
    {
        List<Order> AddOrders(Order orderItems);
        List<Order> GetOrderListBasedOnCustomerId(Guid CustomerId, List<Order> orders);
        Task<List<TblVendorList>> GetVendorListAsync();
        Task<MenuDisplayList> GetMenuListForVednorIdAsync(int VendorId);
    }
}