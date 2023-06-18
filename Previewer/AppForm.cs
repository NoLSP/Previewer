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
            //var openFileDialog1 = new FolderBrowserDialog();
            var openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                Manager.SourceFilesPaths = openFileDialog1.FileNames;
                p_SelectedSourceDirectoryLabel.Text = $"выбрано {openFileDialog1.FileNames.Length} файлов";
                p_SelectSourceDirectoryButton.Enabled = false;
                new Thread(() => Manager.LoadFiles()).Start();
                Thread.Sleep(100);
                new Thread(() =>
                {
                    var (currentVideo, videosMaxCount, currentFrame, framesMaxCount) = Manager.GetLoadFilesStatus();

                    ActionInFormThread(() =>
                    {
                        p_LoadFilesProgressBar.Maximum = videosMaxCount;
                        p_LoadFramesProgressBar.Maximum = framesMaxCount;
                        p_StatusesPanel.Visible = true;
                    });
                    
                    while (currentVideo != videosMaxCount || currentFrame != framesMaxCount)
                    {
                        ActionInFormThread(() => 
                        { 
                            p_LoadFilesProgressBar.Value = currentVideo; 
                            p_LoadFramesProgressBar.Maximum = framesMaxCount;
                            p_LoadFramesProgressBar.Value = currentFrame;
                        });

                        (currentVideo, videosMaxCount, currentFrame, framesMaxCount) = Manager.GetLoadFilesStatus();
                        Thread.Sleep(100);
                    }

                    ActionInFormThread(() =>{ p_StatusesPanel.Visible = false; });

                    var (videoName, videoNumber, videosCount, frameNumber, framesCount, frame) = Manager.GetCurrentFrame();
                    ActionInFormThread(() =>
                    {
                        SetCurrentFrame(videoName, videoNumber, videosCount, frameNumber, framesCount, frame);
                        p_FramesManipulationsPanel.Visible = true;
                        p_SelectSourceDirectoryButton.Enabled = true;
                    });
                }).Start();
            }
        }

        private void ActionInFormThread(Action action)
        {
            MethodInvoker m = new MethodInvoker(() =>
            {
                action();
            });
            this.Invoke(m);
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
            p_SelectSourceDirectoryButton.Enabled = false;
            p_ReloadVideoButton.Enabled = false;
            p_SaveFrameButton.Enabled = false;
            p_PreviousVideoButton.Enabled = false;
            p_NextVideoButton.Enabled = false;
            p_PreviousFrameButton.Enabled = false;
            p_NextFrameButton.Enabled = false;
            new Thread(() => Manager.ReloadCurrentVideo()).Start();
            Thread.Sleep(100);
            new Thread(() =>
            {
                var (currentVideo, videosMaxCount, currentFrame, framesMaxCount) = Manager.GetLoadFilesStatus();

                ActionInFormThread(() =>
                {
                    p_LoadFilesProgressBar.Maximum = videosMaxCount;
                    p_LoadFramesProgressBar.Maximum = framesMaxCount;
                    p_StatusesPanel.Visible = true;
                });

                while (currentVideo != videosMaxCount || currentFrame != framesMaxCount)
                {
                    ActionInFormThread(() =>
                    {
                        p_LoadFilesProgressBar.Value = currentVideo;
                        p_LoadFramesProgressBar.Maximum = framesMaxCount;
                        p_LoadFramesProgressBar.Value = currentFrame;
                    });

                    (currentVideo, videosMaxCount, currentFrame, framesMaxCount) = Manager.GetLoadFilesStatus();
                    Thread.Sleep(100);
                }

                var (videoName, videoNumber, videosCount, frameNumber, framesCount, frame) = Manager.GetCurrentFrame();
                ActionInFormThread(() =>
                {
                    p_StatusesPanel.Visible = false;
                    SetCurrentFrame(videoName, videoNumber, videosCount, frameNumber, framesCount, frame);
                    p_FramesManipulationsPanel.Visible = true;
                    p_SelectSourceDirectoryButton.Enabled = true;
                    p_ReloadVideoButton.Enabled = true;
                    p_SaveFrameButton.Enabled = true;
                    p_PreviousVideoButton.Enabled = true;
                    p_NextVideoButton.Enabled = true;
                    p_PreviousFrameButton.Enabled = true;
                    p_NextFrameButton.Enabled = true;
                });
            }).Start();
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