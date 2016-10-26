namespace SmartFighter {
    partial class RegisterDialog {
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
            this.cardLabel = new System.Windows.Forms.Label();
            this.playerLabel = new System.Windows.Forms.Label();
            this.cardValue = new System.Windows.Forms.Label();
            this.nameValue = new System.Windows.Forms.TextBox();
            this.actionLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.layout.SuspendLayout();
            this.actionLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.ColumnCount = 2;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.Controls.Add(this.cardLabel, 0, 0);
            this.layout.Controls.Add(this.playerLabel, 0, 1);
            this.layout.Controls.Add(this.cardValue, 1, 0);
            this.layout.Controls.Add(this.nameValue, 1, 1);
            this.layout.Controls.Add(this.actionLayout, 1, 3);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.RowCount = 4;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.Size = new System.Drawing.Size(284, 95);
            this.layout.TabIndex = 0;
            // 
            // cardLabel
            // 
            this.cardLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cardLabel.AutoSize = true;
            this.cardLabel.Location = new System.Drawing.Point(3, 6);
            this.cardLabel.Name = "cardLabel";
            this.cardLabel.Size = new System.Drawing.Size(46, 13);
            this.cardLabel.TabIndex = 0;
            this.cardLabel.Text = "Card ID:";
            // 
            // playerLabel
            // 
            this.playerLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.playerLabel.AutoSize = true;
            this.playerLabel.Location = new System.Drawing.Point(3, 31);
            this.playerLabel.Name = "playerLabel";
            this.playerLabel.Size = new System.Drawing.Size(38, 13);
            this.playerLabel.TabIndex = 1;
            this.playerLabel.Text = "Name:";
            // 
            // cardValue
            // 
            this.cardValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cardValue.AutoSize = true;
            this.cardValue.Location = new System.Drawing.Point(55, 6);
            this.cardValue.Name = "cardValue";
            this.cardValue.Size = new System.Drawing.Size(0, 13);
            this.cardValue.TabIndex = 2;
            // 
            // nameValue
            // 
            this.nameValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nameValue.Location = new System.Drawing.Point(55, 28);
            this.nameValue.Name = "nameValue";
            this.nameValue.Size = new System.Drawing.Size(226, 20);
            this.nameValue.TabIndex = 3;
            this.nameValue.TextChanged += new System.EventHandler(this.nameValue_TextChanged);
            // 
            // actionLayout
            // 
            this.actionLayout.AutoSize = true;
            this.actionLayout.Controls.Add(this.okButton);
            this.actionLayout.Controls.Add(this.cancelButton);
            this.actionLayout.Dock = System.Windows.Forms.DockStyle.Right;
            this.actionLayout.Location = new System.Drawing.Point(119, 63);
            this.actionLayout.Name = "actionLayout";
            this.actionLayout.Size = new System.Drawing.Size(162, 29);
            this.actionLayout.TabIndex = 4;
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
            // RegisterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 95);
            this.Controls.Add(this.layout);
            this.Name = "RegisterDialog";
            this.Text = "Register a Player";
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            this.actionLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label cardLabel;
        private System.Windows.Forms.Label playerLabel;
        private System.Windows.Forms.Label cardValue;
        private System.Windows.Forms.FlowLayoutPanel actionLayout;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        public System.Windows.Forms.TextBox nameValue;
    }
}