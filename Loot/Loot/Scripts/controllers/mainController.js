var app = angular.module('app', ['ui.bootstrap']);

app.controller('MainController', ['$scope', '$interval', function ($scope, $interval) {

    buildViewModel($scope, $interval);
}]);