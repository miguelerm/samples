(function () {

    angular.module("app").directive("topTiles", topTilesDirective);

    function topTilesDirective() {
        return {
            restrict: "E",
            templateUrl: "app/dashboard/top-tiles.template.html"
        };
    }

})();