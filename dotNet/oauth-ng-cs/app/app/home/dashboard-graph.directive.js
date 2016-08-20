(function () {

    angular.module("app").directive("dashboardGraph", dashboardGraphDirective);

    function dashboardGraphDirective() {
        return {
            restrict: "E",
            templateUrl: "app/home/dashboard-graph.template.html"
        };
    }

})();