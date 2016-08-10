var app = angular.module('flightGateApp', []);
app.controller('gateEditCtrl', function ($scope, $http, $window) {
    $scope.flightDetailId = null;
    $scope.selectedGate = null;
    $scope.selectedFlight = null;
    $scope.selectedArrivalDateTime = null;
    $scope.selectedDepartureDateTime = null;

    $scope.selectedDate = null;
    $scope.gates = null;
    $scope.flights = null;
    $scope.flightDetails = null;

    $http.get("/api/gates")
        .then(function (response) {
            $scope.gates = response.data;
        });

    $http.get("/api/flights")
        .then(function (response) {
            $scope.flights = response.data;
        });

    $scope.getFlightDetails = function () {
        $http.get("/api/FlightDetail/" + $scope.flightDetailId).then(function (response) {
            var data = response.data;
            $scope.selectedGate = data.Gate.Id;
            $scope.selectedFlight = data.Flight.Id;
            $scope.selectedArrivalDateTime = data.ArrivalTime;
            $scope.selectedDepartureDateTime = data.DepartureTime;
        });
    }
    
    $scope.saveFlightDetail = function () {
        var data = {
            flightDetailId: $scope.flightDetailId,
            flightId: $scope.selectedFlight,
            gateId: $scope.selectedGate,
            arrivalDateTime: $scope.selectedArrivalDateTime,
            departureDateTime: $scope.selectedDepartureDateTime
        };

        return $http.post('/api/FlightDetail/Post', data).then(function (response) {
        });
    }

    $scope.cancelFlight = function() {
        return $http.delete('/api/FlightDetail/Delete/' + $scope.flightDetailId).then(function (response) {
            $window.location.href = '/';
        });
    }

    $scope.$watch($scope.flightDetailId, function () {
        $scope.getFlightDetails();
    });
});
