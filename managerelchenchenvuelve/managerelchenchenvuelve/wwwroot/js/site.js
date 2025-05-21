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

 

function showCancelModal() {
    $('#confirmationModal').modal('show');
}

function submitForm() {
    $('#approvalModal').modal('hide');
    $('form').submit();
}

function showCommentModal(title, type) {
 
    $('#commentModalLabel').text(title);
    $('#commentModalMessage').text(`¿Está seguro que desea ${title.toLowerCase()} la solicitud?`);
    
 
    $('#commentType').val(type);
     
    $('#commentText').val(''); 
 
    $('#commentModal').modal('show');
}

function submitComment(textareaId, TypeRequest) {
    var commentText = $('#' + textareaId).val();
    var typeRequest = $('#' + TypeRequest).val();
    var requestCode = $('#CodigoDeSolicitud').val(); 
    var gestor = $('#Gestor').val(); 
    var etapa = $('#Etapa').val(); 

    if (!commentText || !requestCode || !gestor) {
        alert('Por favor complete todos los campos requeridos');
        return;
    }

    $.ajax({
        url: '/Requests/AddComment',
        type: 'POST',
        data: {
            requestCode: requestCode,
            commentText: commentText,
            gestor: gestor,
            Etapa: etapa,
            TypeRequest: typeRequest
        },
        headers: {
            'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
        },
        success: function (response) {
            if (response.success) {
                $('#commentModal').modal('hide'); 
                $('#commentText').val('');  
                alert('Comentario agregado exitosamente'); 
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

function actualizarSeleccion() {
    const checkboxes = document.querySelectorAll('.tarea-checkbox:checked');
    const actionBar = document.getElementById('action-bar');
    const selectedCount = document.getElementById('selected-count');

    selectedCount.textContent = checkboxes.length;

    if (checkboxes.length > 0) {
        actionBar.style.display = 'flex';
        setTimeout(() => actionBar.classList.add('visible'), 10);
    } else {
        actionBar.classList.remove('visible');
        setTimeout(() => {
            if (checkboxes.length === 0) {
                actionBar.style.display = 'none';
            }
        }, 300);
    }
}

function limpiarSeleccion() {
    const checkboxes = document.querySelectorAll('.tarea-checkbox');
    checkboxes.forEach(checkbox => checkbox.checked = false);
    actualizarSeleccion();
}

function asignarSeleccionados() {
    const selectedIds = Array.from(document.querySelectorAll('.tarea-checkbox:checked'))
        .map(checkbox => checkbox.dataset.id); 

    console.log('IDs seleccionados:', selectedIds);
}

function mostrarModalUsuarios() {
    const selectedIds = Array.from(document.querySelectorAll('.tarea-checkbox:checked'))
        .map(checkbox => checkbox.dataset.id);

    if (selectedIds.length === 0) {
        alert("Selecciona al menos una tarea.");
        return;
    }

    const modal = new bootstrap.Modal(document.getElementById('modalUsuarios'));
    modal.show();
}

function asignarSeleccionadosA(nombreUsuario) {
    const selectedIds = Array.from(document.querySelectorAll('.tarea-checkbox:checked'))
        .map(checkbox => checkbox.dataset.id);

    if (selectedIds.length === 0) {
        alert("Selecciona al menos una tarea.");
        return;
    }

    fetch('/Process/AsignarTareas', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value
        },
        body: JSON.stringify({
            ids: selectedIds,
            usuario: nombreUsuario
        })
    })
        .then(response => {
            if (response.ok) {
                alert("Tareas asignadas correctamente.");
                location.reload();
            } else {
                alert("Error al asignar tareas.");
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert("Error al asignar tareas.");
        });

  
    const modal = bootstrap.Modal.getInstance(document.getElementById('modalUsuarios'));
    modal.hide();
}



function CargaInfo(data) {
    window.location.href = '../Archivos/SubirArchivo?ProcessId=' + data;

}


