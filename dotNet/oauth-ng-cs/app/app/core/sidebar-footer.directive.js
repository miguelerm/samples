(function () {

    angular.module("app").directive("sidebarFooter", sidebarFooterDirective);

    function sidebarFooterDirective() {
        return {
            restrict: "E",
            templateUrl: "app/core/sidebar-footer.template.html"
        };
    }

})();