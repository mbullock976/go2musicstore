
var storeArtistController = function ($scope, $q, $window, $state, $stateParams, albumService, jumbotronService, storeAccountService) {
    $scope.artistId = $stateParams.artistId;
    $scope.jumbotronService = jumbotronService;
    $scope.albumService = albumService;
    $scope.storeAccountService = storeAccountService;

    $scope.sortOrderTypes = [
        { displayName: "Title asc", sortType: "title" },
        { displayName: "Title desc", sortType: "-title" },
        { displayName: "Description asc", sortType: "description" },
        { displayName: "Description desc", sortType: "-description" },
        { displayName: "Price asc", sortType: "-price | currency" },
        { displayName: "Price desc", sortType: "price | currency" },
        { displayName: "Rating asc", sortType: "-totalStarRating" },
        { displayName: "Rating desc", sortType: "totalStarRating" },
        { displayName: "Release Date asc", sortType: "-releaseDate" },
        { displayName: "Release Date desc", sortType: "releaseDate"}
    ];    

    albumService.getArtist($scope.artistId)
        .then(function(artist) {
                $scope.artist = artist;
            },
            function() {
                console.log("storeArtistController - error occured retrieving artist");
            });

    jumbotronService.setJumobtron(false);

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
                console.log("storeArtistController - error occured add to basket");
            });


        }, function () {

        });
    }


    _getStoreAccount();
};


storeApp.controller("storeArtistController", ["$scope", "$q", "$window", "$state", "$stateParams", "albumService", "jumbotronService", "storeAccountService", storeArtistController]);