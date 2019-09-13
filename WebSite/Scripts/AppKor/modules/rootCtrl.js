(function (app) {
    'use strict';

    app.controller('rootCtrl', rootCtrl);

    rootCtrl.$inject = ['$scope', '$location', 'membershipService', '$rootScope', '$http'];

    function rootCtrl($scope, $location, membershipService, $rootScope, $http) {
        //$scope.translateLanguage = 'pt_BR';

        $scope.setLocalLang = function (langKey) {
            $scope.translateLanguage = langKey;
        }
        $scope.setLang = function (langKey) {
            $translate.use(langKey);
        };

        $http.get('/', {
            cache: false
        });

        $scope.userData = {};
        $scope.userData.displayUserInfo = displayUserInfo;

        $scope.logout = logout;


        function displayUserInfo() {
            $rootScope.flag = membershipService.isUserLoggedIn();
            $scope.userData.isUserLoggedIn = $rootScope.flag;
        }

        function logout() {
            $rootScope.repository = {};
            membershipService.removeCredentials();
            $location.path("/login");
            $scope.userData.displayUserInfo();
        }

        $scope.userData.displayUserInfo();


        $scope.$on('$stateChangeStart', function () {
            // reset layout setting
            $rootScope.setting.layout.pageSidebarMinified = false;
            $rootScope.setting.layout.pageFixedFooter = false;
            $rootScope.setting.layout.pageRightSidebar = false;
            $rootScope.setting.layout.pageTwoSidebar = false;
            $rootScope.setting.layout.pageTopMenu = false;
            $rootScope.setting.layout.pageBoxedLayout = false;
            $rootScope.setting.layout.pageWithoutSidebar = false;
            $rootScope.setting.layout.pageContentFullHeight = false;
            $rootScope.setting.layout.pageContentFullWidth = false;
            $rootScope.setting.layout.paceTop = false;
            $rootScope.setting.layout.pageLanguageBar = false;
            $rootScope.setting.layout.pageSidebarTransparent = false;
            $rootScope.setting.layout.pageWideSidebar = false;
            $rootScope.setting.layout.pageLightSidebar = false;
            $rootScope.setting.layout.pageFooter = false;
            $rootScope.setting.layout.pageMegaMenu = false;
            $rootScope.setting.layout.pageWithoutHeader = false;
            $rootScope.setting.layout.pageBgWhite = false;
            $rootScope.setting.layout.pageContentInverseMode = false;
        });
        $scope.$on('$locationChangeSuccess', function () {
            if ($location.path() == '/' && !localStorage.getItem('onLoadEvents')) {
                handleSlimScroll();
                App.initSidebar();
                localStorage.setItem('onLoadEvents', true);
            }
        });
    }

})(angular.module('AppKor'));