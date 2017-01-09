namespace Go2MusicStore.Controllers.Mvc
{
    using System.Linq;
    using System.Web.Mvc;
    using API.Interfaces;

    using Go2MusicStore.Models;
    
    public class HomeController : BaseController
    {
        public HomeController(IApplicationManager applicationManager) 
            : base(applicationManager)
        {
        }

        public ActionResult Index()
        {
            var userIdentityName = System.Web.HttpContext.Current.User.Identity.Name;
            var storeAccount =
                this.StoreAccountManager.Get<StoreAccount>().FirstOrDefault(m => m.UserIdentityName == userIdentityName);

            ViewBag.BasketCount = 0;
            if (storeAccount != null)
            {
                ViewBag.UserIdentityName = storeAccount.UserIdentityName;
                ViewBag.BasketCount =  this.StoreAccountManager.Get<ShoppingCartItem>()
                        .Count(m => m.ShoppingCartId == storeAccount.ShoppingCartId);
            }
           
            return this.View();
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }

        public ActionResult Admin()
        {
            this.ViewBag.Message = "Your admin page.";

            return this.View();
        }
    }
}