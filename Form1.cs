using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.FlightSimulator.SimConnect;
using NAudio.Wave;
using System.IO;

namespace PAX_Chatter;

public partial class Form1 : Form
{
    SimConnect? simconnect = null;
    const int WM_USER_SIMCONNECT = 0x0402;
    WaveOutEvent? outputDevice;
    AudioFileReader? audioFile;
    
    bool isPlaying = false;
    bool manualMuted = false;
    double lastSimTime = 0;
    int pauseThreshold = 0;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct CameraData { 
        public double CameraState; 
        public double SimTime; 
    }

    enum DEFINITION { MyData = 1 }
    enum REQUEST { MyRequest = 1 }

    public Form1() {
        this.Size = new Size(300, 180);
        this.Text = "PAX CHATTER";
        this.BackColor = Color.FromArgb(25, 25, 25);
        this.TopMost = true;

        Button btnConnect = new Button { 
            Text = "START SYSTEM", 
            Dock = DockStyle.Top, Height = 70, 
            BackColor = Color.DodgerBlue, ForeColor = Color.White, 
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 12, FontStyle.Bold)
        };
        btnConnect.Click += (s, e) => { Connect(); };

        Button btnMute = new Button { 
            Text = "PAUSE CHATTER", 
            Dock = DockStyle.Bottom, Height = 50, 
            BackColor = Color.FromArgb(50, 50, 50), ForeColor = Color.White, 
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold)
        };
        btnMute.Click += (s, e) => {
            manualMuted = !manualMuted;
            btnMute.Text = manualMuted ? "RESUME" : "PAUSE CHATTER";
            if (manualMuted) { StopAudio(); }
        };

        this.Controls.Add(btnMute);
        this.Controls.Add(btnConnect);
    }

    private void Connect() {
        try {
            simconnect = new SimConnect("PaxChatter", this.Handle, WM_USER_SIMCONNECT, null, 0);
            
            simconnect.AddToDataDefinition(DEFINITION.MyData, "CAMERA STATE", "enum", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simconnect.AddToDataDefinition(DEFINITION.MyData, "SIMULATION TIME", "seconds", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simconnect.RegisterDataDefineStruct<CameraData>(DEFINITION.MyData);

            simconnect.OnRecvSimobjectDataBytype += (sender, data) => {
                CameraData cam = (CameraData)data.dwData[0];
                
                if (Math.Abs(cam.SimTime - lastSimTime) < 0.0001) { pauseThreshold++; } else { pauseThreshold = 0; }
                lastSimTime = cam.SimTime;

                int s = (int)cam.CameraState;
                bool isInternal = (s == 2 || s == 5 || s == 10);
                bool isSimActive = (pauseThreshold < 50);

                if (isInternal && isSimActive && !manualMuted) {
                    if (!isPlaying) { PlayAudio(); }
                    if (outputDevice != null) { 
                        outputDevice.Volume = (s == 2) ? 0.15f : 1.0f; 
                    }
                } else { 
                    StopAudio(); 
                }
                
                simconnect.RequestDataOnSimObjectType(REQUEST.MyRequest, DEFINITION.MyData, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
            };

            simconnect.RequestDataOnSimObjectType(REQUEST.MyRequest, DEFINITION.MyData, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
            this.Text = "PAX CHATTER - ACTIVE";
        } catch { 
            MessageBox.Show("Connection Error"); 
        }
    }

    private void PlayAudio() {
        if (isPlaying || !File.Exists("chatter.mp3")) return;
        try {
            outputDevice = new WaveOutEvent();
            audioFile = new AudioFileReader("chatter.mp3");
            outputDevice.PlaybackStopped += (s, e) => { isPlaying = false; };
            outputDevice.Init(audioFile);
            outputDevice.Play();
            isPlaying = true;
        } catch { }
    }

    private void StopAudio() {
        if (!isPlaying) return;
        if (outputDevice != null) { 
            outputDevice.Stop(); 
            outputDevice.Dispose(); 
            outputDevice = null; 
        }
        if (audioFile != null) { 
            audioFile.Dispose(); 
            audioFile = null; 
        }
        isPlaying = false;
    }

    protected override void WndProc(ref Message m) {
        if (m.Msg == WM_USER_SIMCONNECT && simconnect != null) { 
            simconnect.ReceiveMessage(); 
        }
        base.WndProc(ref m);
    }
}