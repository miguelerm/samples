(function () {

    angular.module("app").service("productosServicio", ProductosServicio);

    function ProductosServicio($http) {

        var api = "http://localhost:51902/api";
        var elementosPorPagina = 20;

        var svc = this;

        svc.obtenerTodos = obtenerTodos;

        function obtenerTodos(pagina) {

            if (!pagina || pagina < 1) {
                pagina = 1;
            }

            var url = api + "/productos?pagina=" + pagina + "&elementos=" + elementosPorPagina;
            return $http.get(url, { headers : getAuthorizationHeader() }).then(function (result) { return result.data });
        }

        function getAuthorizationHeader() {
            // obtiene el header de HTTP necesario para enviar el token 
            // de autenticación para la api.
            //
            // auth es una variable global, definida por /app/auth.js
            return { 'Authorization': 'Bearer ' + auth.currentUser().access_token };
        }

    }

})();