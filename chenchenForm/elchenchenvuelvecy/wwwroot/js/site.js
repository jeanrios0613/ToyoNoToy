


$(document).ready(function () {
 

    $("#TipoFormulario").change(function () {
        var seleccion = $(this).val();

        if (seleccion == "Emprendimiento") {
            $("#envio").show();
            $("#Details").show();
            $("#Conctact").show();
            $("#EnterprisesEmpre").show(); 
            $("#Emprendedor").show(); 
            $("#EnterprisesNegoc").hide(); 
            $("#Negocio").hide();  

            $('div').removeClass('position-absolute');
            

        } else if (seleccion == "Negocio existente") { 
            $("#envio").show();
            $("#Details").show();
            $("#Conctact").show();
            $("#EnterprisesNegoc").show(); 
            $("#Negocio").show(); 
            $("#EnterprisesEmpre").hide(); 
            $("#Emprendedor").hide();   

            $('div').removeClass('position-absolute');

        } else {
            $("#envio").hide();
            $("#Details").hide();
            $("#Conctact").hide();
            $("#EnterprisesEmpre").hide();
            $("#EnterprisesNegoc").hide(); 
            $("#Emprendedor").hide();
            $("#Negocio").hide();  
        }
            

    }).change();
     
    if ('@TempData["SuccessMessage"]' != '') {
        $('#successModal').modal('show');
    }

     
   /* $('#confirmButton').click(function () {
        var email = $('#Email').val();
        $.ajax({
            url: '@Url.Action("SendConfirmationEmail", "Email")',
            type: 'POST',
            data: { email: email },
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                } else {
                    alert("Error: " + response.message);
                }
            },
            error: function () {
                alert("Ocurrió un error al enviar el correo.");
            }
        });
    });*/




});



document.getElementById("openModal").addEventListener("click", function () {
    let checkbox = document.getElementById("termsCheckbox");
    if (!checkbox.checked) {
        alert("Debes aceptar los términos y condiciones.");
        return;
    }
    $("# ").modal("show");
});




document.getElementById("closeModal").addEventListener("click", function () {
    $("#confirmationModal").modal("hide");
});


