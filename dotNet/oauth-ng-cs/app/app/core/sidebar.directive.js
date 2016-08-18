(function () {

    angular.module("app").directive("sidebar", sidebarDirective);

    function sidebarDirective() {
        return {
            restrict: "E",
            templateUrl: "app/core/sidebar.template.html"
        };
    }

})();