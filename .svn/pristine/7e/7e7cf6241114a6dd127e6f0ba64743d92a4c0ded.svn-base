(function (app) {
    'use strict';

    app.controller('modalDetalhesFiscalizacaoCtrl', modalDetalhesFiscalizacaoCtrl);
    modalDetalhesFiscalizacaoCtrl.$inject = ['$scope', '$modalInstance', 'items', 'apiService', 'notificationService', '$translate'];

    function modalDetalhesFiscalizacaoCtrl($scope, $uibModalInstance, items, apiService, notificationService, $translate) {

        $scope.cancel = cancel;

        function cancel() {
            $uibModalInstance.dismiss();
        };

        function loadFiscalizacao(idFiscalizacao) {

            apiService.get('/api/fiscalizacoes/fiscalizacao/' + idFiscalizacao, null,
                function (result) {

                    $scope.Fiscalizacao = result.data;
                },
                function (result) {

                    notificationService.displayError($translate.instant('ERROR_LOAD_INSPECTION') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
                });

        };


        loadFiscalizacao(items);
    }

})(angular.module('AppKor'));