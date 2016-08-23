var auth = (function () {
    var _user = JSON.parse(sessionStorage.getItem("user") || null);
    var _authSetting = JSON.parse(sessionStorage.getItem("oidc-config") || null);
    var mgr;

    init();

    return {
        redirectToSignIn: redirectToSignIn,
        currentUser: currentUser,
        getUser: getUser
    };

    function init() {
        if (!_user || !_authSetting) {
            redirectToSignIn();
            return;
        }

        mgr = new Oidc.UserManager(_authSetting)

        mgr.events.addUserLoaded(function (u) {
            _user = u;
        });

        mgr.events.addUserUnloaded(function () {
            _user = null;
            redirectToSignOut();
        });

        mgr.events.addAccessTokenExpiring(function () {
            console.log("token expiring");
            console.log(_user.access_token);
        });

        mgr.events.addAccessTokenExpired(function () {
            console.log("token expired");
        });

        mgr.events.addSilentRenewError(function (e) {
            console.log("silent renew error", e.message);
        });

        window.onmessage = function (e) {
            if (e.origin === window.location.protocol + "//" + window.location.host && e.data === "changed") {
                console.log("user session has changed");
                mgr.removeUser();
                mgr.signinSilent().then(function () {
                    // Session state changed but we managed to silently get a new identity token, everything's fine
                    console.log('renewTokenSilentAsync success');
                }).catch(function (err) {
                    // Here we couldn't get a new identity token, we have to ask the user to log in again
                    console.log('renewTokenSilentAsync failed', err.message);
                });
            }
        }
    }

    function redirectToSignIn() {
        window.location.href = window.location.protocol + "//" + window.location.host + "/signin.html";
    }

    function redirectToSignOut() {
        window.location.href = window.location.protocol + "//" + window.location.host + "/signedout.html";
    }

    function currentUser() {
        return _user;
    }

    function setUser(user) {
        _user = user;
    }

    function getUser() {
        return mgr.getUser();
    }
})();