//routes
storeApp.config(['$stateProvider', '$urlRouterProvider', '$locationProvider',
    function ($stateProvider, $urlRouterProvider, $locationProvider) {
           
            $urlRouterProvider.otherwise('/');

            $stateProvider
                .state('/',
                {
                    url: '/',
                    templateUrl: "/templates/storeView.html",
                    controller: "storeController"
                })

                .state('home',
                {
                    url: '/',
                    templateUrl: "/templates/storeView.html",
                    controller: "storeController"
                })

                .state('genre',
                {
                    url: "genre/:genreId", //this is the url the user sees when going to this page (good for hiding sensitive links to actual web api signatues)
                    templateUrl: "/templates/storeGenreView.html",
                    controller: "storeGenreController",
                    parent: "home" // this means this view is parent is the 2nd level angular ui-view div
                })

                .state('artist',
                {
                    url: "artist/:artistId",
                    templateUrl: "/templates/storeArtistView.html",
                    controller: "storeArtistController",
                    parent: "home"

                })

                .state('album',
                {
                    url: "album/:albumId", 
                    templateUrl: "/templates/storeAlbumView.html",
                    controller: "storeAlbumController",
                    parent: "home"

                })

                 .state('albumAddReview',
                {
                    url: "review/:albumId",
                    templateUrl: "/templates/storeAlbumAddReviewView.html",
                    controller: "storeAlbumAddReviewController",
                    parent: "album"

                })

                .state("storeAccount",
                    {
                        url: "storeAccount/:userIdentityName",
                        templateUrl: "/templates/storeAccountView.html",
                        controller: "storeAccountController",
                        //commented out line below so this view's parent is 1st level angular ui-view div.
                        parent: "home" 
                    })

                .state("createStoreAccount",
                        {
                            url: "createStoreAccount/:userIdentityName",
                            templateUrl: "/templates/createStoreAccountView.html",
                            controller: "storeAccountCreateController",
                            //commented out line below so this view's parent is 1st level angular ui-view div.
                            parent: "home"     
                        })

                .state("creditCards",
                            {                                
                                url: "creditCards/:userIdentityName",
                                templateUrl: "/templates/storeAccountCreditCardsView.html",
                                controller: "storeAccountCreditCardsController",
                                //commented out line below so this view's parent is 1st level angular ui-view div.
                                parent: "home"     
                            })

                 .state("addCreditCard",
                            {
                                url: "addCreditCard/:userIdentityName",
                                templateUrl: "/templates/storeAccountAddCreditCardView.html",
                                controller: "storeAccountCreditCardsController",
                                //commented out line below so this view's parent is 1st level angular ui-view div.
                                parent: "creditCards"
                            })

                .state("shoppingCart",
                            {
                                url: "shoppingCart/:userIdentityName",
                                templateUrl: "/templates/storeAccountShoppingCartView.html",
                                controller: "storeAccountShoppingCartsController",
                                //commented out line below so this view's parent is 1st level angular ui-view div.
                                parent: "home"
                            })

                .state("checkout",
                            {
                                url: "checkout/:storeAccountId/:albumId",
                                templateUrl: "/templates/storeAccountCheckoutView.html",
                                controller: "storeAccountCheckoutController",
                                //commented out line below so this view's parent is 1st level angular ui-view div.
                                parent: "home"
                            })

                .state("purchasedOrders",
                            {
                                url: "purchasedOrders/:storeAccountId",
                                templateUrl: "/templates/storeAccountPurchasedOrdersView.html",
                                controller: "storeAccountPurchasedOrdersController",
                                //commented out line below so this view's parent is 1st level angular ui-view div.
                                parent: "home"
                            })

                .state("upload",
                            {
                                url: "upload/:albumId",
                                templateUrl: "/templates/uploadView.html",
                                controller: "uploadController",
                                //commented out line below so this view's parent is 1st level angular ui-view div.
                                parent: "home"
                            });

        
    }]);


    

