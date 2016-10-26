namespace SmartFighter {
    partial class App {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent() {
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.inputBox = new System.Windows.Forms.GroupBox();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.inputButton = new System.Windows.Forms.Button();
            this.inputLabel = new System.Windows.Forms.Label();
            this.nfcBox = new System.Windows.Forms.GroupBox();
            this.nfcPanel = new System.Windows.Forms.Panel();
            this.nfcButton = new System.Windows.Forms.Button();
            this.nfcLabel = new System.Windows.Forms.Label();
            this.apiBox = new System.Windows.Forms.GroupBox();
            this.apiPanel = new System.Windows.Forms.Panel();
            this.apiButton = new System.Windows.Forms.Button();
            this.apiLabel = new System.Windows.Forms.Label();
            this.connectorBox = new System.Windows.Forms.GroupBox();
            this.connectorPanel = new System.Windows.Forms.Panel();
            this.connectorButton = new System.Windows.Forms.Button();
            this.connectorLabel = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.smartFighterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerAPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layout.SuspendLayout();
            this.inputBox.SuspendLayout();
            this.inputPanel.SuspendLayout();
            this.nfcBox.SuspendLayout();
            this.nfcPanel.SuspendLayout();
            this.apiBox.SuspendLayout();
            this.apiPanel.SuspendLayout();
            this.connectorBox.SuspendLayout();
            this.connectorPanel.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // logBox
            // 
            this.layout.SetColumnSpan(this.logBox, 2);
            this.logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logBox.Location = new System.Drawing.Point(3, 123);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(480, 434);
            this.logBox.TabIndex = 5;
            this.logBox.Text = "";
            // 
            // layout
            // 
            this.layout.ColumnCount = 2;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.Controls.Add(this.inputBox, 1, 1);
            this.layout.Controls.Add(this.nfcBox, 0, 1);
            this.layout.Controls.Add(this.apiBox, 1, 0);
            this.layout.Controls.Add(this.logBox, 0, 2);
            this.layout.Controls.Add(this.connectorBox, 0, 0);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 24);
            this.layout.Name = "layout";
            this.layout.RowCount = 3;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.Size = new System.Drawing.Size(486, 500);
            this.layout.TabIndex = 6;
            // 
            // inputBox
            // 
            this.inputBox.Controls.Add(this.inputPanel);
            this.inputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputBox.Location = new System.Drawing.Point(246, 63);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(237, 54);
            this.inputBox.TabIndex = 8;
            this.inputBox.TabStop = false;
            this.inputBox.Text = "Input";
            // 
            // inputPanel
            // 
            this.inputPanel.Controls.Add(this.inputButton);
            this.inputPanel.Controls.Add(this.inputLabel);
            this.inputPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputPanel.Location = new System.Drawing.Point(3, 16);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(231, 35);
            this.inputPanel.TabIndex = 3;
            // 
            // inputButton
            // 
            this.inputButton.Location = new System.Drawing.Point(7, 7);
            this.inputButton.Name = "inputButton";
            this.inputButton.Size = new System.Drawing.Size(75, 23);
            this.inputButton.TabIndex = 2;
            this.inputButton.Text = "Start";
            this.inputButton.UseVisualStyleBackColor = true;
            this.inputButton.Click += new System.EventHandler(this.inputButton_Click);
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Location = new System.Drawing.Point(88, 12);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(47, 13);
            this.inputLabel.TabIndex = 3;
            this.inputLabel.Text = "Running";
            // 
            // nfcBox
            // 
            this.nfcBox.Controls.Add(this.nfcPanel);
            this.nfcBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nfcBox.Location = new System.Drawing.Point(3, 63);
            this.nfcBox.Name = "nfcBox";
            this.nfcBox.Size = new System.Drawing.Size(237, 54);
            this.nfcBox.TabIndex = 7;
            this.nfcBox.TabStop = false;
            this.nfcBox.Text = "NFC Reader";
            // 
            // nfcPanel
            // 
            this.nfcPanel.Controls.Add(this.nfcButton);
            this.nfcPanel.Controls.Add(this.nfcLabel);
            this.nfcPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nfcPanel.Location = new System.Drawing.Point(3, 16);
            this.nfcPanel.Name = "nfcPanel";
            this.nfcPanel.Size = new System.Drawing.Size(231, 35);
            this.nfcPanel.TabIndex = 3;
            // 
            // nfcButton
            // 
            this.nfcButton.Location = new System.Drawing.Point(7, 7);
            this.nfcButton.Name = "nfcButton";
            this.nfcButton.Size = new System.Drawing.Size(75, 23);
            this.nfcButton.TabIndex = 2;
            this.nfcButton.Text = "Start";
            this.nfcButton.UseVisualStyleBackColor = true;
            this.nfcButton.Click += new System.EventHandler(this.nfcButton_Click);
            // 
            // nfcLabel
            // 
            this.nfcLabel.AutoSize = true;
            this.nfcLabel.Location = new System.Drawing.Point(88, 12);
            this.nfcLabel.Name = "nfcLabel";
            this.nfcLabel.Size = new System.Drawing.Size(47, 13);
            this.nfcLabel.TabIndex = 3;
            this.nfcLabel.Text = "Running";
            // 
            // apiBox
            // 
            this.apiBox.Controls.Add(this.apiPanel);
            this.apiBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.apiBox.Location = new System.Drawing.Point(246, 3);
            this.apiBox.Name = "apiBox";
            this.apiBox.Size = new System.Drawing.Size(237, 54);
            this.apiBox.TabIndex = 5;
            this.apiBox.TabStop = false;
            this.apiBox.Text = "API Queue";
            // 
            // apiPanel
            // 
            this.apiPanel.Controls.Add(this.apiButton);
            this.apiPanel.Controls.Add(this.apiLabel);
            this.apiPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.apiPanel.Location = new System.Drawing.Point(3, 16);
            this.apiPanel.Name = "apiPanel";
            this.apiPanel.Size = new System.Drawing.Size(231, 35);
            this.apiPanel.TabIndex = 3;
            // 
            // apiButton
            // 
            this.apiButton.Location = new System.Drawing.Point(7, 7);
            this.apiButton.Name = "apiButton";
            this.apiButton.Size = new System.Drawing.Size(75, 23);
            this.apiButton.TabIndex = 2;
            this.apiButton.Text = "Start";
            this.apiButton.UseVisualStyleBackColor = true;
            this.apiButton.Click += new System.EventHandler(this.apiButton_Click);
            // 
            // apiLabel
            // 
            this.apiLabel.AutoSize = true;
            this.apiLabel.Location = new System.Drawing.Point(88, 12);
            this.apiLabel.Name = "apiLabel";
            this.apiLabel.Size = new System.Drawing.Size(47, 13);
            this.apiLabel.TabIndex = 3;
            this.apiLabel.Text = "Running";
            // 
            // connectorBox
            // 
            this.connectorBox.Controls.Add(this.connectorPanel);
            this.connectorBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectorBox.Location = new System.Drawing.Point(3, 3);
            this.connectorBox.Name = "connectorBox";
            this.connectorBox.Size = new System.Drawing.Size(237, 54);
            this.connectorBox.TabIndex = 6;
            this.connectorBox.TabStop = false;
            this.connectorBox.Text = "SFV Connector";
            // 
            // connectorPanel
            // 
            this.connectorPanel.Controls.Add(this.connectorButton);
            this.connectorPanel.Controls.Add(this.connectorLabel);
            this.connectorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectorPanel.Location = new System.Drawing.Point(3, 16);
            this.connectorPanel.Name = "connectorPanel";
            this.connectorPanel.Size = new System.Drawing.Size(231, 35);
            this.connectorPanel.TabIndex = 3;
            // 
            // connectorButton
            // 
            this.connectorButton.Location = new System.Drawing.Point(7, 7);
            this.connectorButton.Name = "connectorButton";
            this.connectorButton.Size = new System.Drawing.Size(75, 23);
            this.connectorButton.TabIndex = 2;
            this.connectorButton.Text = "Start";
            this.connectorButton.UseVisualStyleBackColor = true;
            this.connectorButton.Click += new System.EventHandler(this.connectorButton_Click);
            // 
            // connectorLabel
            // 
            this.connectorLabel.AutoSize = true;
            this.connectorLabel.Location = new System.Drawing.Point(88, 12);
            this.connectorLabel.Name = "connectorLabel";
            this.connectorLabel.Size = new System.Drawing.Size(47, 13);
            this.connectorLabel.TabIndex = 3;
            this.connectorLabel.Text = "Running";
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smartFighterMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(486, 24);
            this.menu.TabIndex = 7;
            this.menu.Text = "menu";
            // 
            // smartFighterMenuItem
            // 
            this.smartFighterMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationMenuItem,
            this.registerAPlayerToolStripMenuItem,
            this.quitMenuItem});
            this.smartFighterMenuItem.Name = "smartFighterMenuItem";
            this.smartFighterMenuItem.Size = new System.Drawing.Size(87, 20);
            this.smartFighterMenuItem.Text = "SmartFighter";
            // 
            // configurationMenuItem
            // 
            this.configurationMenuItem.Name = "configurationMenuItem";
            this.configurationMenuItem.Size = new System.Drawing.Size(160, 22);
            this.configurationMenuItem.Text = "Configuration";
            this.configurationMenuItem.Click += new System.EventHandler(this.configurationMenuItem_Click);
            // 
            // quitMenuItem
            // 
            this.quitMenuItem.Name = "quitMenuItem";
            this.quitMenuItem.Size = new System.Drawing.Size(160, 22);
            this.quitMenuItem.Text = "Quit";
            // 
            // registerAPlayerToolStripMenuItem
            // 
            this.registerAPlayerToolStripMenuItem.Name = "registerAPlayerToolStripMenuItem";
            this.registerAPlayerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.registerAPlayerToolStripMenuItem.Text = "Register a player";
            this.registerAPlayerToolStripMenuItem.Click += new System.EventHandler(this.registerAPlayerToolStripMenuItem_Click);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 524);
            this.Controls.Add(this.layout);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "App";
            this.Text = "SmartFighter";
            this.layout.ResumeLayout(false);
            this.inputBox.ResumeLayout(false);
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            this.nfcBox.ResumeLayout(false);
            this.nfcPanel.ResumeLayout(false);
            this.nfcPanel.PerformLayout();
            this.apiBox.ResumeLayout(false);
            this.apiPanel.ResumeLayout(false);
            this.apiPanel.PerformLayout();
            this.connectorBox.ResumeLayout(false);
            this.connectorPanel.ResumeLayout(false);
            this.connectorPanel.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.GroupBox apiBox;
        private System.Windows.Forms.Panel apiPanel;
        private System.Windows.Forms.Button apiButton;
        private System.Windows.Forms.Label apiLabel;
        private System.Windows.Forms.GroupBox connectorBox;
        private System.Windows.Forms.Panel connectorPanel;
        private System.Windows.Forms.Button connectorButton;
        private System.Windows.Forms.Label connectorLabel;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem smartFighterMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitMenuItem;
        private System.Windows.Forms.GroupBox nfcBox;
        private System.Windows.Forms.Panel nfcPanel;
        private System.Windows.Forms.Button nfcButton;
        private System.Windows.Forms.Label nfcLabel;
        private System.Windows.Forms.GroupBox inputBox;
        private System.Windows.Forms.Panel inputPanel;
        private System.Windows.Forms.Button inputButton;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.ToolStripMenuItem registerAPlayerToolStripMenuItem;
    }
}

