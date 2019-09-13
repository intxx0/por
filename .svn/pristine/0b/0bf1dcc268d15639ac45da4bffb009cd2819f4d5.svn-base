(function (app) {
    'use strict';

    app.controller('fiscalizacaoCtrl', fiscalizacaoCtrl);

    fiscalizacaoCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$translate', '$modal'];

    function fiscalizacaoCtrl($scope, apiService, notificationService, $translate, $modal) {

        $scope.clearFields = clearFields;
        $scope.doSearch = doSearch;
        $scope.loadFiscalizacao = loadFiscalizacao;
        $scope.loadFichaDigital = loadFichaDigital;

        $scope.NaturezaOperacao = [];
        $scope.TiposFiscalizacao = [];
        $scope.Kor = [];
        $scope.TiposFiscalizacao = [];
        $scope.SubTiposFiscalizacao = [];
        $scope.Fiscalizacoes = [];
        $scope.TotalFiscalizacoes = 0;

        $scope.modelFiscalizacao = {};

        var config = {
            params: {
                page: 0,
                pageSize: 10,
                filter: null
            }
        };

        function loadNaturezaOperacao(page) {

            apiService.get('/api/naturezaoperacao/listanaturezaoperacao', config,
                function (result) {
                    var newItem = new function () {
                        this.IdNaturezaOperacao = undefined;
                        this.DescnaturezaOperacao = $translate.instant('ALL');
                    };
                    result.data.Items.push(newItem);
                    $scope.NaturezaOperacao = result.data.Items;
                },
                function (result) {
                    notificationService.displayError($translate.instant('ERROR_LOAD_NATURE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
                });

        }

        function loadKor() {

            apiService.get('/api/kor/listakor', config,
                function (result) {
                    var newItem = new function () {
                        this.IdKor = undefined;
                        this.Nome = $translate.instant('ALL');
                    };
                    result.data.Items.push(newItem);
                    $scope.Kor = result.data.Items;
                },
                function (result) {
                    notificationService.displayError($translate.instant('ERROR_LOAD_MISSIONS') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
                });

        }

        function loadOperacao() {

            apiService.get('/api/operacoes/listaoperacao', config,
                function (result) {
                    var newItem = new function () {
                        this.IdOperacao = undefined;
                        this.DescOperacao = $translate.instant('ALL');
                    };
                    result.data.Items.push(newItem);
                    $scope.Operacao = result.data.Items;
                },
                function (result) {
                    notificationService.displayError($translate.instant('ERROR_LOAD_OPERATIONS') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
                });

        }

        function loadInstituicao() {

            apiService.get('/api/instituicao/listainstituicao', config,
                function (result) {
                    var newItem = new function () {
                        this.IdInstituicao = undefined;
                        this.NomeInstituicao = $translate.instant('ALL');
                    };
                    result.data.Items.push(newItem);
                    $scope.Instituicao = result.data.Items;
                },
                function (result) {
                    notificationService.displayError($translate.instant('ERROR_LOAD_OPERATIONS') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
                });

        }

        function loadTipoFiscalizacao() {

            apiService.get('/api/tipofiscalizacao/listatipofiscalizacao', config,
                function (result) {
                    var newItem = new function () {
                        this.IdTipoFiscalizacao = undefined;
                        this.DescTipoFiscalizacao = $translate.instant('ALL');
                    };
                    result.data.Items.push(newItem);
                    $scope.TiposFiscalizacao = result.data.Items;
                },
                function (result) {
                    notificationService.displayError($translate.instant('ERROR_LOAD_INSPECTION_TYPE') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
                });

        }

        function loadSubTipoFiscalizacao() {

            apiService.get('/api/subtipofiscalizacao/listasubtipofiscalizacao', config,
                function (result) {
                    var newItem = new function () {
                        this.IdSubTipoFiscalizacao = undefined;
                        this.DescSubTipoFiscalizacao = $translate.instant('ALL');
                    };
                    result.data.Items.push(newItem);
                    $scope.SubTiposFiscalizacao = result.data.Items;
                },
                function (result) {
                    notificationService.displayError($translate.instant('ERROR_LOAD_INSPECTION_SUBTYPES') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
                });

        }

        function doSearch(modelFiscalizacao) {

            var newModel = angular.copy(modelFiscalizacao);

            if (newModel.DataInicio != null && newModel.DataTermino != null) {
                var date1 = angular.copy(newModel.DataInicio);
                var date2 = angular.copy(newModel.DataTermino);
                newModel.DataInicio = date1.substring(4, 8) + date1.substring(2, 4) + date1.substring(0, 2);
                newModel.DataTermino = date2.substring(4, 8) + date2.substring(2, 4) + date2.substring(0, 2);
            }

            apiService.post('/api/fiscalizacoes/listafiscalizacao', newModel,
                function (result) {
                    $scope.Fiscalizacoes = result.data;
                    $scope.TotalFiscalizacoes = result.data.length;
                },
                function (result) {
                    notificationService.displayError($translate.instant('ERROR_LOAD_INSPECTIONS') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
                });

        }

        function loadFiscalizacao(idFiscalizacao) {

            $modal.open({
                animation: true,
                templateUrl: 'scripts/AppKor/fiscalizacao/detalhefiscalizacao.html',
                controller: 'detalhefiscalizacaoCtrl',
                size: 'lg',
                resolve: {
                    items: function () {
                        return idFiscalizacao;
                    }
                }
            });

        }

        function loadFichaDigital(idFiscalizacao) {

            $modal.open({
                animation: true,
                templateUrl: 'scripts/AppKor/fiscalizacao/fichadigital.html',
                controller: 'fichadigitalCtrl',
                size: 'lg',
                resolve: {
                    items: function () {
                        return idFiscalizacao;
                    }
                }
            });

        }

        function clearFields() {
            $scope.modelFiscalizacao = {};
        }

        loadNaturezaOperacao();
        loadKor();
        loadOperacao();
        loadInstituicao();
        loadTipoFiscalizacao();
        loadSubTipoFiscalizacao();
    }

})(angular.module('AppKor'));