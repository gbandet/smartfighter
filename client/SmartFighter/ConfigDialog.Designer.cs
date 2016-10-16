namespace SmartFighter {
    partial class ConfigDialog {
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
            this.apiLabel = new System.Windows.Forms.Label();
            this.apiText = new System.Windows.Forms.TextBox();
            this.player2Panel = new System.Windows.Forms.FlowLayoutPanel();
            this.player2Button = new System.Windows.Forms.Button();
            this.player2ButtonLabel = new System.Windows.Forms.Label();
            this.actionLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.nfcLabel = new System.Windows.Forms.Label();
            this.nfcCombo = new System.Windows.Forms.ComboBox();
            this.player1Label = new System.Windows.Forms.Label();
            this.player1Panel = new System.Windows.Forms.FlowLayoutPanel();
            this.player1Button = new System.Windows.Forms.Button();
            this.player1ButtonLabel = new System.Windows.Forms.Label();
            this.player2Label = new System.Windows.Forms.Label();
            this.layout.SuspendLayout();
            this.player2Panel.SuspendLayout();
            this.actionLayout.SuspendLayout();
            this.player1Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.ColumnCount = 2;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.Controls.Add(this.apiLabel, 0, 0);
            this.layout.Controls.Add(this.apiText, 1, 0);
            this.layout.Controls.Add(this.player2Panel, 1, 3);
            this.layout.Controls.Add(this.actionLayout, 0, 5);
            this.layout.Controls.Add(this.nfcLabel, 0, 1);
            this.layout.Controls.Add(this.nfcCombo, 1, 1);
            this.layout.Controls.Add(this.player1Label, 0, 2);
            this.layout.Controls.Add(this.player1Panel, 1, 2);
            this.layout.Controls.Add(this.player2Label, 0, 3);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.RowCount = 6;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.Size = new System.Drawing.Size(425, 174);
            this.layout.TabIndex = 0;
            // 
            // apiLabel
            // 
            this.apiLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.apiLabel.AutoSize = true;
            this.apiLabel.Location = new System.Drawing.Point(3, 6);
            this.apiLabel.Name = "apiLabel";
            this.apiLabel.Size = new System.Drawing.Size(52, 13);
            this.apiLabel.TabIndex = 0;
            this.apiLabel.Text = "API URL:";
            // 
            // apiText
            // 
            this.apiText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.apiText.Location = new System.Drawing.Point(75, 3);
            this.apiText.Name = "apiText";
            this.apiText.Size = new System.Drawing.Size(347, 20);
            this.apiText.TabIndex = 2;
            // 
            // player2Panel
            // 
            this.player2Panel.AutoSize = true;
            this.player2Panel.Controls.Add(this.player2Button);
            this.player2Panel.Controls.Add(this.player2ButtonLabel);
            this.player2Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.player2Panel.Location = new System.Drawing.Point(75, 91);
            this.player2Panel.Name = "player2Panel";
            this.player2Panel.Size = new System.Drawing.Size(347, 29);
            this.player2Panel.TabIndex = 8;
            // 
            // player2Button
            // 
            this.player2Button.Location = new System.Drawing.Point(3, 3);
            this.player2Button.Name = "player2Button";
            this.player2Button.Size = new System.Drawing.Size(75, 23);
            this.player2Button.TabIndex = 0;
            this.player2Button.Text = "Select";
            this.player2Button.UseVisualStyleBackColor = true;
            this.player2Button.Click += new System.EventHandler(this.player2Button_Click);
            // 
            // player2ButtonLabel
            // 
            this.player2ButtonLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.player2ButtonLabel.AutoSize = true;
            this.player2ButtonLabel.Location = new System.Drawing.Point(84, 8);
            this.player2ButtonLabel.Name = "player2ButtonLabel";
            this.player2ButtonLabel.Size = new System.Drawing.Size(0, 13);
            this.player2ButtonLabel.TabIndex = 1;
            // 
            // actionLayout
            // 
            this.actionLayout.AutoSize = true;
            this.layout.SetColumnSpan(this.actionLayout, 2);
            this.actionLayout.Controls.Add(this.okButton);
            this.actionLayout.Controls.Add(this.cancelButton);
            this.actionLayout.Dock = System.Windows.Forms.DockStyle.Right;
            this.actionLayout.Location = new System.Drawing.Point(260, 142);
            this.actionLayout.Name = "actionLayout";
            this.actionLayout.Size = new System.Drawing.Size(162, 29);
            this.actionLayout.TabIndex = 3;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(3, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(84, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // nfcLabel
            // 
            this.nfcLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nfcLabel.AutoSize = true;
            this.nfcLabel.Location = new System.Drawing.Point(3, 33);
            this.nfcLabel.Name = "nfcLabel";
            this.nfcLabel.Size = new System.Drawing.Size(66, 13);
            this.nfcLabel.TabIndex = 4;
            this.nfcLabel.Text = "NFC Reader";
            // 
            // nfcCombo
            // 
            this.nfcCombo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nfcCombo.FormattingEnabled = true;
            this.nfcCombo.Location = new System.Drawing.Point(75, 29);
            this.nfcCombo.Name = "nfcCombo";
            this.nfcCombo.Size = new System.Drawing.Size(121, 21);
            this.nfcCombo.TabIndex = 5;
            // 
            // player1Label
            // 
            this.player1Label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.player1Label.AutoSize = true;
            this.player1Label.Location = new System.Drawing.Point(3, 64);
            this.player1Label.Name = "player1Label";
            this.player1Label.Size = new System.Drawing.Size(45, 13);
            this.player1Label.TabIndex = 6;
            this.player1Label.Text = "Player 1";
            // 
            // player1Panel
            // 
            this.player1Panel.AutoSize = true;
            this.player1Panel.Controls.Add(this.player1Button);
            this.player1Panel.Controls.Add(this.player1ButtonLabel);
            this.player1Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.player1Panel.Location = new System.Drawing.Point(75, 56);
            this.player1Panel.Name = "player1Panel";
            this.player1Panel.Size = new System.Drawing.Size(347, 29);
            this.player1Panel.TabIndex = 7;
            // 
            // player1Button
            // 
            this.player1Button.Location = new System.Drawing.Point(3, 3);
            this.player1Button.Name = "player1Button";
            this.player1Button.Size = new System.Drawing.Size(75, 23);
            this.player1Button.TabIndex = 0;
            this.player1Button.Text = "Select";
            this.player1Button.UseVisualStyleBackColor = true;
            this.player1Button.Click += new System.EventHandler(this.Player1Button_Click);
            // 
            // player1ButtonLabel
            // 
            this.player1ButtonLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.player1ButtonLabel.AutoSize = true;
            this.player1ButtonLabel.Location = new System.Drawing.Point(84, 8);
            this.player1ButtonLabel.Name = "player1ButtonLabel";
            this.player1ButtonLabel.Size = new System.Drawing.Size(0, 13);
            this.player1ButtonLabel.TabIndex = 1;
            // 
            // player2Label
            // 
            this.player2Label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.player2Label.AutoSize = true;
            this.player2Label.Location = new System.Drawing.Point(3, 99);
            this.player2Label.Name = "player2Label";
            this.player2Label.Size = new System.Drawing.Size(45, 13);
            this.player2Label.TabIndex = 8;
            this.player2Label.Text = "Player 2";
            // 
            // ConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 174);
            this.Controls.Add(this.layout);
            this.Name = "ConfigDialog";
            this.Text = "ConfigDialog";
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            this.player2Panel.ResumeLayout(false);
            this.player2Panel.PerformLayout();
            this.actionLayout.ResumeLayout(false);
            this.player1Panel.ResumeLayout(false);
            this.player1Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label apiLabel;
        private System.Windows.Forms.FlowLayoutPanel actionLayout;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        public System.Windows.Forms.TextBox apiText;
        private System.Windows.Forms.Label nfcLabel;
        public System.Windows.Forms.ComboBox nfcCombo;
        private System.Windows.Forms.Label player1Label;
        private System.Windows.Forms.FlowLayoutPanel player1Panel;
        private System.Windows.Forms.Button player1Button;
        private System.Windows.Forms.Label player1ButtonLabel;
        private System.Windows.Forms.FlowLayoutPanel player2Panel;
        private System.Windows.Forms.Button player2Button;
        private System.Windows.Forms.Label player2ButtonLabel;
        private System.Windows.Forms.Label player2Label;
    }
}