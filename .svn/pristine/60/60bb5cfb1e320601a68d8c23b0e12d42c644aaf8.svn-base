(function (app) {
    'use strict';

    app.controller('cadgrupoCtrl', cadgrupoCtrl);

    cadgrupoCtrl.$inject = ['$scope', '$http', 'apiService', 'notificationService', '$translate'];

    function cadgrupoCtrl($scope, $http, apiService, notificationService, $translate) {


        //var urlListaGrupos = "http://localhost:62272/api/grupos/listagrupos";
        //var urlInseriGrupo = "http://localhost:62272/api/grupos/inserirgrupo";
        //var urlAtualizaGrupo = "http://localhost:62272/api/grupos/alterargrupo";


        $scope.Grupos = [];


        $scope.carregarGrupos = carregarGrupos;
        $scope.editarGrupo = editarGrupo;
        $scope.limparCampos = limparCampos;
        $scope.verificaIdGrupoInserirAtualizar = verificaIdGrupoInserirAtualizar;

        $scope.Ativo = [{ tipo: $translate.instant('INACTIVE'), value: "0" }, { tipo: $translate.instant('ACTIVE'), value: "1" }];

        // 2-------------------------Carrega Grupos--------------------------------

        function carregarGrupos(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 6,
                    filter: $scope.filtroGrupos
                }
            };

            apiService.get('/api/grupos/listagrupos', config, loadlGruposSuccess, loadGruposFailed);

        }

        function loadlGruposSuccess(result) {

            $scope.Grupos = result.data.Items;

            notificationService.displaySuccess("(" + $scope.Grupos.length + ")" + " grupos carregados !", "Atenção<br /><br />");

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
        }

        function loadGruposFailed(result) {

            notificationService.displayError('Erro ao carregar lista de grupos !', 'Atenção !\n');
        }
        // 2-----------------------------------------------------------------------



        // 3-------------------------Editar Grupo--------------------------------

        function editarGrupo(grupo) {

            $scope.novoGrupo = grupo;

        };

        // 3---------------------------------------------------------------------



        //4 -------------Função para limpar campos do formulário------------------

        function limparCampos() {

            $scope.novoGrupo = {};

        }

        //4-----------------------------------------------------------------------


        // 5-------------------------Inserir Grupo--------------------------------

        function insenrirGrupo() {

            apiService.post('/api/grupos/inserirgrupo', $scope.novoGrupo, loadInserirSuccess, loadInserirFailed);

        }

        function loadInserirSuccess(result) {

            $scope.novoGrupo = result.data;

            notificationService.displaySuccess('Grupo cadastrado com sucesso!', 'Atenção');


            carregarGrupos();
            limparCampos();
        }

        function loadInserirFailed(result) {

            notificationService.displayError('Erro ao inserir grupo!', 'Atenção');
        }

        // 5-------------------------Fim Inserir Grupo--------------------------------



        // 6-------------------------Alterar Grupo--------------------------------
        
        function alterarGrupo() {

            apiService.post('/api/grupos/alterargrupo', $scope.novoGrupo, loadAlterarSuccess, loadAlterarFailed);
        }

        function loadAlterarSuccess(result) {
            
            $scope.Grupos = result.data;

            notificationService.displaySuccess('Grupo alterado com sucesso!', 'Atenção');
        }

        function loadAlterarFailed(result) {
            notificationService.displayError('Erro ao alterar grupo!', 'Atenção');
            
        }

        // 6----------------------------------------------------------------------


        // 7----------------Verifica o Id do usuário-------------------------------

        function verificaIdGrupoInserirAtualizar(idGrupo) {

            if (!idGrupo > 0) {

                insenrirGrupo();
            }
            else {

                alterarGrupo();
            }

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

        carregaStatus();
        carregarGrupos();

    }

})(angular.module('AppKor'));