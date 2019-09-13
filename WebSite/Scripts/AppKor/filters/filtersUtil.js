(function (app) {
   
     app.filter('filterDataHora', function() {

         return function (input) {

             if (input) {
                 
             var mascaradata = '';

             var dia = 0;
             var mes = 0;
             var ano = 0;
             var hora = 0;
             var minuto = 0;


             dia = input.substring(0, 2);
             mes = input.substring(2, 4);
             ano = input.substring(4, 8);
             hora = input.substring(8, 10);
             minuto = input.substring(10, 12);


             mascaradata = dia + "/" + mes + "/" + ano + " " + hora + ":" + minuto;

             return mascaradata;

             }

             return null;
         }
        
     });


     app.filter('filterTelefone', function () {

         return function (input) {

             if (input) {

                 var mascaradata = '';

                 var ddd = 0;
                 var prefixo = 0;
                 var sufixo = 0;
                 

                 ddd = input.substring(0, 2);
                 prefixo = input.substring(2, 7);
                 sufixo = input.substring(7);
              


                 mascaradata ="("+ ddd + ")" + prefixo + "-" + sufixo;

                 return mascaradata;

             }

             return null;
         }

     });
 

  
})(angular.module('AppKor'));
