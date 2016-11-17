//Vra Globals
var url= 'http://bryan:7580';
var userI = localStorage.userID;
var userN = localStorage.userName;
var SucursalID;
var ClientID;
/**
 * Modal where the user can sign in
*/
var stageForm = angular.module('clientView',[])
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
stageForm = angular.module('clientView')
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
