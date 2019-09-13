(function (app) {
    'use strict';

    app.controller('dashboardCtrl', dashboardCtrl);

    dashboardCtrl.$inject = ['$scope', '$http', 'notificationService', 'apiService', '$routeParams', '$filter'];

    function dashboardCtrl($scope, $http, notificationService, apiService, $routeParams, $filter) {

        var idFiscalizacao = $routeParams.id;
        $scope.morrisdataOrigem = [];
        $scope.morrisdata = [];

        //1-------------------Carrega gráfico de autuação por fiscalização----------------

        function carregaFiscalizacaoSemDocumentacao() {
            apiService.get('/api/dashboard/fiscalizacaosemdocumentacao/' + idFiscalizacao, null,
                loadFiscalizacaoSemDocumentacaoSuccess,
                loadFiscalizacaoSemDocumentacaoFailed);
        }

        function loadFiscalizacaoSemDocumentacaoSuccess(result) {
            $scope.teste = [];
            var fsd = result.data;
            for (var j = 0; j < fsd.length; j++) {
                $scope.morrisdata.push(
                  {
                      y: fsd[j].DataFiscalizacao, a: fsd[j].QtdFiscalizacoes
                  });
            }
        }

        function loadFiscalizacaoSemDocumentacaoFailed(result) {
            notificationService.displayError('Erro ao carregar gráfico fiscalização sem documentação');
        }

        //1-------------------------------------------------------------------------------------

        //2-------------------Carrega gráfico de autuação por tipo de fiscalização----------------

        function carregaFiscalizacaoTipoAutuacao() {
            apiService.get('/api/dashboard/fiscalizacaotipoautuacoes/' + idFiscalizacao, null,
                loadFiscalizacaoTipoAutuacaoSuccess,
                loadFiscalizacaoTipoAutuacaoFailed);
        }

        function loadFiscalizacaoTipoAutuacaoSuccess(result) {
            var fsd = result.data;
            var pier = [];
            for (var i = 0; i < fsd.length; i++) {
                pier.push({ "key": fsd[i].StatusFiscalizacao, "y": fsd[i].QtdFiscalizacoes });
            }
            /* Chart PieChart options */
            $scope.opcoes = {
                chart: {
                    type: 'pieChart',
                    height: 650,
                    x: function (d) { return d.key; },
                    y: function (d) { return d.y; },
                    showLabels: true,
                    duration: 50000,
                    labelThreshold: 0.1,
                    labelSunbeamLayout: true,
                    legend: {
                        margin: {
                            top: 5,
                            right: 0,
                            bottom: 0,
                            left: 0
                        }
                    },
                    labelType: 'percent',
                    valueFormat: function (d) {
                        return d3.format('')(d);
                    }
                }
            };
            /* Chart data */
            $scope.dados = pier;
        }

        function loadFiscalizacaoTipoAutuacaoFailed(result) {
            notificationService.displayError('Erro ao carregar gráfico fiscalização por tipo de autuação');
        }

        //2-------------------------------------------------------------------------------------

        //3-------------Carregar gráfico por tipo de materia prima mais autuada--------------

        function carregaFiscalizacaoTipoMateriaPrima() {
            apiService.get('/api/dashboard/fiscalizacaotipomateriaprima/' + idFiscalizacao, null,
                loadFiscalizacaoTipoMateriaPrimaSuccess,
                loadFiscalizacaoTipoMateriaPrimaFailed);
        }

        function loadFiscalizacaoTipoMateriaPrimaSuccess(result) {
            var fsd = result.data;
            var dados = [];
            for (var i = 0; i < fsd.length; i++) {
                dados.push({ "label": fsd[i].DescMateriaPrima, "value": fsd[i].QtdAutuacoesMateriaPrima });
            }
            /* Chart discreteBarChart options */
            $scope.options = {
                chart: {
                    type: 'discreteBarChart',
                    height: 450,
                    margin: {
                        top: 20,
                        right: 20,
                        bottom: 50,
                        left: 55
                    },
                    x: function (d) { return d.label; },
                    y: function (d) { return d.value; },
                    showValues: true,
                    labelType: '',
                    valueFormat: function (d) {
                        return d3.format('0')(d);
                    },
                    duration: 1000,
                    xAxis: {
                        axisLabelDistance: 10
                    },
                    yAxis: {

                        axisLabelDistance: -10
                    }
                }
            };
            /* Chart data */
            $scope.data = [
                {
                    key: "Cumulative Return",
                    values: dados
                }
            ];
        }

        function loadFiscalizacaoTipoMateriaPrimaFailed(result) {
            notificationService.displayError('Erro ao carregar gráfico fiscalização por tipo de matéria prima');
        }

        //3-----------------------------------------------------------------------------------

        //4---------------carrega Fiscalizações autuadas por origem---------------------------

        function carregaFiscalizacoesAutuadasPorOrigem() {
            apiService.get('/api/dashboard/fiscalizacaoautuadasorigem/' + idFiscalizacao, null,
                loadFiscalizacoesAutuadasPorOrigemSuccess,
                loadFiscalizacoesAutuadasPorOrigemFailed);
        }

        function loadFiscalizacoesAutuadasPorOrigemSuccess(result) {
            var FpO = result.data;
            for (var j = 0; j < FpO.length; j++) {
                $scope.morrisdataOrigem.push(
                  {
                      "y": FpO[j].DescAutudasOrigem, "a": FpO[j].QtdAutuacoesOrigem
                  });
            }
        }

        function loadFiscalizacoesAutuadasPorOrigemFailed(result) {
            notificationService.displayError('Erro ao carregar gráfico de fiscalizações por nome da origem origem!');
        }
        //4-----------------------------------------------------------------------------------

        function pegaIdOperacao() {
            apiService.get('/api/dashboard/pegaIdOperacao/' + idFiscalizacao, null,
                loadIdOperacaoSuccess,
                loadIdOperacaoFailed);
            function loadIdOperacaoSuccess(result) {
                console.log(result);
                $scope.IdNatureza = result.data.IdNaturezaOperacao;
                $scope.DescOperacao = result.data.DescnaturezaOperacao;
                $scope.InstituicaoResp = result.data.InstituicaoResp;
                $scope.DataCriado = result.data.DataCriado;
                $scope.DataFinalOperacao = result.data.DataFinalOperacao;
            }
            function loadIdOperacaoFailed(result) {
                notificationService.displayError('Erro ao pegar Id natureza da operação!');
            }
        }

        carregaFiscalizacaoSemDocumentacao();
        carregaFiscalizacaoTipoAutuacao();
        carregaFiscalizacaoTipoMateriaPrima();
        carregaFiscalizacoesAutuadasPorOrigem();

        pegaIdOperacao();
    }

})(angular.module('AppKor'));