var app = angular.module("gateApp");

app.controller("gateEditCtrl", function ($scope, $http, $window, $timeout, dataFactory) {
    $scope.flightDetailId = null;
    $scope.selectedGate = null;
    $scope.selectedFlight = null;
    $scope.selectedArrivalDateTime = null;
    $scope.selectedDepartureDateTime = null;

    $scope.selectedDate = null;
    $scope.gates = null;
    $scope.flights = null;
    $scope.flightDetails = null;

    var showSuccess = function (message) {
        $scope.errorMessage = null;

        if (message == null) {
            message = "Saved Successfully";
        }

        $scope.successMessage = message;
        $timeout(function () {
            $scope.successMessage = null;
        }, 2000);
    }

    var showFailure = function (message) {
        $scope.successMessage = null;

        if (message == null) {
            message = "Failed";
        }

        $scope.errorMessage = message;
        $timeout(function () {
            $scope.errorMessage = null;
        }, 2000);
    }

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
                showSuccess();
            }, function(error) {
                showFailure(error);
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
