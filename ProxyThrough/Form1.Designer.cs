namespace ProxyThrough
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.NameLabel = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.LineLabel1 = new System.Windows.Forms.Label();
            this.SetProxyButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.ListPopulatorTimer = new System.Windows.Forms.Timer(this.components);
            this.ProxyListView = new BrightIdeasSoftware.ObjectListView();
            this.PingAllButton = new System.Windows.Forms.Button();
            this.StatusUpdater = new System.Windows.Forms.Timer(this.components);
            this.CPSLabel = new System.Windows.Forms.Label();
            this.GithubLink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // NameLabel
            // 
            this.NameLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NameLabel.Font = new System.Drawing.Font("Consolas", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.NameLabel.Location = new System.Drawing.Point(71, 0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(188, 33);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "ProxyThrough v1.2";
            this.NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 346);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(751, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "StatusStrip";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(63, 17);
            this.toolStripStatusLabel1.Text = "Ready...";
            // 
            // LineLabel1
            // 
            this.LineLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LineLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LineLabel1.Location = new System.Drawing.Point(12, 50);
            this.LineLabel1.Name = "LineLabel1";
            this.LineLabel1.Size = new System.Drawing.Size(727, 1);
            this.LineLabel1.TabIndex = 2;
            this.LineLabel1.Text = "label2";
            // 
            // SetProxyButton
            // 
            this.SetProxyButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SetProxyButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SetProxyButton.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetProxyButton.Location = new System.Drawing.Point(323, 320);
            this.SetProxyButton.Name = "SetProxyButton";
            this.SetProxyButton.Size = new System.Drawing.Size(110, 23);
            this.SetProxyButton.TabIndex = 3;
            this.SetProxyButton.Text = "Set Proxy";
            this.SetProxyButton.UseVisualStyleBackColor = true;
            this.SetProxyButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.RefreshButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RefreshButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshButton.Location = new System.Drawing.Point(242, 320);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 4;
            this.RefreshButton.Text = "Update";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ListPopulatorTimer
            // 
            this.ListPopulatorTimer.Enabled = true;
            this.ListPopulatorTimer.Tick += new System.EventHandler(this.ListPopulatorTimer_Tick);
            // 
            // ProxyListView
            // 
            this.ProxyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProxyListView.FullRowSelect = true;
            this.ProxyListView.GridLines = true;
            this.ProxyListView.HeaderUsesThemes = true;
            this.ProxyListView.Location = new System.Drawing.Point(12, 59);
            this.ProxyListView.Name = "ProxyListView";
            this.ProxyListView.ShowCommandMenuOnRightClick = true;
            this.ProxyListView.ShowFilterMenuOnRightClick = false;
            this.ProxyListView.ShowHeaderInAllViews = false;
            this.ProxyListView.Size = new System.Drawing.Size(727, 255);
            this.ProxyListView.TabIndex = 6;
            this.ProxyListView.UseCompatibleStateImageBehavior = false;
            this.ProxyListView.UseExplorerTheme = true;
            this.ProxyListView.View = System.Windows.Forms.View.Details;
            // 
            // PingAllButton
            // 
            this.PingAllButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.PingAllButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PingAllButton.Location = new System.Drawing.Point(439, 320);
            this.PingAllButton.Name = "PingAllButton";
            this.PingAllButton.Size = new System.Drawing.Size(75, 23);
            this.PingAllButton.TabIndex = 7;
            this.PingAllButton.Text = "Ping All";
            this.PingAllButton.UseVisualStyleBackColor = true;
            this.PingAllButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // StatusUpdater
            // 
            this.StatusUpdater.Enabled = true;
            this.StatusUpdater.Tick += new System.EventHandler(this.StatusUpdater_Tick);
            // 
            // CPSLabel
            // 
            this.CPSLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CPSLabel.AutoSize = true;
            this.CPSLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CPSLabel.Location = new System.Drawing.Point(656, 351);
            this.CPSLabel.Name = "CPSLabel";
            this.CPSLabel.Size = new System.Drawing.Size(25, 13);
            this.CPSLabel.TabIndex = 8;
            this.CPSLabel.Text = "CPS";
            this.CPSLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // GithubLink
            // 
            this.GithubLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GithubLink.AutoSize = true;
            this.GithubLink.Cursor = System.Windows.Forms.Cursors.Default;
            this.GithubLink.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GithubLink.Location = new System.Drawing.Point(693, 351);
            this.GithubLink.Name = "GithubLink";
            this.GithubLink.Size = new System.Drawing.Size(43, 13);
            this.GithubLink.TabIndex = 9;
            this.GithubLink.TabStop = true;
            this.GithubLink.Text = "GitHub";
            this.GithubLink.VisitedLinkColor = System.Drawing.Color.Blue;
            this.GithubLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GithubLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(680, 351);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = " ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Logo
            // 
            this.Logo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Logo.Image = global::ProxyThrough.Properties.Resources.comet;
            this.Logo.Location = new System.Drawing.Point(39, 2);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(30, 28);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Logo.TabIndex = 10;
            this.Logo.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.NameLabel);
            this.panel1.Controls.Add(this.Logo);
            this.panel1.Location = new System.Drawing.Point(226, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(313, 34);
            this.panel1.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(751, 368);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GithubLink);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CPSLabel);
            this.Controls.Add(this.PingAllButton);
            this.Controls.Add(this.ProxyListView);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.SetProxyButton);
            this.Controls.Add(this.LineLabel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "ProxyThrough v1.2";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label LineLabel1;
        private System.Windows.Forms.Button SetProxyButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Timer ListPopulatorTimer;
        private BrightIdeasSoftware.ObjectListView ProxyListView;
        private System.Windows.Forms.Button PingAllButton;
        private System.Windows.Forms.Timer StatusUpdater;
        private System.Windows.Forms.Label CPSLabel;
        private System.Windows.Forms.LinkLabel GithubLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Panel panel1;
    }
}

