//this needs to be injected at the top level page which is storeController,
//so that storeAlbumController is connected to the storeHub

storeApp.factory('signalRService',
["$rootScope",
 
    function ($rootScope) {

        var _outOfstockEvent = {
            
            subscribe: function(scope, callback) {
                var handler = $rootScope.$on('notifyStock-signalr-event', callback);
                scope.$on('$destroy', handler);
            },

            notify: function(albumId, isOutOfStock) {
                $rootScope.$emit('notifyStock-signalr-event', { albumId: albumId, isOutOfStock: isOutOfStock });
            }
        
        };
        
        //real time signalR updates for out of stock notications
        var connection = $.hubConnection();
        var storeHub = connection.createHubProxy("StoreHub");
        storeHub.on("notifyStock", function (albumId, isOutOfStock) {
            outOfStockEvent.notify(albumId, isOutOfStock);
        });

        connection.start()
            .done(function () {
                console.log('Connected');
            })
            .fail(function () { console.log('Failed to connect Connected'); });

        return {
            outOfStockEvent: _outOfstockEvent
        };

    }]);