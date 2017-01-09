
            
var storeGenreController = function ($scope, $q, $state, $stateParams, $window, albumService, jumbotronService, storeAccountService) {

    $scope.selectedGenreId = $stateParams.genreId;
    $scope.albumService = albumService;
    $scope.jumbotronService = jumbotronService;
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
        { displayName: "Release Date desc", sortType: "releaseDate" }
    ];
  
    albumService.getGenreById($scope.selectedGenreId)
    .then(function(genre) {
            //success
            $scope.selectedGenre = genre;
               
        }, function () {            
            //error
            console.log("storeGenreController - error occured retrieving selected genre");
        });

    albumService.getGenreAlbums($scope.selectedGenreId)
        .then(function(result) {
            //success                                            
            },
            function() {
                //error
                console.log("storeGenreController - error occured retrieving genre albums");
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

storeApp.controller("storeGenreController", ["$scope", "$q", "$state", "$stateParams", "$window", "albumService", "jumbotronService", "storeAccountService", storeGenreController]);