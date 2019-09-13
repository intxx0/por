(function (app) {
    'use strict';

    app.controller('cadUsuariosOperacaoModalCtrl', cadUsuariosOperacaoModalCtrl);

    cadUsuariosOperacaoModalCtrl.$inject = ['$scope', '$http', '$modalInstance', 'items', 'apiService', 'notificationService', '$modal'];

    function cadUsuariosOperacaoModalCtrl($scope, $http, $uibModalInstance, items, apiService, notificationService, $modal) {

        $scope.Operacao = items;
        $scope.cancel = cancel;
        $scope.inserirUsuarioOperacao = inserirUsuarioOperacao;

        $scope.novoUsuarioOperacao = {};
        $scope.Usuarios = [];

        
        //1--------------------Carrega Usuários--------------------------------

        function carregaUsuarios() {

            apiService.get('/api/fiscalizacaoonline/listausuarios', null,
                loadUsuariosSuccess,
                loadUsuariosFailed);
        }

        function loadUsuariosSuccess(result) {

            $scope.Usuarios = result.data.Items;
        }

        function loadUsuariosFailed(result) {

            notificationService.displayError('Erro ao carregar usuários!', 'Atenção!');
        }
        
        //1----------------------------------------------------------------------



       
        //2----------------Fechar Modal------------------------------
        function cancel() {
            $uibModalInstance.dismiss();
        };
        //2----------------Fim Modal----------------------------------



        //3--------------Associar usuários a uma Operação------------

        function inserirUsuarioOperacao() {

            $scope.novoUsuarioOperacao.IdOperacao = items.IdFiscalizacao;

            for (var i = 0; i < $scope.novoUsuarioOperacao.IdUsuario - 1; i++) {

                $scope.novoUsuarioOperacao.IdUsuario = $scope.novoUsuarioOperacao.IdUsuario[i];
            }

            apiService.post('/api/fiscalizacaoonline/inserirusuariooperacao', $scope.novoUsuarioOperacao,
                loadInserirUsuarioOperacaoSuccess,
                loadInserirUsuarioOperacaoFailed);
        }

        function loadInserirUsuarioOperacaoSuccess(result) {

            $scope.novoUsuarioOperacao = result.data.Items;

            notificationService.displaySuccess('Sucesso ao cadastrar usuários para Operação','Atenção');

        }

        function loadInserirUsuarioOperacaoFailed(resutl) {
            
            notificationService.displayError('Erro ao cadastrar usuário para Operação', 'Atenção');
        }


        //3----------------------------------------------------------


        carregaUsuarios();

    }

})(angular.module('AppKor'));