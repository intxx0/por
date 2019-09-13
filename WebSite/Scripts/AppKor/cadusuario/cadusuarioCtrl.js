﻿    (function (app) {
    'use strict';

    app.controller('cadusuarioCtrl', cadusuarioCtrl);

    cadusuarioCtrl.$inject = ['$scope', '$http', '$timeout', '$interval', '$base64', 'apiService', 'notificationService', '$translate', '$location', '$modal'];

    function cadusuarioCtrl($scope, $http, $timeout, $interval, $base64, apiService, notificationService, $translate, $location, $modal) {

        $scope.limparCampos = limparCampos;
        $scope.verificaIdUsuarioInserirAtualizar = verificaIdUsuarioInserirAtualizar;
        $scope.carregarUsuario = carregarUsuario;
        $scope.carregaInstituicoes = carregaInstituicoes;
        $scope.confirmSenha = "";
        $scope.abreModalAlteraUsuario = abreModalAlteraUsuario;

        $scope.Usuarios = [];
        $scope.Grupos = [];
        $scope.Cargos = [];
        $scope.Instituicoes = [];

        $scope.Ativo = [{ tipo: $translate.instant('INACTIVE'), value: "0" }, { tipo: $translate.instant('ACTIVE'), value: "1" }];
        
        var parts = $location.path().split('/');

        if (parts.length > 2) {
            var usuario = JSON.parse(localStorage.getItem('userData'));
            $scope.novoUsuario = usuario;
        }

        //1-----------------Carrega lista de usuários-----------------------------

        function carregarUsuario(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 3,
                    filter: $scope.filtroUsuario
                }
            };


            apiService.get('/api/usuarios/listausuarios', config, loadUsuarioSuccess, loadUsuarioFailed);
        }

        function loadUsuarioSuccess(result) {

            $scope.Usuarios = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
        }

        function loadUsuarioFailed(result) {

            notificationService.displayError('Erro ao carregar lista de usuários !', 'Atenção');
        }

        //1-------------------------Fim ------------------------------------------


        //2----------------Verifica o Id do usuário-------------------------------
        function verificaIdUsuarioInserirAtualizar() {

            if (!$scope.novoUsuario.UsuarioId > 0) 
                confirmarSenha(inserirUsuario);
            else 
                confirmarSenha(alterarUsuario);
        }

        function confirmarSenha(callback) {
            if ($scope.novoUsuario.Senha == $scope.confirmSenha) 
                callback();
            else 
                notificationService.displayError($translate.instant("ERROR_NOT_EQUAL_PASS"));
        }

        //2-----------------------------------------------------------------------


        //3--------------------Inseri Usuário-------------------------------------

        function inserirUsuario() {

            apiService.post('/api/usuarios/inserirusuario', $scope.novoUsuario,
                loadInserirUsuarioSuccess,
                loadInserirUsuarioFailed);
        }

        function loadInserirUsuarioSuccess(result) {

            if (result.data) {
                $scope.novoUsuario = result.data;

                notificationService.displaySuccess("Usuario cadastrado com sucesso!", "Atenção");

                carregarUsuario();
                limparCampos();
            }
            else {
                notificationService.displayWarning("Este email já está sendo usado", "Atenção");
            }
           
        }

        function loadInserirUsuarioFailed(result) {

            notificationService.displayError("Erro ao inserir usuário!", "Atenção");
        }

        //3------------------Fim--------------------------------------------------



        //6 -------------Função para limpar campos do formulário------------------
        function limparCampos() {

            $scope.novoUsuario = {};
        }
        //6-----------------------------------------------------------------------


        // 7--------------------------Carrrega Grupos-----------------------------

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

        // 7-----------------------------------------------------------------------

        // 8--------------------------Carrrega Status-----------------------------
        function carregaStatus() {

            var newItem = new function () {
                this.value = undefined;
                this.tipo = $translate.instant('SELECT') + '...';

            };

            $scope.Ativo.push(newItem);

        }
        // 8-----------------------------------------------------------------------

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

        function abreModalAlteraUsuario(usuario) {

            $modal.open({
                animation: true,
                templateUrl: 'scripts/AppKor/cadusuario/modalalterausuario.html',
                controller: 'modalalterausuarioCtrl',
                size: 'lg',
                resolve: {
                    items: function () {
                        return usuario;
                    }
                }
            });
        }

        //-----------------Chamada dos métodos--------------------------------------
        carregaStatus();
        carregaGrupos();
        carregarUsuario();
        carregaCargos();
        carregaInstituicoes();
        //----------------Fim da chamada dos métodos-------------------------------
    }

})(angular.module('AppKor'));