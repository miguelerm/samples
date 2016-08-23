(function () {
    angular.module("app").directive("profile", profileDirective);

    function profileDirective() {
        return {
            restrict: "E",
            templateUrl: "app/core/profile.template.html"
        };
    }
})();