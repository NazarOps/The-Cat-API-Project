document.getElementById('loginForm').addEventListener('submit', function(e) {
    e.preventDefault();

    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const errorMessage = document.getElementById('errorMessage');

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!emailRegex.test(email)) {
        errorMessage.textContent = "Please enter a valid email address.";
        return;
    }

    if (password.length < 8) {
        errorMessage.textContent = "Password must be at least 8 characters.";
        return;
    }

    errorMessage.textContent = "";
    alert("Login form is valid!");
});