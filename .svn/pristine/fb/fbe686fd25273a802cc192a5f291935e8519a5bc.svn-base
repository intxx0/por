(function (app) {
    'use strict';

    app.controller('detalhefiscalizacaoCtrl', detalhefiscalizacaoCtrl);
    detalhefiscalizacaoCtrl.$inject = ['$scope', '$modalInstance', 'items', 'apiService', 'notificationService', '$translate'];

    function detalhefiscalizacaoCtrl($scope, $uibModalInstance, items, apiService, notificationService, $translate) {

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

        console.log(items);

        loadFiscalizacao(items);

        

    }

})(angular.module('AppKor'));