angular
    .module('demo', ['ui.bootstrap'])
    .config(configurePromise)
    .controller('HerenciaController', HerenciaController)
    .controller('NuevoActivoController', NuevoActivoController)
    .controller('NuevoHerederoController', NuevoHerederoController);

function configurePromise($provide) {

    // Se modifican las promesas generadas por el $q.defer para que siempre
    // tengan las funciones success y error.
    $provide.decorator('$q', function ($delegate) {
        var defer = $delegate.defer;
        $delegate.defer = function () {
            var deferred = defer();
            var promise = deferred.promise;

            promise.success = function (fn) { promise.then(fn); return promise; };
            promise.error = function (fn) { promise.then(null, fn); return promise; };

            return deferred;
        };
        return $delegate;
    });
}

function HerenciaController($window, $modal, $log) {
    var vm = this;
    
    vm.activo = [];
    
    vm.nuevoActivo = nuevoActivo;
    vm.nuevoHeredero = nuevoHeredero;
    
    function nuevoActivo() {
        $modal.open({
          templateUrl: '/templates/nuevo-activo.html',
          controller: 'NuevoActivoController as vm',
        }).result.success(nuevoActivoCreado);
    }
    
    function nuevoHeredero() {
        $modal.open({
          templateUrl: '/templates/nuevo-heredero.html',
          controller: 'NuevoHerederoController as vm',
        }).result.success(nuevoHerederoCreado);
    }
    
    function nuevoActivoCreado(activoCreado) {
        vm.activo.push(activoCreado);
    }
    
    function nuevoHerederoCreado(heredero) {
        $log.debug('heredero: ', heredero);
    }
}

function NuevoActivoController($scope, $log) {
    var vm = this;
    
    vm.form = {};
    
    vm.cancelar = cancelar;
    vm.invalid = invalid;
    vm.aceptar = aceptar;
    
    function cancelar() {
        $scope.$dismiss();
    }
    
    function invalid(field) {
        return vm.form[field].$dirty && vm.form[field].$invalid; 
    }
    
    function aceptar() {
        var modelo = {};
        angular.copy(vm.modelo, modelo);
        $scope.$close(modelo);
    }
}

function NuevoHerederoController($scope) {
    var vm = this;
    
    vm.cancelar = cancelar;
    
    function cancelar() {
        $scope.$dismiss();
    }
}