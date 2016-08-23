(function () {
	angular.module("app").directive("ping", PingDirective);

	function PingDirective() {
		return {
			restrict: "E",
			template: "<h2>Ping Result</h2><ul><li ng-repeat=\"result in vm.results\" ng-bind=\"::result\"></li></ul>",
			controller: PingController,
			controllerAs: "vm"
		};
	}

	function PingController($http, $log) {
		var vm = this;

		vm.results = [];

		init();

		function init() {
			loadPingResults();
		}

		function loadPingResults() {
			var methods = ["GET", "POST", "PUT", "PATCH", "DELETE"];
			for (var i = 0; i < methods.length; i++) {
				var method = methods[i];
				$http({
					method: method,
					url: 'http://localhost:51902/api/ping',
					headers: { 'Authorization': 'Bearer ' + auth.currentUser().access_token },
				}).then(function (r) {
					vm.results.push(r.data);
				}).catch(function (e) {
					$log.error(e);
				});
			}
		}
	}
})();