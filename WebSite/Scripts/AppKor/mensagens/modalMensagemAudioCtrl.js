(function (app) {
    'use strict';

    app.controller('modalMensagemAudioCtrl', modalMensagemAudioCtrl);

    modalMensagemAudioCtrl.$inject = ['$scope', '$modalInstance', 'items', 'apiService', 'notificationService'];

    function modalMensagemAudioCtrl($scope, $uibModalInstance, items, apiService, notificationService) {

        $scope.cancel = cancel;







        //----------------Fechar Modal------------------------------
        function cancel() {
            $uibModalInstance.dismiss();
        };
        //----------------Fim Modal----------------------------------



    }

})(angular.module('AppKor'));