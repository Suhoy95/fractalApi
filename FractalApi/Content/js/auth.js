'use strict';

var FractalAuth = angular.module("FractalAuth", []);

FractalAuth.factory('authManager', ["$http", function($http) {

    var manager = {
        name: "",
        login: "",
        password: "",
        isAuth: false,
        badlogin: false,
        authorizate: authorizate,
        out: out
    };
    return manager;

function authorizate() {
    manager.badlogin = false;
    $http.post('/api/auth/login', {Login: manager.login, Password: manager.password })
         .success(function (data) {
            data = data.split("#");
            $http.defaults.headers.common.Authorization = 'Basic ' + data[0];
            manager.name = data[1];
            manager.login = manager.password = "";
            manager.isAuth = true;
        })
         .error(function(data, status){
            if(status == 401)
                manager.badlogin = true;
        });
};

function out() {
    $http.defaults.headers.common.Authorization = 'Basic ';
    manager.name = "";
    manager.isAuth = false;
    manager.badlogin = false;
};


}]);