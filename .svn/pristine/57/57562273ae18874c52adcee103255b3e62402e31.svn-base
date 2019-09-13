(function (app) {
    'use strict';

    app.controller('cadkorCtrl', cadkorCtrl);

    cadkorCtrl.$inject = ['$scope', 'apiService', 'notificationService'];

    function cadkorCtrl($scope, apiService, notificationService) {

        $scope.limparCampos = limparCampos;
        $scope.pegaDadosKor = pegaDadosKor;
        $scope.verificaInserirAtualizarKor = verificaInserirAtualizarKor;
        $scope.excluirKor = excluirKor;

        $scope.novokor = {};


        //1---------------Inserir Kor-------------------------------------

        function carregarKor(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterkor
                }
            };

            apiService.get('/api/kor/listakor', config,
                loadkorSucess,
                loadKorFailed);

        }

        function loadkorSucess(result) {

            $scope.Kors = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;

        }

        function loadKorFailed(result) {

            notificationService.displayError($translate.instant('ERROR_LOAD_KORS') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        //1---------------------------------------------------------------


        //2---------------Verifica Inserir ou Atualizar------------------
        function verificaInserirAtualizarKor(novoKor) {

            if (novoKor) {

                atualizaKor();

            } else {

                inserirKor();
            }

        }

        //2--------------------------------------------------------------


        //3--------------Inseri Kor--------------------------------------

        function inserirKor() {

         
            apiService.post('/api/kor/inserirkor', $scope.novokor,
                loadInserirKorSuccess,
                loadInserirKorFailed);
        }

        function loadInserirKorSuccess(result) {

            $scope.novokor = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_SAVE_KOR'), $translate.instant('ATTENTION') + '!');
            limparCampos();
            carregarKor();

        }

        function loadInserirKorFailed(result) {

            notificationService.displayError($translate.instant('ERROR_INSERT_KOR') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        //3--------------------------------------------------------------


        //4--------------Editar Kor--------------------------------------

        function pegaDadosKor(kor) {
            $scope.novokor = kor;
        }


        function atualizaKor() {
            apiService.post('/api/kor/alterarkor', $scope.novokor, loadAtualizaKorSuccess, loadAtualizaKorFailed);
        }

        function excluirKor(kor) {
            if (confirm($translate.instant('CONFIRM_DELETE')))
                apiService.post('/api/kor/excluirkor', kor, loadExcluirKorSuccess, loadExcluirKorFailed);
        }

        function loadAtualizaKorSuccess(result) {

            $scope.novokor = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_UPDATE_KOR'), $translate.instant('ATTENTION') + '!');
            carregarKor();
        }

        function loadAtualizaKorFailed(result) {
         
            notificationService.displayError($translate.instant('ERROR_UPDATE_KOR') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function loadExcluirKorSuccess(result) {

            $scope.novokor = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_DELETE_KOR'), $translate.instant('ATTENTION') + '!');
            carregarKor();
        }

        function loadExcluirKorFailed(result) {

            notificationService.displayError($translate.instant('ERROR_DELETE_KOR') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }


        //4--------------------------------------------------------------





        //---------------------Limpa Campos---------------------------------
        function limparCampos() {

            $scope.novokor = {};

        }
        //------------------------------------------------------------------


        carregarKor();
    }



})(angular.module('AppKor'));