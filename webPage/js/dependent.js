var link='http://bryan:7580';
var app=angular.module('dependentView',[])
.controller('movilCtrl', ['$scope', '$http',function ($scope, $http) {
     var orders;$http.get(link + '/api/order/get/S_ID/undefined')
            .then( function (response) {    
              $scope.branchStores = response.data;           
        });
}]);

app = angular.module('dependentView')
.controller('storeCtrl', ['$scope', '$http', function ($scope, $http) {
     var orders;
     $http.get(link + '/api/order/get/ordenes/undefined')
            .then( function (response) {    
              $scope.orders = response.data;
              console.log(response.data);
        });
}]);

app = angular.module('dependentView')
.controller('employeeCtrl', ['$scope', '$http', function ($scope, $http) {
    var branchStores;
    var employees;
    var Charges;
    $http.get(link + '/api/sucursal/get/S_ID/undefined')
            .then( function (response) {    
              $scope.branchStores = response.data;           
        });
     $http.get(link + '/api/role/get/R_ID/undefined')
            .then( function (response) {    
              $scope.Charges = response.data;           
        });
      
    $scope.createEmployee = function () {
        var Employee = {
            "W_ID": $scope.E_ID,
            "W_Name": $scope.E_Name,
            "W_LName": $scope.E_LName,
            "W_Address": $scope.E_Address,
            "W_Password": $scope.E_Pass,
            "R_ID":$scope.E_Charge
            
        }
        console.log(Employee);
        $http.post(link + '/api/worker/post',Employee).
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
    var sucursals;
    var employees;
    $http.get(link + '/api/category/get/CA_ID/undefined')
            .then( function (response) {    
              $scope.categories = response.data;           
        });
    $http.get(link + '/api/sucursal/get/S_ID/undefined')
            .then( function (response) {    
              $scope.sucursals = response.data;           
        });
      
    $scope.createProduct = function () {
        var product = {
            "PR _ID": $scope.P_ID,
            "PR_Name": $scope.P_Name,
            "PR_Descrition": $scope.P_Description,
            "PR_Exempt": $scope.P_Excent,
            "PR_Price": $scope.P_Price,
            "PR_Quantity":$scope.P_Amount,
            "PR_Status":$scope.P_Status,
            "CA_ID": $scope.CA_ID,
            "P_ID":$scope.PR_ID,
            "S_ID":$scope.S_ID
            
        }
        console.log(product);
        $http.post(link+'/api/product/post',product).
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
    
    $http.get(link+'/api/sucursal/get/S_ID/undefined')
            .then( function (response) {    
              $scope.branchStores = response.data;           
        });
      
    $scope.createProvider = function () {
       // var branch= document.getElementById("branchStore");
        var Provider = {
            "P_ID": $scope.PR_ID,
            "P_Name": $scope.PR_Name,
            "P_LName": $scope.PR_LName,
            "P_Address": $scope.PR_Address,
            "P_Date": $scope.PR_Date,
            "P_Status": $scope.PR_Status
        }
        console.log(Provider);
        $http.post(link + '/api/provider/post',Provider).
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
            "CDescription": $scope.CA_Description,
            "CA_Status":$scope.CA_Status
            
        }
        console.log(category);
        $http.post(link + '/api/category/post',category).
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
            "S_Name": $scope.S_Name,
            "S_Address": $scope.S_Address,
          
            
        }
        console.log(Branch); $http.post(link + '/api/sucursal/post',Branch).
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
      
        
        month = date.getMonth() + 1;
        var Client = {
            "C_ID": $scope.C_ID,
            "C_Name": $scope.C_Name,
            "C_LName": $scope.C_LName,
            "C_Address": $scope.C_Address,
            "C_Phone": $scope.C_Phone,
            "C_Date": $scope.C_BirthDate,      "C_Penalization":$scope.C_Penalization,
            "C_Password":$scope.C_Pass
        }
        console.log(Client); 
        $http.post(link + '/api/client/post',Client).
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
        $http.get(link+'/api/employees/get/W_ID/undefined')
                .then( function (response) {    
                  $scope.employees = response.data;           
            });
    }
    $scope.deleteEmployee = function(item){
         console.log("eliminando");
         $http.get(link+'/api/employees/delete/W_ID/item')
                .then( function (response) {    
                  $scope.employees = response.data;           
            });
    }
  
    
    
}]);

app = angular.module('dependentView')
.controller('editEmployeeCtrl', ['$scope', '$http', function ($scope, $http) { 
      $scope.updateEmployee = function () {
        var Employee = {
            "W_ID": $scope.E_ID,
            "W_Name": $scope.E_Name,
            "W_LName": $scope.E_LName,
            "W_Address": $scope.E_Address,
            "W_Password": $scope.E_Pass
        }
        console.log(Employee);
        $http.post(link +'/api/employees/post',Employee).
        success(function (data, status, headers, config) {
            alert('the new employee has been posted!');
        }).
        error(function (data, status, headers, config) {
            alert('Error while posting the new employee')
        });
      
    }

}]);