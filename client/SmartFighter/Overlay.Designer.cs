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
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.player1layout = new System.Windows.Forms.FlowLayoutPanel();
            this.player1Label = new System.Windows.Forms.Label();
            this.player1Name = new System.Windows.Forms.Label();
            this.player2Layout = new System.Windows.Forms.FlowLayoutPanel();
            this.player2Label = new System.Windows.Forms.Label();
            this.player2Name = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.layout.SuspendLayout();
            this.player1layout.SuspendLayout();
            this.player2Layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.ColumnCount = 2;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layout.Controls.Add(this.player1layout, 0, 0);
            this.layout.Controls.Add(this.player2Layout, 1, 0);
            this.layout.Controls.Add(this.infoLabel, 0, 1);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.RowCount = 2;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layout.Size = new System.Drawing.Size(350, 50);
            this.layout.TabIndex = 0;
            // 
            // player1layout
            // 
            this.player1layout.Controls.Add(this.player1Label);
            this.player1layout.Controls.Add(this.player1Name);
            this.player1layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.player1layout.Location = new System.Drawing.Point(3, 3);
            this.player1layout.Name = "player1layout";
            this.player1layout.Size = new System.Drawing.Size(169, 19);
            this.player1layout.TabIndex = 2;
            // 
            // player1Label
            // 
            this.player1Label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.player1Label.AutoSize = true;
            this.player1Label.Location = new System.Drawing.Point(3, 0);
            this.player1Label.Name = "player1Label";
            this.player1Label.Size = new System.Drawing.Size(45, 13);
            this.player1Label.TabIndex = 0;
            this.player1Label.Text = "Player1:";
            // 
            // player1Name
            // 
            this.player1Name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.player1Name.AutoSize = true;
            this.player1Name.Location = new System.Drawing.Point(54, 0);
            this.player1Name.Name = "player1Name";
            this.player1Name.Size = new System.Drawing.Size(0, 13);
            this.player1Name.TabIndex = 1;
            // 
            // player2Layout
            // 
            this.player2Layout.Controls.Add(this.player2Label);
            this.player2Layout.Controls.Add(this.player2Name);
            this.player2Layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.player2Layout.Location = new System.Drawing.Point(178, 3);
            this.player2Layout.Name = "player2Layout";
            this.player2Layout.Size = new System.Drawing.Size(169, 19);
            this.player2Layout.TabIndex = 3;
            // 
            // player2Label
            // 
            this.player2Label.AutoSize = true;
            this.player2Label.Location = new System.Drawing.Point(3, 0);
            this.player2Label.Name = "player2Label";
            this.player2Label.Size = new System.Drawing.Size(45, 13);
            this.player2Label.TabIndex = 1;
            this.player2Label.Text = "Player2:";
            // 
            // player2Name
            // 
            this.player2Name.AutoSize = true;
            this.player2Name.Location = new System.Drawing.Point(54, 0);
            this.player2Name.Name = "player2Name";
            this.player2Name.Size = new System.Drawing.Size(0, 13);
            this.player2Name.TabIndex = 2;
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.layout.SetColumnSpan(this.infoLabel, 2);
            this.infoLabel.Location = new System.Drawing.Point(3, 25);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(0, 13);
            this.infoLabel.TabIndex = 4;
            // 
            // Overlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 50);
            this.ControlBox = false;
            this.Controls.Add(this.layout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(785, 0);
            this.Name = "Overlay";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Overlay";
            this.TopMost = true;
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            this.player1layout.ResumeLayout(false);
            this.player1layout.PerformLayout();
            this.player2Layout.ResumeLayout(false);
            this.player2Layout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label player1Label;
        private System.Windows.Forms.Label player2Label;
        private System.Windows.Forms.FlowLayoutPanel player1layout;
        private System.Windows.Forms.FlowLayoutPanel player2Layout;
        public System.Windows.Forms.Label infoLabel;
        public System.Windows.Forms.Label player1Name;
        public System.Windows.Forms.Label player2Name;
    }
}