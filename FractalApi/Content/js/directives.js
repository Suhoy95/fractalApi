'use strict';

/* Directives */

var FractalDirectives = angular.module("FractalDirectives", []);

FractalDirectives.directive("compliteWidth", function(){
    return function(scope, element, attr)
    {
        var amountOfColumns;
        var columnsWidth = 17;

        function setWidth()
        {
            element.css("width", amountOfColumns*columnsWidth + "em");
        }

        attr.$observe('compliteWidth', function(value){
            amountOfColumns = value;
            setWidth();
        });
    }
});