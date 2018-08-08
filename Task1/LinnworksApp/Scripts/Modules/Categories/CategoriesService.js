app.service("CategoriesService", function ($http) {
    this.getCategories = function () {
        return $http.get("api/categories/getcategories");
    };
    this.updateCategory = function (category) {
        return $http.post("api/categories/updatecategory/" + category.CategoryId + "/" + category.CategoryName);
    };
    this.createCategory = function (categoryName) {
        return $http.post("api/categories/createcategory/" + categoryName);
    };
    this.deleteCategory = function (categoryId) {
        return $http.post("api/categories/deletecategory/" + categoryId);
    };
}); 