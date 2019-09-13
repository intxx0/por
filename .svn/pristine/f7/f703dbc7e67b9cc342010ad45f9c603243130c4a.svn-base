(function (app) {
    'use strict';

    app.controller('cadcargoCtrl', cadcargoCtrl);

    cadcargoCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$translate'];

    function cadcargoCtrl($scope, apiService, notificationService, $translate) {

        $scope.limparCampos = limparCampos;
        $scope.pegaDadosCargo = pegaDadosCargo;
        $scope.verificaInserirAtualizarCargo = verificaInserirAtualizarCargo;
        $scope.excluirCargo = excluirCargo;

        $scope.modelcargo = {};

        function carregarCargo(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filtercargo
                }
            };

            apiService.get('/api/cargo/listacargo', config,
                loadCargoSucess,
                loadCargoFailed);

        }

        function loadCargoSucess(result) {

            $scope.Missoes = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;

        }

        function loadCargoFailed(result) {

            notificationService.displayError($translate.instant('ERROR_LOAD_ROLES') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function verificaInserirAtualizarCargo(novoCargo) {

            if (novoCargo) {
                atualizaCargo();
            } else {
                inserirCargo();
            }

        }

        function inserirCargo() {
            apiService.post('/api/cargo/inserircargo', $scope.modelcargo,
                loadInserirCargoSuccess,
                loadInserirCargoFailed);
        }

        function loadInserirCargoSuccess(result) {

            $scope.modelcargo = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_SAVE_ROLE'), $translate.instant('ATTENTION') + '!');
            limparCampos();
            carregarCargo();

        }

        function loadInserirCargoFailed(result) {
            notificationService.displayError($translate.instant('ERROR_INSERT_ROLE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function pegaDadosCargo(cargo) {
            $scope.modelcargo = cargo;
        }

        function atualizaCargo() {
            apiService.post('/api/cargo/alterarcargo', $scope.modelcargo, loadAtualizaCargoSuccess, loadAtualizaCargoFailed);
        }

        function excluirCargo(cargo) {
            if (confirm($translate.instant('CONFIRM_DELETE')))
                apiService.post('/api/cargo/excluircargo', cargo, loadExcluirCargoSuccess, loadExcluirCargoFailed);
        }

        function loadAtualizaCargoSuccess(result) {

            $scope.modelcargo = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_UPDATE_ROLE'), $translate.instant('ATTENTION') + '!');
            carregarCargo();
        }

        function loadAtualizaCargoFailed(result) {
            notificationService.displayError($translate.instant('ERROR_UPDATE_ROLE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function loadExcluirCargoSuccess(result) {

            $scope.modelcargo = result.data.Items;

            notificationService.displaySuccess($translate.instant('SUCCESS_DELETE_ROLE'), $translate.instant('ATTENTION') + '!');
            carregarCargo();
        }

        function loadExcluirCargoFailed(result) {

            notificationService.displayError($translate.instant('ERROR_DELETE_ROLE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
        }

        function limparCampos() {

            $scope.modelcargo = {};

        }

        carregarCargo();
    }

})(angular.module('AppKor'));