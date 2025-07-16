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
    console.log(`Checking requirement for: ${elementId}, Valid: ${isValid}`);

    const element = document.getElementById(elementId);
    if (!element) {
        console.error(`Element with ID ${elementId} not found.`);
        return;
    }

    const icon = element.querySelector('i');
    if (!icon) {
        console.error(`Icon element inside ${elementId} not found.`);
        return;
    }

    // Change text color
    if (isValid) {
        element.classList.remove('text-danger');
        element.classList.add('text-success');
    } else {
        element.classList.remove('text-success');
        element.classList.add('text-danger');
    }

    // Directly assign icon classes
    icon.className = isValid
        ? 'bi bi-check-circle-fill me-1'
        : 'bi bi-x-circle-fill me-1';

    console.log(`Icon classes after update: ${icon.className}`);
}

