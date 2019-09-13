﻿var app = angular.module('AppKor', ['pascalprecht.translate']);
app.config(['$translateProvider', function ($translateProvider) {

    $translateProvider.translations('en_US', {
        'SUBMIT': 'Submit',
        'CLEAR': 'Clear'
    });
    $translateProvider.translations('pt_BR', {
        'SUBMIT': 'Enviar',
        'CLEAR': 'Limpar'
    });
    $translateProvider.translations('es_ES', {
        'SUBMIT': 'Enviar',
        'CLEAR': 'Limpiar'
    });

    $translateProvider.preferredLanguage('pt_BR');
    //$translateProvider.fallbackLanguage('pt_BR');
    //$translateProvider.forceAsyncReload(true);
    $translateProvider.setLang('pt_BR');
    $translateProvider.setLocalLang('pt_BR');
}]);
app.controller('translateCtrl', function ($scope, $translate) {
    $scope.tlData = {
        staticValue: 42,
        randomValue: Math.floor(Math.random() * 1000)
    };
    $scope.jsTrSimple = $translate.instant('SERVICE');
    $scope.jsTrParams = $translate.instant('SERVICE_PARAMS', $scope.tlData);
    $scope.setLocalLang = function (langKey) {
        $scope.translateLanguage = langKey;
    }
    $scope.setLang = function (langKey) {
        // You can change the language during runtime
        $translate.use(langKey);
        // A data generated by the script have to be regenerated
        $scope.jsTrSimple = $translate.instant('SERVICE');
        $scope.jsTrParams = $translate.instant('SERVICE_PARAMS', $scope.tlData);
    };
});