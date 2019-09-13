(function (app) {
    'use strict';

    app.controller('chatmensagensCtrl', chatmensagensCtrl);

    chatmensagensCtrl.$inject = ['$scope', 'apiService', 'notificationService'];

    function chatmensagensCtrl($scope, apiService, notificationService) {




        $(".mytext").on("keyup", function (e) {
            if (e.which == 13) {
                var text = $(this).val();
                if (text !== "") {
                    insertChat("me", text);
                    $(this).val('');
                }
            }
        });

    }
})(angular.module('AppKor'));