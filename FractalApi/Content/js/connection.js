'use strict';

var FractalConnection = angular.module("FractalConnection", ["FractalItemFactory"]);

FractalConnection.factory('connection', ["$http", "$location", "$window" ,"itemFactory", 
                                         function($http, $location, $window, itemFactory) {

    return { 
      home: home,
      loadGrid: loadGrid,

      createNote: createNote,
      updateNote: updateNote,
      deleteNote: deleteNote,

      createGridItem: createGridItem,
      updateGridItem: updateGridItem,
      updateCoord: updateCoord,
      deleteGrid: deleteGrid
    };

function home()
{
  $location.path("/");
}

function loadGrid(slug)
{
  var scope = this["scope"];

  var url = "/api";

  if(slug != undefined)
    url += "/grid/" + slug;
  else
    url += ($location.path() == "/" ? "/grid/main" : $location.path());
  

  scope.messager.show("load " + url)
  $http.get(url).success(function(data) {
      scope.messager.tmpShow("Loaded " + url, 3000);
      
      scope.items.splice(0, scope.items.length);
      for(var x = 0; x < data.Items.length; x++)
      {
        scope.items[x] = [];
        for(var y = 0; y < data.Items[x].length; y++)
          scope.items[x][y] = itemFactory.recovery(data.Items[x][y]);

      }
      scope.setting = {
            gridId: data.Id,
            title: data.Title,
            minWidth: data.Width,
            minHeight: 1,
            width: data.Width,
            fixedWidth: data.FixedWidth,
            pageTitle: data.PageTitle,
            pageDescription: data.PageDescription,
            pageKeywords: data.PageKeywords
      };
      scope.completeGrid();
  }).error(function(data, status_code){
      if(status_code == 404){
        setNotFoundGrid(scope);
        return;
      }
      scope.messager.show("Fail in load " + url);
  });
}

function setNotFoundGrid(scope)
{
  scope.items.splice(0, scope.items.length);
  var message = itemFactory.noteItem({
    title: "Ошибка", 
    text: "К сожалению, данной страницы не существует."
  });
  var main = itemFactory.gridItem({
    slug: "main",
    title: "Главная",
    text: "Вернуться на главную страницу"
  });
  scope.items[0] = [message];
  scope.items[1] = [main];

  scope.setting = {
        title: "404: Страница не найдена",
        width: 2,
        minWidth: 2,
        minHeight: 1,
        fixedWidth: true,
        pageTitle: "Страница не найдена - Fractal"
  };
}

function createNote(item)
{
  var scope = this["scope"];
  var url = "/api/note/"
  scope.messager.show("Save Note...")
 
  item.gridId = scope.setting.gridId;
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
        scope.messager.show("Fail in delete grid");
      });
}

function createGridItem(item)
{
  var scope = this["scope"];
  var url = "/api/griditem/"
  scope.messager.show("Save Grid...")

  item.gridId = scope.setting.gridId;
  $http.post(url, item)
       .success(function(data){
          scope.messager.tmpShow("Success", 3000);
          item.realId = data.id;
       }).error(function(data){
          if(data == "BadSlug"){
            $window.alert("Не удается сохранить лист по ссылке #/grid/" + item.slug);
            item.edit();
            return;
          }
          item.state = "error";
          scope.messager.show("Fail in save grid");
       });
}

function updateGridItem(item)
{
  var scope = this["scope"];
  var url = "/api/griditem/"
  scope.messager.show("Update Grid...")
  $http.put(url, item)
       .success(function(data){
          scope.messager.tmpShow("Success", 3000);
       }).error(function(data){
          if(data == "BadSlug"){
            $window.alert("Не удается сохранить лист по ссылке #/grid/" + item.slug);
            item.edit();
            return;
          }
          item.state = "error";
          scope.messager.show("Fail in update grid");
       });
}

function updateCoord(coord)
{
  var scope = this["scope"];
  var url = "/api/coord/"
  scope.messager.show("Update items moving...")
  $http.put(url, coord)
       .success(function(data){
          scope.messager.tmpShow("Success", 3000);
       }).error(function(data){
          scope.messager.show("Fail in update items coord");
       });
}

function deleteGrid(item)
{
  var scope = this["scope"];
  var url = "/api/grid/" + item.id;
  scope.messager.show("Delete Grid...")
  $http.delete(url)
      .success(function(){
        scope.messager.tmpShow("Success", 3000);
        item.state = "success";
      }).error(function(){
        item.state = "error";
        scope.messager.show("Fail in delete grid");
      });
}

}]);