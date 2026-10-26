using System;
using System.Windows.Forms;

namespace ImportApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form form = new Form
            {
                Text = "Import",
                Width = 400,
                Height = 200,
                StartPosition = FormStartPosition.CenterScreen
            };

            Button importButton = new Button
            {
                Text = "Import",
                Width = 100,
                Height = 35,
                Left = 150,
                Top = 60
            };

            importButton.Click += (sender, e) =>
            {
                MessageBox.Show("Import clicked!");
            };

            form.Controls.Add(importButton);

            Application.Run(form);
        }
    }
}

In Visual Studio

Create a Windows Forms App project targeting .NET, replace the contents of Program.cs with the code above, and build the project. Visual Studio will produce the executable in the project's bin\Debug or bin\Release folder.

If you want, I can also make the Import button open a file-selection dialog and let the user choose a file to import.
