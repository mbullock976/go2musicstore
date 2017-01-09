
var storeAccountShoppingCartsController = function($scope,
    $window,
    $state,
    $stateParams,
    jumbotronService,
    storeAccountService) {

    $scope.userIdentityName = $stateParams.userIdentityName;
    $scope.jumbotronService = jumbotronService;
    $scope.storeAccountService = storeAccountService;
    jumbotronService.setJumobtron(false);

    function _getItemsForShoppingCart(storeAccount) {
        storeAccountService.getItemsForShoppingCart(storeAccount.shoppingCartId)
            .then(function (data) {
                    _calculateTotalPricesForEachItem(data);
                    _calculateStockEachItem(data);
                    $scope.shoppingCartItemsList = data;
                },
            function () {
                console.log("storeAccountShoppingCartsController - failed to get shopping cart items");
            });
    }

    function _calculateTotalPricesForEachItem(data) {
        for (var i = 0; i < data.length; i++) {
            data[i].totalPrice = data[i].quantity * data[i].album.price;
        }
    }

    function _calculateStockEachItem(data) {
        for (var i = 0; i < data.length; i++) {
            //data[i].stock = data[i].quantity > data[i].stockCount;

            if (data[i].quantity <= data[i].album.stockCount) {

                data[i].inStock = true;
            } else {
                data[i].inStock = false;
            }

        }
    }

    storeAccountService.getStoreAccount($scope.userIdentityName)
        .then(function(data) {
                $scope.storeAccount = data;

                _getItemsForShoppingCart(data);                
            },
            function() {
                console.log("storeAccountShoppingCartsController - failed to get store account");
            });

    $scope.update = function (shoppingCartItemId) {

        var itemsList = $scope.shoppingCartItemsList;

        for (var i = 0; i < itemsList.length; i++) {
            if (itemsList[i].shoppingCartItemId == shoppingCartItemId) {
                $scope.shoppingCartItemModel = itemsList[i];
            }
        }

        storeAccountService.updateShoppingCartItem($scope.shoppingCartItemModel)
         .then(function () {
             $scope.lastSavedDateTime = "Last Updated: " + new Date().toUTCString();
         },
             function () {
                 console.log("storeAccountShoppingCartsController - failed to update basket");
             });
    }

    $scope.delete = function (shoppingCartItemId) {

        storeAccountService.deleteShoppingCartItemForStoreAccount(shoppingCartItemId)
         .then(function () {
             $scope.lastSavedDateTime = "Deleted shopping cart item: " + new Date().toUTCString();

             $state.go('shoppingCart', { userIdentityName: $scope.storeAccount.userIdentityName }, { reload: true });
         },
                function () {
                    console.log("error deleting shopping cart item for store Account");
                });
    };

};


storeApp.controller('storeAccountShoppingCartsController'
    , ["$scope", "$window", "$state", "$stateParams", "jumbotronService", "storeAccountService"
        , storeAccountShoppingCartsController]);