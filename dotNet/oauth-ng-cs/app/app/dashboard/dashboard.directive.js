(function () {

    angular.module("app").directive("dashboard", dashboardDirective);

    function dashboardDirective() {
        return {
            restrict: "E",
            templateUrl: "app/dashboard/dashboard.template.html"
        };
    }

})();