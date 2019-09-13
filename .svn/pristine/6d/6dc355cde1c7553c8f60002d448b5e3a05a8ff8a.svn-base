(function (app) {
    'use strict';

    app.controller('fichadigitalCtrl', fichadigitalCtrl);
    fichadigitalCtrl.$inject = ['$scope', '$modalInstance', 'items', 'apiService', 'notificationService', '$translate'];

    function fichadigitalCtrl($scope, $uibModalInstance, items, apiService, notificationService, $translate) {

        $scope.cancel = cancel;

        function cancel() {
            $uibModalInstance.dismiss();
        };

        function loadFichaDigital(idFiscalizacao) {

            apiService.get('/api/fiscalizacoes/fiscalizacao/' + idFiscalizacao, null,
                function (result) {
                    $scope.NumeroAutuacao = result.data.NumeroAutuacao;
                },
                function (result) {
                    notificationService.displayError($translate.instant('ERROR_LOAD_INSPECTION') + '. <br />' + $translate.instant('ERROR') + ': ' + result.status, $translate.instant('ATTENTION') + '!');
                });

        };

        loadFichaDigital(items);

    }

})(angular.module('AppKor'));