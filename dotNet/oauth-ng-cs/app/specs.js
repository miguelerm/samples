function successPromise(result) {
    return function () {
        return {
            then: function (callback) {
                return callback(result);
            }
        };
    };
}