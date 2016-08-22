(function () {

    angular.module("app").config(configRoutes);

    function configRoutes($urlRouterProvider, $stateProvider) {
        $urlRouterProvider
            .when("",  "/home/dashboard")
            .when("/", "/home/dashboard");

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
                url: "/home",
                template: "<div ui-view />",
                parent: "default"
            })
            .state("home.dashboard", {
                url: "/dashboard",
                templateUrl: "app/home/dashboard.template.html"
            })
            .state("home.dashboard-two", {
                url: "/dashboard2",
                template: "<h1>Dashboard 2</h1>"
            })
            .state("home.dashboard-three", {
                url: "/dashboard3",
                template: "<h1>Dashboard 3</h1>"
            })
            .state("forms", {
                abstract: true,
                url: "/forms",
                template: "<div ui-view />",
                parent: "default"
            })
            .state("forms.general", {
                url: "/general",
                template: "<h1>Forms General</h1>"
            })
            .state("forms.advanced", {
                url: "/advanced",
                template: "<h1>Forms advanced</h1>"
            });
    }

})();