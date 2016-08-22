(function () {

    angular.module("app").controller("SidebarMenuController", SidebarMenuController);

    function SidebarMenuController(sidebarMenuService, $state, $rootScope, $log) {
        var vm = this;

        var lastCategory;
        var lastLink;

        vm.sections = [];

        vm.expand = expand;
        vm.hasChilds = hasChilds;

        init();

        function init() {
            var state = $state && $state.$current && $state.$current.name;
            $log.debug('current state: ', state);
            sidebarMenuService.getSections().then(function (result) {
                loadSections(result);
                setActive(state);
            });
            $rootScope.$on('$stateChangeSuccess', stateChangeSuccess);
        }

        function expand(item) {
            var shouldExpandItem = lastCategory !== item;

            resetLastCategoryAndLink();

            item.active = shouldExpandItem;
            item.expanded = shouldExpandItem;

            if (shouldExpandItem) {
                lastCategory = item;
            } else {
                lastCategory = null;
            }
        }

        function resetLastCategoryAndLink() {
            if (lastCategory) {
                lastCategory.active = false;
                lastCategory.expanded = false;
            }

            if (lastLink) {
                lastLink.active = false;
                lastLink = null;
            }
        }

        function hasChilds(item) {
            return !!(item.items && item.items.length);
        }

        function loadSections(sections) {
            vm.sections = sections;
        }

        function setActive(state) {

            resetLastCategoryAndLink();

            var sections = vm.sections;

            if (!sections || !sections.length) return;

            for (var i = 0; i < sections.length; i++) {
                var categories = sections[i].items;
                expandCategoryByState(categories, state);
            }
        }

        function expandCategoryByState(categories, state) {

            if (!categories || !categories.length) return;

            for (var i = 0; i < categories.length; i++) {
                var category = categories[i];
                if (linksContainsState(category.items, state)) {
                    category.active = true;
                    category.expanded = true;
                    lastCategory = category;
                    return true;
                }
                
            }
        }

        function linksContainsState(links, state) {
            if (!links || !links.length) return;

            for (var i = 0; i < links.length; i++) {
                var link = links[i];
                if (link.state === state) {
                    link.active = true;
                    lastLink = link;
                    return true;
                }
            }
        }

        function stateChangeSuccess(event, toState, toParams, fromState, fromParams) {
            var state = toState && toState.name;
            $log.debug('state changed to: ', state);
            setActive(state);
        }
    }

})();