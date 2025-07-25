// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification

// Authentication JavaScript from Master
document.addEventListener('DOMContentLoaded', function () {
    // Password visibility toggle
    const togglePasswordIcon = document.getElementById('togglePasswordIcon');
    const passwordInput = document.getElementById('Password');

    if (togglePasswordIcon && passwordInput) {
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
        });

        // Password Requirements List
        passwordInput.addEventListener('input', function () {
            const value = passwordInput.value;

            //checking the rules
            toggleRequirement('lengthCheck', value.length >= 8);
            toggleRequirement('uppercaseCheck', /[A-Z]/.test(value));
            toggleRequirement('lowercaseCheck', /[a-z]/.test(value));
            toggleRequirement('numberCheck', /\d/.test(value));
        });
    }

    // Professional Sidebar Navigation JavaScript from Forge
    const sidebarToggle = document.querySelector('.sidebar-toggle');
    const sidebar = document.getElementById('sidebar');
    const sidebarOverlay = document.getElementById('sidebarOverlay');
    const body = document.body;

    // Toggle Sidebar Function
    function toggleSidebar() {
        body.classList.toggle('sidebar-open');
        if (sidebar) sidebar.classList.toggle('show');
        if (sidebarOverlay) sidebarOverlay.classList.toggle('show');
    }

    // Close Sidebar Function
    function closeSidebar() {
        body.classList.remove('sidebar-open');
        if (sidebar) sidebar.classList.remove('show');
        if (sidebarOverlay) sidebarOverlay.classList.remove('show');
    }

    // Event Listeners for Sidebar
    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', function (e) {
            e.preventDefault();
            toggleSidebar();
        });
    }

    if (sidebarOverlay) {
        sidebarOverlay.addEventListener('click', function () {
            closeSidebar();
        });
    }

    // Close sidebar when clicking on menu items on mobile
    const menuItems = document.querySelectorAll('.menu-item');
    menuItems.forEach(function (item) {
        item.addEventListener('click', function () {
            if (window.innerWidth < 992) {
                setTimeout(closeSidebar, 150); // Small delay for better UX
            }
        });
    });

    // Handle window resize
    window.addEventListener('resize', function () {
        if (window.innerWidth >= 992) {
            closeSidebar();
        }
    });

    // Keyboard navigation (ESC to close sidebar)
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape' && body.classList.contains('sidebar-open')) {
            closeSidebar();
        }
    });

    // Add smooth scrolling to menu items
    menuItems.forEach(function (item) {
        item.addEventListener('click', function (e) {
            // Add loading state
            const icon = this.querySelector('i');
            if (icon) {
                const originalClass = icon.className;
                icon.className = 'bi bi-arrow-clockwise spin';

                // Restore icon after navigation
                setTimeout(function () {
                    icon.className = originalClass;
                }, 500);
            }
        });
    });
});

// Helper function for password requirements
function toggleRequirement(elementId, isValid) {
    const element = document.getElementById(elementId);
    if (!element) return;
    
    const icon = element.querySelector('i');

    if (isValid) {
        element.classList.remove('text-danger');
        element.classList.add('text-success');

        if (icon) {
            icon.classList.remove('bi-x-circle-fill');
            icon.classList.add('bi-check-circle-fill');
        }
    }
    else {
        element.classList.remove('text-success');
        element.classList.add('text-danger');
        
        if (icon) {
            icon.classList.remove('bi-check-circle-fill');
            icon.classList.add('bi-x-circle-fill');
        }
    }
}

// CSS Animation for loading spinner
const style = document.createElement('style');
style.textContent = `
    .spin {
        animation: spin 1s linear infinite;
    }
    
    @keyframes spin {
        from { transform: rotate(0deg); }
        to { transform: rotate(360deg); }
    }
`;
document.head.appendChild(style);