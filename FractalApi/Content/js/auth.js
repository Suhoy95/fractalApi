'use strict';

var FractalAuth = angular.module("FractalAuth", ["ngCookies"]);

FractalAuth.factory('authManager', ["$http", "$cookies", function($http, $cookies) {
    var cookieKey = "authorization";
    var manager = {
        name: "",
        login: "",
        password: "",
        isAuth: false,
        badlogin: false,
        authorizate: authorizate,
        checkAuth: checkAuth,
        out: out
    };

    manager.checkAuth();
    return manager;

function authorizate(success) {
    manager.badlogin = false;
    $http.post('/api/auth/login', {Login: manager.login, Password: manager.password })
         .success(function (data) {
            $cookies.put(cookieKey, data);
            data = data.split("#");
            $http.defaults.headers.common.Authorization = 'Basic ' + data[0];            
            manager.name = data[1];
            manager.login = manager.password = "";
            manager.isAuth = true;
            if(success) 
                success();
        })
         .error(function(data, status){
            if(status == 401)
                manager.badlogin = true;
        });
};

function checkAuth(){
    var token = $cookies.get(cookieKey);

    if(!token)
        return;
    
    token = token.split("#");
    $http.defaults.headers.common.Authorization = 'Basic ' + token[0];     
    $http.post('/api/auth/check')
         .success(function(){
            manager.name = token[1];
            manager.login = manager.password = "";
            manager.isAuth = true;  
         })
         .error(function(){
            manager.out();
         })
}

function out() {
    $http.defaults.headers.common.Authorization = 'Basic ';
    manager.name = "";
    manager.isAuth = false;
    manager.badlogin = false;
    $cookies.remove(cookieKey);
};


}]);