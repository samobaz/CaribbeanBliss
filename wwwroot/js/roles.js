let checkboxToChange;  // Guardamos el checkbox que se quiere cambiar

document.querySelectorAll('.confirm-checkbox').forEach(function(checkbox) {
    checkbox.addEventListener('change', function(event) {
        event.preventDefault(); // Evita el desmarcado inmediato
        checkboxToChange = this; // Guardamos el checkbox actual
        // Si se está intentando desmarcar (checked = false)
        if (!this.checked) {

            // Mostrar el modal de confirmación
            let confirmModal = new bootstrap.Modal(document.getElementById('confirmModal'), {});
            confirmModal.show();
        }
    });
});

// Acción cuando se presiona "Desmarcar" en el modal
document.getElementById('confirmAction').addEventListener('click', function() {
    // Finalmente desmarcamos el checkbox si el usuario confirma
    checkboxToChange.checked = false;  
    let confirmModal = bootstrap.Modal.getInstance(document.getElementById('confirmModal'));
    confirmModal.hide();  // Cerramos el modal
});

// Acción cuando se presiona "Desmarcar" en el modal
document.getElementById('denyAction').addEventListener('click', function() {
    // Finalmente desmarcamos el checkbox si el usuario confirma
    checkboxToChange.checked = true;  
    let confirmModal = bootstrap.Modal.getInstance(document.getElementById('confirmModal'));
    confirmModal.hide();  // Cerramos el modal
});

document.getElementById('clearFields').addEventListener('click', function() {
    document.getElementById('addRoleForm').reset();
});

document.getElementById('addRole').addEventListener('click', function() {
    const roleName = document.getElementById('roleName').value;
    const accesses = [...document.querySelectorAll('.confirm-checkbox-rol:checked')]
        .map(checkbox => checkbox.name);

    // Aquí puedes implementar la lógica para añadir el rol, como una llamada a la API o actualización de la tabla.

    // Cerrar el modal después de añadir el rol
    $('#addRoleModal').modal('hide');
});

document.getElementById('assignRole').addEventListener('click', function() {
    const employee = document.getElementById('employeeSelect').value;
    const role = document.getElementById('roleSelect').value;

    if (employee && role) {
        // Aquí puedes implementar la lógica para asignar el rol al empleado
        // como una llamada a la API o actualización de la tabla.

        alert(`Rol ${role} asignado a ${employee}`);

        // Cerrar el modal después de asignar el rol
        $('#assignRoleModal').modal('hide');
    } else {
        alert('Por favor, selecciona un empleado y un rol.');
    }
});

var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl);
});


