(function () {

    angular.module("app").directive("dashboardGraph", dashboardGraphDirective);

    function dashboardGraphDirective() {
        return {
            restrict: "E",
            templateUrl: "app/dashboard/dashboard-graph.template.html"
        };
    }

})();