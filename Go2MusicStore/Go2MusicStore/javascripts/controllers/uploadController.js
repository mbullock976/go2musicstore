var uploadController = function ($scope, $state, $stateParams, fileReader, albumService, jumbotronService) {

    $scope.albumId = $stateParams.albumId;
    $scope.albumService = albumService;
    $scope.jumbotronService = jumbotronService;
    jumbotronService.setJumobtron(false);

    albumService.getAlbumById($scope.albumId)
        .then(function(data) {

                $scope.album = data;

            },
            function() {
                console.log("error uploadcontroler");
            });


    $scope.getFile = function () {
        $scope.progress = 0;
        fileReader.readAsDataUrl($scope.file, $scope)
                      .then(function (result) {
                          $scope.imageSrc = result;
                      });
    };

    $scope.$on("fileProgress", function (e, progress) {
        $scope.progress = progress.loaded / progress.total;
    });

    $scope.upload = function() {
        if ($scope.imageSrc) {
            $scope.album.albumArtUrl = $scope.imageSrc;
            alert($scope.imageSrc);
            albumService.uploadAlbumCover($scope.imageSrc)
                .then(function(result) {
                    $state.go('album', { albumId: $scope.album.albumId });
                    },
                    function() {

                    });
        }        
    };

};

storeApp.directive("ngFileSelect",
function () {

    return {
        link: function ($scope, el) {

            el.bind("change",
                function (e) {

                    $scope.file = (e.srcElement || e.target).files[0];
                    $scope.getFile();
                });

        }
    }

});

storeApp.controller('uploadController', ["$scope", "$state", "$stateParams", "fileReader", "albumService", "jumbotronService", uploadController]);

