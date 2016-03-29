angular.module("umbraco").controller("NorthernGround.uContentDropdown",
    function ($scope, uContentDropdownResource) {

        $scope.xpath = $scope.model.config.xpath;

        // value of property
        //$scope.model.value

        if ($scope.model.value === null || $scope.model.value === undefined) {
            $scope.model.value = "";
        }

        uContentDropdownResource.getAll($scope.model.config.xpath).then(function (response) {
            $scope.result = response.data;
        });

    });