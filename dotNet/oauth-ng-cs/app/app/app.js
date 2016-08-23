(function () {
    angular.module("app", ["ui.router", "ui.bootstrap"]).run(setTitle);

    function setTitle($rootScope) {
        $rootScope.title = "App";
    }
})();