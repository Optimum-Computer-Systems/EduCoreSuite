# EduCore – Academic Enrollment and Course Management System

### Supervisor: Nick Silver  

---

## Executive Summary

**EduCore** is a modular, data-driven university enrollment and academic management platform. It enables administrators to register students, manage courses, assign enrollments, and generate academic reports, all within a clean, accessible, and normalized system. The application adheres to formal software engineering practices using the ASP.NET MVC framework, Entity Framework ORM, and Microsoft SQL Server as the persistence layer.

The system reflects the institution’s operations as prototyped in the official **Figma wireframes** and is supervised by **Mr. Nick Silver**.

🔗 [View Figma Prototype](https://www.figma.com/proto/k3kXH5IK77TaCMqGO3AAiK/EduCore-UI-System-%E2%80%93-Design-v1?node-id=50-2&t=mNp8QHc2rj79fd4a-1)

---

## System Architecture

| Layer        | Technology                          |
|--------------|--------------------------------------|
| Frontend     | ASP.NET Razor Views, Bootstrap 5     |
| Backend      | ASP.NET MVC, Entity Framework        |
| Database     | Microsoft SQL Server                 |
| Audit Log    | `ActivityLog` for all key operations |

- MVC separation of concerns (Models, Views, Controllers)
- Code-first Entity Framework with normalized relational schema
- Git workflows: feature branches (e.g., `vincent`, `gerry`) merged into `forge` for integration

📊 [View System Architecture Diagram (draw.io)](https://drive.google.com/file/d/1-Lf5Uhqk5jsyYNi0-iZWKJOzIQTpDk-s/view?usp=sharing)

---

## Figma-Aligned UI Modules

### 1. **Dashboard**

- Real-time statistics: Today’s enrollments, weekly totals
- Top enrolled courses (e.g., *Computer Science – 1250*, *Nursing – 150*)
- Total faculty count
- Activity feed with audit trail: new enrollments, course completions, student registrations

> **Implemented by**: *Mercy Migendi* and  *Brian Vuhuga* 

---

### 2. **Register**

- **Add Student** form: Full name, email, gender, date of birth, admission number
- **Student List**: Paginated, searchable, showing department, progress, year
- **Enrollments**:
  - Enroll a student in a course
  - Bulk enrollment: multiple students to one course, or all students to all courses

> **Implemented by**: *Gerry Migiro*, *Elvis Karinge*

---

### 3. **Courses**

- **Course List**:
  - Accordion UI grouped by department
  - Shows course name, level, exam body, study status

- **Add Course Form**:
  - Fields: course name, department, programme, level, exam body, study status, campus
  - Normalized dropdowns linked to backend reference tables

> **Implemented by**: *Vincent Omondi Owuor*

---

### 4. **Reports**

- Dynamic filtering:
  - Department, course, faculty, year, status
- Generated reports include:
  - Student ID, course, department, exam score, grade, transcript status

> **[In Development]**

---

## Database Schema Overview

EduCore implements a normalized SQL Server database with full referential integrity. Key entities:

### `Students`
Captures biodata, academic affiliation, gender, and campus.

### `Courses`
Defines academic offerings and links to programmes, levels, departments, and delivery modes.

### `Enrollments`
Many-to-many table connecting students to courses with status tracking (`Enrolled`, `Completed`, `Dropped`).

### Lookup Tables
- `Departments`, `Programmes`, `Levels`, `Campuses`
- `Genders`, `StudyStatuses`, `ExamBodies`

### `ActivityLog`
Tracks student and course-related events with timestamps, action type, and references.

#### ER Relationship Highlights:

- Students ↔ Enrollments ↔ Courses (Many-to-Many via Enrollments)
- Programmes, Departments, Levels, Campuses used as foreign key lookups
- Composite key on `(StudentID, CourseID)` in `Enrollments` ensures uniqueness

---

## Feature Map

| Feature                          | Status       | Notes                                                                 |
|----------------------------------|--------------|-----------------------------------------------------------------------|
| Student Registration             | Complete     | Validates fields, links to gender, level, programme                   |
| Course Creation + Listing        | Complete     | Accordion design by department, relational backend integration        |
| Enrollment (Single/Bulk)         | Complete     | Bulk enrollment logic via dropdown filtering and email batch input    |
| Dashboard Metrics & Activities   | In Dev       | Fetches recent data, displays live counters, tracks audit logs        |
| Reports Filtering                | In Dev       | Filterable UI scaffold completed, export/download logic pending       |
| Data Normalization               | Complete     | All dropdowns reference normalized tables                             |
| Accessibility (WCAG 2.2 AA)      | Compliant    | Semantic HTML, ARIA support, proper label associations                |

---

## Development Standards

- **Branching Model**: Feature branches → `forge` for staging
- **Commits**: Conventional commits (`feat:`, `fix:`, `refactor:`)
- **Code Reviews**: Peer reviews required for all merges
- **Security**: Input validation, SQL constraints, no direct SQL execution
- **Testing**: Manual UI testing + DB-level constraint verification

---

## Team Roles & Division of Work

| Name                  | Role            | Area of Focus                       |
|-----------------------|------------------|--------------------------------------|
| **Vincent Omondi**    | Scrum Master     | Course Module                        |
| Mercy Migendi         | Developer        | Dashboard UI                         |
| Brian Vuhuga          | Developer        | Dashboard Logic, Metrics             |
| Gerry Migiro          | Developer        | Student Registration                 |
| Elvis Karinge         | Developer        | Enrollment Workflow                  |

Project supervised by **Mr. Nick Silver**
