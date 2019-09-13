(function (app) {
    'use strict';

    app.factory('membershipService', membershipService);

    membershipService.$inject = ['apiService', 'notificationService', '$http', '$base64', '$cookieStore', '$rootScope'];

    function membershipService(apiService, notificationService, $http, $base64, $cookieStore, $rootScope) {

        var service = {
            login: login,
            saveCredentials: saveCredentials,
            removeCredentials: removeCredentials,
            isUserLoggedIn: isUserLoggedIn


        }

        function login(user, completed) {
            apiService.post('/api/conta/autenticar', user,
            completed,
            loginFailed);
        }


        function saveCredentials(usuario, nomeUsuario) {

            var membershipData = nomeUsuario;

            $rootScope.repository = {
                loggedUser: {
                    username: usuario.Email,
                    authdata: membershipData,
                    nome: nomeUsuario
                }
            };

            localStorage.setItem("kor", $rootScope.repository.loggedUser.authdata);

            $http.defaults.headers.common['Authorization'] = 'Bearer ' + nomeUsuario;

            $cookieStore.put('repositoryAdmKor', $rootScope.repository);
        }


        function removeCredentials() {
            $rootScope.repository = null;
            $cookieStore.remove('repositoryAdmKor');
            localStorage.removeItem('kor','');
            $http.defaults.headers.common.Authorization = '';
        }


        function loginFailed(response) {
            notificationService.displayError(response.data);
        }

        
        function isUserLoggedIn() {

            var token = localStorage["kor"];

            if (token !== null && token !== undefined) {
                
                $http.defaults.headers.common['Authorization'] = "Bearer " + token;
              return true;
            }
            return false;
        }
        return service;
    }

})(angular.module('AppKor'));