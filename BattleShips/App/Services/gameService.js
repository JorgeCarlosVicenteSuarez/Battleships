(function (angular) {

    angular.module("battleships").factory("gameService", ["$rootScope", "$http", function ($rootScope, $http) {

        var object = {};

        $rootScope.models = {};

        object.initGame = function() {
            $http.get("/api/Game/InitGame").then(function(result) {
                $rootScope.models = result.data;
            });
        };

        object.userShoot = function(x, y) {
            $http.get("/api/Game/UserShoot?x=" + x + "&y=" + y).then(function(result) {
                $rootScope.models = result.data;
            });
        };

        return object;
    }]);

})(window.angular);