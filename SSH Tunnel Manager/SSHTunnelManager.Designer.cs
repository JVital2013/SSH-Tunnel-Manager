namespace SSH_Tunnel_Manager
{
    partial class SSHTunnelManager
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tunnelList = new DataGridView();
            addConnection = new Button();
            sshGroupBox = new GroupBox();
            connectButton = new Button();
            usernameLabel = new Label();
            hostLabel = new Label();
            usernameBox = new TextBox();
            hostBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)tunnelList).BeginInit();
            sshGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // tunnelList
            // 
            tunnelList.AllowUserToAddRows = false;
            tunnelList.AllowUserToDeleteRows = false;
            tunnelList.AllowUserToResizeRows = false;
            tunnelList.BackgroundColor = SystemColors.Control;
            tunnelList.BorderStyle = BorderStyle.Fixed3D;
            tunnelList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tunnelList.EditMode = DataGridViewEditMode.EditOnEnter;
            tunnelList.Location = new Point(6, 60);
            tunnelList.Margin = new Padding(2, 1, 2, 1);
            tunnelList.Name = "tunnelList";
            tunnelList.RowHeadersVisible = false;
            tunnelList.RowHeadersWidth = 82;
            tunnelList.Size = new Size(618, 262);
            tunnelList.TabIndex = 0;
            tunnelList.CellClick += tunnelList_CellClick;
            tunnelList.CurrentCellDirtyStateChanged += tunnelList_CurrentCellDirtyStateChanged;
            tunnelList.SelectionChanged += tunnelList_SelectionChanged;
            // 
            // addConnection
            // 
            addConnection.Location = new Point(6, 324);
            addConnection.Margin = new Padding(2, 1, 2, 1);
            addConnection.Name = "addConnection";
            addConnection.Size = new Size(618, 22);
            addConnection.TabIndex = 5;
            addConnection.Text = "Add Connection";
            addConnection.UseVisualStyleBackColor = true;
            addConnection.Click += addConnection_Click;
            // 
            // sshGroupBox
            // 
            sshGroupBox.Controls.Add(connectButton);
            sshGroupBox.Controls.Add(usernameLabel);
            sshGroupBox.Controls.Add(hostLabel);
            sshGroupBox.Controls.Add(usernameBox);
            sshGroupBox.Controls.Add(hostBox);
            sshGroupBox.Location = new Point(6, 6);
            sshGroupBox.Margin = new Padding(2, 1, 2, 1);
            sshGroupBox.Name = "sshGroupBox";
            sshGroupBox.Padding = new Padding(2, 1, 2, 1);
            sshGroupBox.Size = new Size(618, 46);
            sshGroupBox.TabIndex = 2;
            sshGroupBox.TabStop = false;
            sshGroupBox.Text = "SSH Connection";
            // 
            // connectButton
            // 
            connectButton.Location = new Point(339, 18);
            connectButton.Margin = new Padding(2, 1, 2, 1);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(276, 22);
            connectButton.TabIndex = 0;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(160, 21);
            usernameLabel.Margin = new Padding(2, 0, 2, 0);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(60, 15);
            usernameLabel.TabIndex = 3;
            usernameLabel.Text = "Username";
            // 
            // hostLabel
            // 
            hostLabel.AutoSize = true;
            hostLabel.Location = new Point(9, 21);
            hostLabel.Margin = new Padding(2, 0, 2, 0);
            hostLabel.Name = "hostLabel";
            hostLabel.Size = new Size(32, 15);
            hostLabel.TabIndex = 2;
            hostLabel.Text = "Host";
            // 
            // usernameBox
            // 
            usernameBox.Location = new Point(228, 20);
            usernameBox.Margin = new Padding(2, 1, 2, 1);
            usernameBox.Name = "usernameBox";
            usernameBox.Size = new Size(110, 23);
            usernameBox.TabIndex = 2;
            // 
            // hostBox
            // 
            hostBox.Location = new Point(46, 20);
            hostBox.Margin = new Padding(2, 1, 2, 1);
            hostBox.Name = "hostBox";
            hostBox.Size = new Size(110, 23);
            hostBox.TabIndex = 1;
            // 
            // SSHTunnelManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(631, 352);
            Controls.Add(sshGroupBox);
            Controls.Add(addConnection);
            Controls.Add(tunnelList);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2, 1, 2, 1);
            MaximizeBox = false;
            Name = "SSHTunnelManager";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SSH Tunnel Manager";
            Load += SSHTunnelManager_Load;
            ((System.ComponentModel.ISupportInitialize)tunnelList).EndInit();
            sshGroupBox.ResumeLayout(false);
            sshGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button addConnection;
        private GroupBox sshGroupBox;
        private TextBox usernameBox;
        private TextBox hostBox;
        private Label usernameLabel;
        private Label hostLabel;
        private Button connectButton;
        internal DataGridView tunnelList;
    }
}
