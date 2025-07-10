# EduCore – Academic Enrollment and Course Management System

### Supervisor: Mr. Nick Silver  

---

## Executive Summary

**EduCore** is a modular, scalable, and data-driven university enrollment and academic management system. It enables administrators to register students, configure academic entities, manage courses, assign enrollments, and generate academic reports. The system follows formal software engineering conventions, using the **ASP.NET MVC** framework, **Entity Framework Core** (Code First), and **Microsoft SQL Server** as the persistence layer.

The platform models university operations as designed in the official **Figma wireframes** and was developed under the supervision of **Mr. Nick Silver**.

🔗 [View Figma Prototype](https://www.figma.com/proto/k3kXH5IK77TaCMqGO3AAiK/EduCore-UI-System-%E2%80%93-Design-v1?node-id=50-2&t=mNp8QHc2rj79fd4a-1)

---

## System Architecture

| Layer        | Technology                          |
|--------------|--------------------------------------|
| Frontend     | ASP.NET Razor Views, Bootstrap 5     |
| Backend      | ASP.NET MVC, Entity Framework Core   |
| Database     | Microsoft SQL Server                 |
| Audit Log    | `ActivityLog` for all key operations |

- Separation of concerns via MVC architecture (Models, Views, Controllers)
- Entity Framework Code First with normalized relational database schema
- Git-based collaboration using feature branches (e.g., `vincent`, `gerry`) merged into `forge` for integration and testing

📊 [View System Architecture Diagram (draw.io)](https://drive.google.com/file/d/1-Lf5Uhqk5jsyYNi0-iZWKJOzIQTpDk-s/view?usp=sharing)

---

## Figma-Aligned UI Modules

### 1. **Dashboard**

- Real-time statistics: today’s enrollments, weekly totals
- Most enrolled courses (e.g., *Computer Science – 1250*, *Nursing – 150*)
- Total faculty count
- Activity feed with audit trail showing:
  - New enrollments
  - Course completions
  - Student registrations

> **Implemented by**: *Mercy Migendi* and *Brian Vuhuga*

---

### 2. **Register**

- **Add Student Form**:
  - Captures: Full name, email, gender, date of birth, national ID, county, phone number, admission number, and campus
  - Relational dropdowns for: gender, department, program, level, and campus
- **Student List**:
  - Paginated, searchable list showing department, progress, academic year
- **Enrollments**:
  - Enroll student into one or more courses
  - Bulk enrollment supported:
    - Multiple students to one course
    - All students to all courses
    - Filtered by program or academic year

> **Implemented by**: *Gerry Migiro*, *Elvis Karinge*  
> **Extended by**: *Elvis Karinge* – for enhanced student detail capture

---

### 3. **Courses & Academic Configuration (Dropdown Navigation)**

The "Courses" navigation now includes a **dropdown menu** that leads to modular sub-pages for configuring all academic metadata. This makes the platform scalable and eliminates hardcoded values.

#### Submodules:

- **Course List**
  - Accordion UI grouped by department
  - Each course shows: name, level, exam body, study status
- **Add Course Form**
  - Relational dropdowns for: department, program, exam body, level, campus, study mode, and study status
- **Departments**
- **Programs**
- **Exam Bodies**
- **Campuses**
- **Study Modes**
- **Study Levels**

Each submodule allows administrators to manage institutional data dynamically from the UI, reflecting updates immediately throughout the system.

> **Implemented by**: *Vincent Omondi Owuor* and *Gerry Migiro*

---

### 4. **Reports**

- Dynamic filters:
  - Department
  - Course
  - Faculty
  - Year
  - Enrollment Status
- Report Output Includes:
  - Student ID
  - Course
  - Department
  - Exam score
  - Grade
  - Transcript status
- Export functionality (CSV, PDF) currently in development

> **[In Development]**

---

## Database Schema Overview

EduCore uses a **normalized SQL Server** schema with referential integrity, foreign key constraints, and support for auditability. The system is extensible, ensuring new data types or structures can be added without affecting core functionality.

### Main Entities

- `Students`: Stores biodata, contact information, department, gender, program, and campus
- `Courses`: Defines academic offerings and links to departments, programs, levels, study modes, campuses, and exam bodies
- `Enrollments`: Many-to-many relationship between students and courses, tracking enrollment status
- `ActivityLog`: Captures entity operations (e.g., "Student Registered", "Course Enrolled") with user reference and timestamp

### Lookup Tables

- `Departments`
- `Programs`
- `ExamBodies`
- `Campuses`
- `StudyModes`
- `StudyLevels`
- `Genders`
- `Counties`
- `StudyStatuses`

### Entity Relationship Highlights

- **Students ↔ Enrollments ↔ Courses** (many-to-many)
- **Courses** reference multiple normalized lookup entities
- **Enrollments** use composite primary key (`StudentID`, `CourseID`) to enforce uniqueness
- **Audit Logs** are linked to user actions for traceability

---

## Feature Map

| Feature                          | Status       | Notes                                                                 |
|----------------------------------|--------------|-----------------------------------------------------------------------|
| Student Registration             | ✅ Complete  | Extended to include ID number, county, campus, and contact details   |
| Course Creation + Listing        | ✅ Complete  | Relational form fields and department-grouped UI                     |
| Academic Configuration Dropdown  | ✅ Complete  | Dynamic CRUD for all academic lookup tables                          |
| Enrollment (Single/Bulk)         | ✅ Complete  | Batch assignments supported via filters and dropdown logic           |
| Dashboard Metrics & Activities   | 🔄 In Dev    | Audit feed implemented; metrics integration in progress              |
| Reports Filtering & Export       | 🔄 In Dev    | Filtering complete; CSV/PDF export pending                           |
| Data Normalization               | ✅ Complete  | All form inputs reference backend lookup tables                      |
| Accessibility (WCAG 2.2 AA)      | ✅ Compliant | Semantic HTML, ARIA attributes, proper form labels                   |

---

## Development Standards

| Area               | Enforcement Details                                                     |
|--------------------|--------------------------------------------------------------------------|
| **Branching**      | Git feature branches → `forge` integration branch                        |
| **Commits**        | Conventional format: `feat:`, `fix:`, `refactor:`, etc.                  |
| **Code Reviews**   | Mandatory peer reviews for all pull requests                             |
| **Security**       | Form validation, SQL injection protection, EF-safe queries only          |
| **Testing**        | Manual UI walkthroughs, DB constraints, and schema validation            |
| **Accessibility**  | Conformance to WCAG 2.2 AA via semantic HTML, ARIA roles, tab order      |

---

## Team Roles & Division of Work

| Name                    | Role            | Area of Focus                                         |
|-------------------------|------------------|--------------------------------------------------------|
| **Vincent Omondi**      | Scrum Master     | Course module, academic configuration dropdowns        |
| Gerry Migiro            | Developer        | Student registration, dropdown integrations            |
| Elvis Karinge           | Developer        | Enhanced student form, bulk enrollment logic           |
| Mercy Migendi           | Developer        | Dashboard UI and page composition                      |
| Brian Vuhuga            | Developer        | Dashboard logic, real-time metric calculations         |

> Project supervised by **Mr. Nick Silver**

---
 
