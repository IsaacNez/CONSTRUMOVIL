//Vra Globals
var url= 'http://bryan:7580';
var userI = localStorage.userID;
var userN = localStorage.userName;

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
            var d = new Date();
            var randomOrder = parseInt((Math.floor((Math.random() * 1000000) + 1)* d.getHours())/d.getMinutes()+d.getMilliseconds());
            console.log(randomOrder);
            
          order={
              "O_ID": randomOrder,
              "S_ID": $scope.branchStore,
              "C_ID": $scope.clientid,
              "W_ID": userI,
              "O_Status":$scope.status,
              "PR_Name": products,
              "PR_Amount": amounts,
              "O_Priority": 0,
              "O_Date": $scope.date
              
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