'use strict';

var FractalConnection = angular.module("FractalConnection", ["FractalItemFactory"]);

FractalConnection.factory('connection', ["$http", "$location", "itemFactory", 
                                         function($http, $location, itemFactory) {

    return { 
      loadGrid: loadGrid,
      // deleteItem: deleteItem
    };


function loadGrid()
{
  var scope = this["scope"];

  var url = "/api" + ($location.path() == "" ? "/grid/main" : $location.path());
  
  scope.messager.show("load " + url)
  $http.get(url).success(function(data) {
      scope.messager.tmpShow("Loaded", 1000);
      scope.data = data;

      for(var x = 0; x < data.Items.length; x++)
      {
        scope.items[x] = [];
        for(var y = 0; y < data.Items[x].length; y++)
          scope.items[x][y] = itemFactory.recovery(data.Items[x][y]);

      }
      scope.setting = {
            title: data.Title,
            minWidth: data.Width,
            width: data.Width,
            fixedWidth: data.FixedWidth,
            pageTitle: data.PageTitle,
            pageDescription: data.PageDescription,
            pageKeywords: data.PageKeywords
      };
      scope.completeGrid();
  }).error(function(){
      scope.messager.show("Faild to load " + url);
  });
}

function deleteItem(item)
{
  var url = /api/ + item.type +  "/" + item.id
  $http.delete(url).success(function(){
    item.state = "success";
  }).error(function(){
    item.state = "error";
  });

}
}]);