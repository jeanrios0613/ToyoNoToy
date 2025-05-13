////////////////////////////////
$(document).ready(function () {
    $("#TareaUser").change(function () {

        var seleccion = $(this).val();

        if (seleccion == "C") {
            $("#TareaC").show();
            $("#TareaP").hide();
            $("#TareaA").hide();
            $("duraTareaP").hide();
            $("bandejaTareaP").hide();
             
        } else if (seleccion == "P") {
            $("#TareaC").hide();
            $("#TareaP").show();
            $("#TareaA").hide();
            $("duraTareaP").show();
            $("bandejaTareaP").show();
        } else {
            $("#TareaC").hide();
            $("#TareaP").hide();
            $("#TareaA").show();
            $("duraTareaP").show();
            $("bandejaTareaP").show();
        }

    }).change();

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



function submitComment() {
    var commentText = $('#comentarioCambio').val();
    var requestCode = '@Model.Code';
    var gestor      = '@Model.Gestor';

    $.ajax({
        url: '@Url.Action("AddComment", "Requests")',
        type: 'POST',
        data: {
            requestCode: requestCode,
            commentText: commentText,
            gestor: gestor
        },
        headers: {
            'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
        },
        success: function (response) {
            if (response.success) {
                $('#confirmacionCambioModal').modal('hide');
                // Optionally show a success message
                alert('Comentario agregado exitosamente');
            } else {
                alert(response.message || 'Error al agregar el comentario');
            }
        },
        error: function () {
            alert('Error al procesar la solicitud');
        }
    });
}


function buscarFormulario() {
    var buscaform = document.getElementById('buscaform').value;  
    if (buscaform || nicruc) { 
         window.location.href = '../Process/Index?search=' + buscaform;
    } else {
        window.location.href = '../Process/Index?';
    }
}