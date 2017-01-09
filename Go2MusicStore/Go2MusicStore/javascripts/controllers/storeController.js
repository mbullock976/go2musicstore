var storeController =
    function ($scope, $q, $window, $state, albumService,
        jumbotronService, storeAccountService, signalRService) {


    $scope.title = "Store Page!";
    $scope.albumService = albumService;
    jumbotronService.setJumobtron(true);
    $scope.jumbotronService = jumbotronService;
    $scope.storeAccountService = storeAccountService;
        $scope.signalRService = signalRService;
    
    
    albumService.getGenres()
        .then(function (result) {
            //success                
            
        },
            function () {
                //error
                console.log("storeController - error occured retrieving genres");
            });

    albumService.getLatestAlbums(10)
    .then(function (result) {
        //success                
    },
        function () {
            //error
            console.log("storeController - error occured retrieving latest albums");
        });

    function _getStoreAccount() {

        var deferred = $q.defer();

        storeAccountService.getStoreAccountAndAuthenticate()
        .then(function (data) {                    
            if (data == null || data.storeAccountId < 1) {
                $window.location = "Account/Login";
                return;
            }

            $scope.storeAccount = data;
            deferred.resolve();

        },
            function () {
                console.log("storeAlbumController - error occured retrieving album for id " + $stateParams.albumId);
                deferred.reject();
            });

        return deferred.promise;
    };

    $scope.addToBasket = function (album) {


        _getStoreAccount()
        .then(function () {

            var shoppingCartItemModel = {
                shoppingCartId: $scope.storeAccount.shoppingCartId,
                albumId: album.albumId,
                quantity: 1,
                totalPrice: album.price
            };

            storeAccountService.addNewShoppingCartItem(shoppingCartItemModel)
                .then(function () {
                    $scope.lastSavedDateTime = "Added to Basket: " + new Date().toUTCString();
                    $state.go($state.current, {}, { reload: true });
                    },
            function () {
                console.log("storeAlbumController - error occured add to basket");
            });


        }, function () {

        });
    }


    //_getStoreAccount();
};

storeApp.controller("storeController", ["$scope", "$q", "$window",
    "$state", "albumService", "jumbotronService", "storeAccountService",
    "signalRService", storeController]);
