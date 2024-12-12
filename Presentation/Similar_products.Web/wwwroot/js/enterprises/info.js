function info(deleteButton)
{
    const row = deleteButton.closest('tr');
    const id = row.dataset.id;

    const modal = document.getElementById("modal");
    const modalContent = modal.querySelector(".modal-info-content");

    modalContent.innerHTML = 
    `
        <h3>Предприятия</h3>
        <p><strong>Имя:</strong> ${row.cells[0].innerText}</p>
        <p><strong>Имя руководителя:</strong> ${row.cells[1].innerText}</p>
        <p><strong>Вид деятельности:</strong> ${row.cells[2].innerText}</p>
        <p><strong>Форма собственности:</strong> ${row.cells[3].innerText}</p>
        <button onclick="closeModal()">Close</button>
        <button class="edit-buttons" onclick="deleteRow('${id}')">Delete</button>
    `;

    modal.style.display = "block";
    const editButtons = document.querySelectorAll('.edit-buttons');
    if (localStorage.getItem('role') === 'admin') {
        // Показать все кнопки редактирования
        editButtons.forEach(button => {
            button.style.display = 'inline-block';
        });
    } else {
        // Скрыть все кнопки редактирования
        editButtons.forEach(button => {
            button.style.display = 'none';
        });
    }
}
