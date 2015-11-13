'use strict';

/* App Module */

var FractalApp = angular.module('FractalApp', ['ui.sortable',
                                               'FractalConnection',
                                               'FractalAuth',
                                               'FractalGridMaster',
                                               'FractalLinker',
                                               'FractalShower',
                                               'FractalControllers', 
                                               'FractalDirectives']);

FractalApp.config(["$locationProvider", function($locationProvider) {
        $locationProvider.html5Mode({ enabled: true,
                                      requireBase: false});
}])