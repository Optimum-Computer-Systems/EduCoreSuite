// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification

//For password visibility toggle
const togglePassword = document.getElementById('togglePassword');
const togglePasswordIcon = document.getElementById('togglePasswordIcon');
const passwordInput = document.getElementById('Password');

togglePassword.addEventListener('click', function () {
    let type = passwordInput.getAttribute('type');

    if (type === 'password') {
        type = 'text';
    }
    else {
        type = 'password';
    }

    passwordInput.setAttribute('type', type);
    //toggle icon
    togglePasswordIcon.classList.toggle('bi-eye');
    togglePasswordIcon.classList.toggle('bi-eye-slash');
})

