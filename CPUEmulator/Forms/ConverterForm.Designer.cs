namespace CPUEmulator.Forms {
	partial class ConverterForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConverterForm));
			this.controlPanel = new System.Windows.Forms.Panel();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.convertButton = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.codeBox = new System.Windows.Forms.TextBox();
			this.errorLabel = new System.Windows.Forms.Label();
			this.controlPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// controlPanel
			// 
			this.controlPanel.Controls.Add(this.errorLabel);
			this.controlPanel.Controls.Add(this.convertButton);
			this.controlPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.controlPanel.Location = new System.Drawing.Point(0, 272);
			this.controlPanel.Name = "controlPanel";
			this.controlPanel.Size = new System.Drawing.Size(584, 39);
			this.controlPanel.TabIndex = 0;
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.codeBox);
			this.splitContainer.Panel1.Controls.Add(this.panel1);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.panel2);
			this.splitContainer.Size = new System.Drawing.Size(584, 272);
			this.splitContainer.SplitterDistance = 194;
			this.splitContainer.TabIndex = 1;
			// 
			// convertButton
			// 
			this.convertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.convertButton.Image = ((System.Drawing.Image)(resources.GetObject("convertButton.Image")));
			this.convertButton.Location = new System.Drawing.Point(447, 7);
			this.convertButton.Name = "convertButton";
			this.convertButton.Size = new System.Drawing.Size(130, 25);
			this.convertButton.TabIndex = 0;
			this.convertButton.Text = "В редактор";
			this.convertButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.convertButton.UseVisualStyleBackColor = true;
			this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(194, 24);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.label2);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(386, 24);
			this.panel2.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Инструкции";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 5);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(132, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Сконвертированный код";
			// 
			// codeBox
			// 
			this.codeBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.codeBox.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.codeBox.Location = new System.Drawing.Point(0, 24);
			this.codeBox.Multiline = true;
			this.codeBox.Name = "codeBox";
			this.codeBox.Size = new System.Drawing.Size(194, 248);
			this.codeBox.TabIndex = 1;
			this.codeBox.TextChanged += new System.EventHandler(this.codeBox_TextChanged);
			// 
			// errorLabel
			// 
			this.errorLabel.AutoSize = true;
			this.errorLabel.ForeColor = System.Drawing.Color.DarkRed;
			this.errorLabel.Location = new System.Drawing.Point(12, 13);
			this.errorLabel.Name = "errorLabel";
			this.errorLabel.Size = new System.Drawing.Size(0, 13);
			this.errorLabel.TabIndex = 1;
			// 
			// ConverterForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 311);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.controlPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConverterForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Конвертер инструкций";
			this.controlPanel.ResumeLayout(false);
			this.controlPanel.PerformLayout();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel1.PerformLayout();
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel controlPanel;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.Button convertButton;
		private System.Windows.Forms.TextBox codeBox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label errorLabel;
	}
}