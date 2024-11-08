// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.getElementById('logoutButton').addEventListener('click', function () {
    // 1. Eliminar la sesión (por ejemplo, si usas localStorage o cookies)
    // Ejemplo: localStorage.removeItem('userSession');

    // 2. Redirigir a la página de inicio de sesión
    window.location.href = '/login.html';

    // 3. Evitar que el usuario regrese a la página anterior
    history.pushState(null, null, '/login.html'); // Reemplaza el historial actual
    window.addEventListener('popstate', function () {
        history.pushState(null, null, '/login.html'); // Evita el retroceso
    });
});


window.onload = function () {
    if (performance.navigation.type === 2) {
        location.reload(true);
    }
};



