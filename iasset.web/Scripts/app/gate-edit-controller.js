var app = angular.module("gateApp");

app.controller("gateEditCtrl", function ($scope, $http, $window, dataFactory) {
    $scope.flightDetailId = null;
    $scope.selectedGate = null;
    $scope.selectedFlight = null;
    $scope.selectedArrivalDateTime = null;
    $scope.selectedDepartureDateTime = null;

    $scope.selectedDate = null;
    $scope.gates = null;
    $scope.flights = null;
    $scope.flightDetails = null;

    dataFactory.getGates()
    .then(function (response) {
        $scope.gates = response.data;
    });

    dataFactory.getFlights()
    .then(function (response) {
        $scope.flights = response.data;
    });

    $scope.getFlightDetails = function () {
        dataFactory.getFlightDetails($scope.flightDetailId)
            .then(function (response) {
                var data = response.data;
                $scope.selectedGate = data.Gate.Id;
                $scope.selectedFlight = data.Flight.Id;
                $scope.selectedArrivalDate = dataFactory.formatDate(data.ArrivalTime);
                $scope.selectedArrivalTime = dataFactory.formatTime(data.ArrivalTime);
                $scope.selectedDepartureDate = dataFactory.formatDate(data.DepartureTime);
                $scope.selectedDepartureTime = dataFactory.formatTime(data.DepartureTime);
        });
    }
    
    $scope.saveFlightDetail = function () {
        var data = {
            flightDetailId: $scope.flightDetailId,
            flightId: $scope.selectedFlight,
            gateId: $scope.selectedGate,
            arrivalDateTime: $scope.selectedArrivalDate + " " + $scope.selectedArrivalTime,
            departureDateTime: $scope.selectedDepartureDate + " " + $scope.selectedDepartureTime
        };

        dataFactory.saveFlightDetail(data)
            .then(function (response) {
                alert("saved");
            });
    }

    $scope.cancelFlight = function () {

        dataFactory.cancelFlight($scope.flightDetailId)
            .then(function (response) {
            $window.location.href = "/";
        });
    }

    $scope.$watch($scope.flightDetailId, function () {
        $scope.getFlightDetails();
    });
});
