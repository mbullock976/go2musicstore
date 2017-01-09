//services
storeApp.factory("albumService",
[
    "$http", "$q",
    function($http, $q) {

        var _isGenreListInit = false;

        var _isReady = function () {
            return _isGenreListInit;
        }

        var _genreList = [];
        var _latestAlbumsList = [];
        var _genreAlbumsList = [];       
        
        var _getGenres = function() {

            var _deferred = $q.defer(); //instantiates        

            $http.get("/api/v1/GenresApi")
                .then(function(result) {
                        //success
                        angular.copy(result.data, _genreList);

                        _isGenreListInit = true;

                        //allows caller to know whether the promise has succeeded
                        _deferred.resolve();
                    },
                    function() {
                        //error                        
                        //allows caller to know whether the promise has failed
                        _deferred.reject();
                    });

            //returns promise so that caller can wire up '.then' & error handlers
            return _deferred.promise;
        };

        //private function
        function _findGenre(id) {
            var found = null;

            $.each(_genreList, function (i, item) {
                if (item.genreId == id) {
                    found = item;
                    return false;
                }
            });

            return found;
        }

        var _getGenreById = function (genreId) {
            var deferred = $q.defer();

            if (_isReady()) {
                var genre = _findGenre(genreId);
                if (genre) {
                    deferred.resolve(genre);
                } else {
                    deferred.reject();
                }
            } else {
                _getGenres()
                  .then(function () {
                      // success
                      var genre = _findGenre(genreId);
                      if (genre) {
                          deferred.resolve(genre);
                      } else {
                          deferred.reject();
                      }
                  },
                  function () {
                      // error
                      deferred.reject();
                  });
            }

            return deferred.promise;
        };

        var _getLatestAlbums = function(count) {

            var _deferred = $q.defer(); //instantiates        
            $http.get("/api/v1/AlbumsApi/GetLatestAlbums/?count=" + count)
                .then(function(result) {
                        //success
                        angular.copy(result.data, _latestAlbumsList);

                        _deferred.resolve();
                    },
                    function() {
                        //error

                        _deferred.reject();
                    });

            //returns promise so that caller can wire up '.then' & error handlers
            return _deferred.promise;
        };

        var _getGenreAlbums = function(genreId) {

            var _deferred = $q.defer(); //instantiates        

            $http.get("/api/v1/AlbumsApi/GetAlbumsByGenre/?genreId=" + genreId)
                .then(function(result) {
                        //success
                        angular.copy(result.data, _genreAlbumsList);
                        _deferred.resolve();
                    },
                    function() {
                        //error
                        _deferred.reject();
                    });

            return _deferred.promise;
        };

        var _getArtist = function (artistId) {
            var _deferred = $q.defer();

            $http.get("api/v1/ArtistsApi/?artistId=" + artistId)
                .then(function(result) {
                    //success                        
                        _deferred.resolve(result.data);
                    },
                    function() {
                        //error
                        _deferred.reject();
                    });

            return _deferred.promise;
        }

        var _getAlbumById = function(albumId) {
            var _deferred = $q.defer();

            $http.get("api/v1/AlbumsApi/?albumId=" + albumId)
                .then(function(result) {
                        _deferred.resolve(result.data);
                    },
                    function() {
                        _deferred.reject();
                    });

            return _deferred.promise;
        }

        var _saveReview = function (newReviewModel) {
            var _deferred = $q.defer();

            $http.post("api/v1/ReviewsApi/", newReviewModel)
                .then(function(result) {

                    _deferred.resolve(result.data);

                }, function() {
                    _deferred.reject();
                });

            return _deferred.promise;
        };

        var _uploadAlbumCover = function(imagesrc) {
            var _deferred = $q.defer();

            $http.post("api/v1/AlbumsApi/?imageSrc=" + imagesrc)
                .then(function (result) {

                    _deferred.resolve(result.data);

                }, function () {
                    _deferred.reject();
                });

           

        


            return _deferred.promise;
        };

        //public api
        return {
            //properties
            genreList: _genreList,            
            latestAlbumsList: _latestAlbumsList,
            genreAlbumsList: _genreAlbumsList,
            
            //methods
            getGenres: _getGenres,
            getGenreAlbums: _getGenreAlbums,
            getLatestAlbums: _getLatestAlbums,
            getGenreById: _getGenreById,
            getArtist: _getArtist,
            getAlbumById: _getAlbumById,
            saveReview: _saveReview,

            uploadAlbumCover: _uploadAlbumCover

        };
    }
]);