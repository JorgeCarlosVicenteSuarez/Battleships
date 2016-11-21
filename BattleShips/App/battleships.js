(function (angular) {

    var app = angular.module("battleships", ["ngMaterial"]);

    app.config(["$locationProvider", function ($locationProvider) {
        $locationProvider.html5Mode(true);
    }]);

})(window.angular);