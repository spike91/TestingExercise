app.controller('CategoriesController', function ($scope, $rootScope, $q, $window, CategoriesService, uiGridConstants) {
    $scope.token = $rootScope.token;
    $scope.countRows = 0;
    $scope.createFormShow = false;
    $scope.category = null;
    $scope.showError = true;

    $scope.gridOptions = {
        enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
        enableRowSelection: true,
        columnDefs: [
            { name: ' ', minWidth: 50, maxWidth: 100, enableCellEdit: false, enableSorting: false },
            {
                name: 'CategoryName', displayName: "Name", field: 'CategoryName', minWidth: 200, enableCellEdit: true, cellEditableCondition: function ($scope) {
                    return !isDefaultCategory($scope.row.entity);
                }
            },
            { name: 'ProductsCount', displayName: "Products", field: 'ProductsCount', minWidth: 100, maxWidth: 200, enableCellEdit: false }
        ],
        data: []
    };

    $scope.gridOptions.multiSelect = false;
    $scope.gridOptions.modifierKeysToMultiSelect = false;

    $scope.gridOptions.onRegisterApi = function (gridApi) {
        $scope.gridApi = gridApi;
        gridApi.rowEdit.on.saveRow($scope, $scope.saveRow);

        gridApi.selection.on.rowSelectionChanged($scope, rowIsSelected);

        gridApi.selection.on.rowSelectionChangedBatch($scope, rowIsSelected);
    };

    function rowIsSelected(row) {
        if (row.entity.CategoryId === "00000000-0000-0000-0000-000000000000") {
            $scope.countRows = 0;
            return;
        }
        $scope.countRows = $scope.gridApi.selection.getSelectedRows().length;
    }

    $scope.saveRow = function (rowEntity) {
        var promise = $q.defer();
        $scope.gridApi.rowEdit.setSavePromise(rowEntity, promise.promise);
        CategoriesService.updateCategory(rowEntity).then(
            function (result) {
                promise.resolve();
            },
            function (error) {
                promise.reject();
                $scope.getCategories();
                $window.alert("Category not saved!");
            }
        );
        promise.resolve();
    };

    function isDefaultCategory(rowEntity) {
        return rowEntity.CategoryId === "00000000-0000-0000-0000-000000000000";
    }

    $scope.createCategory = function (name) {
        var createCategory = CategoriesService.createCategory(name);
        createCategory.then(
            function (result) {
                if (result.data !== null && typeof result.data !== 'undefined') {
                    $scope.gridOptions.data.push(result.data);
                    $scope.createFormShow = false;
                    $scope.category = null;
                }
            },
            function (error) {
                $window.alert("Category not saved!");
            }
        );
    };

    $scope.deleteCategory = function () {
        var selectedRow = null;
        if ($scope.gridApi.selection.getSelectedCount() === 0) return;
        selectedRow = $scope.gridApi.selection.getSelectedRows()[0];

        var deleteCategory = CategoriesService.deleteCategory(selectedRow.CategoryId);
        deleteCategory.then(
            function (result) {
                var index = $scope.gridOptions.data.indexOf(selectedRow);
                if (index >= 0) $scope.gridOptions.data.splice(index, 1);
            },
            function (error) {
            }
        );
        $scope.countRows = 0;
    };

    $scope.getCategories = function () {
        var getCategories = CategoriesService.getCategories();
        getCategories.then(
            function (result) {
                if (result.data !== null && typeof result.data !== 'undefined') {
                    $scope.gridOptions.data = [];
                    result.data.forEach(function (item) {
                        $scope.gridOptions.data.push(item);
                    });
                }
            },
            function (error) {
            }
        );
    };

    $rootScope.$watch('token', function (newValue, oldValue) {
        $scope.token = newValue;
        if ($scope.token !== null && typeof $scope.token !== 'undefined') {
            $scope.getCategories();
        }
    });
});   