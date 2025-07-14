// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification

//For password visibility toggle
const togglePasswordIcon = document.getElementById('togglePasswordIcon');
const passwordInput = document.getElementById('Password');

togglePasswordIcon.addEventListener('click', function () {
    let type = passwordInput.getAttribute('type');

    if (type === 'password') {
        type = 'text';
    }
    else {
        type = 'password';
    }

    passwordInput.setAttribute('type', type);
    //toggle icon
    this.classList.toggle('bi-eye');
    this.classList.toggle('bi-eye-slash');
})

