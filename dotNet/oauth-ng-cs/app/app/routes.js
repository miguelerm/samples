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
            .state("mantenimientos", {
                abstract: true,
                url: "/mantenimientos",
                template: "<div ui-view />",
                parent: "default"
            })
                .state("mantenimientos.productos", {
                    url: "/productos",
                    controller: "ProductosListadoController",
                    controllerAs: "vm",
                    templateUrl: "app/mantenimientos/productos-listado.template.html"
                })
                .state("mantenimientos.productos-crear", {
                    url: "/productos/crear",
                    controller: "ProductosCrearController",
                    controllerAs: "vm",
                    templateUrl: "app/mantenimientos/productos-crear.template.html"
                });
    }
})();