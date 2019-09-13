(function (app) {
    'use strict';

    app.controller('modalnovaconversaCtrl', modalnovaconversaCtrl);

    modalnovaconversaCtrl.$inject = ['$scope', '$modalInstance', 'items', 'apiService', 'notificationService', '$rootScope'];

    function modalnovaconversaCtrl($scope, $uibModalInstance, items, apiService, notificationService, $rootScope) {

        $scope.cancel = cancel;
        $scope.carregaUsuarios = carregaUsuarios;
        $scope.usuarios = [];
        $scope.usuario = {};

        $scope.selecionaUsuario = selecionaUsuario;
        $scope.abrirConversa = abrirConversa;

        var usuarioId = items;
        $rootScope.usuariosSelecionados = [];

        function carregaUsuarios() {

            apiService.get('/api/mensagem/listausuariosconversa/' + usuarioId, null,
                carregarConversasSuccess,
                carregarConversasFailed);
        }

        function carregarConversasSuccess(result) {

            $scope.usuarios = result.data;
        }
        function carregarConversasFailed(result) {

            console.log(result.data);
        }


        function selecionaUsuario(usuario) {

            $scope.usuario = usuario;
        }

        function abrirConversa() {

            if ($scope.usuario.UsuarioId != -1) {

                $uibModalInstance.close($scope.usuario); 
            }
            else {

                notificationService.displayWarning("Nenhum usuário selecionado!");
            }
        }

        carregaUsuarios();

        //----------------Fechar Modal------------------------------
        function cancel() {
            $uibModalInstance.dismiss();
        }
        //----------------Fim Modal----------------------------------

    }
})(angular.module('AppKor'));