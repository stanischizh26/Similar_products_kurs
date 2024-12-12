﻿function editRow(editButton) {
    const row = editButton.closest('tr');
    const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));
    const isEditing = row.classList.contains('editing');

    if (isEditing) {
        const id = row.dataset.id;
        const updatedData = {
            id: id,
            name: cells[0].innerText.trim(),
            characteristics: cells[1].innerText.trim(),
            unit: cells[2].innerText.trim(),
            productTypeId: cells[3].dataset.productTypeId,
            photo: cells[4].innerText.trim(),
        };

        saveChanges(id, updatedData, row);
    } else {
        // Начало редактирования
        row.classList.add('editing');
        row.dataset.originalData = JSON.stringify(cells.map(cell => cell.innerText.trim()));

        cells.forEach(cell => {
            if (cell.dataset.field === "productType") {
                cell.addEventListener('click', () => openSelectModal(cell));
            }
        });
        cells.forEach(cell => cell.setAttribute('contenteditable', 'true')); // Только данные можно редактировать

        editButton.innerHTML = '<i class="bi bi-check-circle-fill"></i>';
        editButton.title = "Save";

        // Добавляем кнопку отмены
        const cancelButton = document.createElement('a');
        cancelButton.innerHTML = '<i class="bi bi-x-circle-fill"></i>';
        cancelButton.title = "Cancel";
        cancelButton.className = "cancel-button";
        cancelButton.onclick = () => cancelEditingDiseasemedicine(row);
        row.querySelector('td.actions').appendChild(cancelButton);
    }
}

async function saveChanges(id, updatedData, row) {
    try {
        const response = await axios.put(`${apiBaseUrl}/${id}`, updatedData, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });

        row.classList.remove('editing');
        const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));

        location.reload();

        // Отключаем возможность редактирования (клик по ячейкам)
        cells.forEach(cell => {
            cell.removeEventListener('click', openSelectModal);  // Убираем обработчик события
            cell.classList.remove('editable');  // Можно добавить стиль, если нужно
        });

        // Отключаем кнопки редактирования
        const editButton = row.querySelector('a[title="Save"]');
        editButton.innerHTML = '<i class="bi bi-pencil-fill"></i>';
        editButton.title = "Edit";

        const cancelButton = row.querySelector('.cancel-button');
        if (cancelButton) cancelButton.remove();

    } catch (error) {
        console.error("Error saving changes:", error);
        alert("Failed to save changes. Please try again.");
    }
}
function cancelEditingDiseasemedicine(row) {
    // удалить модальные окна если есть
    const existingModal = document.querySelector('.modal-list');
    if (existingModal) {
        existingModal.remove();
    }

    const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));
    const originalData = JSON.parse(row.dataset.originalData);

    // Возвращаем исходные значения
    cells.forEach((cell, index) => {
        cell.innerText = originalData[index];
    });

    row.classList.remove('editing');

    const editButton = row.querySelector('a[title="Save"]');
    if (editButton) {
        editButton.innerHTML = '<i class="bi bi-pencil-fill"></i>';
        editButton.title = "Edit";
    }

    const cancelButton = row.querySelector('.cancel-button');
    if (cancelButton) cancelButton.remove();

    // Отключаем обработчики кликов, если редактирование отменено
    cells.forEach(cell => {
        cell.removeEventListener('click', openSelectModal);  // Убираем обработчик события
    });
}
function openSelectModal(cell) {
    const type = cell.dataset.field === "productType" ? 'productType' : 'productType';

    // Удаляем все открытые модальные окна, если они есть
    const existingModal = document.querySelector('.modal-list');
    if (existingModal) {
        existingModal.remove();
    }

    // Создаем новое модальное окно
    const modal = document.createElement('div');
    modal.classList.add('modal-list');
    modal.innerHTML = `
        <div class="modal-list-content">
            <div class="modal-list-header">
                <span class="close">&times;</span>
                <h2>Select ${type}</h2>
            </div>
            <div class="modal-list-body">
                <table id="select-table">
                    <!-- Данные для выбора загружаются динамически -->
                </table>
            </div>
        </div>
    `;
    document.body.appendChild(modal);

    modal.querySelector('.close').onclick = () => modal.remove();

    // Позиционируем модальное окно под ячейкой
    const cellRect = cell.getBoundingClientRect();
    modal.style.left = `${cellRect.left}px`;
    modal.style.top = `${cellRect.bottom + window.scrollY}px`; // Учитываем прокрутку страницы

    loadSelectData(type, cell);
}

async function loadSelectData(type, cell) {
    try {
        const response = await axios.get(`${apiBaseUrl}/${type}s`, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });

        const table = document.getElementById('select-table');
        table.innerHTML = '';

        response.data.forEach(item => {
            const row = document.createElement('tr');
            row.dataset.id = item.id;
            row.innerHTML = `<td>${item.name}</td>`;
            row.onclick = () => selectItem(item, cell, type);
            table.appendChild(row);
        });

    } catch (error) {
        console.error("Error loading select data:", error);
        alert("Failed to load data. Please try again.");
    }
}
function selectItem(item, cell, type) {
    if (type === 'productType') {
        cell.dataset.productTypeId = item.id;
        cell.innerText = item.name;
    }

    const modal = document.querySelector('.modal-list');
    if (modal) modal.remove();
}