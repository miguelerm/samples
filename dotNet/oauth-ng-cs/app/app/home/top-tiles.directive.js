(function () {

    angular.module("app").directive("topTiles", topTilesDirective);

    function topTilesDirective() {
        return {
            restrict: "E",
            templateUrl: "app/home/top-tiles.template.html"
        };
    }

})();