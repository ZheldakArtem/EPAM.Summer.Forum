using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace DAL.Interface.Repository
{
   public interface IUnitOfWork:IDisposable
   {
        EntityModel Context { get; }
        void Commit();
   }
}
