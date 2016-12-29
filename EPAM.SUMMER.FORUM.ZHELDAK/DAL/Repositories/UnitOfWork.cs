using DAL.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public EntityModelContext Context { get; }
        public UnitOfWork(EntityModelContext context)
        {
            Context = context;
        }
        
        public void Commit()
        {
            Context?.SaveChanges();
        }
        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
