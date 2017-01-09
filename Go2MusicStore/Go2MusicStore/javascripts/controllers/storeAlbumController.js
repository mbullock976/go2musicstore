
var storeAlbumController =
    function ($scope, $q, $state, $stateParams, $window, $rootScope,
        albumService, jumbotronService, storeAccountService, signalRService) {

    $scope.albumId = $stateParams.albumId;
    $scope.albumService = albumService;
    $scope.jumbotronService = jumbotronService;
    $scope.signalRService = signalRService;

    jumbotronService.setJumobtron(false);

    $scope.isOutOfStock = false;

    /***********************************************************************************/
    ////real time signalR updates for out of stock notications
    //METHOD 1

    //var connection = $.hubConnection();
    //var hub = connection.createHubProxy("StoreHub");
    //hub.on("notifyStock", function (albumId, isOutOfStock) {
    //    $scope.$apply(function () {
    //        if ($scope.albumId == albumId) {
    //            $scope.isOutOfStock = isOutOfStock;
    //        }
    //    });
    //});

    //connection.start();

    //METHOD 2
    signalRService.outOfStockEvent.subscribe($scope, function (event, args) {
        // Handle notification

        $scope.$apply(function () {            
            if ($scope.albumId == args.albumId) {
                $scope.isOutOfStock = args.isOutOfStock;
            }
        }, function () {
            console.log("storeAlbumController - error signalr");
        });
    });

    /***********************************************************************************/

    albumService.getAlbumById($scope.albumId)
        .then(function(album) {
                $scope.album = album;

            },
            function() {
                console.log("storeAlbumController - error occured retrieving album for id " + $stateParams.albumId);
            });

    function _getStoreAccount() {

        var deferred = $q.defer();

        storeAccountService.getStoreAccountAndAuthenticate()
        .then(function (data) {

                    if (!data) {
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

    $scope.addToBasket = function () {

        _getStoreAccount()
        .then(function() {

                var shoppingCartItemModel = {
                    shoppingCartId: $scope.storeAccount.shoppingCartId,
                    albumId: $scope.albumId,
                    quantity: 1,
                    totalPrice: $scope.album.price
                };                                   
            
                storeAccountService.addNewShoppingCartItem(shoppingCartItemModel)
                    .then(function () {
                        $scope.lastSavedDateTime = "Added to Basket: " + new Date().toUTCString();
                    },
                function () {
                    console.log("storeAlbumController - error occured add to basket");
                });


            }, function() {
                
            });
    }


    _getStoreAccount();
};

storeApp.controller("storeAlbumController", ["$scope", "$q", "$state", "$stateParams", "$window", "$rootScope", "albumService", "jumbotronService",
    "storeAccountService", "signalRService", storeAlbumController])