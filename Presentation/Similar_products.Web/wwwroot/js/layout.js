const accountLink = document.querySelector('a[href="/Home/Auth"]');
const accountLinkText = localStorage.getItem('accountLinkText');

// Проверяем, что ссылка найдена
if (accountLink) {
    if (accountLinkText) {
        accountLink.textContent = accountLinkText;
    }
    else {
        accountLink.textContent = "Аккаунт";
    }
}

const usersLink = document.querySelector('a[href="/Tables/Users"]');
const role = localStorage.getItem('role');
if (usersLink && role !== "Admin")
{
    usersLink.style.display = 'none';
}