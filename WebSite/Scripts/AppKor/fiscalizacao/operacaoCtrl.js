(function (app) {
    'use strict';

    app.controller('operacaoCtrl', operacaoCtrl);

    operacaoCtrl.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', '$translate', '$modal', '$location'];

    function operacaoCtrl($scope, $rootScope, apiService, notificationService, $translate, $modal, $location) {

        $scope.doSearch = doSearch;
        
        $scope.Usuarios = [];
        $scope.Kor = [];
        $scope.TiposFiscalizacao = [];
        $scope.Operacoes = [];

        $scope.loadAtividades = listarAtividades;
        $scope.loadKor = listarKors;
        $scope.loadProfissionais = listarProfissionais;
        $scope.formatDate = formatDate;

        $scope.modelOperacao = {};
        $rootScope.FiltroOperacao = {};


        $('.datepicker').datepicker({
            format: 'dd/MM/yyyy'
        });

        function getFormattedDate(date) {
            date = new Date(date);

            var year = date.getFullYear();

            var month = (1 + date.getMonth()).toString();
            month = month.length > 1 ? month : '0' + month;

            var day = date.getDate().toString();
            day = day.length > 1 ? day : '0' + day;

            return day + '/' + month + '/' + year;
        }

        function formatDate(modelOp, date) {

            switch (date) {
                case "inicio":
                    modelOp.DataInicio = getFormattedDate(modelOp.DataInicio);
                    break;
                case "fim":
                    modelOp.DataTermino = getFormattedDate(modelOp.DataTermino);
                    break;
            }
        }

        function doSearch(modelOp, rota) {

            if (validaFiltro(modelOp)) {

                var newItem = new function () {
                    this.DescNome = $scope.Usuarios.find(u => u.UsuarioId == modelOp.IdUsuario).DescNome;
                    this.NomeKor = $scope.Kor.find(u => u.IdKor == modelOp.IdKor).Nome;
                    this.DescAtividade = $scope.TiposFiscalizacao.find(u => u.IdTipoFiscalizacao == modelOp.IdTipoFiscalizacao).DescTipoFiscalizacao;
                    this.DescOperacao = $scope.Operacoes.find(u => u.IdOperacao == modelOp.IdOperacao).DescOperacao;
                }

                $rootScope.FiltroOperacao = modelOp;
                $rootScope.FiltroOperacao.Items = newItem;

                $rootScope.FiltroOperacao = JSON.stringify($rootScope.FiltroOperacao);

                $location.path('/' + rota);
            }
        }

        function validaFiltro(modelOp) {

            if (modelOp.IdOperacao == undefined) {
                notificationService.displayWarning($translate.instant('ENTER_OPERATION'));
                return false;
            }
            if (modelOp.IdTipoFiscalizacao == undefined) {
                notificationService.displayWarning($translate.instant('ENTER_ACTIVITY'));
                return false;
            }

            if (modelOp.DataInicio == undefined) {
                notificationService.displayWarning($translate.instant('ENTER_START_DATE'));
                return false;
            }

            if (modelOp.DataTermino == undefined) {
                notificationService.displayWarning($translate.instant('ENTER_END_DATE'));
                return false;
            }

            var d1 = modelOp.DataInicio.split("/");
            var date1 = new Date(d1[2], d1[1] - 1, d1[0]);

            var d2 = modelOp.DataTermino.split("/");
            var date2 = new Date(d2[2], d2[1] - 1, d2[0]);

            if (date1 > date2) {
                notificationService.displayWarning($translate.instant('START_DATE_GT_END_DATE'));
                return false;
            }

            return true;
        }


        function listarOperacoes() {

            apiService.get('/api/operacoes/listaoperacao', null,
                function (result) {
                    $scope.Operacoes = result.data.Items;
                    $scope.TotalOperacoes = result.data.Count;

                    forEach($scope.Operacoes, function (value, key) {
                        var descStatus = "";

                        if ($scope.Operacoes[key].Ativo == 0)
                            descStatus = "Ativo";
                        else 
                            descStatus = $scope.Operacoes[key].Ativo == 1 ? "Aguardando" : "Inativo";

                        $scope.Operacoes[key].DescStatus = descStatus;
                    });
                },
                function (result) {
                    notificationService.displayError($translate.instant('ERROR_LOAD_INSPECTIONS') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
                });
        }

        function listarAtividades(IdOperacao) {

            apiService.get('/api/tipofiscalizacao/listaatividades/' + IdOperacao , null,
                (result) => {
                    $scope.TiposFiscalizacao = result.data;
                },
                (result) => {
                    console.log(result.data);
                    notificationService.displayError($translate.instant('ERROR_LOAD_INSPECTIONS') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
                });
        }

        function listarKors(IdAtividade) {

            apiService.get('/api/kor/listakorbyativadade/' + IdAtividade, null,
                (result) => {
                    var newItem = new function () {
                        this.IdKor = undefined;
                        this.Nome = $translate.instant('ALL');
                    };
                    var allUsers = new function () {
                        this.UsuarioId = undefined;
                        this.DescNome = $translate.instant('ALL');
                    };

                    $scope.Usuarios.push(allUsers);
                    result.data.push(newItem);
                    $scope.Kor = result.data;
                },
                (result) => {
                    notificationService.displayError($translate.instant('ERROR_LOAD_KORS') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
                });
        }

        function listarProfissionais(IdAtividade, IdKor) {

            var newItem = new function () {
                this.UsuarioId = undefined;
                this.DescNome = $translate.instant('ALL');
            };

            if (IdKor != undefined) {
                apiService.get('/api/usuarios/listausuariosfiltro/' + IdAtividade + '/' + IdKor, null,
                    (result) => {
                        result.data.push(newItem);
                        $scope.Usuarios = result.data;
                    },
                    (result) => {
                        notificationService.displayError($translate.instant('ERROR_LOADING_USERS') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
                    });
            }
            else
                $scope.Usuarios = [];
                $scope.Usuarios.push(newItem);
        }
        

        function clearFields() {
            $scope.Operacao = [];
            $scope.modelOperacao = {}; 
        }


        clearFields();
        listarOperacoes();
    }

})(angular.module('AppKor'));