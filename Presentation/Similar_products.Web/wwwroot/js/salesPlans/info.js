function info(deleteButton)
{
    const row = deleteButton.closest('tr');
    const id = row.dataset.id;

    const modal = document.getElementById("modal");
    const modalContent = modal.querySelector(".modal-info-content");

    modalContent.innerHTML = 
    `
        <h3>Продукция</h3>
        <p><strong>Enterprise Name:</strong> ${row.cells[0].innerText}</p>
        <p><strong>Product Name:</strong> ${row.cells[1].innerText}</p>
        <p><strong>PlannedSales:</strong> ${row.cells[2].innerText}</p>
        <p><strong>ActualSales:</strong> ${row.cells[3].innerText}</p>
        <p><strong>Quarter:</strong> ${row.cells[4].innerText}</p>
        <p><strong>Year:</strong> ${row.cells[5].innerText}</p>
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
