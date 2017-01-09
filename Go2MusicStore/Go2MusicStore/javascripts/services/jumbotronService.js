storeApp.factory("jumbotronService", 
[
    function() {
    
        var _showJumbotron = new Boolean;

        var _setJumobtron = function (show) {

            _showJumbotron = show;
        };
        
        var _getValue = function() {
            return _showJumbotron;
        }

        return {
            showJumbotron: _showJumbotron,
            getValue: _getValue,
            setJumobtron: _setJumobtron
        };
    }
]);