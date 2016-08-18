(function () {

    angular.module("app").directive("sidebarMenu", sidebarMenuDirective);

    function sidebarMenuDirective() {
        return {
            restrict: "E",
            templateUrl: "app/core/sidebar-menu.template.html"
        };
    }

})();