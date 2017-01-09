namespace Go2MusicStore.Platform.Implementation.Managers
{
    using System;
    using System.Linq;

    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.Models;
    using Go2MusicStore.Platform.Interfaces.DataLayer;

    public class SecurityManager : ISecurityManager
    {
        private readonly IUnitOfWork unitOfWork;

        private bool isDisposed = false;

        public SecurityManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            Initialise();
        }

        public IQueryable<ApplicationUser> ApplicationUsers { get; private set; }

        public void Dispose()
        {            
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Initialise()
        {
            this.ApplicationUsers = this.unitOfWork.ApplicationUsers;
        }        

        private void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing)
                {
                    this.unitOfWork.Dispose();
                }

                isDisposed = true;
            }
        }
    }
}