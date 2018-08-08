app.service("MainService", function ($http) {
    this.setToken = function (token) {
        return $http.get("api/main/settoken/" + token);
    };
    this.getToken = function () {
        return $http.get("api/main/gettoken");
    };
}); 