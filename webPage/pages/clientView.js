//Vra Globals
var url= 'http://desktop-6upj287:7575';
var userID = localStorage.user;
var userCode = localStorage.code;
/**
 * Modal where the user can sign in
*/
var stageForm = angular.module('clientView',[])
.controller('clientCtrl', ['$scope', '$http', function ($scope, $http) {
        
        //Show the user name
        document.getElementById("idUser").innerHTML = "Welcome "+userID;
     
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
      var clientID = localStorage.user;
      var productList;
      var Order;
      var  listProduct=[]; 
      var products=""; 
      var amounts=""; 
      var date=new Date();
      var factura;

     console.log(clientID); $http.get(url+'/api/sucursal/get/S_ID/undefined')
            .then( function (response) {    
              $scope.branchStores = response.data;           
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
          console.log(url+'/api/preorder/'+products+'/'+amounts);
           
      } 
      $scope.addProduct = function () {
          
         $http.get(url+'/api/product/get/PName/'+$scope.productName)
            .then( function (response) {
            $scope.productList = response.data;
          
        });
       
        console.log(Order);

        $scope.sendOrder = function () {
          order={
              "S_ID": $scope.branchStore,
              "C_ID": $scope.clientID,
              "Phone":$scope.phone,
              "Status":$scope.status
              
              
          }}

    
    
    
    }
    
}]);