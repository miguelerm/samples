(function () {

    angular.module("app").config(configRoutes);

    function configRoutes($urlRouterProvider, $stateProvider) {
        $urlRouterProvider
            .when("",  "/dashboard")
            .when("/", "/dashboard");

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
                abstract: true,
                template: "<div ui-view />",
                parent: "default"
            })
            .state("home.dashboard", {
                url: "/dashboard",
                templateUrl: "app/home/dashboard.template.html"
            });
    }

})();