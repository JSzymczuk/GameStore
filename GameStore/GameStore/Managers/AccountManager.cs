using GameStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameStore.Managers
{
    public class AccountManager : Manager
    {
        public AccountManager(GameStoreDbContext context) : base(context) { }

        public IQueryable<Address> Addresses
        {
            get { return Context.Addresses.Where(a => !a.IsDeleted); }
        }

        public Address FindAddressById(Guid id)
        {
            try { return Context.Addresses.First(c => c.Id == id); }
            catch (Exception) { return null; }
        }

        public void CreateAddress(Address address)
        {
            if (address != null)
            {
                Context.Addresses.Add(address);
            }
        }

        public bool DeleteAddress(Guid id)
        {
            try
            {
                Address address = Context.Addresses.Find(id);
                address.IsDeleted = true;
                Context.Entry(address).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}