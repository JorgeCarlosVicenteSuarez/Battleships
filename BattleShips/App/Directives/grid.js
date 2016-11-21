(function (angular) {
    "use strict";
    angular.module("battleships")
        .directive("grid", ["gameService", function (gameService) {
            return {
                restrict: "E",
                replace: true,
                scope: {
                    table: "=",
                    disabled: "=",
                    computer: "@"
                },
                templateUrl: "/App/Directives/grid.html",
                link: function($scope, element, attrs) {
                    $scope.columns = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J"];
                    $scope.rows = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

                    $scope.cell = function(x, y) {
                        if ($scope.table) {
                            var cell = $scope.table[x][y];
                            if (cell) {
                                switch (cell.Content) {
                                case 0:
                                case 1:
                                    return " ";
                                case 2:
                                    case 3:
                                        if ($scope.computer === "true" && !cell.Discovered)
                                            return " ";
                                        else
                                            return "X";
                                default:
                                    return undefined;
                                }
                            }
                        }

                        return undefined;
                    };

                    $scope.isHit = function (x, y) {
                        if ($scope.table) {
                            var cell = $scope.table[x][y];
                            return cell.Discovered && cell.Content > 1;
                        }

                        return false;
                    };

                    $scope.isDiscovered = function (x, y) {
                        if ($scope.table) {
                            var cell = $scope.table[x][y];
                            return cell.Discovered;
                        }

                        return false;
                    };

                    $scope.shoot = function (x, y) {
                        if (!$scope.disabled && $scope.computer === "true" && !$scope.isDiscovered(x,y)) {
                            gameService.userShoot(x, y);
                        }
                    };
                }
            };
        }]);
})(window.angular);