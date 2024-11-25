// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function formatUSD(input) {

    let value = input.value.replace(/[^0-9.]/g, '');


    if (value.includes('.')) {
        let parts = value.split('.');
        parts[0] = parseFloat(parts[0] || 0).toLocaleString(); 
        value = parts[0] + '.' + (parts[1] || '').slice(0, 2); 
    } else {
        value = parseFloat(value || 0).toLocaleString();
    }

    input.value = '$' + value;
}


$(document).ready(function () {
    if ('@TempData["SuccessMessage"]' != '') {
        $('#successModal').modal('show');
    }
});