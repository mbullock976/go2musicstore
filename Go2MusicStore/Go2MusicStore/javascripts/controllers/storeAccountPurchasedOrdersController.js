var storeAccountPurchasedOrdersController = function($scope,
    $q,
    $state,
    $stateParams,
    $window,
    jumbotronService,
    storeAccountService) {

    $scope.jumbotronService = jumbotronService;
    $scope.storeAccountService = storeAccountService;
    jumbotronService.setJumobtron(false);

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
                console.log("storeAccountPurchasedOrdersController - error occured retrieving purchase orders");
                deferred.reject();
            });

        return deferred.promise;
    };

    _getStoreAccount()
       .then(function () {

           storeAccountService.getPurchasedOrders($scope.storeAccount.storeAccountId)
            .then(function(data) {

                   $scope.purchasedOrdersList = data;

               }, function () {
                    console.log("storeAccountPurchasedOrdersController - error occured retrieving purchase orders");
                });

            }, function () {
                console.log("storeAccountPurchasedOrdersController - error occured retrieving store account");
           });


    
};


storeApp.controller("storeAccountPurchasedOrdersController", ["$scope", "$q", "$state", "$stateParams", "$window", "jumbotronService",
    "storeAccountService", storeAccountPurchasedOrdersController])