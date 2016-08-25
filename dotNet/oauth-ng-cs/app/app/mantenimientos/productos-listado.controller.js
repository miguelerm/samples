(function () {

    angular.module("app").controller("ProductosListadoController", ProductosListadoController);

    function ProductosListadoController(productosServicio) {

        var vm = this;

        vm.pagina = 1;
        vm.productos = [];

        init();

        function init() {
            obtenerProductos();
        }

        function obtenerProductos() {
            productosServicio.obtenerTodos(vm.pagina).then(cargarProductos);
        }

        function cargarProductos(productos) {
            console.log(productos);
            vm.productos = productos;
        }
    }

})();