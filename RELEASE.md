# Release Process

## Creating a New Release

### Method 1: GitHub Web Interface (Recommended)
1. Go to your repository: https://github.com/celestialtheyapper/PMDG-B737-PAX-Chatter
2. Click on **Releases** → **Create a new release**
3. **Choose a tag**: Enter `v1.0.1` (or new version number)
4. **Release title**: `Version 1.0.1`
5. **Description**:
   ```
   Version 1.0.1

   Changes:
   - Bug fixes
   - Improvements
   - New features
   ```
6. Click **Publish release**

### Method 2: Command Line (PowerShell)
```powershell
# Create and push a new tag
git tag v1.0.1
git push origin v1.0.1

# GitHub Actions will automatically build and create the release
```

### Method 3: Using the Build Script
```batch
# The build script can also tag releases
# Edit the script to include tagging if needed
```

## Version Naming Convention
- **Major**: `v1.0.0` - Breaking changes
- **Minor**: `v1.1.0` - New features
- **Patch**: `v1.0.1` - Bug fixes

## What Happens Automatically
✅ **GitHub Actions** will:
- Build the project on Windows
- Create a release on GitHub
- Upload `PAX-Chatter-v1.0.1.exe` as an asset
- Mark the release as published

## Download for Users
After release, users can:
1. Go to **Releases** page
2. Download the `.exe` file
3. Run it directly (no installation needed)

## Build Artifacts Location
- Executable: `./publish/PAX Chatter.exe`
- Release asset: `PAX-Chatter-{version}.exe`

## Next Release
Just increment the version number and create a new tag!