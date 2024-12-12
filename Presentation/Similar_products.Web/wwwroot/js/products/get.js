const apiBaseUrl = "/api/products";
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
                <th>Имя</th>
                <th>Характеристики</th>
                <th>Единица измерения</th>
                <th>Тип продукта</th>
                <th>Фото</th>
                <th></th>
            </tr>
        `;
        const tableBody = response.data.items.map(item => `
            <tr data-id="${item.id}">
                <td contenteditable="false">${item.name}</td>
                <td contenteditable="false">${item.characteristics}</td>
                <td contenteditable="false">${item.unit}</td>
                <td data-field="productType" data-product-type-id="${item.productType.id}">${item.productType.name}</td>
                <td contenteditable="false">${item.photo}</td>
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

// Инициализация
loadData();