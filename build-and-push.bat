@echo off
echo ====================================
echo PMDG B737 PAX Chatter - Build & Push Script
echo ====================================
echo.

:: Check if git is installed
git --version >nul 2>&1
if errorlevel 1 (
    echo ERROR: Git is not installed or not in PATH
    echo Please install Git from https://git-scm.com/download/win
    pause
    exit /b 1
)

:: Check if dotnet is installed
dotnet --version >nul 2>&1
if errorlevel 1 (
    echo ERROR: .NET is not installed or not in PATH
    echo Please install .NET 8.0 SDK from https://dotnet.microsoft.com/download/dotnet/8.0
    pause
    exit /b 1
)

:: Step 1: Build the project
echo [1/4] Building project...
dotnet build -c Release
if errorlevel 1 (
    echo ERROR: Build failed
    pause
    exit /b 1
)
echo Build successful!
echo.

:: Step 2: Stage all changes
echo [2/4] Staging changes...
git add .
if errorlevel 1 (
    echo ERROR: Failed to stage changes
    pause
    exit /b 1
)
echo Changes staged!
echo.

:: Step 3: Commit changes
echo [3/4] Committing changes...
set /p commit_msg="Enter commit message: "
if "%commit_msg%"=="" set commit_msg="Update project"

git commit -m "%commit_msg%"
if errorlevel 1 (
    echo No changes to commit
    goto skip_push
)
echo Commit successful!
echo.

:: Step 4: Push to GitHub
echo [4/4] Pushing to GitHub...
git push
if errorlevel 1 (
    echo ERROR: Push failed
    echo Make sure you have:
    echo 1. GitHub CLI installed and authenticated, OR
    echo 2. SSH keys set up for your GitHub account
    pause
    exit /b 1
)
echo Push successful!
echo.

:skip_push
echo ====================================
echo Process completed successfully!
echo ====================================
pause