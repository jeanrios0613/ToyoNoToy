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
 

 

function showApprovalModal() {
    $('#approvalModal').modal('show');
}

function showConfirmationModal() {
    $('#confirmationModal').modal('show');
}

function showCancelModal() {
    $('#confirmationModal').modal('show');
}

function submitForm() {
    $('#approvalModal').modal('hide');
    $('#solicitudForm').submit();
}
 