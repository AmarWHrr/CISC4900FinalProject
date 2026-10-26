using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;

using System;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace VideoPlayer;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        Core.Initialize();

        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}

public class MainForm : Form
{
    private readonly LibVLC _libVLC; //library
    private readonly MediaPlayer _mediaPlayer; //player 
    private readonly VideoView _videoView; //viewport for video
    private readonly Button _importButton; //button

    public MainForm()
    {
        Text = "Simple Video Player";
        Width = 1000;
        Height = 700;
        StartPosition = FormStartPosition.CenterScreen;

        _libVLC = new LibVLC();
        _mediaPlayer = new MediaPlayer(_libVLC);

        // Video display
        _videoView = new VideoView
        {
            Dock = DockStyle.Fill,
            MediaPlayer = _mediaPlayer
        };

        // Import button
        _importButton = new Button
        {
            Text = "Import Video",
            Dock = DockStyle.Top,
            Height = 50
        };

        _importButton.Click += ImportButton_Click;

        Controls.Add(_videoView);
        Controls.Add(_importButton);
    }

    private void ImportButton_Click(object? sender, EventArgs e)
    {
        using OpenFileDialog dialog = new OpenFileDialog();

        dialog.Title = "Select a Video";
        dialog.Filter = //file types
            "Video Files|*.mp4;*.mkv;*.avi;*.mov;*.wmv;*.webm|" +
            "All Files|*.*";

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            using var media = new Media(
                _libVLC,
                new Uri(dialog.FileName)
            );

            _mediaPlayer.Play(media);
        }
    }

    protected override void OnFormClosed(FormClosedEventArgs e) //ensures things close right, no mem-leaks
    {
        _mediaPlayer.Stop();
        _mediaPlayer.Dispose();
        _libVLC.Dispose();

        base.OnFormClosed(e);
    }
}
```
