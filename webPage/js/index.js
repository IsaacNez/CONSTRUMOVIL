
//ANGULAR MODULE TO MANAGE INDEX.HTML
var url='http://bryan:7580';
var indexApp = angular.module('index',[])
.controller('indexCtrl', ['$scope', '$http', function ($scope, $http) {
    
    var loginmodal = document.getElementById('myLoginModal');

    // Get the button that opens the modal
    var btnSIn = document.getElementById("myBtnSignIn");
    
     
    
    // Get the <span> element that closes the modal
    var span = document.getElementById("close1");
    
    // When the user clicks the button Sign In, open the modal
    btnSIn.onclick = function() {loginmodal.style.display = "block";}
    
    // When the user clicks on <span> (x), close the modal Sign in
    span.onclick = function() {loginmodal.style.display = "none";}
    
    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function(event) {
        if (event.target == loginmodal) {loginmodal.style.display = "none";}} 
    
    var mensaje = {};
  
    //FUNCTION TO CHECK IF USER EXIST IN DB
    $scope.checkUser = function()
        {
     
        
            $http.get(url+'/api/Employees/get/W_ID,W_Password/'+$scope.U_ID +","+$scope.U_Password)
                    .then(function (response) 
                {
                console.log(response.data[0]);
                mensaje=response.data[0];
               
                console.log(mensaje.W_ID);
                localStorage.setItem("userName", mensaje.W_Name); 
                localStorage.setItem("userID", mensaje.W_ID);
                window.location.assign("/pages/clientView.html");

                });        
            
        }}]);

indexApp = angular.module('index')
.controller('employeeCtrl', ['$scope', '$http', function ($scope, $http) {
    var branchStores;
    var employees;
    var Charges;
    
    var SignUpmodal = document.getElementById('mySignUpModal');

    // Get the button that opens the modal
    var btnSUp = document.getElementById("myBtnSignUp");
    
     
    
    // Get the <span> element that closes the modal
    var span = document.getElementById("close2");
    
    // When the user clicks the button Sign In, open the modal
    btnSUp.onclick = function() {SignUpmodal.style.display = "block";}
    
    // When the user clicks on <span> (x), close the modal Sign in
    span.onclick = function() {SignUpmodal.style.display = "none";}
    
    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function(event) {
        if (event.target == SignUpmodal) {SignUpmodal.style.display = "none";}} 
    
    $http.get(url + '/api/sucursal/get/S_ID/undefined')
            .then( function (response) {    
              $scope.branchStores = response.data;           
        });
     $http.get(url + '/api/role/get/R_ID/undefined')
            .then( function (response) {    
              $scope.Charges = response.data;           
        });
      
 
    $http.get(url + '/api/sucursal/get/S_ID/undefined')
            .then( function (response) {    
              $scope.branchStores = response.data;           
        });
     $http.get(url + '/api/role/get/R_ID/undefined')
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
        $http.post(url + '/api/employees/post',Employee).
        success(function (data, status, headers, config) {
            alert('the new employee has been posted!');
        }).
        error(function (data, status, headers, config) {
            alert('Error while posting the new employee')
        });
      
    }
    
    
}]);


