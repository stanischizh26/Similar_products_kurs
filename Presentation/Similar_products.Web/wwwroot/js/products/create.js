function addEmptyRow() {
    const table = document.querySelector("#table-container table tbody");

    // Создаём новую строку
    const newRow = document.createElement("tr");
    newRow.dataset.id = "new"; // Временный ID для новой строки

    newRow.innerHTML = `

        <td contenteditable="false"></td>
        <td contenteditable="false"></td>
        <td contenteditable="false"></td>
        <td data-field="productType" data-product-type-id="0"></td>
        <td contenteditable="false"></td>
        <td style="padding: 8px;">
            <a href="javascript:void(0);" onclick="saveNewRow(this)" title="Save">
                <i class="bi bi-check-circle-fill"></i>
            </a>
            <a href="javascript:void(0);" onclick="cancelNewRow(this)" title="Cancel">
                <i class="bi bi-x-circle-fill"></i>
            </a>
        </td>
    `;

    const cells = Array.from(newRow.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));
    newRow.classList.add('editing');
    newRow.dataset.originalData = JSON.stringify(cells.map(cell => cell.innerText.trim()));
    cells.forEach(cell => cell.setAttribute('contenteditable', 'true')); // Только данные можно редактировать

    cells.forEach(cell => {
        if (cell.dataset.field === "productType") {
            cell.addEventListener('click', () => openSelectModal(cell));
        }
    });

    // Вставляем новую строку в начало таблицы
    table.prepend(newRow);
}
async function saveNewRow(saveButton) {
    const row = saveButton.closest("tr");
    const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));

    const id = row.dataset.id;
    const updatedData = {
        name: cells[0].innerText.trim(),
        characteristics: cells[1].innerText.trim(),
        unit: cells[2].innerText.trim(),
        productTypeId: cells[3].dataset.productTypeId,
        photo: cells[4].innerText.trim(),
    };

    // Проверяем заполненность поля
    if (!updatedData.name && !updatedData.characteristics && !updatedData.unit && !updatedData.productTypeId
        && !updatedData.photo) {
        alert("Не все поля заполнены");
        return;
    }

    try {
        // Отправляем данные на сервер
        const response = await axios.post(apiBaseUrl, updatedData, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });

        if (response.status === 201) {
            alert("data created successfully!");

            
            location.reload();

        } else {
            throw new Error("Failed to create data.");
        }
    } catch (error) {
        console.error("Error creating data:", error);
        alert("Failed to create data. Please try again.");

        // Удаляем строку при ошибке
        row.remove();
    }
}

function cancelNewRow(cancelButton) {
    const row = cancelButton.closest("tr");
    row.remove(); // Удаляем строку
}