///*
//Template Name: Color Admin - Responsive Admin Dashboard Template build with Twitter Bootstrap 3.3.6
//Version: 2.0.0
//Author: Sean Ngu
//Website: http://www.seantheme.com/color-admin-v2.0/admin/html/
//*/var handleGoogleMapSetting=function() {
//    "use strict";


//    function t() {
//        var t = {
//            zoom: 4, center: new google.maps.LatLng(-12.0444388, -60.8345666),
//            mapTypeId: google.maps.MapTypeId.ROADMAP, disableDefaultUI: true
//        };

//        e = new google.maps.Map(document.getElementById("google-map-default"), t)
//    }

//    var e;

//    google.maps.event.addDomListener(window, "load", t);

//    //$(window).resize(function () { google.maps.event.trigger(e, "resize") });

//};



//var MapGoogle = function() {
//    "use strict";
//    return {
//        init: function ()
//        {
//            handleGoogleMapSetting() 

//        }
//    }
//}()


function initMap() {


    var imagemVerde = "/Content/images/marcadorVerdeMaps.png"

    var ponto = { lat: -5.0622854, lng: -67.5981752 };

    var ponto2 = { lat: -4.0622854, lng: -67.5881752 };

    var ponto3 = { lat: -3.0622854, lng: -57.5881752 };

    var ponto4 = { lat: -2.0622854, lng: -57.5881752 };

    // Create a map object and specify the DOM element for display.
    var map = new google.maps.Map(document.getElementById('google-map-default'), {
        center: ponto3,
        scrollwheel: false,
        zoom:4
    });

    var marker = new google.maps.Marker({
        map: map,
        position: ponto,
        title: 'Maleta 01',
        icon: imagemVerde
    });

    var marker = new google.maps.Marker({
        map: map,
        position: ponto2,
        title: 'Maleta 02'
    });

    var marker = new google.maps.Marker({
        map: map,
        position: ponto3,
        title: 'Maleta 03',
        icon: imagemVerde
    });

    var marker = new google.maps.Marker({
        map: map,
        position: ponto4,
        title: 'Maleta 03'
    });
}

var MapGoogle = function () {
    "use strict";
    return {
        init: function () {
            initMap()

        }
    }
}()

