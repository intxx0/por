var app = angular.module('AppKor', ['pascalprecht.translate']);
app.config(function ($translateProvider) {

    $translateProvider.translations('en_US', {
        SUBMIT: 'Submit',
        CLEAR: 'Clear'
    });
    $translateProvider.preferredLanguage('en_US');
});
