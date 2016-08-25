(function () {
    angular.module("app").service("sidebarMenuService", SidebarMenuService);

    function SidebarMenuService($q) {
        var svc = this;

        svc.getSections = getSections;

        function getSections() {
            return $q.resolve([
                {
                    text: "General",
                    items: [
                        {
                            text: "Home",
                            icon: "home",
                            items: [
                                {
                                    text: "Dashboard",
                                    state: "home.dashboard"
                                }
                            ]
                        },
                        {
                            text: "Mantenimientos",
                            icon: "edit",
                            items: [
                                {
                                    text: "Productos",
                                    state: "mantenimientos.productos"
                                }
                            ]
                        }
                    ]
                }
            ]);
        }
    }
})();