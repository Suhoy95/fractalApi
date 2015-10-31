'use strict';

var FractalConnection = angular.module("FractalConnection", ["FractalItemFactory"]);

FractalConnection.factory('connection', ["$http", "$location", "itemFactory", 
                                         function($http, $location, itemFactory) {

    return { 
      loadGrid: loadGrid
    };


function loadGrid()
{
  var scope = this["scope"];

  var url = "/api" + $location.path();
  $http.get(url).success(function(data) {
      scope.status = "ok";
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
            fixedWidth: data.FixedWidth
      };
      scope.completeGrid();
  }).error(function(){
      scope.status = "error";
  });
}

}]);