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

        private Panel p_SourceDirectoryPanel = new Panel();
        private Label p_SelectSourceDirectoryLabel = new Label();
        private Label p_SelectedSourceDirectoryLabel = new Label();
        private Button p_SelectSourceDirectoryButton = new Button();
        private Label p_SelectionFramesCountLabel = new Label();
        private NumericUpDown p_SelectionFramesCountNumericUpDown = new NumericUpDown();

        private Panel p_TargetDirectoryPanel = new Panel();
        private Label p_SelectTargetDirectoryLabel = new Label();
        private Label p_SelectedTargetDirectoryLabel = new Label();
        private Button p_SelectTargetDirectoryButton = new Button();

        private Panel p_StatusesPanel = new Panel();
        private Label p_LoadFilesLabel = new Label();
        private Label p_LoadFramesLabel = new Label();
        private ProgressBar p_LoadFilesProgressBar = new ProgressBar();
        private ProgressBar p_LoadFramesProgressBar = new ProgressBar();

        private Panel p_FramesManipulationsPanel = new Panel();
        private Label p_FramePictureLabel = new Label();
        private PictureBox p_FramePictureBox = new PictureBox();

        private Button p_NextFrameButton = new Button();
        private Button p_PreviousFrameButton = new Button();
        private Button p_NextVideoButton = new Button();
        private Button p_PreviousVideoButton = new Button();

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
            this.ClientSize = new System.Drawing.Size(850, 680);
            this.Text = "Previewer";

            #region Source files

            p_SourceDirectoryPanel.Size = new Size(400, 50);
            p_SourceDirectoryPanel.Location = new Point(10, 10);
            //p_SourceDirectoryPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(p_SourceDirectoryPanel);

            p_SelectSourceDirectoryLabel.Size = new Size(100, 20);
            p_SelectSourceDirectoryLabel.Location = new Point(0, 0);
            p_SelectSourceDirectoryLabel.Text = "Source files:";
            p_SourceDirectoryPanel.Controls.Add(p_SelectSourceDirectoryLabel);

            p_SelectedSourceDirectoryLabel.Size = new Size(300, 20);
            p_SelectedSourceDirectoryLabel.Location = new Point(100, 0);
            p_SourceDirectoryPanel.Controls.Add(p_SelectedSourceDirectoryLabel);

            p_SelectSourceDirectoryButton.Size = new Size(100, 30);
            p_SelectSourceDirectoryButton.Location = new Point(0, 20);
            p_SelectSourceDirectoryButton.Text = "Select";
            p_SelectSourceDirectoryButton.Click += SelectSourceDirectoryButton_Click;
            p_SourceDirectoryPanel.Controls.Add(p_SelectSourceDirectoryButton);

            p_SelectionFramesCountLabel.Size = new Size(100, 20);
            p_SelectionFramesCountLabel.Location = new Point(102, 25);
            p_SelectionFramesCountLabel.Text = "Frames count:";
            p_SourceDirectoryPanel.Controls.Add(p_SelectionFramesCountLabel);

            p_SelectionFramesCountNumericUpDown.Size = new Size(80, 30);
            p_SelectionFramesCountNumericUpDown.Location = new Point(204, 25);
            p_SelectionFramesCountNumericUpDown.Value = 10;
            p_SourceDirectoryPanel.Controls.Add(p_SelectionFramesCountNumericUpDown);

            #endregion

            #region Target directory

            p_TargetDirectoryPanel.Size = new Size(400, 50);
            p_TargetDirectoryPanel.Location = new Point(410, 10);
            //p_TargetDirectoryPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(p_TargetDirectoryPanel);

            p_SelectTargetDirectoryLabel.Size = new Size(100, 20);
            p_SelectTargetDirectoryLabel.Location = new Point(0, 0);
            p_SelectTargetDirectoryLabel.Text = "Target directory:";
            p_TargetDirectoryPanel.Controls.Add(p_SelectTargetDirectoryLabel);

            p_SelectedTargetDirectoryLabel.Size = new Size(300, 50);
            p_SelectedTargetDirectoryLabel.Location = new Point(100, 0);
            p_TargetDirectoryPanel.Controls.Add(p_SelectedTargetDirectoryLabel);

            p_SelectTargetDirectoryButton.Size = new Size(100, 30);
            p_SelectTargetDirectoryButton.Location = new Point(0, 20);
            p_SelectTargetDirectoryButton.Text = "Select";
            p_SelectTargetDirectoryButton.Click += SelectTargetDirectoryButton_Click;
            p_TargetDirectoryPanel.Controls.Add(p_SelectTargetDirectoryButton);

            #endregion

            #region Statuses

            p_StatusesPanel.Size = new Size(500, 20);
            p_StatusesPanel.Location = new Point(10, 65);
            //p_StatusesPanel.BorderStyle = BorderStyle.FixedSingle;
            p_StatusesPanel.Visible = false;
            this.Controls.Add(p_StatusesPanel);

            p_LoadFilesLabel.Size = new Size(80, 20);
            p_LoadFilesLabel.Location = new Point(0, 0);
            p_LoadFilesLabel.Text = "Loading files:";
            p_StatusesPanel.Controls.Add(p_LoadFilesLabel);

            p_LoadFilesProgressBar.Size = new Size(150, 20);
            p_LoadFilesProgressBar.Location = new Point(80, 0);
            p_LoadFilesProgressBar.Maximum = 1;
            p_LoadFilesProgressBar.Value = 0;
            p_StatusesPanel.Controls.Add(p_LoadFilesProgressBar);

            p_LoadFramesLabel.Size = new Size(100, 20);
            p_LoadFramesLabel.Location = new Point(240, 0);
            p_LoadFramesLabel.Text = "Loading frames:";
            p_StatusesPanel.Controls.Add(p_LoadFramesLabel);

            p_LoadFramesProgressBar.Size = new Size(150, 20);
            p_LoadFramesProgressBar.Location = new Point(340, 0);
            p_LoadFramesProgressBar.Maximum = 1;
            p_LoadFramesProgressBar.Value = 0;
            p_StatusesPanel.Controls.Add(p_LoadFramesProgressBar);

            #endregion

            #region Frames manipulations

            p_FramesManipulationsPanel.Size = new Size(830, 580);
            p_FramesManipulationsPanel.Location = new Point(10, 85);
            //p_FramesManipulationsPanel.BorderStyle = BorderStyle.FixedSingle;
            p_FramesManipulationsPanel.Visible = false;
            this.Controls.Add(p_FramesManipulationsPanel);

            p_FramePictureLabel.Size = new Size(300, 50);
            p_FramePictureLabel.Location = new Point(0, 0);
            //p_FramePictureLabel.BorderStyle = BorderStyle.FixedSingle;
            p_FramesManipulationsPanel.Controls.Add(p_FramePictureLabel);

            p_SaveFrameButton.Size = new Size(100, 40);
            p_SaveFrameButton.Location = new Point(310, 5);
            p_SaveFrameButton.Text = "Save";
            p_SaveFrameButton.Click += SaveFrameButton_Click;
            p_FramesManipulationsPanel.Controls.Add(p_SaveFrameButton);

            p_ReloadVideoButton.Size = new Size(100, 40);
            p_ReloadVideoButton.Location = new Point(420, 5);
            p_ReloadVideoButton.Text = "Reload video";
            p_ReloadVideoButton.Click += ReloadVideoButton_Click;
            p_FramesManipulationsPanel.Controls.Add(p_ReloadVideoButton);

            p_SaveFrameStatusLabel.Size = new Size(300, 50);
            p_SaveFrameStatusLabel.Location = new Point(530, 0);
            //p_SaveFrameStatusLabel.BorderStyle = BorderStyle.FixedSingle;
            p_FramesManipulationsPanel.Controls.Add(p_SaveFrameStatusLabel);

            p_PreviousVideoButton.Size = new Size(50, 40);
            p_PreviousVideoButton.Location = new Point(0, 55);
            p_PreviousVideoButton.Text = "<<";
            p_PreviousVideoButton.Click += PreviousVideoButton_Click;
            p_FramesManipulationsPanel.Controls.Add(p_PreviousVideoButton);

            p_PreviousFrameButton.Size = new Size(50, 40);
            p_PreviousFrameButton.Location = new Point(52, 55);
            p_PreviousFrameButton.Text = "<";
            p_PreviousFrameButton.Click += PreviousFrameButton_Click;
            p_FramesManipulationsPanel.Controls.Add(p_PreviousFrameButton);

            p_NextFrameButton.Size = new Size(50, 40);
            p_NextFrameButton.Location = new Point(104, 55);
            p_NextFrameButton.Text = ">";
            p_NextFrameButton.Click += NextFrameButton_Click;
            p_FramesManipulationsPanel.Controls.Add(p_NextFrameButton);

            p_NextVideoButton.Size = new Size(50, 40);
            p_NextVideoButton.Location = new Point(156, 55);
            p_NextVideoButton.Text = ">>";
            p_NextVideoButton.Click += NextVideoButton_Click;
            p_FramesManipulationsPanel.Controls.Add(p_NextVideoButton);

            p_FramePictureBox.Size = new Size(720, 480);
            p_FramePictureBox.Location = new Point(0, 100);
            p_FramePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            //p_FramePictureBox.BorderStyle = BorderStyle.FixedSingle;
            p_FramesManipulationsPanel.Controls.Add(p_FramePictureBox);

            #endregion
        }

        #endregion
    }
}