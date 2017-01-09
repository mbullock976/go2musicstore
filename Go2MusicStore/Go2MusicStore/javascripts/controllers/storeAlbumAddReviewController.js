var storeAlbumAddReviewController = function($scope, $state, $stateParams, $window, albumService, jumbotronService) {

    $scope.albumId = $stateParams.albumId;
    $scope.albumService = albumService;
    $scope.jumbotronService = jumbotronService;

    jumbotronService.setJumobtron(false);

    var init_newReviewModel = function () {
        $scope.newReviewModel = {
            title: "",
            comment: "",
            isRecommended: false,
            starRating: 0,
            albumId: $scope.albumId
        };
    }

    init_newReviewModel();

    //reload page function
    //$scope.reload = function () {

    //    getAlbum($scope.albumId);
    //    init_newReviewModel();
    //}

    $scope.save = function () {

        albumService.saveReview($scope.newReviewModel)
            .then(function (result) {

                    //$scope.reload(); //reload page after post
                    $state.go('album',{ albumId: $scope.albumId }, {reload : true});
                    


                },
                function () {
                    alert("could not save new review for " + $scope.album.name);
                });
    };
};

storeApp.controller("storeAlbumAddReviewController", ["$scope", "$state", "$stateParams", "$window", "albumService", "jumbotronService", storeAlbumAddReviewController])