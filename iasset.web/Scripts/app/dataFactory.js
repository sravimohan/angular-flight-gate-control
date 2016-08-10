angular.module("gateApp")
    .factory("dataFactory", ["$http", function ($http) {

        var dataFactory = {};

        dataFactory.getGates = function () {
            return $http.get("/api/gates");
        };

        dataFactory.getFlights = function () {
            return $http.get("/api/flights");
        };

        dataFactory.searchFlightDetails = function (selectedGate, selectedDate) {
            return $http.get("/api/flightsquery?gateId=" + selectedGate + "&date=" + this.formatDateApi(selectedDate));
        };

        dataFactory.addNewFlightDetail = function (data) {
            return $http.put("/api/FlightDetail/put", data);
        }

        dataFactory.getFlightDetails = function (flightDetailId) {
            return $http.get("/api/FlightDetail/" + flightDetailId);
        }

        dataFactory.saveFlightDetail = function (data) {
            return $http.post("/api/FlightDetail/Post", data);
        }

        dataFactory.cancelFlight = function (flightDetailId) {
            return $http.delete("/api/FlightDetail/Delete/" + flightDetailId);
        }

        dataFactory.formatDate = function (date) {
            var d = new Date(date),
                month = "" + (d.getMonth() + 1),
                day = "" + d.getDate(),
                year = d.getFullYear();

            if (month.length < 2) month = "0" + month;
            if (day.length < 2) day = "0" + day;

            return [day, month, year].join("/");
        }

        dataFactory.formatDateApi = function (date) {
            var d = date.split("/");
            return [d[2], d[1], d[0]].join("-");
        }

        return dataFactory;
    }]);