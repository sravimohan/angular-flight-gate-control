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
        }, 10000);
    }

    var showFailure = function (message) {
        $scope.successMessage = null;

        if (message == null) {
            message = "Failed";
        }

        $scope.errorMessage = message;
    }

    var showEditSuccess = function (message) {
        $scope.editErrorMessage = null;

        if (message == null) {
            message = "Saved Successfully";
        }

        $scope.editErrorMessage = null;
        $scope.editSuccessMessage = message;

        $timeout(function () {
            $scope.editSuccessMessage = null;
        }, 10000);
    }

    var showEditFailure = function (message) {
        $scope.editSuccessMessage = null;

        if (message == null) {
            message = "Failed";
        }

        $scope.editSuccessMessage = null;
        $scope.editErrorMessage = message;

        $timeout(function () {
            $scope.editErrorMessage = null;
        }, 8000);
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
        $scope.selectedArrivalDate = dataFactory.formatDate(new Date());
        $scope.selectedDepartureDate = dataFactory.formatDate(new Date());
        $scope.selectedArrivalDateTime = dataFactory.formatDate(new Date());
        $scope.selectedDepartureDateTime = dataFactory.formatDate(new Date());

        $scope.$watch('selectedGate', function () {
            $scope.searchFlightDetails();
        });
    };

    $scope.searchFlightDetails = function () {

        if ($scope.selectedGate == null || $scope.selectedGate === "")
            return;

        if ($scope.selectedDate == null || $scope.selectedDate === "")
            return;

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
                var result = response.data;
                if (result.IsSuccess) {
                    showSuccess(result.Message);
                } else {
                    showFailure(result.Message);
                }
                $scope.frmNewFlightDetail.$setPristine();
                $scope.searchFlightDetails();
            }, function (error) {
                showFailure(error);
            });
    }

    $scope.onselectedArrivalDateChange = function() {
        $scope.selectedDepartureDate = $scope.selectedArrivalDate;
        $scope.selectedDate = $scope.selectedArrivalDate;
    }

    $scope.onselectedSearchDateChange = function () {
        $scope.selectedDepartureDate = $scope.selectedDate;
        $scope.selectedArrivalDate = $scope.selectedDate;
    }

    $scope.onEditFlightDetail = function (flightDetailId) {
        $scope.editFlightDetailId = flightDetailId;
        $scope.getFlightDetails(flightDetailId);
        $("#myModal").modal("show");
    }

    $scope.getFlightDetails = function (flightDetailId) {
        dataFactory.getFlightDetails(flightDetailId)
            .then(function (response) {
                var data = response.data;
                $scope.editSelectedGate = data.Gate.Id;
                $scope.editSelectedFlight = data.Flight.Id;
                $scope.editSelectedArrivalDate = dataFactory.formatDate(data.ArrivalTime);
                $scope.editSelectedArrivalTime = dataFactory.formatTime(data.ArrivalTime);
                $scope.editSelectedDepartureDate = dataFactory.formatDate(data.DepartureTime);
                $scope.editSelectedDepartureTime = dataFactory.formatTime(data.DepartureTime);
            });
    }

    $scope.saveFlightDetail = function () {
        var data = {
            flightDetailId: $scope.editFlightDetailId,
            flightId: $scope.editSelectedFlight,
            gateId: $scope.editSelectedGate,
            arrivalDateTime: $scope.editSelectedArrivalDate + " " + $scope.editSelectedArrivalTime,
            departureDateTime: $scope.editSelectedDepartureDate + " " + $scope.editSelectedDepartureTime
        };

        dataFactory.saveFlightDetail(data)
            .then(function (response) {
                var result = response.data;
                if (result.IsSuccess) {
                    showEditSuccess(result.Message);
                    $scope.getFlightDetails($scope.editFlightDetailId);
                    $scope.searchFlightDetails();
                } else {
                    showEditFailure(result.Message);
                }
            }, function (error) {
                showEditFailure(error);
            });
    }

    $scope.cancelFlight = function (flightDetailId) {
        dataFactory.cancelFlight(flightDetailId)
            .then(function (response) {
                $scope.searchFlightDetails();
            });
    }

    init();
});
