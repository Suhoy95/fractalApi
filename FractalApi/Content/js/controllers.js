'use strict';

/* Controllers */

var FractalControllers = angular.module('FractalControllers', []);

FractalControllers.controller("dataController", ["$scope", "connection", "gridMaster", "shower", "linker",
                                                 function($scope, connection, gridMaster, shower, linker){

    connection.scope = $scope;
    $scope.items = [];
    $scope.connection = connection;
    $scope.linker = linker;
    $scope.shower = shower;

    $scope.getBindingItems = function()
    {
        var items = $scope.shower.filterItems($scope.items);
        var result = gridMaster.createMinGrid(items, $scope.setting);
        return result;
    }

    $scope.setBinding = function(item, relation)
    {
        $scope.shower.setBinding(item, relation);
        $scope.filterItems = $scope.getBindingItems();
    }

    $scope.unsetBinding = function(relation)
    {
        $scope.shower.unsetBinding(relation);
        $scope.filterItems = $scope.getBindingItems();
    }
    
    $scope.completeGrid = function()
    {
        $scope.items = gridMaster.completeGrid($scope.items, $scope.setting);
    }

    $scope.connection.loadGrid();
}]);

FractalControllers.controller("settingController", ["$scope", "gridMaster" , 
                                                    function($scope, gridMaster){
   
    
}]);

FractalControllers.controller("gridController", ["$scope", "gridMaster", "$window", "$timeout", 
                                                 function($scope, gridMaster, $window, $timeout){

    $scope.completeGrid = function(){
        var x = $window.scrollX,
            y = $window.scrollY;
        $scope.items = gridMaster.completeGrid($scope.items, $scope.setting);
    
        $timeout(function() {
            $window.scrollTo(x, y);
        }, 0);
    };

    $scope.sortableOptions = {
        tolerance: "pointer",
        handle: "div.controll .sort-item",
        connectWith: ".column",
        stop: $scope.completeGrid
    };
}]);

FractalControllers.controller("itemController", ["$scope", "$window", function($scope, $window){
    $scope.createItem = function(item)
    {
        item.create();
        $scope.completeGrid();
    }

    $scope.deleteItem = function(item)
    {
        item.delete();
        $scope.completeGrid();
    }

    $scope.deleteNote = function(item)
    {
        if(!(item.title === "" && item.text === "") &&
            !$window.confirm("Вы уверены, что хотите заметку?"))
            return;

        if(item == $scope.linker.currentItem) 
            $scope.linker.disable();

        item.delete();
        $scope.completeGrid();
    }

    $scope.saveNote = function(item)
    {
        if(item == $scope.linker.currentItem) 
            $scope.linker.disable();
        item.save();
    }
}]);