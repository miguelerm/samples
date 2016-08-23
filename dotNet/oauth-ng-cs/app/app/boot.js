(function () {
    auth.getUser().then(function (u) {
        if (u) {
            angular.bootstrap(document, ['app']);
        } else {
            auth.redirectToSignIn();
        }
    }).catch(function (e) {
        console.log("==== user error ==== ");
        console.log(e);
    });
})();