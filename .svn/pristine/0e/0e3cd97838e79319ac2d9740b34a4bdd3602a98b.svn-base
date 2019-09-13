(function (app) {
    'use strict';

    app.controller('modalalterausuarioCtrl', modalalterausuarioCtrl);

    modalalterausuarioCtrl.$inject = ['$scope', '$http', '$modalInstance', 'items', 'apiService', 'notificationService', '$translate', '$location'];

    function modalalterausuarioCtrl($scope, $http, $uibModalInstance, items, apiService, notificationService, $translate, $location) {

        $scope.cancel = cancel;
        $scope.atualizarUsuario = atualizarUsuario;
        $scope.confirmSenha = "";

        $scope.Grupos = [];
        $scope.Cargos = [];
        $scope.Instituicoes = [];

        $scope.Ativo = [{ tipo: $translate.instant('INACTIVE'), value: "0" }, { tipo: $translate.instant('ACTIVE'), value: "1" }];

        delete items.Senha;

        $scope.novoUsuario = items;

        console.log(items);

        function atualizarUsuario() {

            if ($scope.novoUsuario.Senha == $scope.confirmSenha)
                alterarUsuario();
            else
                notificationService.displayError($translate.instant("ERROR_NOT_EQUAL_PASS"));
        }

        // 1-------------------------Altera Usuário-------------------------------
        function alterarUsuario() {

            apiService.post('/api/usuarios/alterarusuario', $scope.novoUsuario,
                loadAlterarUsuarioSuccess,
                loadAlterarUsuarioFailed);

        }

        function loadAlterarUsuarioSuccess(result) {

            notificationService.displaySuccess("Usuário alterado com sucesso!", "Atenção");
        }

        function loadAlterarUsuarioFailed(result) {

            notificationService.displayError("Erro ao alterar usuário!", "Atenção");
        }
        // 1----------------------------------------------------------------------


        // 2--------------------------Carrrega Grupos-----------------------------
        function carregaGrupos() {

            apiService.get('/api/grupos/listagrupos', null, loadGruposSuccess, loadGruposFailed);
        }

        function loadGruposSuccess(result) {

            var newItem = new function () {
                this.IdGrupo = undefined;
                this.DescGrupo = $translate.instant('SELECT') + '...';

            };

            result.data.Items.push(newItem);


            $scope.Grupos = result.data.Items;
        }

        function loadGruposFailed(result) {
            $scope.Grupos = result.data;
        }
        // 2-----------------------------------------------------------------------


        // 3--------------------------Carrrega Status-----------------------------
        function carregaStatus() {

            var newItem = new function () {
                this.value = undefined;
                this.tipo = $translate.instant('SELECT') + '...';

            };

            $scope.Ativo.push(newItem);

        }
        // 3-----------------------------------------------------------------------


        // 4--------------------------Carrrega Cargos-----------------------------
        function carregaCargos() {
            apiService.get('/api/cargo/listacargo', null, loadCargosSuccess, loadCargosFailed);
        }

        function loadCargosSuccess(result) {

            var newItem = new function () {
                this.IdCargo = undefined;
                this.NomeCargo = $translate.instant('SELECT') + '...';

            };
            result.data.Items.push(newItem);
            $scope.Cargos = result.data.Items;

        }

        function loadCargosFailed(result) {
            $scope.Cargos = result.data;
        }
        // 4---------------------------------------------------------------------


        // 5-----------------------Carrrega Instituições-------------------------
        function carregaInstituicoes() {
            apiService.get('/api/instituicao/listainstituicao', null, loadInstituicoesSuccess, loadInstituicoesFailed);
        }

        function loadInstituicoesSuccess(result) {

            var newItem = new function () {
                this.IdInstituicao = undefined;
                this.NomeInstituicao = $translate.instant('SELECT') + '...';

            };
            result.data.Items.push(newItem);
            $scope.Instituicoes = result.data.Items;

        }

        function loadInstituicoesFailed(result) {
            $scope.Instituicoes = result.data;
        }
        // 5--------------------------------------------------------------------


        //-----------------Chamada dos métodos--------------------------------------
        carregaStatus();
        carregaGrupos();
        carregaCargos();
        carregaInstituicoes();
        //----------------Fim da chamada dos métodos-------------------------------

        //----------------Fechar Modal------------------------------
        function cancel() {

            $uibModalInstance.dismiss();
        };

        //----------------Fim Modal----------------------------------
    }

})(angular.module('AppKor'));