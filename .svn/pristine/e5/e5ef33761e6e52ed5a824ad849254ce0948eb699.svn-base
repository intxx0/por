(function (app) {
    'use strict';

    app.factory('apiService', apiService);

    apiService.$inject = ['$http', '$location', 'notificationService', '$rootScope', 'configApi'];

    function apiService($http, $location, notificationService, $rootScope, configApi) {
        var service = {
            get: get,
            post: post
        };

        function get(url, config, success, failure) {
            return $http.get(configApi + url, config)
                    .then(function (result) {
                        success(result);
                    }, function (error) {
                        if (error.status == '401') {
                            //notificationService.displayError('Autenticação requerida.');
                            $rootScope.previousState = $location.path();
                            //$location.path('/login');
                            $location.path('/error');
                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        }

        function post(url, data, success, failure) {
            return $http.post(configApi + url, data)
                    .then(function (result) {
                        success(result);
                    }, function (error) {
                        if (error.status == '401') {
                            //notificationService.displayError('Favor efetuar o login.');
                            $rootScope.previousState = $location.path();
                            //$location.path('/login');
                            $location.path('/error');
                        }
                    
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        }

        return service;
    }

})(angular.module('AppKor'));