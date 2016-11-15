var app=angular.module('dependentView',[])
.controller('movilCtrl', ['$scope', '$http',function ($scope, $http) {
     var orders;$http.get('http://isaac:7549/api/order/get/S_ID/undefined')
            .then( function (response) {    
              $scope.branchStores = response.data;           
        });
}]);

app = angular.module('dependentView')
.controller('storeCtrl', ['$scope', '$http', function ($scope, $http) {
     var orders;
     $http.get('http://isaac:7549/api/order/get/ordenes/undefined')
            .then( function (response) {    
              $scope.orders = response.data;
              console.log(response.data);
        });
}]);

app = angular.module('dependentView')
.controller('employeeCtrl', ['$scope', '$http', function ($scope, $http) {
    var branchStores;
    var employees;
    $http.get('http://isaac:7549/api/sucursal/get/S_ID/undefined')
            .then( function (response) {    
              $scope.branchStores = response.data;           
        });
      
    $scope.createEmployee = function () {
        var Employee = {
            "E_ID": $scope.E_ID,
            "CName": $scope.E_Name,
            "LName": $scope.E_LName,
            "CAddress": $scope.E_Address,
            "Charge": $scope.E_Charge,
            "CPassword": $scope.E_Pass,
            "S_ID": $scope.E_BranchStore
        }
        console.log(Employee);
        $http.post('http://isaac:7549/api/employees/post',Employee).
        success(function (data, status, headers, config) {
            alert('the new employee has been posted!');
        }).
        error(function (data, status, headers, config) {
            alert('Error while posting the new employee')
        });
      
    }
    
    
}]);

app = angular.module('dependentView')
.controller('productCtrl', ['$scope', '$http', function ($scope, $http) {
    var categories;
    var employees;
    $http.get('http://isaac:7549/api/category/get/CA_ID/undefined')
            .then( function (response) {    
              $scope.categories = response.data;           
        });
      
    $scope.createProduct = function () {
        var product = {
            "P_ID": $scope.P_ID,
            "PName": $scope.P_Name,
            "PDescrition": $scope.P_Description,
            "Extent": $scope.P_Excent,
            "Price": $scope.P_Price,
            "Quantity":$scope.P_Amount,
            "CA_ID": $scope.CA_ID,
            "PDR_ID":$scope.PR_ID
            
        }
        console.log(product);
        $http.post('http://isaac:7549/api/product/post',product).
        success(function (data, status, headers, config) {
            alert('the new product has been posted!');
        }).
        error(function (data, status, headers, config) {
            alert('Error while posting the new product')
        });
      
    }
}]);

app = angular.module('dependentView')
.controller('providerCtrl', ['$scope', '$http', function ($scope, $http) {
    var branchStores;
    
    $http.get('http://isaac:7549/api/sucursal/get/S_ID/undefined')
            .then( function (response) {    
              $scope.branchStores = response.data;           
        });
      
    $scope.createProvider = function () {
       // var branch= document.getElementById("branchStore");
        var Provider = {
            "P_ID": $scope.PR_ID,
            "PName": $scope.PR_Name,
            "LName": $scope.PR_LName,
            "PAddress": $scope.PR_Address,
            "Phone": $scope.PR_Phone,
            "Day": $scope.PR_Day,
            "Month": $scope.PR_Month,
            "Year": $scope.PR_Year,
            "S_ID": $scope.PR_BranchStore
        }
        console.log(Provider);
        $http.post('http://isaac:7549/api/provider/post',Provider).
        success(function (data, status, headers, config) {
            alert('Provider has been posted');
        }).
        error(function (data, status, headers, config) {
            alert('Error posting the new provider!')
        });
    }
    
}]);

app = angular.module('dependentView')
.controller('categoryCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.createCategory = function () {
        var category = {
            "CA_ID": $scope.CA_ID,
            "CDescription": $scope.CA_Description
            
        }
        console.log(category);
        $http.post('http://isaac:7549/api/category/post',category).
        success(function (data, status, headers, config) {
            alert('the new category has been posted!');
        }).
        error(function (data, status, headers, config) {
            alert('Error while posting the new category')
        });
      
    }
}]);

app = angular.module('dependentView')
.controller('storesCtrl', ['$scope', '$http', function ($scope, $http) {
    var branchs;
   
    $scope.createBranchStore = function () {

        var Branch = {
            "S_ID": $scope.S_ID,
            "SName": $scope.S_Name,
            "SAddress": $scope.S_Address,
          
            
        }
        console.log(Branch); $http.post('http://isaac:7549/api/sucursal/post',Branch).
        success(function (data, status, headers, config) {
            alert('Branch has been posted');
        }).
        error(function (data, status, headers, config) {
            alert('error posting branch')
        });
    }
}]);

app = angular.module('dependentView')
.controller('clientCtrl', ['$scope', '$http', function ($scope, $http) {
    var clients;
    var idClient;
    var date;
    var year ;
    var day;
    var month;

    $scope.createClient = function () {
        console.log("here");
        date = new Date($scope.C_BirthDate);
        day = date.getDate();
        year = date.getFullYear();
        
        month = date.getMonth() + 1;
        var Client = {
            "C_ID": $scope.C_ID,
            "FName": $scope.C_Name,
            "LName": $scope.C_LName,
            "CAddress": $scope.C_Address,
            "Phone": $scope.C_Phone,
            "Day":day,
            "Month":month,
            "Year":year,
            "Penalization":$scope.C_Penalization,
            "CPassword":$scope.C_Pass
        }
        console.log(Client); 
        $http.post('http://isaac:7549/api/client/post',Client).
        success(function (data, status, headers, config) {
            alert('Client has been posted');
        }).
        error(function (data, status, headers, config) {
            alert('error posting client')
        });
    }
}]);

app = angular.module('dependentView')
.controller('editEmployeesCtrl', ['$scope', '$http', function ($scope, $http) { 
    
    var employees;
    $scope.getEmployees = function(){
        $http.get('http://isaac:7549/api/employees/get/W_ID/undefined')
                .then( function (response) {    
                  $scope.employees = response.data;           
            });
    }
    $scope.deleteEmployee = function(item){
         console.log("eliminando");
         $http.get('http://isaac:7549/api/employees/delete/W_ID/item')
                .then( function (response) {    
                  $scope.employees = response.data;           
            });
    }
  
    
    
}]);

app = angular.module('dependentView')
.controller('editEmployeeCtrl', ['$scope', '$http', function ($scope, $http) { 
      $scope.updateEmployee = function () {
        var Employee = {
            "E_ID": $scope.idEmployee,
            "CName": $scope.nameEmployee,
            "LName": $scope.lnameEmployee,
            "CAddress": $scope.addressEmployee,
            "Password": $scope.passEmployee

        }
        console.log(Employee);
        $http.post('http://isaac:7549/api/employees/post',Employee).
        success(function (data, status, headers, config) {
            alert('the new employee has been posted!');
        }).
        error(function (data, status, headers, config) {
            alert('Error while posting the new employee')
        });
      
    }

}]);