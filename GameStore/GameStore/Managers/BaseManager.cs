using GameStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Managers
{
    public abstract class Manager : IDisposable
    {
        protected GameStoreDbContext Context { get; private set; }

        public Manager(GameStoreDbContext context)
        {
            Context = context;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}