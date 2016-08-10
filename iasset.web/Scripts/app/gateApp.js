var app = angular.module("gateApp", []);

app.directive("jqdatepicker", function () {
    return {
        restrict: "A",
        require: "ngModel",
        link: function (scope, element) {
            element.datepicker({
                dateFormat: "d/mm/yy",
                onSelect: function (date) {
                    scope.selectedDate = date;
                    scope.$apply();
                }
            });
        }
    };
});