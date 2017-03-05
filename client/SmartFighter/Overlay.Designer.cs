namespace SmartFighter {
    partial class Overlay {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.panel = new System.Windows.Forms.Panel();
            this.player2Score = new System.Windows.Forms.Label();
            this.player1Score = new System.Windows.Forms.Label();
            this.player2Info = new System.Windows.Forms.Label();
            this.player1Info = new System.Windows.Forms.Label();
            this.player1Name = new System.Windows.Forms.Label();
            this.player2Name = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.Transparent;
            this.panel.BackgroundImage = global::SmartFighter.Properties.Resources.smartfighter_bar_score;
            this.panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel.Controls.Add(this.player2Score);
            this.panel.Controls.Add(this.player1Score);
            this.panel.Controls.Add(this.player2Info);
            this.panel.Controls.Add(this.player1Info);
            this.panel.Controls.Add(this.player1Name);
            this.panel.Controls.Add(this.player2Name);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(800, 30);
            this.panel.TabIndex = 2;
            // 
            // player2Score
            // 
            this.player2Score.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.player2Score.Location = new System.Drawing.Point(433, 3);
            this.player2Score.Name = "player2Score";
            this.player2Score.Size = new System.Drawing.Size(40, 24);
            this.player2Score.TabIndex = 5;
            this.player2Score.Text = "0";
            this.player2Score.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // player1Score
            // 
            this.player1Score.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.player1Score.Location = new System.Drawing.Point(328, 3);
            this.player1Score.Name = "player1Score";
            this.player1Score.Size = new System.Drawing.Size(40, 24);
            this.player1Score.TabIndex = 4;
            this.player1Score.Text = "0";
            this.player1Score.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // player2Info
            // 
            this.player2Info.BackColor = System.Drawing.Color.Transparent;
            this.player2Info.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player2Info.ForeColor = System.Drawing.Color.White;
            this.player2Info.Location = new System.Drawing.Point(485, 3);
            this.player2Info.Name = "player2Info";
            this.player2Info.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.player2Info.Size = new System.Drawing.Size(300, 24);
            this.player2Info.TabIndex = 3;
            this.player2Info.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player1Info
            // 
            this.player1Info.BackColor = System.Drawing.Color.Transparent;
            this.player1Info.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player1Info.ForeColor = System.Drawing.Color.White;
            this.player1Info.Location = new System.Drawing.Point(15, 3);
            this.player1Info.Name = "player1Info";
            this.player1Info.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.player1Info.Size = new System.Drawing.Size(300, 24);
            this.player1Info.TabIndex = 2;
            // 
            // player1Name
            // 
            this.player1Name.BackColor = System.Drawing.Color.Transparent;
            this.player1Name.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.player1Name.ForeColor = System.Drawing.Color.White;
            this.player1Name.Location = new System.Drawing.Point(15, 3);
            this.player1Name.Name = "player1Name";
            this.player1Name.Size = new System.Drawing.Size(300, 24);
            this.player1Name.TabIndex = 0;
            // 
            // player2Name
            // 
            this.player2Name.BackColor = System.Drawing.Color.Transparent;
            this.player2Name.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.player2Name.ForeColor = System.Drawing.Color.White;
            this.player2Name.Location = new System.Drawing.Point(485, 3);
            this.player2Name.Name = "player2Name";
            this.player2Name.Size = new System.Drawing.Size(300, 24);
            this.player2Name.TabIndex = 1;
            this.player2Name.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Overlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(800, 30);
            this.ControlBox = false;
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(560, 0);
            this.Name = "Overlay";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Overlay";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Thistle;
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label player1Name;
        public System.Windows.Forms.Label player2Name;
        private System.Windows.Forms.Panel panel;
        public System.Windows.Forms.Label player1Info;
        public System.Windows.Forms.Label player2Info;
        public System.Windows.Forms.Label player1Score;
        public System.Windows.Forms.Label player2Score;
    }
}