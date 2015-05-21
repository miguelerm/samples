/* globals angular */

angular
    .module('demo', ['ui.bootstrap'])
    .constant('underscore', window._)
    .config(configurePromise)
    .service('activos', ActivosServicio)
    .service('herederos', HerederosServicio)
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

function ActivosServicio($q, $timeout) {
    
    var lastId = 1;
    var activos = [{ id: lastId, codigo: 100, nombre: 'Terreno', valor: 158320.57, valorHeredado: 0 }];
    
    var svc = this;
    
    svc.agregar = agregar;
    svc.obtenerTodos = obtenerTodos;
    
    function agregar(activo) {
        var item = {id:0, codigo:0, nombre:'', valor: 0, valorHeredado: 0 };
        angular.copy(activo, item);
        item.id = ++lastId;
        item.valorHeredado = 0;
        activos.push(item);
        return resolvePromise(item);
    }
    
    function obtenerTodos() {
        return resolvePromise(activos);
    }
    
    function resolvePromise(resultado) {
        var deferred = $q.defer();
        
        $timeout(function () {
            deferred.resolve(resultado);
        }, 500);
        
        return deferred.promise;
    }
}

function HerederosServicio($q, $timeout, underscore) {
    var lastId = 1;
    var herederos = [{ id: lastId, nombre: 'Juan Pérez', cantidadActivos: 0, montoActivos: 0 }];
    
    var svc = this;
    
    svc.agregar = agregar;
    svc.obtenerTodos = obtenerTodos;
    svc.eliminar = eliminar;

    function agregar(heredero) {
        var item = { id: 0, nombre: '', cantidadActivos: 0, montoActivos: 0 };
        angular.copy(heredero, item);
        item.id = ++lastId;
        item.cantidadActivos = 0;
        item.montoActivos = 0;
        herederos.push(item);
        return resolvePromise(item);
    }
    
    function obtenerTodos() {
        return resolvePromise(herederos);
    }
    
    function eliminar(id) {
        var heredero = underscore.findWhere(herederos, { id: id });
        if (heredero) {
            var index = herederos.indexOf(heredero);
            herederos.splice(index, 1);
        }
        return resolvePromise(heredero);
    }
    
    function resolvePromise(resultado) {
        var deferred = $q.defer();
        
        $timeout(function () {
            deferred.resolve(resultado);
        }, 500);
        
        return deferred.promise;
    }
}

function HerenciaController($window, $modal, $log, activos, herederos) {
    var vm = this;
    
    vm.activos = [];
    vm.herederos = [];
    
    vm.nuevoActivo = nuevoActivo;
    vm.nuevoHeredero = nuevoHeredero;
    vm.eliminarHeredero = eliminarHeredero;
    
    init();
    
    function init() {
        consultarActivos();
        consultarHerederos();
    }
    
    function consultarActivos() {
        activos.obtenerTodos().success(cargarActivos);
    }
    
    function consultarHerederos() {
        herederos.obtenerTodos().success(cargarHerederos);
    }

    function cargarHerederos(items) {
        vm.herederos = items;
    }
    
    function cargarActivos(items) {
        vm.activos = items;
    }
    
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
        if (!activoCreado) return;
        vm.activos.push(activoCreado);
    }
    
    function nuevoHerederoCreado(heredero) {
        if (!heredero) return;
        vm.herederos.push(heredero);
    }
    
    function eliminarHeredero(heredero) {
        herederos.eliminar(heredero.id).success(function () {
            var index = vm.herederos.indexOf(heredero);
            vm.herederos.splice(index, 1);
        });
    }
}

function NuevoActivoController($scope, $log, activos) {
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
        if (vm.form.$invalid) return;
        var modelo = {};
        angular.copy(vm.modelo, modelo);
        activos.agregar(modelo).success($scope.$close);
    }
}

function NuevoHerederoController($scope, herederos) {
    
    var vm = this;
    
    vm.form = {};
    
    vm.cancelar = cancelar;
    vm.invalid = invalid;
    vm.aceptar = aceptar;
    
    function aceptar() {
        if (vm.form.$invalid) return;
        var modelo = {};
        angular.copy(vm.modelo, modelo);
        herederos.agregar(modelo).success($scope.$close);
    }
    
    function cancelar() {
        $scope.$dismiss();
    }
    
    function invalid(field) {
        return vm.form[field].$dirty && vm.form[field].$invalid; 
    }
}