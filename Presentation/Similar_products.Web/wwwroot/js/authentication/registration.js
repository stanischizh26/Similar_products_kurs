document.getElementById('registrationForm').addEventListener('submit', async function (e) {
    e.preventDefault();

    const userName = document.getElementById('registrationUsername').value;
    const fullName = document.getElementById('registrationFullName').value;
    const hashedPassword = document.getElementById('registrationPassword').value;
    const errorMsg = document.getElementById('registrationErrorMsg');
    const newUser = {
        userName: userName,
        fullName: fullName,
        hashedPassword: hashedPassword,
        role: "",
    };

    // Clear previous error message
    errorMsg.style.display = 'none';

    try {
        const response = await axios.post('/api/users/registration', newUser);

        if (typeof response.ok !== "undefined" && !response.ok) {
            throw new Error('Ошибка регистрации');
        }

        alert('Регистрация прошла успешно, теперь можете войти в аккаунт');
    } catch (error) {
        console.error("Error fetching symptoms:", error);

        errorMsg.style.display = 'block';
        errorMsg.textContent = error.message;
    }
});
