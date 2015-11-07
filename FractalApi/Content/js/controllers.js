'use strict';

/* Controllers */

var FractalControllers = angular.module('FractalControllers', ["FractalMessager"]);

FractalControllers.controller("dataController", 
                            ["$scope", "connection", "gridMaster", "shower", "linker", "messager",
                            function($scope, connection, gridMaster, shower, linker, messager){

    connection.scope = $scope;
    $scope.items = [];
    $scope.messager = messager;
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

FractalControllers.controller("itemController", ["$scope", "$window", "$timeout", "gridMaster", 
                                                 function($scope, $window, $timeout, gridMaster){
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
        item.action = "deleting";
        if(!(item.title === "" && item.text === "") &&
            !$window.confirm("Вы уверены, что хотите заметку?"))
            return;

        if(item == $scope.linker.currentItem) 
            $scope.linker.disable();

        $scope.connection.deleteNote(item);
        $timeout(deleteInFront, 500);

        function deleteInFront(){
            if(item.state != "success")
                $timeout(deleteInFront, 500);
            else if(item.state == "success"){
                item.delete();
                $scope.completeGrid();
            }
        }
    }

    $scope.saveNote = function(item)
    {
        item.action = "saving";
        if(item == $scope.linker.currentItem) 
            $scope.linker.disable();

        if(item.id < 0){
            $scope.connection.createNote(item);
            $timeout(recoveryId, 500);
        } else
            $scope.connection.updateNote(item);

        item.save();

        function recoveryId(){
            if(item.realId != undefined)
                gridMaster.recoveryId(item, $scope.items);
            if(item.state != "error")
                $timeout(recoveryId, 500);
        }
    }

    $scope.tryComleteNote = function(item)
    {
        if(item.action == "saving")
        {
            item.state = "edit";
            $scope.saveNote(item);
        } else if( item.action == "deleting")
            $scope.deleteNote(item);
    }
}]);