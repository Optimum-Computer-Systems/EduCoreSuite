@echo off
echo Setting up Counties and SubCounties tables...

REM Get SQL Server instance name
set /p instance="Enter SQL Server instance name (e.g., localhost or .\SQLEXPRESS): "

REM Get database name
set /p database="Enter database name (e.g., ForgeDB): "

REM Run the SQL script
echo Running SQL script...
sqlcmd -S %instance% -d %database% -i setup_counties_subcounties.sql

if %ERRORLEVEL% EQU 0 (
    echo Setup completed successfully.
    echo.
    echo If you want to load the complete set of Kenyan counties and subcounties,
    echo run the following command:
    echo sqlcmd -S %instance% -d %database% -i complete_counties_subcounties.sql
) else (
    echo Error running SQL script. Please check your SQL Server instance and database name.
)

pause