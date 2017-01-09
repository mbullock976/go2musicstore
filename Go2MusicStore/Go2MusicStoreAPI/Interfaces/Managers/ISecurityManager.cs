namespace Go2MusicStore.API.Interfaces.Managers
{
    using System;
    using System.Linq;

    using Go2MusicStore.Models;

    public interface ISecurityManager : IDisposable
    {
        IQueryable<ApplicationUser> ApplicationUsers { get; }
    }
}