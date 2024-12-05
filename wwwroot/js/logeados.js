
    const editButtons = document.querySelectorAll('.btn-warning');
    
    editButtons.forEach(button => {
        button.addEventListener('click', (event) => {
            const row = event.target.closest('tr');
            const username = row.cells[0].innerText;
            const cedula = row.cells[1].innerText;
            const email = row.cells[2].innerText;
            const roles = row.cells[3].innerText;
            
            // Rellenar el formulario del modal
            document.getElementById('username').value = username;
            document.getElementById('cedula').value = cedula;
            document.getElementById('email').value = email;
            document.getElementById('roles').value = roles;

            // Mostrar el modal
            const modal = new bootstrap.Modal(document.getElementById('editUserModal'));
            modal.show();
        });
    });

    // Manejar el envío del formulario
    document.getElementById('editUserForm').addEventListener('submit', (event) => {
        event.preventDefault();
        // Aquí puedes agregar la lógica para guardar los cambios, por ejemplo, enviarlos a tu servidor
        console.log("Datos guardados:", {
            username: document.getElementById('username').value,
            cedula: document.getElementById('cedula').value,
            email: document.getElementById('email').value,
            roles: document.getElementById('roles').value,
        });
        
        // Cerrar el modal
        const modal = bootstrap.Modal.getInstance(document.getElementById('editUserModal'));
        modal.hide();
    });



    // registrar



    document.getElementById('registerUserForm').addEventListener('submit', function(event) {
        event.preventDefault();
        
        const username = document.getElementById('newUsername').value;
        const cedula = document.getElementById('newCedula').value;
        const email = document.getElementById('newEmail').value;
        const roles = document.getElementById('newRoles').value;

        // Agregar el nuevo usuario a la tabla
        const tableBody = document.querySelector('table tbody');
        const newRow = document.createElement('tr');
        newRow.innerHTML = `
            <td>${username}</td>
            <td>${cedula}</td>
            <td>${email}</td>
            <td>${roles}</td>
            <td>
                <button class="btn btn-warning"><i class="fas fa-edit"></i> Editar</button>
                <button class="btn btn-danger"><i class="fas fa-trash-alt"></i> Eliminar</button>
            </td>
        `;
        tableBody.appendChild(newRow);

        // Cerrar el modal
        const modal = bootstrap.Modal.getInstance(document.getElementById('registerUserModal'));
        modal.hide();

        // Limpiar el formulario
        document.getElementById('registerUserForm').reset();
    });


    // buscar


    let userRowToDelete;

    function confirmDelete(button) {
        userRowToDelete = button.closest('tr');
        const modal = new bootstrap.Modal(document.getElementById('confirmDeleteModal'));
        modal.show();
    }

    document.getElementById('confirmDeleteButton').addEventListener('click', function() {
        if (userRowToDelete) {
            userRowToDelete.remove();
            userRowToDelete = null; // Reset variable
            const modal = bootstrap.Modal.getInstance(document.getElementById('confirmDeleteModal'));
            modal.hide();
        }
    });

    document.getElementById('registerUserForm').addEventListener('submit', function(e) {
        e.preventDefault();
        const newRow = document.createElement('tr');
        newRow.innerHTML = `
            <td>${document.getElementById('newUsername').value}</td>
            <td>${document.getElementById('newCedula').value}</td>
            <td>${document.getElementById('newEmail').value}</td>
            <td>${document.getElementById('newRoles').value}</td>
            <td>
                <button class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#editUserModal"><i class="fas fa-edit"></i> Editar</button>
                <button class="btn btn-danger" onclick="confirmDelete(this)"><i class="fas fa-trash-alt"></i> Eliminar</button>
            </td>
        `;
        document.getElementById('userTableBody').appendChild(newRow);
        this.reset();
        const modal = bootstrap.Modal.getInstance(document.getElementById('registerUserModal'));
        modal.hide();
    });

    function filterTable() {
        const searchValue = document.getElementById('searchInput').value.toLowerCase();
        const rows = document.querySelectorAll('#userTableBody tr');

        rows.forEach(row => {
            const cells = row.getElementsByTagName('td');
            const rowContainsSearchValue = Array.from(cells).some(cell => 
                cell.textContent.toLowerCase().includes(searchValue)
            );

            row.style.display = rowContainsSearchValue ? '' : 'none';
        });
    }
