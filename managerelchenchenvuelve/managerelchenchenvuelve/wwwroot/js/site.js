////////////////////////////////
$(document).ready(function () {
    $("#TareaUser").change(function () {

        var seleccion = $(this).val();

        if (seleccion == "C") {
            $("#TareaC").show();
            $("#TareaP").hide();
            $("#TareaA").hide();
        
             
        } else if (seleccion == "P") {
            $("#TareaC").hide();
            $("#TareaP").show();
            $("#TareaA").hide();
        
        } else {
            $("#TareaC").hide();
            $("#TareaP").hide();
            $("#TareaA").show();
    
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



function submitComment(textareaId, TypeRequest) {
    var commentText = $('#' + textareaId).val();
    var TypeRequest = $('#' + TypeRequest).val();
    var requestCode = document.getElementById('Code').value; 
    var gestor = document.getElementById('Gestor').value; 
    var Etapa = document.getElementById('Etapa').value; 

    if (!commentText || !requestCode || !gestor) {
        alert('Por favor complete todos los campos requeridos');
        return;
    }

    $.ajax({
        url: '../Requests/AddComment',
        type: 'POST',
        data: {
            requestCode: requestCode,
            commentText: commentText,
            gestor: gestor,
            Etapa: Etapa,
            TypeRequest: TypeRequest

        },
        headers: {
            'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
        },
        success: function (response) {
            if (response.success) {
                $('#confirmacionCambioModal').modal('hide'); 
                $('#comentarioCambio').val(''); // Clear the comment field 
                alert('Comentario agregado exitosamente');
                // Optionally refresh the page or update the comments list
                location.reload();
            } else {
                alert(response.message || 'Error al agregar el comentario');
            }
        },
        error: function (xhr, status, error) {
            console.error('Error details:', {xhr: xhr, status: status, error: error});
            alert('Error al procesar la solicitud: ' + requestCode);
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


