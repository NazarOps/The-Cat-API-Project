document.getElementById('loginForm').addEventListener('submit', async function(e) {
    e.preventDefault();

        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;
        const errorMessage = document.getElementById('errorMessage');

        errorMessage.textContent = "";

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        const passRegex = /^(?=.*[!%#&])(?=.*[A-Z])(?=.*\d).{8,}$/;

        if (!emailRegex.test(email)) {
            errorMessage.textContent = "Please enter a valid email address.";
            return;
        }

        if (password.length < 8) {
            errorMessage.textContent = "Password must be at least 8 characters.";
            return;
        }

        if (!passRegex.test(password)) {
            errorMessage.textContent = "password must contain a special character"
            return;
        }

        try {
            const response = await fetch("https://localhost:1000/api/Auth/login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    email: email,
                    password: password
                })
            });

            const data = await response.json();

            if (!response.ok) {
                errorMessage.textContent = data.message || "Login failed.";
                return;
            }

            localStorage.setItem("token", data.token)
            window.location.href = "My-Cats.html";
        }

        catch (error) {
            errorMessage.textContent = "Could not connect to the server.";
            console.log(error);

        }

        alert("Login form is valid!");
    }
);