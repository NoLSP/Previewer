namespace Previewer
{
    public partial class AppForm : Form
    {
        private DateTime ClearSaveExceptionDateTime = DateTime.MinValue;

        public AppForm()
        {
            InitializeComponent();

            var timer = new System.Windows.Forms.Timer();
            timer.Tick += (object? sender, EventArgs e) =>
            {
                if (ClearSaveExceptionDateTime < DateTime.UtcNow)
                    p_SaveFrameStatusLabel.Text = "";
            };
            timer.Interval = 1000;
            timer.Start();
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
                Thread.Sleep(100);
                var (current, max) = Manager.GetLoadFilesStatus();
                p_LoadFilesProgressBar.Maximum = max;
                this.Controls.Add(p_LoadFilesProgressBar);
                while (current != max)
                {
                    p_LoadFilesProgressBar.Value = current;
                    (current, max) = Manager.GetLoadFilesStatus();
                }
                this.Controls.Remove(p_LoadFilesProgressBar);

                var (videoName, videoNumber, videosCount, frameNumber, framesCount, frame) = Manager.GetCurrentFrame();
                SetCurrentFrame(videoName, videoNumber, videosCount, frameNumber, framesCount, frame);
                p_PreviousFrameButton.Visible = true;
                p_NextFrameButton.Visible = true;
                p_SaveFrameButton.Visible = true;
                p_NextVideoButton.Visible = true;
                p_PreviousVideoButton.Visible = true;
                p_ReloadVideoButton.Visible = true;
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

        private void PreviousFrameButton_Click(object sender, EventArgs e)
        {

            var (videoName, videoNumber, videosCount, frameNumber, framesCount, frame) = Manager.GetPreviousFrame();
            SetCurrentFrame(videoName, videoNumber, videosCount, frameNumber, framesCount, frame);
        }

        private void NextFrameButton_Click(object sender, EventArgs e)
        {
            var (videoName, videoNumber, videosCount, frameNumber, framesCount, frame) = Manager.GetNextFrame();
            SetCurrentFrame(videoName, videoNumber, videosCount, frameNumber, framesCount, frame);
        }

        private void PreviousVideoButton_Click(object sender, EventArgs e)
        {
            var (videoName, videoNumber, videosCount, frameNumber, framesCount, frame) = Manager.GetPreviousVideo();
            SetCurrentFrame(videoName, videoNumber, videosCount, frameNumber, framesCount, frame);
        }

        private void NextVideoButton_Click(object sender, EventArgs e)
        {
            var (videoName, videoNumber, videosCount, frameNumber, framesCount, frame) = Manager.GetNextVideo();
            SetCurrentFrame(videoName, videoNumber, videosCount, frameNumber, framesCount, frame);
        }

        private void ReloadVideoButton_Click(object sender, EventArgs e)
        {
            Manager.ReloadCurrentVideo();
            var (videoName, videoNumber, videosCount, frameNumber, framesCount, frame) = Manager.GetCurrentFrame();
            SetCurrentFrame(videoName, videoNumber, videosCount, frameNumber, framesCount, frame);
        }

        private void SetCurrentFrame(string videoName, int videoNumber, int videosCount, int frameNumber, int framesCount, Bitmap frame)
        {
            p_FramePictureBox.Image = frame;
            p_FramePictureLabel.Text = $"Video: {videoName}, video: {videoNumber}/{videosCount}, frame: {frameNumber}/{framesCount}";
        }

        private void SaveFrameButton_Click(object sender, EventArgs e)
        {
            if(Manager.SaveCurrentFrame(out var reason))
            {
                p_SaveFrameStatusLabel.ForeColor = Color.Green;
                p_SaveFrameStatusLabel.Text = "File saved.";
            }
            else
            {
                p_SaveFrameStatusLabel.ForeColor = Color.Red;
                p_SaveFrameStatusLabel.Text = reason;
            }

            ClearSaveExceptionDateTime = DateTime.UtcNow.AddSeconds(3);
        }
    }
}