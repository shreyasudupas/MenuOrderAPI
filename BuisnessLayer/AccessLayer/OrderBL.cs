using BuisnessLayer.DBModels;
using BuisnessLayer.Models;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    public class OrderBL : IOrderBL
    {
        private readonly IProducer producer;
        private readonly MenuOrderManagementContext dbContext;
        public OrderBL(IProducer _prod, MenuOrderManagementContext dbContext)
        {
            producer = _prod;
            this.dbContext = dbContext;
        }
        public List<Order> AddOrders(Order orderItems)
        {
            List<Order> OrderList = new List<Order>();
            OrderList.Add(new Order { CustomerId = orderItems.CustomerId, ItemName = orderItems.ItemName, Quantity = orderItems.Quantity });

            //Add in the  queue
            producer.TopicExchangeQueue(orderItems);

            return OrderList;
        }

        public List<Order> GetOrderListBasedOnCustomerId(Guid CustomerId, List<Order> orders)
        {
            List<Order> orderList = new List<Order>();
            if (orders.Count > 0)
            {
                var getList = orders.Where(x => x.CustomerId == CustomerId).ToList();
                orderList.AddRange(getList);
            }
            return orderList;
        }

        public async Task<List<TblVendorList>> GetVendorListAsync()
        {
            List<TblVendorList> Vendorlist = new List<TblVendorList>();
            try
            {
                 Vendorlist = await dbContext.TblVendorLists.Where(x => x.VendorId > 0).ToListAsync();
                
            }catch(Exception ex)
            {

            }
            return Vendorlist;
        }

        public async Task<MenuDisplayList> GetMenuListForVednorIdAsync(int VendorId)
        {
            MenuDisplayList ListMenu = new MenuDisplayList();
            try
            {
                //ListMenu = dbContext.TblMenus.Where(x => x.VendorId == VendorId).OrderBy(x=>x.MenuTypeId).ToList();
                var MenuList = await (from Menu in dbContext.TblMenus
                        join MenuTypeName in dbContext.TblMenuTypes on Menu.MenuTypeId equals MenuTypeName.MenuTypeId
                        where (Menu.VendorId == VendorId)
                        orderby Menu.MenuTypeId
                        select new MenuList
                        {
                            MenuId = Menu.MenuId,
                            MenuItem = Menu.MenuItem,
                            Price = Menu.Price,
                            VendorId = Menu.VendorId,
                            MenuType = MenuTypeName.MenuTypeName,
                            ImagePath = Menu.ImagePath,
                            OfferPrice = Menu.OfferPrice,
                            CreatedDate = Menu.CreatedDate
                        }).ToListAsync();

                var GetImageLinkOfMenuType = await (from type in dbContext.TblMenuTypes
                                              where (type.MenuTypeId > 0)
                                              select new MenuItemDetail
                                              {
                                                  MenuTypeId = type.MenuTypeId,
                                                  MenuTypeName = type.MenuTypeName,
                                                  ImagePath = type.ImagePath
                                              }).ToListAsync();

                ListMenu.MenuItemDetails = GetImageLinkOfMenuType;
                ListMenu.MenuItemList = MenuList;

            }catch(Exception ex)
            {

            }
            return ListMenu;
        }
    }
}
