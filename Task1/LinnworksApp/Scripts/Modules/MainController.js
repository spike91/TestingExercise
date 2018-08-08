app.controller('MainController', function ($scope, $rootScope, MainService) {
    $rootScope.token = null;
    $scope.input = null;
    $scope.token = null;
    $scope.pattern = "[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}";
    $scope.guid = "00000000-0000-0000-0000-000000000000";
    getToken();

    $scope.initToken = function (token) {
        var setToken = MainService.setToken(token);
        setToken.then(
            function (result) {
                getToken();
            },
            function (error) {
            }
        );
    };

    function getToken() {
        var getToken = MainService.getToken();
        getToken.then(
            function (result) {
                if (result.data !== $scope.guid) {
                    $rootScope.token = result.data;
                    $scope.token = $rootScope.token;
                }
            },
            function (error) {
            }
        );
    }
});  