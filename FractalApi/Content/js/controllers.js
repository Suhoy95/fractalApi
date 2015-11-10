'use strict';

/* Controllers */

var FractalControllers = angular.module('FractalControllers', ["FractalMessager"]);

FractalControllers.controller("dataController", 
                            ["$scope", "$window", "connection", "gridMaster", "shower", "linker", "messager",
                            function($scope, $window, connection, gridMaster, shower, linker, messager){

    connection.scope = $scope;
    $scope.items = [];
    $scope.messager = messager;
    $scope.connection = connection;
    $scope.linker = linker;
    $scope.shower = shower;
    $scope.editing = false;

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

    $scope.goHome = function()
    {
        connection.home();
    }

    $scope.$on('$locationChangeStart', function(event) {
        if(!gridMaster.isAllSave($scope.items) && 
           !$window.confirm("Не все изменения были сохранены.  Вы уверены, что хотите покинуть текущий лист?"))
        {
            event.preventDefault();
            return;
        }
        $scope.shower.clearBinding();
        $scope.linker.disable();
        $scope.connection.loadGrid();
    });

    // $scope.connection.loadGrid();
}]);

FractalControllers.controller("gridController", ["$scope", "gridMaster", "$window", "$timeout", 
                                                 function($scope, gridMaster, $window, $timeout){

    var updateCoordTimer = null;
    $scope.completeGrid = function(){
        $timeout.cancel(updateCoordTimer);
        var x = $window.scrollX,
            y = $window.scrollY;
        $scope.items = gridMaster.completeGrid($scope.items, $scope.setting);
    
        $timeout(function() {
            $window.scrollTo(x, y);
        }, 0);

        updateCoordTimer = $timeout(function(){
            var coord = gridMaster.getItemsCoord($scope.items);
            $scope.connection.updateCoord(coord);
        }, 5000);

    };

    $scope.sortableOptions = {
        tolerance: "pointer",
        containment:".workplace",
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
            !$window.confirm("Вы уверены, что хотите удалить заметку?"))
            return;

        if(item == $scope.linker.currentItem) 
            $scope.linker.disable();

        $scope.connection.deleteNote(item);
        $timeout(deleteInFront, 500);

        function deleteInFront(){
            if(item.state != "success")
                $timeout(deleteInFront, 500);
            else if(item.state == "success"){
                gridMaster.removeItem($scope.items, item);
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

    $scope.tryCompleteNote = function(item)
    {
        if(item.action == "saving")
        {
            item.state = "edit";
            $scope.saveNote(item);
        } else if( item.action == "deleting")
            $scope.deleteNote(item);
    }

    $scope.deleteGrid = function(item)
    {
        item.action = "deleting";
        if(!(item.title === "" && item.text === "") &&
            !$window.confirm("Вы уверены, что хотите удалить лист?"))
            return;

        if(item == $scope.linker.currentItem) 
            $scope.linker.disable();

        $scope.connection.deleteGrid(item);
        $timeout(deleteInFront, 500);

        function deleteInFront(){
            if(item.state != "success")
                $timeout(deleteInFront, 500);
            else if(item.state == "success"){
                gridMaster.removeItem($scope.items, item);
                $scope.completeGrid();
            }
        }
    }

    $scope.saveGridItem = function(item)
    {
        item.action = "saving";
        if(item == $scope.linker.currentItem) 
            $scope.linker.disable();

        if(item.id < 0){
            $scope.connection.createGridItem(item);
            $timeout(recoveryId, 500);
        } else
            $scope.connection.updateGridItem(item);

        item.save();

        function recoveryId(){
            if(item.realId != undefined)
                gridMaster.recoveryId(item, $scope.items);
            if(item.state != "error")
                $timeout(recoveryId, 500);
        }
    }

    $scope.tryCompleteGrid = function(item)
    {
        if(item.action == "saving")
        {
            item.state = "edit";
            $scope.saveGrid(item);
        } else if(item.action == "deleting")
            $scope.deleteGird(item);
    }
}]);