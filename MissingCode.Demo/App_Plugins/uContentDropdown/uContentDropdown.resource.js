//adds the resource to umbraco.resources module:
angular.module('umbraco.resources').factory('uContentDropdownResource',
    function ($q, $http) {

        //the factory object returned
        return {

            getAll: function (xpath) {
                return $http.get("backoffice/uContentDropdown/uContentDropdown/Content?xpath=" + xpath);
            }

        };
    }
);