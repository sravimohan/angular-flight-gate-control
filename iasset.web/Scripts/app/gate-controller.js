var app = angular.module('flightGateApp', []);
app.controller('gateCtrl', function ($scope, $http) {

    $scope.selectedGate = null;
    $scope.selectedDate = null;
    $scope.gates = null;
    $scope.flightDetails = null;

    $http.get("/api/gates")
    .then(function(response) {
        $scope.gates = response.data;
    });

    $scope.searchFlightDetails = function() {

        $http.get("/api/flightsquery?gateId=" + $scope.selectedGate + "&date=" + formatDate($scope.selectedDate))
        .then(function (response) {
            $scope.flightDetails = response.data;
        });
    }

    function formatDate(date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [year, month, day].join('-');
    }
});
