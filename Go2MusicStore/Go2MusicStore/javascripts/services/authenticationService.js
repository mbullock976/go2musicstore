storeApp.factory('authenticationService',
[
    "$http", "$q",
    function($http, $q) {

        var _getAuthentication = function() {
            var _deferred = $q.defer();

            $http.get("api/v1/AuthenticationApi/")
                .then(function (result) {
                        _deferred.resolve(result.data);
                    },
                    function() {
                        _deferred.reject();
                    });

            return _deferred.promise;
        }

        return {
            getAuthentication: _getAuthentication
    };

}]);