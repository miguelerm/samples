(function () {
    angular.module("app").directive("topNav", topNavDirective);

    function topNavDirective() {
        return {
            restrict: "E",
            templateUrl: "app/core/top-nav.template.html"
        };
    }
})();