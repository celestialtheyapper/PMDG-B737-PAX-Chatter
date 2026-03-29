# Developer Quick Start Guide

## For Windows Users

### Easy Method: Use the Build Script

1. **Download the script**: `build-and-push.bat`
2. **Double-click** the script to run it
3. **Enter a commit message** when prompted
4. **Done!** The script will:
   - Build the project
   - Stage all changes
   - Commit with your message
   - Push to GitHub

### Manual Method (if script fails)

```batch
# 1. Build the project
dotnet build -c Release

# 2. Stage changes
git add .

# 3. Commit
git commit -m "Your message here"

# 4. Push
git push
```

## Prerequisites (one-time setup)

### 1. Install Git
Download from: https://git-scm.com/download/win
- Run the installer
- Use default settings
- Restart your computer if prompted

### 2. Install .NET 8.0 SDK
Download from: https://dotnet.microsoft.com/download/dotnet/8.0
- Choose "SDK" (not Runtime)
- Run the installer
- Restart your computer if prompted

### 3. GitHub Authentication (choose one)

#### Option A: GitHub CLI (Recommended)
```powershell
# Install GitHub CLI
# Download from: https://github.com/cli/cli/releases

# Authenticate
gh auth login
```

#### Option B: Personal Access Token
1. Go to GitHub → Settings → Developer settings → Personal access tokens
2. Generate new token with "repo" scope
3. Use in Git:
```powershell
git remote set-url origin https://USERNAME:TOKEN@github.com/celestialtheyapper/PMDG-B737-PAX-Chatter.git
```

## Common Issues

### "Git is not recognized"
- Close and reopen command prompt after installing Git
- Or use Git Bash: right-click folder → "Git Bash Here"

### "Dotnet is not recognized"
- Restart computer after installing .NET
- Open new command prompt

### "Push failed"
- Check your GitHub authentication
- Make sure you have write access to the repository
- Try: `git pull` first, then `git push`

## Workflow

1. Make changes to the code
2. Run `build-and-push.bat`
3. Enter commit message
4. Push to GitHub automatically

That's it! No need to remember Git commands.