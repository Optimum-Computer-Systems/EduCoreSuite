document.addEventListener("DOMContentLoaded", function () {
    // Uses global `courseDeptMap` defined in Razor view

    // Single course dropdown -> one department autofill
    function handleCourseSelect(selectId, inputId) {
        const courseSelect = document.getElementById(selectId);
        const departmentInput = document.getElementById(inputId);

        if (courseSelect && departmentInput) {
            const updateDepartment = () => {
                const selectedCourseId = courseSelect.value;
                departmentInput.value = courseDeptMap[selectedCourseId] || "";
            };
            courseSelect.addEventListener("change", updateDepartment);
            updateDepartment();
        }
    }

    // Multi-course checkboxes -> multiple departments autofill
    function handleMultiCourseCheck(checkboxClass, inputId) {
        const checkboxes = document.querySelectorAll(`.${checkboxClass}`);
        const departmentInput = document.getElementById(inputId);

        const updateMultiDept = () => {
            const departments = Array.from(checkboxes)
                .filter(cb => cb.checked)
                .map(cb => courseDeptMap[cb.value])
                .filter((v, i, a) => v && a.indexOf(v) === i);

            departmentInput.value = departments.join(", ");
        };

        checkboxes.forEach(cb => cb.addEventListener("change", updateMultiDept));
        updateMultiDept();
    }

    // Update dropdown label to show how many items are selected
    function setupDropdownLabel(buttonId, checkboxClass, itemName) {
        const btn = document.getElementById(buttonId);
        const checkboxes = document.querySelectorAll(`.${checkboxClass}`);

        const updateLabel = () => {
            const count = Array.from(checkboxes).filter(cb => cb.checked).length;
            if (count === 0) {
                btn.textContent = `-- Select ${itemName}s --`;
            } else if (count === 1) {
                btn.textContent = `1 ${itemName} selected`;
            } else {
                btn.textContent = `${count} ${itemName}s selected`;
            }
        };

        checkboxes.forEach(cb => cb.addEventListener("change", updateLabel));
        updateLabel();
    }

    // === Hook for all forms ===
    handleCourseSelect("SelectedCourseId_Single", "SelectedDepartment_Single");
    handleCourseSelect("SelectedCourseId_MultipleStudentsSingleCourse", "SelectedDepartment_MultipleStudentsSingleCourse");

    handleMultiCourseCheck("course-checkbox-SingleStudentMultipleCourses", "SelectedDepartment_SingleStudentMultipleCourses");
    handleMultiCourseCheck("multi-course-checkbox", "SelectedDepartment_MultipleStudentsMultipleCourses");

    // === Dropdown label counters ===
    setupDropdownLabel("studentsDropdownBtn", "student-checkbox", "student");
    setupDropdownLabel("multiStudentsDropdownBtn", "multi-student-checkbox", "student");
    setupDropdownLabel("coursesDropdownBtn_SingleStudentMultipleCourses", "course-checkbox-SingleStudentMultipleCourses", "course");
    setupDropdownLabel("multiCoursesDropdownBtn", "multi-course-checkbox", "course");
});
