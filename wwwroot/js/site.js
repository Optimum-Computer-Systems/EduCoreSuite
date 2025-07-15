// Professional Sidebar Navigation JavaScript

document.addEventListener('DOMContentLoaded', function () {
    // Sidebar Elements
    const sidebarToggle = document.querySelector('.sidebar-toggle');
    const sidebar = document.getElementById('sidebar');
    const sidebarOverlay = document.getElementById('sidebarOverlay');
    const body = document.body;

    // Toggle Sidebar Function
    function toggleSidebar() {
        body.classList.toggle('sidebar-open');
        sidebar.classList.toggle('show');
        sidebarOverlay.classList.toggle('show');
    }

    // Close Sidebar Function
    function closeSidebar() {
        body.classList.remove('sidebar-open');
        sidebar.classList.remove('show');
        sidebarOverlay.classList.remove('show');
    }

    // Event Listeners
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
            const originalClass = icon.className;
            icon.className = 'bi bi-arrow-clockwise spin';

            // Restore icon after navigation
            setTimeout(function () {
                icon.className = originalClass;
            }, 500);
        });
    });
});

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