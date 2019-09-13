(function (app) {
    'use strict';

    app.controller('topBarCtrl', topBarCtrl);

    topBarCtrl.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', '$timeout', '$location'];

    function topBarCtrl($scope, $rootScope, apiService, notificationService, $timeout, $location) {

        // #region Variáveis
        var usuarioId = JSON.parse(localStorage.getItem('userData')).UsuarioId;
        var promiseMensagens;
        var promiseEmergencias;
        var promiseEmails;
        var contadorMensagens = 15;
        var contadorEmergencias = 15;
        var contadorEmails = 15;
        $scope.editProfile = editProfile;

        // #endregion
     

        // #region Verifica Mensagens não lidas

        function verificaMensagensNaoLidas() {
            

            apiService.get('/api/mensagem/listamensagem/' + usuarioId, null,
                (result) => {

                    var tamanho = result.data;

                    if (tamanho == "")
                        tamanho = 0;

                    $rootScope.MensagensNaoLidas = tamanho;
                },
                (result) => {

                    console.log(result.data);
                });
        }

        // #endregion
     


        // #region Verifica Emergencias não atendidas

        function verificaEventosNaoLidos() {

            apiService.get('/api/evento/lista_qtd_eventos_nao_visualizados/', null,
                (result) => {

                    var tamanho = result.data;

                    if (tamanho == "")
                          tamanho = 0;

                    $rootScope.EmergenciasAtivas = tamanho;
                },
                (result) => {

                    console.log(result.data);
                });
        }

        // #endregion



        // #region Verifica E-mails não lidos

        function verificaEmailsNaoLidos() {

            apiService.get('/api/email/lista_qtd_emails_nao_lidos/' + usuarioId, null,
                (result) => {

                    var tamanho = result.data;

                    if (tamanho == "")
                          tamanho = 0;

                    $rootScope.EmailsNaoLidos = tamanho;
                },
                (result) => {

                    console.log(result.data);
                });
        }

        // #endregion


        //Chama a API de tempos em tempos para verificar se tem alguma mensagem não lida

        function ativarRefreshMensagens() {
            contadorMensagens--;
            if (contadorMensagens === 0) {

                verificaEventosNaoLidos();
                contadorMensagens = 15;
            }

            promiseMensagens = $timeout(ativarRefreshMensagens, 1000);
        }


        //Chama a API de tempos em tempos para verificar se tem alguma emergência não atendida

        function ativarRefreshEmergenciasAtivas() {
            contadorEmergencias--;
            if (contadorEmergencias === 0) {

                verificaMensagensNaoLidas();
                contadorEmergencias = 15;
            }

            promiseEmergencias = $timeout(ativarRefreshEmergenciasAtivas, 1000);
        }


        //Chama a API de tempos em tempos para verificar se tem algum email não lido

        function ativarRefreshEmailsNaoLidos() {
            contadorEmails--;
            if (contadorEmails === 0) {

                verificaEmailsNaoLidos();
                contadorEmails = 15;
            }

            promiseEmails = $timeout(ativarRefreshEmailsNaoLidos, 1000);
        }

        function loadUserData() {

            var userData = JSON.parse(localStorage.getItem('userData'));

            $scope.UserId = userData.UsuarioId;
            $scope.UserName = userData.DescNome;
        }

        function editProfile(userId) {

            $location.path('/cadusuario/' + userId);
        }

        function getUserData(login) {

            apiService.get('/api/usuarios/getuserdata/' + login + '/', null,
                (result) => {

                    localStorage.setItem('userData', JSON.stringify(result.data));
                    $scope.UserId = result.data.UserId;
                    $scope.UserName = result.data.UserName;
                },
                (result) => {

                    console.log(result.data);
                });
        }

        //cancela todas as requisiçoes de tempos em tempos ao sair da aplicação

        $scope.$on('$destroy', function () {

            $timeout.cancel(promiseMensagens);
            $timeout.cancel(promiseEmergencias);
            $timeout.cancel(promiseEmails);
        });


        // #region Chamada de métodos ao carregar página

        verificaMensagensNaoLidas();
        verificaEventosNaoLidos();
        verificaEmailsNaoLidos();

        ativarRefreshMensagens();
        ativarRefreshEmergenciasAtivas();
        ativarRefreshEmailsNaoLidos();
        setTimeout(loadUserData, 1000);

        // #endregion
    }

})(angular.module('AppKor'));