////////////////////////////////
$(document).ready(function () {
    // Handle gestionreal select changes
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

    // Modal event handlers
    $("#closebutton").on("click", function () {
        $("#confirmationModal").modal("show");
    });

    $("#closeModal").on("click", function () {
        $("#confirmationModal").modal("hide");
    });
});

function showConfirmationModal() {
    $('#confirmationModal').modal('show');
}

function showCancelModal() {
    $('#confirmationModal').modal('show');
}

function submitForm() {
    $('#approvalModal').modal('hide');
    $('form').submit();
}
 