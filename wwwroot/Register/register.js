document.getElementById("registerForm").addEventListener('submit', validateFunction);


function validateFunction(e) {
    e.preventDefault();

    const firstname = document.getElementById('firstName').value;
    const lastname = document.getElementById('lastName').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const username = document.getElementById('username').value;
    const dateofbirth = document.getElementById('dateofbirth').value;

    //payload = bundled package of data that you want to send
    const payLoad = { firstname, lastname, email, password, username, dateofbirth };

    const errorMessage = document.getElementById('Message');

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

    else {
        console.log("register payload:", payLoad);
        errorMessage.textContent = "Account created successfully";
    }

    //fetch(obj)
    //.then
}