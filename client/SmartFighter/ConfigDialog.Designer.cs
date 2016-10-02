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
            this.actionLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.nfcLabel = new System.Windows.Forms.Label();
            this.nfcCombo = new System.Windows.Forms.ComboBox();
            this.layout.SuspendLayout();
            this.actionLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.ColumnCount = 2;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.Controls.Add(this.apiLabel, 0, 0);
            this.layout.Controls.Add(this.apiText, 1, 0);
            this.layout.Controls.Add(this.actionLayout, 0, 3);
            this.layout.Controls.Add(this.nfcLabel, 0, 1);
            this.layout.Controls.Add(this.nfcCombo, 1, 1);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.RowCount = 4;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.Size = new System.Drawing.Size(332, 191);
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
            this.apiText.Size = new System.Drawing.Size(268, 20);
            this.apiText.TabIndex = 2;
            // 
            // actionLayout
            // 
            this.actionLayout.AutoSize = true;
            this.layout.SetColumnSpan(this.actionLayout, 2);
            this.actionLayout.Controls.Add(this.okButton);
            this.actionLayout.Controls.Add(this.cancelButton);
            this.actionLayout.Dock = System.Windows.Forms.DockStyle.Right;
            this.actionLayout.Location = new System.Drawing.Point(181, 159);
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
            this.nfcLabel.Location = new System.Drawing.Point(3, 44);
            this.nfcLabel.Name = "nfcLabel";
            this.nfcLabel.Size = new System.Drawing.Size(66, 13);
            this.nfcLabel.TabIndex = 4;
            this.nfcLabel.Text = "NFC Reader";
            // 
            // nfcCombo
            // 
            this.nfcCombo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nfcCombo.FormattingEnabled = true;
            this.nfcCombo.Location = new System.Drawing.Point(75, 40);
            this.nfcCombo.Name = "nfcCombo";
            this.nfcCombo.Size = new System.Drawing.Size(121, 21);
            this.nfcCombo.TabIndex = 5;
            // 
            // ConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 191);
            this.Controls.Add(this.layout);
            this.Name = "ConfigDialog";
            this.Text = "ConfigDialog";
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            this.actionLayout.ResumeLayout(false);
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
    }
}