document.addEventListener("DOMContentLoaded", function () {
    const provinciaSelect = document.getElementById("provincia");
    const distritoSelect = document.getElementById("distrito");
    const corregimientoSelect = document.getElementById("corregimiento");

    const provincia = ["Panama", "Panama Oeste", "Colon", "Bocas del Toro", "Chiriqui", "Darien", "Veraguas", "Los Santos", "Cocle", "Herrera"];

    const distritos = {
        "Bocas del Toro": ["Bocas del Toro", "Changuinola", "Chiriquí Grande"],
        "Cocle": ["Aguadulce", "Antón", "La Pintada", "Natá", "Olá", "Penonomé"],
        "Colon": ["Colón", "Chagres", "Donoso", "Portobelo", "Santa Isabel"],
        "Chiriqui": ["Alanje", "Barú", "Boquerón", "Boquete", "Bugaba", "David", "Dolega", "Gualaca", "Remedios", "Renacimiento", "San Félix", "San Lorenzo", "Tolé"],
        "Darien": ["Chepigana", "Pinogana", "Santa Fe"],
        "Herrera": ["Chitré", "Las Minas", "Los Pozos", "Ocú", "Parita", "Pesé", "Santa María"],
        "Los Santos": ["Guararé", "Las Tablas", "Los Santos", "Macaracas", "Pedasí", "Pocrí", "Tonosí"],
        "Panama": ["Balboa", "Chepo", "Chimán", "Panamá", "San Miguelito", "Taboga"],
        "Panama Oeste": ["Arraiján", "Capira", "Chame", "La Chorrera", "San Carlos"],
        "Veraguas": ["Atalaya", "Calobre", "Cañazas", "La Mesa", "Las Palmas", "Mariato", "Montijo", "Río de Jesús", "San Francisco", "Santa Fe", "Santiago", "Soná"],
        "Comarca Embera-Wounaan": ["Cémaco", "Sambú"],
        "Comarca Guna Yala": ["Ailigandí", "Cartí Sugdup", "Narganá"],
        "Comarca Ngäbe-Buglé": ["Besikó", "Kankintú", "Kusapín", "Müna", "Ñürüm", "Mironó", "Nole Duima", "Santa Catalina o Calovébora"]
    }
        ;

    const corregimientos = {

        "Bocas del Toro": ["Bastimentos", "Bocas del Toro", "Cauchero", "Punta Laurel"],
        "Changuinola": ["Barriada 4 de Abril", "El Empalme", "Finca 30", "Finca 6", "Finca 60", "Guabito", "La Gloria", "Las Tablas", "El Silencio", "Valle del Risco"],
        "Chiriquí Grande": ["Bajo Cedro", "Chiriquí Grande", "Miramar", "Punta Peña", "Rambala"],
        "Aguadulce": ["Aguadulce", "El Cristo", "El Roble", "Pocrí", "Barrios Unidos"],
        "Antón": ["Antón", "Caballero", "El Chiru", "El Retiro", "El Valle", "Juan Díaz", "Río Hato", "San Juan de Dios", "Santa Rita"],
        "La Pintada": ["La Pintada", "El Harino", "El Potrero", "Llano Grande", "Piedras Gordas"],
        "Natá": ["Natá", "Capellanía", "El Caño", "Guzmán", "Las Huacas", "Toza"],
        "Olá": ["Olá", "El Copé", "El Palmar", "La Pava"],
        "Penonomé": ["Penonomé", "Cañaveral", "Coclé", "El Coco", "Pajonal", "Río Grande", "Río Indio", "Toabré", "Tulú"],
        "Colón": ["Barrio Norte", "Barrio Sur", "Buena Vista", "Cativá", "Cristóbal", "Escobal", "Sabanitas"],
        "Chagres": ["Achiote", "El Guabo", "La Encantada", "Nuevo Chagres", "Palmas Bellas"],
        "Donoso": ["Coclé del Norte", "El Guásimo", "Miguel de La Borda", "Río Indio"],
        "Portobelo": ["Cacique", "Garrote", "Isla Grande", "María Chiquita", "Portobelo"],
        "Santa Isabel": ["Cuango", "Miramar", "Nombre de Dios", "Palmira", "Palma Real", "Santa Isabel"],
        "David": ["Bijagual", "Cochea", "David", "Guaca", "Las Lomas", "Pedregal", "San Carlos", "San Pablo Nuevo", "San Pablo Viejo"],
        "Boquete": ["Alto Boquete", "Bajo Boquete", "Caldera", "Palmira"],
        "Bugaba": ["Bugaba", "Cerro Punta", "El Bongo", "Gómez", "La Concepción", "Santa Marta", "Santo Domingo", "Sortová"],
        "Barú": ["Baco", "Progreso", "Puerto Armuelles"],
        "Alanje": ["Alanje", "Divala", "Guarumal", "Palo Grande", "Querévalo"],
        "Boquerón": ["Boquerón", "Bágala", "Cordillera", "Guabal", "Guayabal", "Paraíso"],
        "Dolega": ["Dolega", "Dos Ríos", "Los Algarrobos", "Potrerillos", "Tinajas"],
        "Gualaca": ["Gualaca", "Hornito", "Los Angeles", "Río Sereno"],
        "Remedios": ["El Nancito", "El Porvenir", "Remedios", "Santa Lucía"],
        "Renacimiento": ["Breñón", "Jaramillo", "Monte Lirio", "Plaza de Caisán", "Río Sereno"],

        "San Félix": ["Juay", "Las Lajas", "San Félix"],
        "San Lorenzo": ["Boca Chica", "Horconcitos", "San Lorenzo"],
        "Tolé": ["Bella Vista", "Cerro Viejo", "El Cristo", "Justo Fidel Palacios", "Potrero de Caña", "Quebrada de Piedra", "Tolé"],
        "Chepigana": ["Garachiné", "Jaqué", "Puerto Piña", "Sambú", "Setegantí"],
        "Pinogana": ["Boca de Cupe", "Metetí", "Paya", "Púcuro", "Yaviza"],
        "Santa Fe": ["Santa Fe"],
        "Chitré": ["Chitré", "La Arena", "Los Pozos", "Monagrillo", "San Juan Bautista"],
        "Las Minas": ["Chepo", "El Toro", "Las Minas", "Quebrada del Rosario"],
        "Los Pozos": ["La Pitaloza", "Los Pozos", "El Capurí"],
        "Ocú": ["Ocú", "Los Llanos", "Peñas Chatas"],
        "Parita": ["Cabecera", "Parita", "Portobelillo"],
        "Pesé": ["El Barrero", "Pesé", "Rincón Hondo"],
        "Santa María": ["Santa María"],
        "Las Tablas": ["La Palma", "Las Tablas", "San José", "Santo Domingo"],
        "Los Santos": ["Guararé", "Los Santos", "Macaracas", "Pedasí", "Pocrí", "Tonosí"],
        "Santiago": ["Canto del Llano", "Edwin Fábrega", "La Colorada", "La Peña", "Santiago"],
        "Atalaya": ["Atalaya"],
        "Calobre": ["Calobre", "El María", "San José"],
        "Cañazas": ["Cañazas", "El Picador"],
        "La Mesa": ["La Mesa"],
        "Las Palmas": ["Las Palmas"],
        "Mariato": ["Mariato", "Quebro"],
        "Montijo": ["Montijo"],
        "Río de Jesús": ["Río de Jesús"],
        "San Francisco": ["San Francisco"],
        "Santa Fe": ["Santa Fe"],
        "Soná": ["Soná"],
        "Cémaco": ["Lajas Blancas", "Manené"],
        "Sambú": ["Sambú"],
        "Ailigandí": ["Ailigandí"],
        "Cartí Sugdup": ["Cartí Sugdup"],
        "Narganá": ["Narganá"],
        "Besikó": ["Soloy"],
        "Kankintú": ["Kankintú"],
        "Kusapín": ["Kusapín"],
        "Müna": ["Müna"],
        "Ñürüm": ["Ñürüm"],
        "Mironó": ["Mironó"],
        "Nole Duima": ["Nole Duima"],
        "Santa Catalina o Calovébora": ["Santa Catalina o Calovébora"],
        "Panamá": ["24 de Diciembre", "Alcalde Díaz", "Ancón", "Bella Vista", "Betania", "Chilibre", "Curundú", "El Chorrillo", "Juan Díaz", "Las Cumbres", "Pacora", "Parque Lefevre", "Pedregal", "Pueblo Nuevo", "Río Abajo", "San Felipe", "San Francisco", "San Martín", "Santa Ana", "Tocumen"],
        "San Miguelito": ["Amelia Denis de Icaza", "Belisario Frías", "Belisario Porras", "José Domingo Espinar", "Mateo Iturralde", "Omar Torrijos", "Rufina Alfaro", "Victoriano Lorenzo"],
        "Chepo": ["Chepo", "Cañita", "Chepillo", "Las Margaritas", "Santa Cruz"],
        "Chimán": ["Brujas", "Chimán", "Gonzalo Vásquez", "Pásiga", "Unión Santeña"],
        "Taboga": ["Otoque Oriente", "Otoque Occidente", "Taboga"],
        "Arraiján": ["Arraiján", "Burunga", "Cerro Silvestre", "Juan Demóstenes Arosemena", "Nuevo Emperador", "Santa Clara", "Vista Alegre"],
        "La Chorrera": ["Barrio Balboa", "Barrio Colón", "El Arado", "Guadalupe", "Hurtado", "Iturralde", "La Represa", "Los Díaz", "Obaldía", "Playa Leona"],
        "Capira": ["Capira", "Cirí Grande", "Cirí de Los Sotos", "El Cacao", "La Trinidad", "Villa Carmen"],
        "Chame": ["Bejuco", "Buenos Aires", "Cabuya", "Chame", "El Líbano", "Las Lajas", "Nueva Gorgona", "Punta Chame", "Sajalices", "Sorá"],
        "San Carlos": ["El Espino", "Guayabito", "La Ermita", "La Laguna", "Las Uvas", "Los Llanitos", "San Carlos", "San José"],
        "Colón": ["Barrio Norte", "Barrio Sur", "Buena Vista", "Cativá", "Ciricito", "Cristóbal", "Escobal", "Limón", "Nueva Providencia", "Sabanitas", "Salamanca"],
        "Chagres": ["Achiote", "El Guabo", "La Encantada", "Nuevo Chagres", "Palmas Bellas"],
        "Donoso": ["Coclé del Norte", "El Guásimo", "Miguel de La Borda", "Río Indio"],
        "Portobelo": ["Cacique", "Garrote", "Isla Grande", "María Chiquita", "Portobelo"],
        "Santa Isabel": ["Cuango", "Miramar", "Nombre de Dios", "Palmira", "Palma Real", "Santa Isabel"]


    }
        ;

    provinciaSelect.innerHTML = '<option>Seleccione una opción</option>';
    provincia.forEach(provincia => {
        const option = document.createElement("option");
        option.textContent = provincia;
        option.value = provincia;
        provinciaSelect.appendChild(option);
    }
    )

    provinciaSelect.addEventListener("change", function () {
        const provincia = this.value;
        distritoSelect.innerHTML = '<option>Seleccione una opción</option>';
        corregimientoSelect.innerHTML = '<option>Seleccione una opción</option>';

        if (provincia in distritos) {
            distritos[provincia].forEach(distrito => {
                const option = document.createElement("option");
                option.textContent = distrito;
                option.value = distrito;
                distritoSelect.appendChild(option);
            });
        }
    });

    distritoSelect.addEventListener("change", function () {
        const distrito = this.value;
        corregimientoSelect.innerHTML = '<option>Seleccione una opción</option>';

        if (distrito in corregimientos) {
            corregimientos[distrito].forEach(corregimiento => {
                const option = document.createElement("option");
                option.textContent = corregimiento;
                option.value = corregimiento;
                corregimientoSelect.appendChild(option);
            });
        }
    });

        
    
});

document.getElementById("termsCheckbox").addEventListener("change", function () {
    if (this.checked) {
        let modal = new bootstrap.Modal(document.getElementById('termsModal'));
        modal.show();
    }
});