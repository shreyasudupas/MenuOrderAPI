using BuisnessLayer.DBModels;
using BuisnessLayer.Models;
using OrderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<TblVendorList> GetVendorList()
        {
            List<TblVendorList> Vendorlist = new List<TblVendorList>();
            try
            {
                 Vendorlist = dbContext.TblVendorLists.Where(x => x.VendorId > 0).ToList();
                
            }catch(Exception ex)
            {

            }
            return Vendorlist;
        }

        public MenuDisplayList GetMenuListForVednorId(int VendorId)
        {
            MenuDisplayList ListMenu = new MenuDisplayList();
            try
            {
                //ListMenu = dbContext.TblMenus.Where(x => x.VendorId == VendorId).OrderBy(x=>x.MenuTypeId).ToList();
                var MenuList = (from Menu in dbContext.TblMenus
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
                        }).ToList();

                var GetImageLinkOfMenuType = (from type in dbContext.TblMenuTypes
                                              where (type.MenuTypeId > 0)
                                              select new MenuItemDetail
                                              {
                                                  MenuTypeId = type.MenuTypeId,
                                                  MenuTypeName = type.MenuTypeName,
                                                  ImagePath = type.ImagePath
                                              }).ToList();

                ListMenu.MenuItemDetails = GetImageLinkOfMenuType;
                ListMenu.MenuItemList = MenuList;

            }catch(Exception ex)
            {

            }
            return ListMenu;
        }
    }
}
