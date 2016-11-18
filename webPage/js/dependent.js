var link='http://bryan:7580';
//Vra Globals
var url= 'http://bryan:7580';
var userI = localStorage.userID;
var userN = localStorage.userName;
var SucursalID;
var ClientID;

var app=angular.module('dependentView',[])
.controller('movilCtrl', ['$scope', '$http',function ($scope, $http) {
     //Show the user name
        document.getElementById("idUser").innerHTML = "Welcome "+userN;
     
     var orders;
    /*$http.get(link + '/api/order/get/S_ID/undefined')
            .then( function (response) {    
              $scope.branchStores = response.data;           
        });*/
}]);

app = angular.module('dependentView')
.controller('storeCtrl', ['$scope', '$http', function ($scope, $http) {
     var orders;
    /* $http.get(link + '/api/order/get/ordenes/undefined')
            .then( function (response) {    
              $scope.orders = response.data;
              console.log(response.data);
        });*/
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
            "W_Status": $scope.E_Pass,
            "R_ID":$scope.E_Charge
        }
        console.log(Employee);
        $http.post(link + '/api/employees/post',Employee).
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
    var providers;
    
    $http.get(link + '/api/category/get/CA_ID/undefined')
            .then( function (response) {    
              $scope.categories = response.data;           
        });
     $http.get(link + '/api/provider/get/P_ID/undefined')
            .then( function (response) {    
              $scope.providers = response.data;           
        });
    $http.get(link + '/api/sucursal/get/S_ID/undefined')
            .then( function (response) {    
              $scope.sucursals = response.data;           
        });
      
    $scope.createProduct = function () {
          var n1 = $scope.PR_ID.indexOf("-");
            var res1 = $scope.PR_ID.substring(0, n1);

            var n2 = $scope.S_ID.indexOf("-");
            var res2 = $scope.S_ID.substring(0, n2);
        var product = {
            "PR _ID": $scope.P_ID,
            "PR_Name": $scope.P_Name,
            "PR_Description": $scope.P_Description,
            "PR_Exempt": $scope.P_Excent,
            "PR_Price": $scope.P_Price,
            "PR_Quantity":$scope.P_Amount,
            "PR_Status":$scope.P_Status,
            "CA_ID": $scope.CA_ID,
            "P_ID":parseInt(res1),
            "S_ID":parseInt(res2)
            
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
            "CA_Description": $scope.CA_Description,
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
      
        
        
        var Client = {
            "C_ID": $scope.C_ID,
            "C_Name": $scope.C_Name,
            "C_LName": $scope.C_LName,
            "C_Address": $scope.C_Address,
            "C_Phone": $scope.C_Phone,
            "C_Date": $scope.C_BirthDate,     
            "C_Penalization":$scope.C_Penalization,
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
var worker;
var values;
app = angular.module('dependentView')
.controller('editEmployeesCtrl', ['$scope', '$http', function ($scope, $http) { 
    var information={};  
    var workers;
    $scope.getEmployees = function(){
        console.log("geteando");
        $http.get(link+'/api/employees/get/W_ID/undefined')
                .then( function (response) {    
                  $scope.workers = response.data;           
            });
    }
    $scope.editEmployee = function(item){
        
       
      $http.get(link+'/api/employees/get/W_ID/'+item)
                .then( function (response) {    
                  information = response.data[0];
                  console.log(information);
                    console.log(information.W_ID);
                  console.log(information.W_Name);
               /*   document.getElementById("E_IDU").innerHTML = information.W_ID;
                  document.getElementById("E_NameU").innerHTML = information.W_Name;
                  document.getElementById("E_LNameU").innerHTML = information.W_LName;
                  document.getElementById("E_AddressU").innerHTML = information.W_Address;
                  document.getElementById("E_PassU").innerHTML = information.W_Password;*/
                  $('#E_IDU').val(information.W_ID);
                  $('#E_NameU').val(information.W_Name);
                  $('#E_LNameU').val(information.W_LName);
                  $('#E_AddressU').val(information.W_Address);
                  $('#E_PassU').val(information.W_Password);
          
          
        });
        values=$scope.information;
        
      
             
    }
    $scope.deleteEmployee = function(item){
         console.log("eliminando "+item);
         $http.get(link+'/api/employees/delete/W_ID/'+item+",0")
                .then( function (response) {    
                  $scope.employees = response.data;           
            });
    }
    $scope.updateEmployee = function () {
        var Employee = {
            "W_ID": $scope.E_IDU,
            "W_Name": $scope.E_NameU,
            "W_LName": $scope.E_LNameU,
            "W_Address": $scope.E_AddressU,
            "W_Password": $scope.E_PassU,
            "W_Status": $scope.E_PassU,
        }
        console.log(Employee);
        $http.put(link +'/api/employees/update',Employee).
        success(function (data, status, headers, config) {
            alert('the new employee has been posted!');
        }).
        error(function (data, status, headers, config) {
            alert('Error while posting the new employee')
        });
      
    }
  
    
    
}]);

app = angular.module('dependentView')
.controller('editProvidersCtrl', ['$scope', '$http', function ($scope, $http) { 
    var information={};  
    var providers;
    var branchStores;
      $http.get(link+'/api/sucursal/get/S_ID/undefined')
            .then( function (response) {    
              $scope.branchStores = response.data;           
        });
    $scope.getProviders = function(){
        console.log("geteando");
        $http.get(link+'/api/provider/get/P_ID/undefined')
                .then( function (response) {    
                  $scope.providers = response.data;           
            });
    }
    $scope.editProvider = function(item){ 
      $http.get(link+'/api/provider/get/P_ID/'+item)
                .then( function (response) {    
                  information = response.data[0];
                  console.log(information);
                  $('#P_IDE').val(information.P_ID);
                  $('#P_NameE').val(information.P_Name);
                  $('#P_LNameE').val(information.P_LName);
                  $('#P_AddressE').val(information.P_Address);
                  $('#P_DateE').val(information.P_DateE);
                  $('#P_StatusE').val(information.P_Status);
                             
          
        });
        values=$scope.information;
        
      
             
    }
    
    $scope.deleteProvider = function(item){
         console.log("eliminando "+item);
         $http.get(link+'/api/provider/delete/P_ID/'+item+",0")
                .then( function (response) {    
                  $scope.employees = response.data;           
            });
    }
    
    $scope.updateProvider= function () {
        var Provider = {
            "P_ID": $scope.P_IDE,
            "P_Name": $scope.P_NameE,
            "P_LName": $scope.P_LNameE,
            "P_Address": $scope.P_AddressE,
            "P_Date": $scope.P_DateE,
            "P_Status": $scope.P_StatusE,
        }
        console.log(Provider);
        $http.put(link +'/api/provider/update',Provider).
        success(function (data, status, headers, config) {
            alert('the new employee has been posted!');
        }).
        error(function (data, status, headers, config) {
            alert('Error while posting the new employee')
        });
      
    }
  
    
    
}]);
app = angular.module('dependentView')
.controller('editProductsCtrl', ['$scope', '$http', function ($scope, $http) { 
    var information={};  
    var products;
    var categories;
    var sucursals;
    var providers;
    
    $http.get(link + '/api/category/get/CA_ID/undefined')
            .then( function (response) {    
              $scope.categories = response.data;           
        });
     $http.get(link + '/api/provider/get/P_ID/undefined')
            .then( function (response) {    
              $scope.providers = response.data;           
        });
    $http.get(link + '/api/sucursal/get/S_ID/undefined')
            .then( function (response) {    
              $scope.sucursals = response.data;           
        });
      
    $scope.getProducts = function(){
        console.log("geteando");
        $http.get(link+'/api/product/get/PR_ID/undefined')
                .then( function (response) {    
                  $scope.products = response.data;           
            });
    }
    $scope.editProduct = function(item){
        
       
      $http.get(link+'/api/product/get/PR_ID/'+item)
                .then( function (response) {    
                  information = response.data[0];
               /*   document.getElementById("E_IDU").innerHTML = information.W_ID;
                  document.getElementById("E_NameU").innerHTML = information.W_Name;
                  document.getElementById("E_LNameU").innerHTML = information.W_LName;
                  document.getElementById("E_AddressU").innerHTML = information.W_Address;
                  document.getElementById("E_PassU").innerHTML = information.W_Password;*/
                  console.log(information);
                  $('#PR_IDE').val(information.PR_ID);
                  $('#PR_NameE').val(information.PR_Name);
                  $('#PR_DescriptionE').val(information.PR_Description);
                  $('#PR_StatusE').val(information.PR_Status);
                  $('#PR_ExemptE').val(information.PR_Exempt);
                  $('#PR_PriceE').val(information.PR_Price);
                  $('#PR_AmountE').val(information.PR_Quantity);
            
             
          
          
        });
        values=$scope.information;
      
    }
    $scope.deleteProduct = function(item){
         console.log("eliminando "+item);
         $http.get(link+'/api/product/delete/PR_ID/'+item+",0")
                .then( function (response) {    
                  $scope.employees = response.data;           
            });
    }
    $scope.updateProduct = function () {
 
        var Product = {
            "PR_ID": $scope.PR_IDE,
            "PR_Name": $scope.PR_NameE,
            "PR_Description": $scope.PR_DescriptionE,
            "PR_Status": $scope.PR_StatusE,
            "PR_Exempt": $scope.PR_ExemptE,
            "PR_Price": $scope.PR_PriceE,
            "PR_Quantity": $scope.PR_AmountE,
            "CA_ID": $scope.CA_ID,
            "S_ID": parseInt($scope.S_ID),
            "P_ID": parseInt($scope.P_ID)
            
        }
        console.log(Product);
        $http.put(link +'/api/product/update',Product).
        success(function (data, status, headers, config) {
            alert('the new employee has been posted!');
        }).
        error(function (data, status, headers, config) {
            alert('Error while posting the new employee')
        });
      
    }
  
    
    
}]);

app = angular.module('dependentView')
.controller('editCategoriesCtrl', ['$scope', '$http', function ($scope, $http) { 
    var information={};  
    var categories;
    $scope.getCategories = function(){
        console.log("geteando");
        $http.get(link+'/api/category/get/CA_ID/undefined')
                .then( function (response) {    
                  $scope.categories = response.data;           
            });
    }
    $scope.editCategory = function(item){
      $http.get(link+'/api/category/get/CA_ID/'+item)
                .then( function (response) {    
                  information = response.data[0];
                  console.log(information);
               /*   document.getElementById("E_IDU").innerHTML = information.W_ID;
                  document.getElementById("E_NameU").innerHTML = information.W_Name;
                  document.getElementById("E_LNameU").innerHTML = information.W_LName;
                  document.getElementById("E_AddressU").innerHTML = information.W_Address;
                  document.getElementById("E_PassU").innerHTML = information.W_Password;*/
                  $('#CA_IDE').val(information.CA_ID);
                  $('#CA_DescriptionE').val(information.CA_Description);
                  $('#CA_StatusE').val(information.CA_Status);
                
          
          
        });
        values=$scope.information;
        
      
             
    }
    $scope.deleteCategory = function(item){
         console.log("eliminando "+item);
         $http.get(link+'/api/category/delete/CA_ID/'+item+",0")
                .then( function (response) {    
                          
            });
    }
    $scope.updateCategory= function () {
        var Category = {
            "CA_ID": $scope.CA_IDE,
            "CA_Description": $scope.CA_DescriptionE,
            "CA_Status": $scope.CA_StatusE}
      
        console.log(Category);
        $http.put(link +'/api/category/update',Category).
        success(function (data, status, headers, config) {
            alert('the new employee has been posted!');
        }).
        error(function (data, status, headers, config) {
            alert('Error while posting the new employee')
        });
      
    }
  
    
    
}]);

app = angular.module('dependentView')
.controller('editStoresCtrl', ['$scope', '$http', function ($scope, $http) { 
    var information={};  
    var stores;
    $scope.getSucursals = function(){
        console.log("geteando");
        $http.get(link+'/api/sucursal/get/S_ID/undefined')
                .then( function (response) {    
                  $scope.stores = response.data;           
            });
    }
    $scope.editSucursal = function(item){
        
       
      $http.get(link+'/api/sucursal/get/S_ID/'+item)
                .then( function (response) {    
                  information = response.data[0];
                
               /*   document.getElementById("E_IDU").innerHTML = information.W_ID;
                  document.getElementById("E_NameU").innerHTML = information.W_Name;
                  document.getElementById("E_LNameU").innerHTML = information.W_LName;
                  document.getElementById("E_AddressU").innerHTML = information.W_Address;
                  document.getElementById("E_PassU").innerHTML = information.W_Password;*/
                  $('#S_IDE').val(information.S_ID);
                  $('#S_NameE').val(information.S_Name);
                  $('#S_AddressE').val(information.S_Address);
                
          
          
        });
        values=$scope.information;
        
      
             
    }
    $scope.deleteSucursal = function(item){
         console.log("eliminando "+item);
         $http.get(link+'/api/sucursal/delete/S_ID/'+item+",0")
                .then( function (response) {    
                          
            });
    }
    $scope.updateSucursal= function () {
        var Sucursal = {
            "S_ID": $scope.S_IDE,
            "S_Name": $scope.S_NameE,
            "S_Address": $scope.S_AddressE,
        }
        console.log(Sucursal);
        $http.put(link +'/api/sucursal/update',Sucursal).
        success(function (data, status, headers, config) {
            alert('the new employee has been posted!');
        }).
        error(function (data, status, headers, config) {
            alert('Error while posting the new employee')
        });     
    }  
}]);

app = angular.module('dependentView')
.controller('editClientsCtrl', ['$scope', '$http', function ($scope, $http) { 
    var information={};  
    var providers;
    var clients;

    $scope.getClients = function(){
        console.log("geteando");
        $http.get(link+'/api/client/get/C_ID/undefined')
                .then( function (response) {    
                  $scope.clients = response.data;           
            });
    }
    $scope.editClient = function(item){
      $http.get(link+'/api/client/get/C_ID/'+item)
                .then( function (response) {    
                  information = response.data[0];
                  console.log(information);
                  $('#C_IDE').val(information.C_ID);
                  $('#C_NameE').val(information.C_Name);
                  $('#C_LNameE').val(information.C_LName);
                  $('#C_AddressE').val(information.C_Address);
                  $('#C_BirthDateE').val(information.C_Date);
                  $('#C_PhoneE').val(information.C_Phone);
                  $('#C_StatusE').val(information.C_Status); 
                  $('#C_PenalizationE').val(information.C_Penalization); 
                  $('#C_StatusE').val(information.C_Status); 
        });
        values=$scope.information;
        
      
             
    }
    $scope.deleteClient = function(item){
         console.log("eliminando "+item);
         $http.get(link+'/api/client/delete/C_ID/'+item+",0")
                .then( function (response) {    
                  $scope.employees = response.data;           
            });
    }
    $scope.updateClient= function () {
        var Client = {
            "C_ID": $scope.C_IDE,
            "C_Name": $scope.C_NameE,
            "C_LName": $scope.C_LNameE,
            "C_Address": $scope.C_AddressE,
            "C_Phone": $scope.C_PhoneE,
            "C_Date": $scope.C_BirthDateE,     
            "C_Penalization":$scope.C_PenalizationE,
            "C_StatusE":$scope.C_StatusE
        }
        console.log(Client);
        $http.put(link +'/api/client/update',Client).
        success(function (data, status, headers, config) {
            alert('the new Client has been posted!');
        }).
        error(function (data, status, headers, config) {
            alert('Error while posting the new employee')
        });
      
    }
  
    
    
}]);


app = angular.module('dependentView')
.controller('clientCtrl', ['$scope', '$http', function ($scope, $http) {
        
        //Show the user name
        document.getElementById("idUser").innerHTML = "Welcome "+userN;
     
        // Get the modal
        var modalClient = document.getElementById('clientModal');

        
    
        // Get the button that opens the modal
        var order = document.getElementById("newOrder");
        
    
        // Get the <span> element that closes the modal
        var span1 = document.getElementById("close1");
       
        
    
        // When the user clicks the button, open the modal
        order.onclick = function() {
            modalClient.style.display = "block";
        }

        span1.onclick = function() {
            modalClient.style.display = "none";
        }

        window.onclick = function(event) {
            if (event.target == modalClient) {
                modalClient.style.display = "none";
            }
        }


      var branchStores;
      var clientID = userI;
      var productList;
      var order;
      var  listProduct=[]; 
      var products=""; 
      var amounts=""; 
      var date=new Date();
      var factura;
      var productList; 
      $http.get(url+'/api/sucursal/get/S_ID/undefined')
            .then( function (response) {    
              $scope.branchStores = response.data;           
        });
    $http.get(url+'/api/Client/get/C_ID/undefined')
            .then( function (response) {    
              $scope.Clients = response.data;           
        });
      
      $scope.addToCart = function (value1,value2){
          
          if(products=="" && amounts=="" ){
              products=value1;
              amounts=value2;
              
          }
          else{
              products = products+ "," + value1 ;
              amounts = amounts+","  + value2  ;
          }
          console.log(url+'/api/order/'+products+'/'+amounts);
           
      } 
      $scope.addProduct = function () {
          console.log(url+'/api/product/get/PR_Name/'+$scope.productName);
         $http.get(url+'/api/product/get/PR_Name/'+$scope.productName)
            .then( function (response) {
            $scope.productList = response.data;
          
        });
          
       }
        
        $scope.sendOrder = function () {
        var today = new Date();// Take the system date
            var randomOrder = parseInt((Math.floor((Math.random() * 1000000) + 1)* today.getHours())/today.getMinutes()+today.getMilliseconds());
            console.log(randomOrder);
            
       
        var dd = today.getDate();
        var mm = today.getMonth()+1; //January is 0!
        var yyyy = today.getFullYear();
        // if the day have one digit return like 9 or 8 ect, so add it 0 to return 09 and 08
        if(dd<10) {
            dd='0'+dd
        } 
        // if the same soution about the day
        if(mm<10) {
            mm='0'+mm
        } 
        // Concatenate the year-month-day
        today = yyyy+'-'+mm+'-'+dd;
            console.log(" date " + date)
            
          order={
              "O_ID": randomOrder,
              "S_ID": $scope.branchStore,
              "C_ID": $scope.clientid,
              "W_ID": userI,
              "O_PPhone": $scope.phone,
              "O_Status":$scope.status,
              "PR_Name": products,
              "PR_Amount": amounts,
              "O_Priority": 0,
              "O_Date": today
              
          }
          console.log(order);
          $http.post(url+'/api/order/post',order).
          success(function (data, status, headers, config) {
            alert('the new order has been posted!');
           }).
          error(function (data, status, headers, config) {
             alert('Error while posting the new order')
        });
        }
    
}]);

var worker;
var values;
app = angular.module('dependentView')
.controller('editOrdersCtrl', ['$scope', '$http', function ($scope, $http) { 
    
    

     
        
    var information={};  
    
 
    $http.get(url+'/api/order/get/O_ID/undefined')
                .then( function (response) {  
               console.log(response.data[0]);
                  $scope.orders = response.data;           
            });
    
    $scope.editOrder = function(item){
       
      $http.get(url+'/api/order/get/O_ID/'+item)
                .then( function (response) {    
                  information = response.data[0];
                  console.log(information);
                    console.log(information.O_ID);
                localStorage.setItem("clientId", information.C_ID); 
                localStorage.setItem("sucursalId", information.S_ID);
                 SucursalID=localStorage.clientId;
                 ClientID=localStorage.sucursalId;
                 console.log("Sucu " + SucursalID + " Clien " + ClientID + " Phone " + information.O_PPhone);

                  $('#O_IDU').val(information.O_ID);
                  $('#O_PriorityU').val(information.O_Priority);
                  $('#O_StatusU').val(information.O_Status);
        
               
                  
                  $('#O_PhoneU').val(information.O_PPhone);
          
          
        });
        values=$scope.information;
        
      
             
    }
    $scope.deleteOrder = function(item){
         console.log("eliminando "+item);
         $http.get(url+'/api/order/delete/O_ID/'+item+",0")
                .then( function (response) {    
                  $scope.orders = response.data;           
            });
    }
    $scope.updateOrder = function () {
        var Order = {
            "O_ID": parseInt($scope.O_IDU),
            "S_ID": parseInt(SucursalID),
            "C_ID": parseInt(ClientID),
            "W_ID": parseInt(userI),
            "O_Priority": parseInt($scope.O_PriorityU),
            "O_Status": $scope.O_StatusU,
            "O_Date": $scope.O_DateU,
            "O_PPhone": parseInt($scope.O_PhoneU)
        }
        console.log(Order);
        $http.put(url +'/api/order/update',Order).
        success(function (data, status, headers, config) {
            alert('the new order has been posted!');
        }).
        error(function (data, status, headers, config) {
            alert('Error while posting the new order')
        });
      
    }
  
    
    
}]);

