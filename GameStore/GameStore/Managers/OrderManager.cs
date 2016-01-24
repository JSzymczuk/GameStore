using GameStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameStore.Managers
{
    public class OrderManager : Manager
    {
        public OrderManager(GameStoreDbContext context) : base(context) { }

        public IQueryable<Order> Orders
        {
            get { return Context.Orders; }
        }

        public Order FindById(Guid id)
        {
            try { return Context.Orders.First(c => c.Id == id); }
            catch (Exception) { return null; }
        }

        public void Create(Order order)
        {
            if (order != null)
            {
                Context.Orders.Add(order);
            }
        }

        public bool Send(Guid id)
        {
            try
            {
                Order order = Context.Orders.Find(id);
                order.SentDate = DateTime.Now;
                Context.Entry(order).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PayFor(Guid id)
        {
            try
            {
                Order order = Context.Orders.Find(id);
                order.Paid = true;
                Context.Entry(order).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Cancel(Guid id)
        {
            try
            {
                Order order = Context.Orders.Find(id);
                order.IsDeleted = true;
                Context.Entry(order).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}