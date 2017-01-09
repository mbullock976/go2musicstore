var storeAccountCheckoutController = function($scope, $q,
    $state,
    $stateParams,
    $window,
    jumbotronService,
    storeAccountService,
    albumService) {

    $scope.albumId = $stateParams.albumId;
    $scope.totalOrderAmount = 0;
    $scope.purchaseOrderItems = [];

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
      

    $scope.jumbotronService = jumbotronService;
    $scope.storeAccountService = storeAccountService;
    jumbotronService.setJumobtron(false);

    var newPurchaseOrderId = -1;

    function _createNewPurchasedOrder () {

        $scope.unSavedNewPurchaseOrder = {
            storeAccountId: $scope.storeAccount.storeAccountId,
            purchaseOrderId: newPurchaseOrderId,
            totalOrderAmount: 0            
        };

    };

    function _createNewPurchaseOrderItems() {

        if ($scope.albumId == null || $scope.albumId == -1) {
            //get all shopping cart items and genereate list of purchase order items

            storeAccountService.getItemsForShoppingCart($scope.storeAccount.shoppingCartId)
            .then(function(data) {

                    $scope.shoppingCartItemsList = data;                    
                    
                    for (var i = 0; i < $scope.shoppingCartItemsList.length; i++) {

                        var purchaseOrderItem = {
                            purchaseOrderId: newPurchaseOrderId,
                            albumId: $scope.shoppingCartItemsList[i].albumId,
                            album: $scope.shoppingCartItemsList[i].album,
                            quantity: $scope.shoppingCartItemsList[i].quantity,
                            totalAmount: $scope.shoppingCartItemsList[i].totalPrice
                        };

                        $scope.purchaseOrderItems.push(purchaseOrderItem);

                    }

                }, function() {
                    console.log("storeAccountCheckoutController - error getting shopping cart items");
                });

        } else {
            //get album and genereate a purchase order items
            albumService.getAlbumById($scope.albumId)
                .then(function(data) {
                    var album = data;

                        var purchaseOrderItem = {
                            purchaseOrderId: newPurchaseOrderId,
                            albumId: album.albumId,
                            album: album,
                            quantity: 1,
                            totalAmount: album.price
                        };

                        $scope.purchaseOrderItems.push(purchaseOrderItem);

                    },
                    function() {
                        console.log("storeAccountCheckoutController - error getting album");
                    });
        }
    };

    _getStoreAccount()
        .then(function() {

                _createNewPurchasedOrder();
                _createNewPurchaseOrderItems();
            },
            function() {

            });

    $scope.getTotal = function () {
        var total = 0;
        for (var i = 0; i < $scope.purchaseOrderItems.length; i++) {
            var orderItem = $scope.purchaseOrderItems[i];
            total += (orderItem.totalAmount);
        }

        //$scope.unSavedNewPurchaseOrder.totalOrderAmount = total;
        $scope.totalOrderAmount = total;

        return total;
    }

    function _emptyShoppingCart() {
        
        if ($scope.albumId > 0) {
            $state.go('purchasedOrders', { storeAccountId: $scope.storeAccount.storeAccountId });
            return;
        };
        
        storeAccountService.emptyShoppingCart($scope.storeAccount.shoppingCartId)
            .then(function() {

                    $state.go('purchasedOrders', { storeAccountId: $scope.storeAccount.storeAccountId });
                },
                function() {
                    console.log("storeAccountCheckoutController - error emptying basket");
                });
    };

        

    $scope.save = function() {

        //Add hyper link to store account page that goes to new purchased orders page
        //$scope.unSavedNewPurchaseOrder = $scope.purchaseOrderItems;
        $scope.unSavedNewPurchaseOrder.totalOrderAmount = $scope.totalOrderAmount;

        storeAccountService.savedNewPurchaseOrder($scope.unSavedNewPurchaseOrder, $scope.purchaseOrderItems)
        .then(function() {

                _emptyShoppingCart();                

            }, function () {
            console.log("storeAccountCheckoutController - error saving purchase order");
        });
    };
};


storeApp.controller("storeAccountCheckoutController", ["$scope", "$q", "$state", "$stateParams", "$window", "jumbotronService",
    "storeAccountService", "albumService", storeAccountCheckoutController])