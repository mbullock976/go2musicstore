namespace Go2MusicStore.Platform.Implementation.DataLayer
{
    using System;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Go2MusicStore.Models;
    using Go2MusicStore.Platform.Implementation.DataLayer.Repositories;
    using Go2MusicStore.Platform.Interfaces.DataLayer;
    using Go2MusicStore.Platform.Interfaces.DataLayer.Factories;
    using Go2MusicStore.Platform.Interfaces.DataLayer.Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRepositoryFactory repositoryFactory;

        private readonly AlbumStoreContext context = new AlbumStoreContext();

        private bool isDisposed = false;

        private IGenericRepository<Genre> genreRepository;

        private IGenericRepository<Artist> artistRepository;

        private IGenericRepository<Album> albumRepository;

        private IGenericRepository<Review> reviewRepository;

        private IGenericRepository<CreditCardType> creditCardTypeRepository;

        private IGenericRepository<CreditCard> creditCardRepository;

        private IGenericRepository<Country> countryRepository;

        private IGenericRepository<ShoppingCartItem> shoppingCartItemRepository;

        private IGenericRepository<ShoppingCart> shoppingCartRepository;

        private IGenericRepository<StoreAccount> storeAccountRepository;

        private IGenericRepository<PurchaseOrder> purchaseOrderRepository;

        private IGenericRepository<PurchaseOrderItem> purchaseOrderItemRepository;

        private UserStore<ApplicationUser> userStore;

        private UserManager<ApplicationUser> userManager;

        public UnitOfWork(IRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;

            Initialise();
            InitialiseUsers();
        }

        public IQueryable<ApplicationUser> ApplicationUsers => this.userManager.Users;

        public IGenericRepository<T> GetRepository<T>() 
            where T : class 
        {
            if (typeof(Genre) == typeof(T))
            {
                return this.genreRepository as IGenericRepository<T>;
            }

            if (typeof(Artist) == typeof(T))
            {
                return this.artistRepository as IGenericRepository<T>;
            }

            if (typeof(Album) == typeof(T))
            {
                return this.albumRepository as IGenericRepository<T>;
            }

            if (typeof(Review) == typeof(T))
            {
                return this.reviewRepository as IGenericRepository<T>;
            }

            if (typeof(CreditCardType) == typeof(T))
            {
                return this.creditCardTypeRepository as IGenericRepository<T>;
            }

            if (typeof(CreditCard) == typeof(T))
            {
                return this.creditCardRepository as IGenericRepository<T>;
            }

            if (typeof(Country) == typeof(T))
            {
                return this.countryRepository as IGenericRepository<T>;
            }

            if (typeof(ShoppingCartItem) == typeof(T))
            {
                return this.shoppingCartItemRepository as IGenericRepository<T>;
            }

            if (typeof(ShoppingCart) == typeof(T))
            {
                return this.shoppingCartRepository as IGenericRepository<T>;
            }

            if (typeof(StoreAccount) == typeof(T))
            {
                return this.storeAccountRepository as IGenericRepository<T>;
            }

            if (typeof(PurchaseOrder) == typeof(T))
            {
                return this.purchaseOrderRepository as IGenericRepository<T>;
            }

            if (typeof(PurchaseOrderItem) == typeof(T))
            {
                return this.purchaseOrderItemRepository as IGenericRepository<T>;
            }

            return null;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Initialise()
        {
            CreateRepositories(this.repositoryFactory);
        }

        private void CreateRepositories(IRepositoryFactory repositoryFactory1)
        {
            this.genreRepository = this.repositoryFactory.Create<Genre>(this.context);
            this.artistRepository = this.repositoryFactory.Create<Artist>(this.context);
            this.albumRepository = this.repositoryFactory.Create<Album>(this.context);
            this.reviewRepository = this.repositoryFactory.Create<Review>(this.context);

            this.creditCardTypeRepository = this.repositoryFactory.Create<CreditCardType>(this.context);
            this.creditCardRepository = this.repositoryFactory.Create<CreditCard>(this.context);
            this.countryRepository = this.repositoryFactory.Create<Country>(this.context);
            this.shoppingCartItemRepository = this.repositoryFactory.Create<ShoppingCartItem>(this.context);
            this.shoppingCartRepository = this.repositoryFactory.Create<ShoppingCart>(this.context);
            this.storeAccountRepository = this.repositoryFactory.Create<StoreAccount>(this.context);

            this.purchaseOrderRepository = this.repositoryFactory.Create<PurchaseOrder>(this.context);
            this.purchaseOrderItemRepository = this.repositoryFactory.Create<PurchaseOrderItem>(this.context);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing)
                {
                    this.context.Dispose();
                }
            }

            this.isDisposed = true;
        }

        private void InitialiseUsers()
        {
            this.userStore = new UserStore<ApplicationUser>(context);
            this.userManager = new UserManager<ApplicationUser>(userStore);
           
            //if (!userManager.Users.Any(m => m.UserName.Equals("mbullock976@hotmail.com")))
            //{
            //    userManager.Create(user, "Password1-");
            //}
        }
    }
}