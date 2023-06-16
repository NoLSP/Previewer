namespace Previewer
{
    partial class AppForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Label p_SelectSourceDirectoryLabel = new Label();
        private Label p_SelectedSourceDirectoryLabel = new Label();
        private Button p_SelectSourceDirectoryButton = new Button();

        private Label p_SelectTargetDirectoryLabel = new Label();
        private Label p_SelectedTargetDirectoryLabel = new Label();
        private Button p_SelectTargetDirectoryButton = new Button();

        private ProgressBar p_LoadFilesProgressBar = new ProgressBar();

        private Label p_FramePictureLabel = new Label();
        private PictureBox p_FramePictureBox = new PictureBox();

        private Button p_NextFrameButton = new Button();
        private Button p_PreviousFrameButton = new Button();

        private Label p_SaveFrameStatusLabel = new Label();
        private Button p_SaveFrameButton = new Button();
        private Button p_ReloadVideoButton = new Button();


        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 1000);
            this.Text = "Previewer";

            p_SelectSourceDirectoryLabel.Size = new Size(210, 50);
            p_SelectSourceDirectoryLabel.Location = new Point(20, 20);
            p_SelectSourceDirectoryLabel.Text = "Chose the directory from which files will come:";
            this.Controls.Add(p_SelectSourceDirectoryLabel);

            p_SelectedSourceDirectoryLabel.Size = new Size(400, 50);
            p_SelectedSourceDirectoryLabel.Location = new Point(230, 20);
            this.Controls.Add(p_SelectedSourceDirectoryLabel);

            p_SelectSourceDirectoryButton.Size = new Size(100, 50);
            p_SelectSourceDirectoryButton.Location = new Point(630, 10);
            p_SelectSourceDirectoryButton.Text = "Выбрать";
            p_SelectSourceDirectoryButton.Click += SelectSourceDirectoryButton_Click;
            this.Controls.Add(p_SelectSourceDirectoryButton);

            p_SelectTargetDirectoryLabel.Size = new Size(210, 50);
            p_SelectTargetDirectoryLabel.Location = new Point(20, 100);
            p_SelectTargetDirectoryLabel.Text = "Chose the directory in which files will save:";
            this.Controls.Add(p_SelectTargetDirectoryLabel);

            p_SelectedTargetDirectoryLabel.Size = new Size(400, 50);
            p_SelectedTargetDirectoryLabel.Location = new Point(230, 100);
            this.Controls.Add(p_SelectedTargetDirectoryLabel);

            p_SelectTargetDirectoryButton.Size = new Size(100, 50);
            p_SelectTargetDirectoryButton.Location = new Point(630, 80);
            p_SelectTargetDirectoryButton.Text = "Выбрать";
            p_SelectTargetDirectoryButton.Click += SelectTargetDirectoryButton_Click;
            this.Controls.Add(p_SelectTargetDirectoryButton);

            p_LoadFilesProgressBar.Size = new Size(500, 20);
            p_LoadFilesProgressBar.Location = new Point(180, 150);

            p_FramePictureLabel.Size = new Size(300, 20);
            p_FramePictureLabel.Location = new Point(140, 180);
            this.Controls.Add(p_FramePictureLabel);

            p_SaveFrameButton.Size = new Size(100, 20);
            p_SaveFrameButton.Location = new Point(440, 180);
            p_SaveFrameButton.Text = "Save";
            p_SaveFrameButton.Click += SaveFrameButton_Click;
            p_SaveFrameButton.Visible = false;
            this.Controls.Add(p_SaveFrameButton);

            p_ReloadVideoButton.Size = new Size(100, 20);
            p_ReloadVideoButton.Location = new Point(550, 180);
            p_ReloadVideoButton.Text = "Reload video";
            p_ReloadVideoButton.Visible = false;
            p_ReloadVideoButton.Click += ReloadVideoButton_Click;
            this.Controls.Add(p_ReloadVideoButton);

            p_SaveFrameStatusLabel.Size = new Size(340, 20);
            p_SaveFrameStatusLabel.Location = new Point(660, 180);
            this.Controls.Add(p_SaveFrameStatusLabel);

            p_FramePictureBox.Size = new Size(720, 480);
            p_FramePictureBox.Location = new Point(140, 200);
            p_FramePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(p_FramePictureBox);

            p_PreviousFrameButton.Size = new Size(50, 100);
            p_PreviousFrameButton.Location = new Point(45, 390);
            p_PreviousFrameButton.Text = "<<";
            p_PreviousFrameButton.Click += PreviousFrameButton_Click;
            p_PreviousFrameButton.Visible = false;
            this.Controls.Add(p_PreviousFrameButton);

            p_NextFrameButton.Size = new Size(50, 100);
            p_NextFrameButton.Location = new Point(905, 390);
            p_NextFrameButton.Text = ">>";
            p_NextFrameButton.Visible = false;
            p_NextFrameButton.Click += NextFrameButton_Click;
            this.Controls.Add(p_NextFrameButton);
        }

        #endregion
    }
}