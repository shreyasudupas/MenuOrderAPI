using BuisnessLayer.DBModels;
using BuisnessLayer.Models;
using OrderAPI.Models;
using System;
using System.Collections.Generic;

namespace BuisnessLayer
{
    public interface IOrderBL
    {
        List<Order> AddOrders(Order orderItems);
        List<Order> GetOrderListBasedOnCustomerId(Guid CustomerId, List<Order> orders);
        List<TblVendorList> GetVendorList();
        MenuDisplayList GetMenuListForVednorId(int VendorId);
    }
}