namespace Go2MusicStore.Platform.Implementation.DataLayer.Factories
{
    using System.Data.Entity;

    using Go2MusicStore.Models;
    using Go2MusicStore.Platform.Implementation.DataLayer.Repositories;
    using Go2MusicStore.Platform.Interfaces.DataLayer.Factories;
    using Go2MusicStore.Platform.Interfaces.DataLayer.Repositories;

    public class RepositoryFactory : IRepositoryFactory
    {
        public IGenericRepository<T> Create<T>(DbContext context) where T: class 
        {
            if (typeof(Genre) == typeof(T))
            {
                return new GenericRepository<T>(context);
            }

            if (typeof(Artist) == typeof(T))
            {
                return new GenericRepository<T>(context);
            }

            if (typeof(Album) == typeof(T))
            {
                return new GenericRepository<T>(context);
            }

            if (typeof(Review) == typeof(T))
            {
                return new GenericRepository<T>(context);
            }

            if (typeof(CreditCardType) == typeof(T))
            {
                return new GenericRepository<T>(context);
            }

            if (typeof(CreditCard) == typeof(T))
            {
                return new GenericRepository<T>(context);
            }

            if (typeof(Country) == typeof(T))
            {
                return new GenericRepository<T>(context);
            }

            if (typeof(ShoppingCartItem) == typeof(T))
            {
                return new GenericRepository<T>(context);
            }

            if (typeof(ShoppingCart) == typeof(T))
            {
                return new GenericRepository<T>(context);
            }

            if (typeof(StoreAccount) == typeof(T))
            {
                return new GenericRepository<T>(context);
            }

            if (typeof(PurchaseOrder) == typeof(T))
            {
                return new GenericRepository<T>(context);
            }

            if (typeof(PurchaseOrderItem) == typeof(T))
            {
                return new GenericRepository<T>(context);
            }

            return null;
        }
    }
}