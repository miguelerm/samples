(function () {

    angular.module("app").controller("SidebarMenuController", SidebarMenuController);

    function SidebarMenuController(sidebarMenuService) {
        var vm = this;

        vm.currentItem = null;
        vm.sections = [];

        vm.isActive = isActive;
        vm.activate = activate;
        vm.hasChilds = hasChilds;
        vm.displayMode = displayMode;

        init();

        function init() {
            sidebarMenuService.getSections().then(loadSections);
        }

        function isActive(item) {

            var current = vm.currentItem;

            if (current === null) return false;

            if (current === item) return true;
            
            var items = current.items;

            if (items && items.length) {
                for (var i = 0; i < items.length; i++) {
                    if (items[i] === item) return true;

                    var subItems = items[i].items;

                    if (subItems && subItems.length) {
                        for (var j = 0; j < subItems.length; j++) 
                            if (subItems[j] === item) return true;
                    }
                }
            }

            return false;
        }

        function activate(item) {
            vm.currentItem = isActive(item) ? null : item;
        }

        function hasChilds(item) {
            return item.items && item.items.length;
        }

        function displayMode(item) {
            return vm.currentItem === item ? 'block' : null;
        }

        function loadSections(sections) {

            vm.sections = sections;

        }
    }

})();