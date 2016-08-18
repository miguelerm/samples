(function () {

    angular.module("app").config(configRoutes);

    function configRoutes($urlRouterProvider, $stateProvider) {
        $urlRouterProvider.when('', '/');

        $stateProvider
            .state("default", {
                abstract: true,
                url: "",
                controller: "LayoutController",
                controllerAs: "vm",
                views: {
                    layout: {
                        templateUrl: "app/core/layout.template.html"
                    }
                }
            })
            .state("home", {
                url: "/",
                controller: "HomeController",
                controllerAs: "vm",
                templateUrl: "app/core/home.template.html",
                parent: "default"
            });
    }

})();