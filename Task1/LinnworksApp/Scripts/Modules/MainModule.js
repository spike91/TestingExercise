var app;
(function () {
    app = angular.module("MainModule", ["CategoriesModule", "ngRoute"]);
    app.config(function ($routeProvider) {
        $routeProvider
            .when("/inventory/categories", {
                templateUrl: "Scripts/Modules/Categories/categories.html"
            })
            .otherwise({ redirectTo: '/' });
    });
})(); 