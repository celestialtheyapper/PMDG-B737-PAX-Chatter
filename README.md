# PMDG B737 PAX Chatter

A Windows Forms application that plays passenger chatter audio for PMDG 737 in Microsoft Flight Simulator.

## Features
- Real-time audio playback based on simulation state
- Pause/resume functionality
- Compact overlay interface
- SimConnect integration

## Requirements
- Microsoft Flight Simulator 2020
- PMDG 737 aircraft
- .NET 8.0 Runtime (or .NET 6/7)
- Windows 10/11

## Building
```bash
# Clone repository
git clone https://github.com/celestialtheyapper/PMDG-B737-PAX-Chatter.git
cd PMDG-B737-PAX-Chatter

# Build
dotnet build -c Release

# Run
dotnet run --configuration Release
```

## Usage
1. Build the project
2. Copy `Microsoft.FlightSimulator.SimConnect.dll` to `bin/Release/net8.0-windows/` (from MSFS installation)
3. Run the application
4. Click "START SYSTEM" to begin

## Project Structure
```
PAX Chatter/
├── Form1.cs              # Main form logic
├── Form1.Designer.cs     # Form designer code
├── Program.cs            # Application entry point
├── PAX Chatter.csproj    # Project configuration
├── chatter.mp3           # Audio asset
├── Microsoft.FlightSimulator.SimConnect.dll  # SimConnect API
└── README.md             # This file
```

## License
MIT License