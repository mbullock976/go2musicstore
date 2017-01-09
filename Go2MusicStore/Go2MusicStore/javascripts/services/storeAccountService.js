storeApp.factory("storeAccountService",
[
    "$http", "$q",
    function($http, $q) {

        var _countries = [];
        var _creditCardTypesList = [];
        var _creditCardsForStoreAccountList = [];
        //var _shoppingCartItemsList = [];

        var _getStoreAccountAndAuthenticate = function () {

            var _deferred = $q.defer();

            $http.get("api/v1/StoreAccountsApi")
                .then(function (result) {
                    _deferred.resolve(result.data);

            },
                    function () {
                        _deferred.reject();
                });

            return _deferred.promise;
        };

        var _getStoreAccount = function(userIdentityName) {

            var _deferred = $q.defer();

            $http.get("api/v1/StoreAccountsApi/?userIdentityName=" + userIdentityName)
                .then(function (result) {                        
                    _deferred.resolve(result.data);

                    },
                    function () {                        
                        _deferred.reject();
                    });

            return _deferred.promise;
        };

        var _getCountries = function () {

            var _deferred = $q.defer();

            $http.get("api/v1/CountriesApi/")
                .then(function (result) {
                        angular.copy(result.data, _countries);

                        _deferred.resolve();

                },
                    function () {
                        _deferred.reject();
                    });

            return _deferred.promise;
        };

        var _getCountryById = function (countryId) {

            var _deferred = $q.defer();

            $http.get("api/v1/CountriesApi/?countryId=" + countryId)
                .then(function (result) {
                    _deferred.resolve(result.data);

                },
                    function () {
                        _deferred.reject();
                    });

            return _deferred.promise;
        };

        var _saveStoreAccount = function (newStoreAccountModel) {
            
            var _deferred = $q.defer();

            $http.post("api/v1/storeAccountsApi/", newStoreAccountModel)
                .then(function (result) {
                    _deferred.resolve(result.data);
                },
                    function () {
                        _deferred.reject();
                    });

            return _deferred.promise;
        }

        var _updateStoreAccount = function (storeAccountModel) {

            var _deferred = $q.defer();
            
            $http.put("api/v1/storeAccountsApi/", storeAccountModel)
                .then(function (result) {
                    _deferred.resolve(result.data);
                },
                    function () {
                        _deferred.reject();
                    });

            return _deferred.promise;
        }

        var _getCreditCardsByStoreAccount = function (userIdentityName) {

            var _deferred = $q.defer();

            $http.get("api/v1/CreditCardsApi/GetByStoreAccount/?userIdentityName=" + userIdentityName)
                .then(function(result) {
                    angular.copy(result.data, _creditCardsForStoreAccountList);
                    _deferred.resolve();
                }, function() {
                    _deferred.reject();
                })

            return _deferred.promise;
        }

        var _saveCreditCardForStoreAccount =
            function (_creditCardTypeId, _cardNumber, storeAccountId) {

            var _deferred = $q.defer();
            var newCreditCardModel = {
                creditCardTypeId: _creditCardTypeId,
                cardNumber: _cardNumber,
                storeAccountId: storeAccountId
            };

            $http.post(
                'api/v1/creditCardsApi/',
                JSON.stringify(newCreditCardModel),
                {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }
            ).then(function (data) {
                _deferred.resolve(data);
            }, function() {
                _deferred.reject();
            });

            return _deferred.promise;
        }

        var _updateCreditCardForStoreAccount = function (creditCardModel) {
            var _deferred = $q.defer();
            $http.put("api/v1/creditCardsApi/", creditCardModel)
                .then(function (result) {
                    _deferred.resolve(result.data);
                },
                    function () {
                        _deferred.reject();
                    });

            return _deferred.promise;
        }

        var _deleteCreditCardForStoreAccount = function(creditCardId) {
            var _deferred = $q.defer();

            $http.delete('api/v1/creditCardsApi/' + creditCardId)
                .then(function (data) {
                    _deferred.resolve(data);
                }, function () {
                    _deferred.reject();
            });

            return _deferred.promise;
        };
        
        var _getCreditCardTypes = function() {
            var _deferred = $q.defer();

            $http.get("api/v1/creditCardTypesApi/")
                .then(function (result) {
                        angular.copy(result.data, _creditCardTypesList);
                    _deferred.resolve(result.data);
                },
                    function () {
                        _deferred.reject();
                    });

            return _deferred.promise;
        }

        var _getItemsForShoppingCart = function (shoppingCartId) {
            var _deferred = $q.defer();

            $http.get("api/v1/shoppingCartItemsApi/GetByShoppingCartId/?shoppingCartId=" + shoppingCartId)
                .then(function (result) {
                        //angular.copy(result.data, _shoppingCartItemsList);
                    _deferred.resolve(result.data);
                },
                    function () {
                        _deferred.reject();
                    });

            return _deferred.promise;
        }

        var _updateShoppingCartItem = function (shoppingCartItemModel) {
            var _deferred = $q.defer();

            $http.put("api/v1/shoppingCartItemsApi/", shoppingCartItemModel)
                .then(function (result) {
                    _deferred.resolve(result.data);
                },
                    function () {
                        _deferred.reject();
                    });

            return _deferred.promise;
        }

        var _deleteShoppingCartItemForStoreAccount = function (shoppingCartItemId) {
            var _deferred = $q.defer();

            $http.delete('api/v1/shoppingCartItemsApi/' + shoppingCartItemId)
                .then(function (data) {
                    _deferred.resolve(data);
                }, function () {
                    _deferred.reject();
                });

            return _deferred.promise;
        };

        var _addNewShoppingCartItem = function(shoppingCartItemModel) {
            var _deferred = $q.defer();

            $http.post('api/v1/shoppingCartItemsApi/', shoppingCartItemModel)
                .then(function (data) {
                    _deferred.resolve(data);
                }, function () {
                    _deferred.reject();
                });

            return _deferred.promise;
        };

        var _getShoppingCartItemByAlbum = function(shoppingCartId, albumId) {
            var _deferred = $q.defer();

            $http.get('api/v1/shoppingCartItemsApi/GetByShoppingCartAndAlbum/', {params: {"shoppingCartId": shoppingCartId, "albumId": albumId} })
                .then(function (result) {
                    _deferred.resolve(result.data);
                }, function () {
                    _deferred.reject();
                });

            return _deferred.promise;
        };

        var _savedNewPurchaseOrder = function (purchaseOrder, purchaseOrderItems) {
            var _deferred = $q.defer();

            $http.post('api/v1/purchaseOrdersApi/', purchaseOrder)
                .then(function (result) {

                    var id = result.data.purchaseOrderId;

                    for (var i = 0; i < purchaseOrderItems.length; i++) {
                        purchaseOrderItems[i].purchaseOrderId = id;
                    }

                    $http.post('api/v1/purchaseOrderItemsApi/', purchaseOrderItems)
                        .then(function() {
                                _deferred.resolve(result.data);
                            },
                            function() {
                                _deferred.reject();
                            });

                    
                }, function () {
                    _deferred.reject();
                });

            return _deferred.promise;
        };

        var _getPurchasedOrders = function (storeAccountId) {
            var _deferred = $q.defer();

            $http.get('api/v1/purchaseOrdersApi/getByStoreAccountId/?storeAccountId=' + storeAccountId)
                .then(function (result) {

                   _deferred.resolve(result.data);
                   
                }, function () {
                    _deferred.reject();
                });

            return _deferred.promise;
        };

        var _emptyShoppingCart = function(shoppingCartId) {
            var _deferred = $q.defer();

            $http.delete('api/v1/shoppingCartItemsApi/deleteByShoppingCartId/?shoppingCartId=' + shoppingCartId)
                .then(function () {

                    _deferred.resolve();

                }, function () {
                    _deferred.reject();
                });

            return _deferred.promise;
        };

        return {
            countries: _countries,
            creditCardTypesList: _creditCardTypesList,
            creditCardsForStoreAccountList: _creditCardsForStoreAccountList,
            //shoppingCartItemsList: _shoppingCartItemsList,

            getStoreAccountAndAuthenticate: _getStoreAccountAndAuthenticate,
            getStoreAccount: _getStoreAccount,
            getCountries: _getCountries,
            getCountryById: _getCountryById,
            getCreditCardTypes: _getCreditCardTypes,
            getCreditCardsByStoreAccount: _getCreditCardsByStoreAccount,

            saveStoreAccount: _saveStoreAccount,
            updateStoreAccount: _updateStoreAccount,

            saveCreditCardForStoreAccount: _saveCreditCardForStoreAccount,
            updateCreditCardForStoreAccount: _updateCreditCardForStoreAccount,
            deleteCreditCardForStoreAccount: _deleteCreditCardForStoreAccount,

            getItemsForShoppingCart: _getItemsForShoppingCart,
            updateShoppingCartItem: _updateShoppingCartItem,
            deleteShoppingCartItemForStoreAccount: _deleteShoppingCartItemForStoreAccount,
            addNewShoppingCartItem: _addNewShoppingCartItem,
            getShoppingCartItemByAlbum: _getShoppingCartItemByAlbum,

            savedNewPurchaseOrder: _savedNewPurchaseOrder,
            getPurchasedOrders: _getPurchasedOrders,

            emptyShoppingCart: _emptyShoppingCart
    };
    }
]);