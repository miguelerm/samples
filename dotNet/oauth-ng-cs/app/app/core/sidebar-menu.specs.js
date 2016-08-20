describe("SidebarMenuController", function () {

    
    var createController;

    beforeEach(module("app"));

    beforeEach(inject(function ($controller) {
        createController = function (data) {
            return $controller('SidebarMenuController', {
                sidebarMenuService: {
                    getSections: successPromise(data)
                }
            });
        };
    }));

    it("Existe", function () {
        var ctrl = createController();
        expect(ctrl).toBeDefined();
    });

});