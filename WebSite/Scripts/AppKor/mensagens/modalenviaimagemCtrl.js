(function (app) {
    'use strict';

    app.controller('modalenviaimagemCtrl', modalenviaimagemCtrl);

    modalenviaimagemCtrl.$inject = ['$scope', '$http', '$modalInstance', 'items', 'apiService', 'notificationService', '$rootScope', '$translate', '$location', 'md5'];

    function modalenviaimagemCtrl($scope, $http, $uibModalInstance, items, apiService, notificationService, $rootScope, $translate, $location, md5) {

        $scope.cancel = cancel;
        $scope.enviar = enviar;
        $scope.remove = remove;
        $scope.imagesrc = [];
        $scope.imgAtual = {};
        var fileReader = new FileReader();

        if (items == undefined)
            $rootScope.imgsrc = [];
        else {
            $rootScope.imgsrc = [];
            $rootScope.imgsrc = items;
        }


        function remove(callback, id) {

            for (var i = 0; i < $rootScope.imgsrc.length; i++) {
                if (id == $rootScope.imgsrc[i].nome) {

                    $rootScope.imgsrc.splice(i, 1);
                }
            }

            callback;
        }

        function enviar(imagem) {

            var image = imagem.file;

            fileReader.readAsDataURL(image);


            fileReader.onload = function (event) {
                var flag = false;

                var base64 = event.target.result + "";
                base64 = base64.split("base64,")[1];

                for (var i = 0; i < $scope.imagesrc.length; i++) {
                    if (image.name == $rootScope.imgsrc[i].nome) {
                        flag = true;
                    }
                }

                if (!flag) {
                    var ext = "." + (image.name).split(".")[1];

                    $scope.imgAtual.NomeImagem = md5.createHash(event.timeStamp.toString()) + ext;
                    $scope.imgAtual.ImagemBase64 = base64;


                    apiService.post('/api/imagens/comprimirimagens', JSON.stringify($scope.imgAtual),
                        comprimirImagemSucesso,
                        comprimirImagemFailed);

                    $scope.imgAtual.name = image.name;

                    console.log($scope.imgAtual);
                }
                else 
                    notificationService.displayWarning($translate.instant('ERROR_LOAD_DUPLICATE_IMG'));
            }
        };
        var cont = 0;

        if (items == undefined)
            cont = 0;
        else
            cont = items.length;

        function comprimirImagemSucesso(result) {

            $scope.imagesrc[cont] = {
                "nome": $scope.imgAtual.name,
                "nomeArq": result.data.nome,
                "base64": result.data.img_base64,
            }


            if (items != undefined) {
                $rootScope.imgsrc = items;
                $rootScope.imgsrc.push($scope.imagesrc[cont]);
            }
            else
                $rootScope.imgsrc = $scope.imagesrc;

            cont++;

            notificationService.displaySuccess($translate.instant('SUCCESS_LOAD_IMG') + " " + $scope.imgAtual.name);
        }

        function comprimirImagemFailed(result) {

            notificationService.displayError($translate.instant('ERROR_LOAD_IMG') + '!', result.status);
        }
        //----------------Fechar Modal------------------------------
        function cancel() {


            $uibModalInstance.dismiss();
        };
        //----------------Fim Modal----------------------------------
    }

})(angular.module('AppKor'));