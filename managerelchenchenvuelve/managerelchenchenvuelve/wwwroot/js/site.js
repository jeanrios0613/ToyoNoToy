////////////////////////////////
$(document).ready(function () {


    $("#gestionreal").change(function () {
          var seleccion = $(this).val();

        if (seleccion == "Atendido") {
            $("#atencion").show();
            $("#razon").hide();
        } else if (seleccion == "Llamada realizada sin exito") {
            $("#atencion").hide();
            $("#razon").show();

        } else {
            $("#atencion").hide();
            $("#razon").hide();

        }
    }).change();

});


document.getElementById("closebutton").addEventListener("click", function () {
  $("#confirmationModal ").modal("show");
});


document.getElementById("closeModal").addEventListener("click", function () {
    $("#confirmationModal").modal("hide");
});
 
 