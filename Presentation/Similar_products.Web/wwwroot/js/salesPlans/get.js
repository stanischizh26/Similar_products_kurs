const apiBaseUrl = "/api/salesPlans";
let currentPage = 1; // Текущая страница
const itemsPerPage = 8; // Количество записей на странице

async function loadData(page = 1) {
    try {
        const nameFilter = document.getElementById("filter-name").value || "";
        const token = localStorage.getItem('token');
        const response = await axios.get(`${apiBaseUrl}`, {
            params: {
                page,
                pageSize: itemsPerPage,
                name: nameFilter,
            },
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });

        // Создание переменных для таблицы
        const itemsLength = response.data.items.length;
        const totalCount = response.data.totalCount;
        const tableTitle = "Продукты";
        const tableHead = `
            <tr>
                <th>Enterprise Name</th>
                <th>Product Name</th>
                <th>PlannedSales</th>
                <th>ActualSales</th>
                <th>Quarter</th>
                <th>Year</th>
                <th></th>
            </tr>
        `;
        const tableBody = response.data.items.map(item => `
            <tr data-id="${item.id}">
                <td data-field="enterprise" data-enterprise-id="${item.enterpriseId}">${item.enterprise.name}</td>
                <td data-field="product" data-product-id="${item.productId}">${item.product.name}</td>
                <td contenteditable="false" oninput="validateInput(this)">${item.plannedSales}</td>
                <td contenteditable="false" oninput="validateInput(this)">${item.actualSales}</td>
                <td contenteditable="false" oninput="validateInput(this)">${item.quarter}</td>
                <td contenteditable="false" oninput="validateInput(this)">${item.year}</td>
                <td class="actions">
                    <a class="edit-buttons" href="javascript:void(0);" onclick="editRow(this)" title="Edit">
                        <i class="bi bi-pencil-fill"></i>
                    </a>
                    <a href="javascript:void(0);" onclick="info(this)" title="Delete Item">
                        <i class="bi bi-eye-fill"></i>
                    </a>
                </td>
            </tr>
        `).join('');

        // Создание таблицы
        createTable(itemsLength, totalCount, page, tableTitle, tableHead, tableBody);
    } catch (error) {
        ERROR(error);
    }
}

function validateInput(cell) {
    const value = cell.textContent;
    if (!/^\d*\.?\d*$/.test(value)) {
        cell.textContent = value.replace(/[^0-9.]/g, '');
    }
}

// Инициализация
loadData();