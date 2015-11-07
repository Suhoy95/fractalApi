'use strict';

var FractalShower = angular.module("FractalMessager", []);

FractalShower.factory('messager', ["$timeout", function($timeout) {
    var messager = {
      active: false,
      message: "",
      show: show,
      tmpShow: tmpShow,
      timerId: null
    };

    return messager;

function show(message)
{
    this.message = message;
    this.active = true;
}

function tmpShow(message, timeout)
{
    if(messager.timerId){
        messager.timerId.clear();
        messager.timerId = null;
    }

    messager.message = message;

    messager.timerId = $timeout(function(){
        messager.active = false;
        messager.timerId = null;
    }, timeout);
}


}]);