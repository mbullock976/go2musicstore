var storeAccountController = function ($scope, $window, $state, jumbotronService, authenticationService, storeAccountService) {
    $scope.title = "My Store Account";
    $scope.jumbotronService = jumbotronService;
    jumbotronService.setJumobtron(false);
    $scope.storeAccountService = storeAccountService;

    storeAccountService.getCountries()
        .then(function (result) {
        },
            function () {
                console.log("unable to get countries");
            });

    authenticationService.getAuthentication()
        .then(function(data) {
            $scope.authentication = data;

            //if user is not authenitcated send them to login page
            if (!$scope.authentication.isAuthenticated) {

                //send to login page
                $window.location = "Account/Login";
                return;
            } else {

                //create store account
                storeAccountService.getStoreAccount($scope.authentication.name)
                   .then(function (data) {

                            $scope.storeAccount = data;
                            $scope.confirmEmailAddress = $scope.storeAccount.emailAddress;
                            $scope.lastSavedDateTime = "Last Updated: " + new Date().toUTCString();;
                       
                       if ($scope.storeAccount.storeAccountId == 0) {
                           //send to create store account page
                           $state.transitionTo('createStoreAccount', { userIdentityName: $scope.authentication.name });
                       } else {

                           //load store account                                   
                       }
                   },
                       function () {
                           alert("storeAccountController - unable to get store account");                          
                       });
            }

            },
            function() {
                alert("storeAccountController - unable to authenticate");
            });

    $scope.update = function () {

        if ($scope.storeAccount.emailAddress != $scope.confirmEmailAddress) {
            alert("Emails do not match");
            return;
        }

        storeAccountService.updateStoreAccount($scope.storeAccount)
            .then(function (storeAccount) {
                    $state.go('storeAccount', { userIdentityName: storeAccount.userIdentityName }, {reload:true});                    
                },
                function () {
                    console.log("error creating store account");
                });
    };   
}

storeApp.controller("storeAccountController", ["$scope", "$window", "$state", "jumbotronService", "authenticationService", "storeAccountService", storeAccountController]);