(function (app) {
    'use strict';

    app.controller('detalhesModalCtrl', detalhesModalCtrl);

    detalhesModalCtrl.$inject = ['$scope', '$http', '$modalInstance', 'items', 'apiService', 'notificationService'];

    function detalhesModalCtrl($scope, $http, $uibModalInstance, items, apiService, notificationService) {

        $scope.guia = items;
        $scope.cancel = cancel;

        $scope.Guias = [];
        


       function carregarDetalhesFiscalizacao() {

            apiService.get('/api/guia/listadetalhesguia/' + items, null,
                loadDetalhesFiscalizacaoSuccess,
                loadDetalhesFiscalizacaoError);
       }


        function loadDetalhesFiscalizacaoSuccess(result) {
            
            $scope.Guias = result.data.Items;
        }


        function loadDetalhesFiscalizacaoError(result) {

            if (result.status == 404) {

                // toastr["error"]("Erro ao carregar detalhes da guia!", "Atenção !<br /><br />");

                $scope.alerta = true;
            }
            else {

                notificationService.displayError("Erro ao carregar detalhes da nota fiscal!", "Atenção !<br /><br />");

            }

        }




        //----------------Fechar Modal------------------------------
        function cancel() {

            $uibModalInstance.dismiss();
        };
        //----------------Fim Modal----------------------------------



        carregarDetalhesFiscalizacao();

    }

})(angular.module('AppKor'));