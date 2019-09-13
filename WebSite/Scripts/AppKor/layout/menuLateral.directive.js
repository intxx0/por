(function (app) {
    'use strict';

    app.directive('menuLateral', menuLateral);

    function menuLateral() {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: 'scripts/AppKor/layout/menuLateral.html'
        }
    }

})(angular.module('AppKor'));