var app = angular.module("gateApp");

app.controller("gateCtrl", function ($scope, $timeout, dataFactory) {
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

    $scope.searchFlightDetails = function () {
        dataFactory.searchFlightDetails($scope.selectedGate, $scope.selectedDate)
        .then(function (response) {
            $scope.flightDetails = response.data;
        });
    }

    $scope.addNewFlightDetail = function () {

        var data = {
            flightId: $scope.selectedFlight,
            gateId: $scope.selectedGate,
            arrivalDateTime: $scope.selectedArrivalDate + " " + $scope.selectedArrivalTime,
            departureDateTime: $scope.selectedDepartureDate + " " + $scope.selectedDepartureTime
        };

        dataFactory.addNewFlightDetail(data)
            .then(function (response) {
                showSuccess();
                $scope.frmNewFlightDetail.$setPristine();
            }, function (error) {
                showFailure(error);
            });
    }

    var init = function () {

        dataFactory.getGates()
            .then(function (response) {
                $scope.gates = response.data;

                if (response.data != null) {
                    $scope.selectedGate = response.data[0].Id;
                    $scope.searchFlightDetails();
                }
            });

        dataFactory.getFlights()
            .then(function (response) {
                $scope.flights = response.data;

                if (response.data != null) {
                    $scope.selectedFlight = response.data[0].Id;
                }
            });

        $scope.selectedDate = dataFactory.formatDate(new Date());
        $scope.selectedArrivalDateTime = dataFactory.formatDate(new Date());
        $scope.selectedDepartureDateTime = dataFactory.formatDate(new Date());
    };

    init();
});
