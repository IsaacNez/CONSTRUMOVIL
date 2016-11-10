//Vra Globals
var url= 'http://desktop-6upj287:7575';

var userN = localStorage.userName;

/**
 * Modal where the user can sign in
*/
var stageForm = angular.module('managerView',[])
.controller('managerCtrl', ['$scope', '$http', function ($scope, $http) {
        
        //Show the user name
        document.getElementById("idUser").innerHTML = "Welcome "+userN;
     
        // Get the modal
        var modalManager = document.getElementById('managerModal');

        
    
        // Get the button that opens the modal
        var statistics = document.getElementById("newStatistics");
        
    
        // Get the <span> element that closes the modal
        var span1 = document.getElementById("close1");
       
        
    
        // When the user clicks the button, open the modal
        statistics.onclick = function() {
            modalManager.style.display = "block";
        }

        span1.onclick = function() {
            modalManager.style.display = "none";
        }

        window.onclick = function(event) {
            if (event.target == modalManager) {
                modalManager.style.display = "none";
            }
        }


    var Sales;
    $scope.case1 = function (){
         $http.get(url+'/api/Statistic/GetMostSold/case1/1').
        success(function (data, status, headers, config) {
             $scope.Sales = data;
        }).
        error(function (data, status, headers, config) {
            alert('Error retrieving sales information!')
        });   
         
    }
    $scope.getSales = function () {
        var attributes="";
        var values = "";
            
            if(Boolean($scope.S_ID)){
                attributes+="S_ID";
                console.log(attributes);
                values+=$scope.S_ID;
                console.log(attributes,values);
            }
            if(Boolean($scope.platform)){
                if(Boolean(attributes)){
                    attributes+=",";
                    values+=",";}
                attributes+="OPlatform";
                values+=$scope.platform;
            }
        if(attributes==""){
        
         $http.get(url+'/api/Statistic/GetMostSold/case0/1').
        success(function (data, status, headers, config) {
             $scope.Sales = data;
        }).
        error(function (data, status, headers, config) {
            alert('Error retrieving sales information!')
        });   
         
    }
            
        else{ 
        console.log(attributes, values);
        $http.get(url+'/api/Statistic/GetMostSold/'+attributes+"/"+values).
        success(function (data, status, headers, config) {
             $scope.Sales = data;
        }).
        error(function (data, status, headers, config) {
            alert('Error retrieving sales information!')
        }); }}     
    
    
    $scope.case2 = function (){
         $http.get(url+'/api/Statistic/GetMostSold/case0/1').
        success(function (data, status, headers, config) {
             $scope.Sales = data;
        }).
        error(function (data, status, headers, config) {
            alert('Error retrieving sales information!')
        });   
         
    }
    
}]);