namespace Previewer
{
    public partial class AppForm : Form
    {
        public AppForm()
        {
            InitializeComponent();


        }

        private void SelectSourceDirectoryButton_Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new FolderBrowserDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                Manager.SourceFilesDirectoryPath = openFileDialog1.SelectedPath;
                p_SelectedSourceDirectoryLabel.Text = openFileDialog1.SelectedPath;
                new Thread(() => Manager.LoadFiles()).Start();
                Thread.Sleep(20);
                var (current, max) = Manager.GetLoadFilesStatus();
                p_LoadFilesProgressBar.Maximum = max;
                this.Controls.Add(p_LoadFilesProgressBar);
                while(current != max)
                {
                    p_LoadFilesProgressBar.Value = current;
                    (current, max) = Manager.GetLoadFilesStatus();
                }
                this.Controls.Remove(p_LoadFilesProgressBar);

                var (path, frame) = Manager.GetCurrentFrame();
                p_FramePictureBox.Image = frame;
            }
        }

        private void SelectTargetDirectoryButton_Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new FolderBrowserDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                Manager.TargetFilesDirectoryPath = openFileDialog1.SelectedPath;
                p_SelectedTargetDirectoryLabel.Text = openFileDialog1.SelectedPath;
            }
        }
    }
}