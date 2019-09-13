(function (app) {
    'use strict';

    app.controller('loginCtrl', loginCtrl);


    loginCtrl.$inject = ['$scope', '$translate', 'membershipService', 'notificationService', '$http', '$base64', '$rootScope', '$location','configApi','apiService'];


    function loginCtrl($scope, $translate, membershipService, notificationService, $http, $base64, $rootScope, $location, configApi, apiService) {

        $scope.login = login;
        $scope.limparCampos = limparCampos;
        $scope.changeLanguage = changeLanguage;

        $scope.usuario = {
            Email: null, 
            Senha: null
        };

        $scope.languages =  [
                { code: 'en_US', name: 'ENGLISH' },
                { code: 'pt_BR', name: 'PORTUGUESE' },
                { code: 'es_ES', name: 'SPANISH' }
        ];
    
        $scope.selectedLanguage = { code: $translate.use() };

        var authurl = configApi + '/api/conta/token';

        function login() {

            localStorage.removeItem('kor','');

            var usuario = $scope.usuario.Email;
            //var senha = $base64.encode($scope.usuario.Senha);
            var senha = $scope.usuario.Senha;

            var data = "grant_type=password&username=" + usuario + "&password=" + senha + "";

            $http.post(authurl, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .success(function (result) {
                    getUserData(usuario);
                    localStorage.setItem('kor', result.access_token);
                    membershipService.saveCredentials($scope.usuario, result.access_token);
                    $location.path('/home');
                    notificationService.displaySuccess($translate.instant('WELCOME') + " " + $scope.usuario.Email);
                    $scope.userData.displayUserInfo();
                }).error(function(result) {
                    toastr["error"]($translate.instant(result.error_description), $translate.instant('ATTENTION') + "!<br><br>");
                });
        }

        function limparCampos()
        {
            $scope.usuario = {};
        }

        function limparCookies() {
            localStorage.removeItem('kor');
        }

        limparCookies();

        function changeLanguage(language) {
            $scope.translateLanguage = language.code;
            $translate.use(language.code);
        }

        function getUserData(login) {
            apiService.get('/api/usuarios/getuserdata/' + login + '/', null, 
                loadUserDataSuccess,
                loadUserDataFailed);
        }

        function loadUserDataSuccess(result) {
            localStorage.setItem('userData', JSON.stringify(result.data));
        }

        function loadUserDataFailed(result) {
        }

        localStorage.removeItem('onLoadEvents');

    }

})(angular.module('AppKor'));

