(function (angular) {
    "use strict";
    angular.module("battleships").controller("gameController", ["$scope", "gameService",
        function ($scope, gameService) {

            $scope.$root.models = {};

            $scope.labels = {
                title: "BATTLESHIPS",
                initGame: "INIT GAME",
                yourShips: "YOUR SHIPS",
                computerShips: "COMPUTER SHIPS",
                destroyer: "DESTROYER",
                battleship: "BATTLESHIP",
                playerWins: "You are the winner!!!",
                computerWins: "Computer wins!!!"
            };

            $scope.initGame = function () {
                gameService.initGame();
            };

            $scope.getShipDescription = function(content) {
                switch (content) {
                    case 2 :
                        return $scope.labels.destroyer;
                    case 3:
                        return $scope.labels.battleship;
                }
            }

            $scope.initGame();
        }
    ]);
})(window.angular);