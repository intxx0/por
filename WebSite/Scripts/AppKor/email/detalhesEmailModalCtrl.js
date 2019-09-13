(function (app) {
    'use strict';

    app.controller('detalhesEmailModalCtrl', detalhesEmailModalCtrl);

    detalhesEmailModalCtrl.$inject = ['$scope', '$http', '$modalInstance', 'items', 'apiService', 'notificationService'];

    function detalhesEmailModalCtrl($scope, $http, $uibModalInstance, items, apiService, notificationService) {

        $scope.email = {};
        $scope.email = items;
        $scope.cancel = cancel;
        $scope.Guias = [];
        $scope.Anexos = [];
        $scope.Hostname = window.location.hostname + ':62272';

        function loadAnexos(idEmail) {

            apiService.get('/api/email/anexos/' + idEmail, null, loadAnexosSuccess, loadAnexosFailed)

        }

        function loadAnexosSuccess(result) {
            $scope.Anexos = result.data;
        }

        function loadAnexosFailed(result) {
            notificationService.displayError('Erro ao carregar anexos', 'Atenção!');
        }

        function cancel() {
            $uibModalInstance.dismiss();
        };

        loadAnexos(items.CaixaEmailId);

    }

})(angular.module('AppKor'));