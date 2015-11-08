'use strict';

var FractalConnection = angular.module("FractalConnection", ["FractalItemFactory"]);

FractalConnection.factory('connection', ["$http", "$location", "itemFactory", 
                                         function($http, $location, itemFactory) {

    return { 
      loadGrid: loadGrid,

      createNote: createNote,
      updateNote: updateNote,
      deleteNote: deleteNote
    };


function loadGrid(slug)
{
  var scope = this["scope"];

  var url = "/api";

  if(slug != undefined)
    url += "/grid/" + slug;
  else
    url += ($location.path() == "" ? "/grid/main" : $location.path());
  

  scope.messager.show("load " + url)
  $http.get(url).success(function(data) {
      scope.messager.tmpShow("Loaded " + url, 3000);
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

function createNote(item)
{
  var scope = this["scope"];
  var url = "/api/note/"
  scope.messager.show("Save Note...")
  $http.post(url, item)
       .success(function(data){
          scope.messager.tmpShow("Success", 3000);
          item.realId = data.id;
       }).error(function(){
          item.state = "error";
          scope.messager.show("Fail in save note");
       });
}

function updateNote(item)
{
  var scope = this["scope"];
  var url = "/api/note/"
  scope.messager.show("Update Note...")
  $http.put(url, item)
       .success(function(data){
          scope.messager.tmpShow("Success", 3000);
       }).error(function(){
          item.state = "error";
          scope.messager.show("Fail in update note");
       });
}

function deleteNote(item)
{
  var scope = this["scope"];
  var url = "/api/note/" + item.id;
  scope.messager.show("Delete Note...")
  $http.delete(url)
      .success(function(){
        scope.messager.tmpShow("Success", 3000);
        item.state = "success";
      }).error(function(){
        item.state = "error";
        scope.messager.show("Fail in delete note");
      });

}
}]);