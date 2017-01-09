var storeAccountCreateController = function ($scope, $state, $stateParams, jumbotronService, storeAccountService) {
    $scope.title = "Create Store Account:";
    $scope.jumbotronService = jumbotronService;
    jumbotronService.setJumobtron(false);
    $scope.userIdentityName = $stateParams.userIdentityName;
    $scope.storeAccountService = storeAccountService;

    //TODO Initialise new Account object to bind to view
    //TODO validate email and confirm email match
    //TODO send new object to webApi to persist 
    //TODO goto the store account main page displaying store account details

    storeAccountService.getCountries()
        .then(function(result) {
            },
            function() {
                console.log("unable to get countries");
            });

    $scope.confirmEmailAddress = "";

    var init_newStoreAccountModel = function () {
        $scope.newStoreAccountModel = {
            userIdentityName: $scope.userIdentityName,
            firstName: "",
            lastName: "",
            telephoneNo: "",
            address: "",
            city: "",
            postCode: "",
            countryId: 0,
            emailAddress: ""
        };
    }

    init_newStoreAccountModel();

    $scope.save = function () {

        if ($scope.newStoreAccountModel.emailAddress != $scope.confirmEmailAddress) {
            alert("Emails do not match");
            return;
        }

        storeAccountService.saveStoreAccount($scope.newStoreAccountModel)
            .then(function(storeAccount) {
                    $state.go('storeAccount', { userIdentityName: storeAccount.userIdentityName });
                },
                function() {
                    console.log("error creating store account");
                });
    };
};

storeApp.controller('storeAccountCreateController', ["$scope", "$state", "$stateParams", "jumbotronService", "storeAccountService", storeAccountCreateController]);