
var storeAccountCreditCardsController = function($scope,
    $window,
    $state,
    $stateParams,
    jumbotronService,
    storeAccountService) {

    $scope.jumbotronService = jumbotronService;
    jumbotronService.setJumobtron(false);
    $scope.storeAccountService = storeAccountService;

    $scope.userIdentityName = $stateParams.userIdentityName;
    
    
    storeAccountService.getStoreAccount($scope.userIdentityName)
        .then(function (data) {
                $scope.storeAccount = data;
            },
            function () {
                console.log("error occured retrieving credit card types.");
            });

    storeAccountService.getCreditCardTypes()
        .then(function(result) {

            },
            function() {
                console.log("error occured retrieving credit card types.");
            });

    storeAccountService.getCreditCardsByStoreAccount($scope.userIdentityName)
        .then(function(result) {

            },
            function() {
                console.log("error getting credit cards for store account");
            });

    function _findCreditModelId(creditCardId) {
        
        var creditCardModel = null;
        for (var i = 0; i < storeAccountService.creditCardsForStoreAccountList.length; i++) {
            if (creditCardId == storeAccountService.creditCardsForStoreAccountList[i].creditCardId) {
                this.creditCardModel = storeAccountService.creditCardsForStoreAccountList[i];
            }
        }

        return this.creditCardModel;

    };

    $scope.update = function(creditCardId) {
        
        var creditCardModel = _findCreditModelId(creditCardId);
        
        if (creditCardModel != null) {
            storeAccountService.updateCreditCardForStoreAccount(creditCardModel)
                .then(function() {
                        $scope.lastSavedDateTime = "Last Updated: " + new Date().toUTCString();
                    },
                    function() {
                        console.log("error updating credit card for store Account");
                    });
        }
    };

    $scope.save = function (creditCardTypeId, cardNumber) {

        storeAccountService.saveCreditCardForStoreAccount(creditCardTypeId, cardNumber, $scope.storeAccount.storeAccountId)
            .then(function () {
                
                $state.go('creditCards', { userIdentityName: $scope.userIdentityName }, { reload: true });
                $scope.lastSavedDateTime = "Added new credit card: " + new Date().toUTCString();
            },
                function () {
                    console.log("error adding credit card for store Account");
                });

    };

    $scope.delete = function (creditCardId) {
        
        storeAccountService.deleteCreditCardForStoreAccount(creditCardId)
         .then(function () {
             $scope.lastSavedDateTime = "Added new credit card: " + new Date().toUTCString();

             $state.go('creditCards', { userIdentityName: $scope.storeAccount.userIdentityName }, { reload: true });
         },
                function () {
                    console.log("error adding credit card for store Account");
                });
    };
};


storeApp.controller("storeAccountCreditCardsController", ["$scope", "$window", "$state", "$stateParams", "jumbotronService", "storeAccountService", storeAccountCreditCardsController]);