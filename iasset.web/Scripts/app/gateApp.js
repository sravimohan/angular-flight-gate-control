var app = angular.module("gateApp", []);

app.directive("jqdatepicker", function () {
    return {
        restrict: "A",
        require: "ngModel",
        link: function (scope, element, attrs, ctrl) {
            $(element).datepicker({
                dateFormat: "d/mm/yy",
                onSelect: function (date) {
                    ctrl.$setViewValue(date);
                    ctrl.$render();
                    scope.$apply();
                }
            });
        }
    };
});

app.directive("jqtimepicker", function () {
    return {
        restrict: "A",
        require: "ngModel",
        link: function (scope, element, attrs, ctrl) {
            $(element).timePicker();
        }
    };
});
