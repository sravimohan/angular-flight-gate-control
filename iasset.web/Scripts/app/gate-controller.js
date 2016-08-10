﻿angular.module('gateApp')
    .controller('gateCtrl', ['$scope', 'dataFactory',
        function ($scope, dataFactory) {

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
                    arrivalDateTime: $scope.selectedArrivalDateTime,
                    departureDateTime: $scope.selectedDepartureDateTime
                };

                dataFactory.addNewFlightDetail(data).then(function (response) {
                    $scope.frmNewFlightDetail.$setPristine();
                });
            }
        }]);
