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

//Password Requirements List
passwordInput.addEventListener('input', function () {
    const value = passwordInput.value;

    //checking the rules
    toggleRequirement('lengthCheck', value.length >=8);
    toggleRequirement('uppercaseCheck', /[A-Z]/.test(value));
    toggleRequirement('lowercaseCheck', /[a-z]/.test(value));
    toggleRequirement('numberCheck', /\d/.test(value));
})

function toggleRequirement(elementId, isValid) {
    const element = document.getElementById(elementId);
    const icon = document.querySelector('i');

    if (isValid) {
        element.classList.remove('text-danger');
        element.classList.add('text-success');

        icon.classList.remove('bi-x-circle-fill');
        icon.classList.add('bi-check-circle-fill')
    }
    else {
        element.classList.remove('text-success');
        element.classList.add('text-danger');
        
        icon.classList.remove('bi-check-circle-fill')
        icon.classList.add('bi-x-circle-fill');        
    }
}

