using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace DAL.Interface.Repository
{
    /// <summary>
    /// The interface provides access to repositories through a single property and Commit method and determines the overall context
    /// </summary>
    public interface IUnitOfWork:IDisposable
   {
        /// <summary>
        /// The property returns the context.
        /// </summary>
        EntityModelContext Context { get; }

        /// <summary>
        /// Save all changes of the context.
        /// </summary>
        void Commit();
   }
}
