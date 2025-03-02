﻿
namespace Fct.Infrastructure.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable, IRepositoryContainer
    {
        /// <summary>
        /// Commits all changes
        /// </summary>
        void Commit();

        /// <summary>
        /// Commits all changes asynchronously
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();

        /// <summary>
        /// Discards all changes that has not been committed
        /// </summary>
        void RejectChanges();
    }
}
