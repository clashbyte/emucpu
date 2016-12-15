namespace EmuCPU.Forms {
	partial class MainForm {
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
			System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
			System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
			System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
			this.menuNew = new System.Windows.Forms.ToolStripMenuItem();
			this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
			this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.menuRedo = new System.Windows.Forms.ToolStripMenuItem();
			this.menuConvert = new System.Windows.Forms.ToolStripMenuItem();
			this.menuPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.menuContinue = new System.Windows.Forms.ToolStripMenuItem();
			this.menuPause = new System.Windows.Forms.ToolStripMenuItem();
			this.menuAdvance = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStop = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.bottomStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripNew = new System.Windows.Forms.ToolStripButton();
			this.toolStripOpen = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripSaveAs = new System.Windows.Forms.ToolStripButton();
			this.toolStripStop = new System.Windows.Forms.ToolStripButton();
			this.toolStripAdvance = new System.Windows.Forms.ToolStripButton();
			this.toolStripPause = new System.Windows.Forms.ToolStripButton();
			this.toolStripContinue = new System.Windows.Forms.ToolStripButton();
			this.toolStripPlay = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripUndo = new System.Windows.Forms.ToolStripButton();
			this.toolStripRedo = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripConverter = new System.Windows.Forms.ToolStripButton();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.dataPage = new System.Windows.Forms.TabPage();
			this.dataSplitter = new System.Windows.Forms.SplitContainer();
			this.dataList = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label1 = new System.Windows.Forms.Label();
			this.stackList = new System.Windows.Forms.ListView();
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label3 = new System.Windows.Forms.Label();
			this.registerList = new System.Windows.Forms.ListView();
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label2 = new System.Windows.Forms.Label();
			this.helpPage = new System.Windows.Forms.TabPage();
			this.helpManager = new EmuCPU.Editing.HelpManager();
			fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.toolStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.dataPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataSplitter)).BeginInit();
			this.dataSplitter.Panel1.SuspendLayout();
			this.dataSplitter.Panel2.SuspendLayout();
			this.dataSplitter.SuspendLayout();
			this.helpPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNew,
            this.menuOpen,
            this.toolStripSeparator4,
            this.menuSave,
            this.menuSaveAs,
            this.toolStripSeparator5,
            this.menuExit});
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			fileToolStripMenuItem.Text = "Файл";
			// 
			// menuNew
			// 
			this.menuNew.Image = ((System.Drawing.Image)(resources.GetObject("menuNew.Image")));
			this.menuNew.Name = "menuNew";
			this.menuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.menuNew.Size = new System.Drawing.Size(172, 22);
			this.menuNew.Text = "Новый";
			this.menuNew.Click += new System.EventHandler(this.toolStripNew_Click);
			// 
			// menuOpen
			// 
			this.menuOpen.Image = ((System.Drawing.Image)(resources.GetObject("menuOpen.Image")));
			this.menuOpen.Name = "menuOpen";
			this.menuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.menuOpen.Size = new System.Drawing.Size(172, 22);
			this.menuOpen.Text = "Открыть";
			this.menuOpen.Click += new System.EventHandler(this.toolStripOpen_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(169, 6);
			// 
			// menuSave
			// 
			this.menuSave.Image = ((System.Drawing.Image)(resources.GetObject("menuSave.Image")));
			this.menuSave.Name = "menuSave";
			this.menuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.menuSave.Size = new System.Drawing.Size(172, 22);
			this.menuSave.Text = "Сохранить";
			this.menuSave.Click += new System.EventHandler(this.toolStripSave_Click);
			// 
			// menuSaveAs
			// 
			this.menuSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("menuSaveAs.Image")));
			this.menuSaveAs.Name = "menuSaveAs";
			this.menuSaveAs.Size = new System.Drawing.Size(172, 22);
			this.menuSaveAs.Text = "Сохранить как...";
			this.menuSaveAs.Click += new System.EventHandler(this.toolStripSaveAs_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(169, 6);
			// 
			// menuExit
			// 
			this.menuExit.Image = ((System.Drawing.Image)(resources.GetObject("menuExit.Image")));
			this.menuExit.Name = "menuExit";
			this.menuExit.Size = new System.Drawing.Size(172, 22);
			this.menuExit.Text = "Выход";
			// 
			// editToolStripMenuItem
			// 
			editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUndo,
            this.menuRedo});
			editToolStripMenuItem.Name = "editToolStripMenuItem";
			editToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			editToolStripMenuItem.Text = "Правка";
			// 
			// menuUndo
			// 
			this.menuUndo.Image = ((System.Drawing.Image)(resources.GetObject("menuUndo.Image")));
			this.menuUndo.Name = "menuUndo";
			this.menuUndo.Size = new System.Drawing.Size(152, 22);
			this.menuUndo.Text = "Отменить";
			this.menuUndo.Click += new System.EventHandler(this.toolStripUndo_Click);
			// 
			// menuRedo
			// 
			this.menuRedo.Image = ((System.Drawing.Image)(resources.GetObject("menuRedo.Image")));
			this.menuRedo.Name = "menuRedo";
			this.menuRedo.Size = new System.Drawing.Size(152, 22);
			this.menuRedo.Text = "Повторить";
			this.menuRedo.Click += new System.EventHandler(this.toolStripRedo_Click);
			// 
			// toolsToolStripMenuItem
			// 
			toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConvert});
			toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			toolsToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
			toolsToolStripMenuItem.Text = "Утилиты";
			// 
			// menuConvert
			// 
			this.menuConvert.Image = ((System.Drawing.Image)(resources.GetObject("menuConvert.Image")));
			this.menuConvert.Name = "menuConvert";
			this.menuConvert.Size = new System.Drawing.Size(132, 22);
			this.menuConvert.Text = "Конвертер";
			this.menuConvert.Click += new System.EventHandler(this.toolStripConverter_Click);
			// 
			// debugToolStripMenuItem
			// 
			debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPlay,
            this.menuContinue,
            this.menuPause,
            this.menuAdvance,
            this.menuStop});
			debugToolStripMenuItem.Name = "debugToolStripMenuItem";
			debugToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
			debugToolStripMenuItem.Text = "Отладка";
			// 
			// menuPlay
			// 
			this.menuPlay.Image = ((System.Drawing.Image)(resources.GetObject("menuPlay.Image")));
			this.menuPlay.Name = "menuPlay";
			this.menuPlay.Size = new System.Drawing.Size(144, 22);
			this.menuPlay.Text = "Запуск";
			this.menuPlay.Click += new System.EventHandler(this.toolStripPlay_Click);
			// 
			// menuContinue
			// 
			this.menuContinue.Image = ((System.Drawing.Image)(resources.GetObject("menuContinue.Image")));
			this.menuContinue.Name = "menuContinue";
			this.menuContinue.Size = new System.Drawing.Size(144, 22);
			this.menuContinue.Text = "Продолжить";
			this.menuContinue.Visible = false;
			this.menuContinue.Click += new System.EventHandler(this.toolStripContinue_Click);
			// 
			// menuPause
			// 
			this.menuPause.Image = ((System.Drawing.Image)(resources.GetObject("menuPause.Image")));
			this.menuPause.Name = "menuPause";
			this.menuPause.Size = new System.Drawing.Size(144, 22);
			this.menuPause.Text = "Пауза";
			this.menuPause.Visible = false;
			this.menuPause.Click += new System.EventHandler(this.toolStripPause_Click);
			// 
			// menuAdvance
			// 
			this.menuAdvance.Image = ((System.Drawing.Image)(resources.GetObject("menuAdvance.Image")));
			this.menuAdvance.Name = "menuAdvance";
			this.menuAdvance.Size = new System.Drawing.Size(144, 22);
			this.menuAdvance.Text = "Шаг вперед";
			this.menuAdvance.Visible = false;
			this.menuAdvance.Click += new System.EventHandler(this.toolStripAdvance_Click);
			// 
			// menuStop
			// 
			this.menuStop.Image = ((System.Drawing.Image)(resources.GetObject("menuStop.Image")));
			this.menuStop.Name = "menuStop";
			this.menuStop.Size = new System.Drawing.Size(144, 22);
			this.menuStop.Text = "Стоп";
			this.menuStop.Visible = false;
			this.menuStop.Click += new System.EventHandler(this.toolStripStop_Click);
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            fileToolStripMenuItem,
            editToolStripMenuItem,
            toolsToolStripMenuItem,
            debugToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(650, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip";
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bottomStatusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 419);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(650, 22);
			this.statusStrip.TabIndex = 1;
			this.statusStrip.Text = "statusStrip";
			// 
			// bottomStatusLabel
			// 
			this.bottomStatusLabel.Name = "bottomStatusLabel";
			this.bottomStatusLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripNew,
            this.toolStripOpen,
            this.toolStripSeparator1,
            this.toolStripSave,
            this.toolStripSaveAs,
            this.toolStripStop,
            this.toolStripAdvance,
            this.toolStripPause,
            this.toolStripContinue,
            this.toolStripPlay,
            this.toolStripSeparator3,
            this.toolStripUndo,
            this.toolStripRedo,
            this.toolStripSeparator2,
            this.toolStripConverter});
			this.toolStrip.Location = new System.Drawing.Point(0, 24);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(650, 25);
			this.toolStrip.TabIndex = 2;
			this.toolStrip.Text = "toolStrip";
			// 
			// toolStripNew
			// 
			this.toolStripNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripNew.Image = ((System.Drawing.Image)(resources.GetObject("toolStripNew.Image")));
			this.toolStripNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripNew.Name = "toolStripNew";
			this.toolStripNew.Size = new System.Drawing.Size(23, 22);
			this.toolStripNew.Text = "Новая программа";
			this.toolStripNew.Click += new System.EventHandler(this.toolStripNew_Click);
			// 
			// toolStripOpen
			// 
			this.toolStripOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripOpen.Image")));
			this.toolStripOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripOpen.Name = "toolStripOpen";
			this.toolStripOpen.Size = new System.Drawing.Size(23, 22);
			this.toolStripOpen.Text = "Открыть";
			this.toolStripOpen.Click += new System.EventHandler(this.toolStripOpen_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSave
			// 
			this.toolStripSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSave.Image")));
			this.toolStripSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSave.Name = "toolStripSave";
			this.toolStripSave.Size = new System.Drawing.Size(23, 22);
			this.toolStripSave.Text = "Сохранить";
			this.toolStripSave.Click += new System.EventHandler(this.toolStripSave_Click);
			// 
			// toolStripSaveAs
			// 
			this.toolStripSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSaveAs.Image")));
			this.toolStripSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSaveAs.Name = "toolStripSaveAs";
			this.toolStripSaveAs.Size = new System.Drawing.Size(23, 22);
			this.toolStripSaveAs.Text = "Сохранить как...";
			this.toolStripSaveAs.Click += new System.EventHandler(this.toolStripSaveAs_Click);
			// 
			// toolStripStop
			// 
			this.toolStripStop.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStop.Image")));
			this.toolStripStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripStop.Name = "toolStripStop";
			this.toolStripStop.Size = new System.Drawing.Size(54, 22);
			this.toolStripStop.Text = "Стоп";
			this.toolStripStop.Visible = false;
			this.toolStripStop.Click += new System.EventHandler(this.toolStripStop_Click);
			// 
			// toolStripAdvance
			// 
			this.toolStripAdvance.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripAdvance.Image = ((System.Drawing.Image)(resources.GetObject("toolStripAdvance.Image")));
			this.toolStripAdvance.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripAdvance.Name = "toolStripAdvance";
			this.toolStripAdvance.Size = new System.Drawing.Size(90, 22);
			this.toolStripAdvance.Text = "Шаг вперед";
			this.toolStripAdvance.Visible = false;
			this.toolStripAdvance.Click += new System.EventHandler(this.toolStripAdvance_Click);
			// 
			// toolStripPause
			// 
			this.toolStripPause.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripPause.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPause.Image")));
			this.toolStripPause.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripPause.Name = "toolStripPause";
			this.toolStripPause.Size = new System.Drawing.Size(59, 22);
			this.toolStripPause.Text = "Пауза";
			this.toolStripPause.Visible = false;
			this.toolStripPause.Click += new System.EventHandler(this.toolStripPause_Click);
			// 
			// toolStripContinue
			// 
			this.toolStripContinue.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripContinue.Image = ((System.Drawing.Image)(resources.GetObject("toolStripContinue.Image")));
			this.toolStripContinue.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripContinue.Name = "toolStripContinue";
			this.toolStripContinue.Size = new System.Drawing.Size(97, 22);
			this.toolStripContinue.Text = "Продолжить";
			this.toolStripContinue.Visible = false;
			this.toolStripContinue.Click += new System.EventHandler(this.toolStripContinue_Click);
			// 
			// toolStripPlay
			// 
			this.toolStripPlay.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripPlay.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPlay.Image")));
			this.toolStripPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripPlay.Name = "toolStripPlay";
			this.toolStripPlay.Size = new System.Drawing.Size(65, 22);
			this.toolStripPlay.Text = "Запуск";
			this.toolStripPlay.Click += new System.EventHandler(this.toolStripPlay_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripUndo
			// 
			this.toolStripUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripUndo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripUndo.Image")));
			this.toolStripUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripUndo.Name = "toolStripUndo";
			this.toolStripUndo.Size = new System.Drawing.Size(23, 22);
			this.toolStripUndo.Text = "Отменить";
			this.toolStripUndo.Click += new System.EventHandler(this.toolStripUndo_Click);
			// 
			// toolStripRedo
			// 
			this.toolStripRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripRedo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripRedo.Image")));
			this.toolStripRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripRedo.Name = "toolStripRedo";
			this.toolStripRedo.Size = new System.Drawing.Size(23, 22);
			this.toolStripRedo.Text = "Повторить";
			this.toolStripRedo.Click += new System.EventHandler(this.toolStripRedo_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripConverter
			// 
			this.toolStripConverter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripConverter.Image = ((System.Drawing.Image)(resources.GetObject("toolStripConverter.Image")));
			this.toolStripConverter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripConverter.Name = "toolStripConverter";
			this.toolStripConverter.Size = new System.Drawing.Size(23, 22);
			this.toolStripConverter.Text = "Конвертер";
			this.toolStripConverter.Click += new System.EventHandler(this.toolStripConverter_Click);
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer.Location = new System.Drawing.Point(0, 49);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.tabControl);
			this.splitContainer.Size = new System.Drawing.Size(650, 370);
			this.splitContainer.SplitterDistance = 380;
			this.splitContainer.TabIndex = 3;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.dataPage);
			this.tabControl.Controls.Add(this.helpPage);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Multiline = true;
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(266, 370);
			this.tabControl.TabIndex = 0;
			// 
			// dataPage
			// 
			this.dataPage.Controls.Add(this.dataSplitter);
			this.dataPage.Location = new System.Drawing.Point(4, 22);
			this.dataPage.Name = "dataPage";
			this.dataPage.Padding = new System.Windows.Forms.Padding(3);
			this.dataPage.Size = new System.Drawing.Size(258, 344);
			this.dataPage.TabIndex = 0;
			this.dataPage.Text = "Данные";
			this.dataPage.UseVisualStyleBackColor = true;
			// 
			// dataSplitter
			// 
			this.dataSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataSplitter.Location = new System.Drawing.Point(3, 3);
			this.dataSplitter.Name = "dataSplitter";
			// 
			// dataSplitter.Panel1
			// 
			this.dataSplitter.Panel1.Controls.Add(this.dataList);
			this.dataSplitter.Panel1.Controls.Add(this.label1);
			// 
			// dataSplitter.Panel2
			// 
			this.dataSplitter.Panel2.Controls.Add(this.stackList);
			this.dataSplitter.Panel2.Controls.Add(this.label3);
			this.dataSplitter.Panel2.Controls.Add(this.registerList);
			this.dataSplitter.Panel2.Controls.Add(this.label2);
			this.dataSplitter.Size = new System.Drawing.Size(252, 338);
			this.dataSplitter.SplitterDistance = 124;
			this.dataSplitter.TabIndex = 1;
			// 
			// dataList
			// 
			this.dataList.AutoArrange = false;
			this.dataList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataList.FullRowSelect = true;
			this.dataList.GridLines = true;
			this.dataList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.dataList.Location = new System.Drawing.Point(0, 16);
			this.dataList.MultiSelect = false;
			this.dataList.Name = "dataList";
			this.dataList.ShowGroups = false;
			this.dataList.Size = new System.Drawing.Size(124, 322);
			this.dataList.TabIndex = 0;
			this.dataList.UseCompatibleStateImageBehavior = false;
			this.dataList.View = System.Windows.Forms.View.Details;
			this.dataList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataList_KeyDown);
			this.dataList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataList_MouseDoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "№";
			this.columnHeader1.Width = 25;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Значение";
			this.columnHeader2.Width = 95;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Память:";
			// 
			// stackList
			// 
			this.stackList.AutoArrange = false;
			this.stackList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
			this.stackList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stackList.FullRowSelect = true;
			this.stackList.GridLines = true;
			this.stackList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.stackList.Location = new System.Drawing.Point(0, 232);
			this.stackList.MultiSelect = false;
			this.stackList.Name = "stackList";
			this.stackList.ShowGroups = false;
			this.stackList.Size = new System.Drawing.Size(124, 106);
			this.stackList.TabIndex = 4;
			this.stackList.UseCompatibleStateImageBehavior = false;
			this.stackList.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "№";
			this.columnHeader5.Width = 25;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Значение";
			this.columnHeader6.Width = 95;
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Top;
			this.label3.Location = new System.Drawing.Point(0, 214);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(124, 18);
			this.label3.TabIndex = 3;
			this.label3.Text = "Стек";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// registerList
			// 
			this.registerList.AutoArrange = false;
			this.registerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
			this.registerList.Dock = System.Windows.Forms.DockStyle.Top;
			this.registerList.FullRowSelect = true;
			this.registerList.GridLines = true;
			this.registerList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.registerList.Location = new System.Drawing.Point(0, 16);
			this.registerList.MultiSelect = false;
			this.registerList.Name = "registerList";
			this.registerList.ShowGroups = false;
			this.registerList.Size = new System.Drawing.Size(124, 198);
			this.registerList.TabIndex = 1;
			this.registerList.UseCompatibleStateImageBehavior = false;
			this.registerList.View = System.Windows.Forms.View.Details;
			this.registerList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.registerList_KeyDown);
			this.registerList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.registerList_MouseDoubleClick);
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Имя";
			this.columnHeader3.Width = 40;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Значение";
			this.columnHeader4.Width = 80;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(124, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Регистры:";
			// 
			// helpPage
			// 
			this.helpPage.Controls.Add(this.helpManager);
			this.helpPage.Location = new System.Drawing.Point(4, 22);
			this.helpPage.Name = "helpPage";
			this.helpPage.Padding = new System.Windows.Forms.Padding(3);
			this.helpPage.Size = new System.Drawing.Size(258, 344);
			this.helpPage.TabIndex = 3;
			this.helpPage.Text = "Справка";
			this.helpPage.UseVisualStyleBackColor = true;
			// 
			// helpManager
			// 
			this.helpManager.AutoScroll = true;
			this.helpManager.Dock = System.Windows.Forms.DockStyle.Fill;
			this.helpManager.Location = new System.Drawing.Point(3, 3);
			this.helpManager.Name = "helpManager";
			this.helpManager.Size = new System.Drawing.Size(252, 338);
			this.helpManager.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(650, 441);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.MinimumSize = new System.Drawing.Size(640, 480);
			this.Name = "MainForm";
			this.Text = "EmuCPU";
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.dataPage.ResumeLayout(false);
			this.dataSplitter.Panel1.ResumeLayout(false);
			this.dataSplitter.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataSplitter)).EndInit();
			this.dataSplitter.ResumeLayout(false);
			this.helpPage.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage dataPage;
		private System.Windows.Forms.ListView dataList;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.TabPage helpPage;
		private System.Windows.Forms.ListView registerList;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ToolStripButton toolStripNew;
		private System.Windows.Forms.ToolStripButton toolStripOpen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripSave;
		private System.Windows.Forms.ToolStripButton toolStripSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolStripConverter;
		private System.Windows.Forms.ToolStripButton toolStripPlay;
		private System.Windows.Forms.ToolStripButton toolStripContinue;
		private System.Windows.Forms.ToolStripButton toolStripAdvance;
		private System.Windows.Forms.ToolStripButton toolStripStop;
		private System.Windows.Forms.ToolStripButton toolStripPause;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton toolStripUndo;
		private System.Windows.Forms.ToolStripButton toolStripRedo;
		private System.Windows.Forms.ToolStripStatusLabel bottomStatusLabel;
		private System.Windows.Forms.ToolStripMenuItem menuNew;
		private System.Windows.Forms.ToolStripMenuItem menuOpen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem menuSave;
		private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem menuExit;
		private System.Windows.Forms.ToolStripMenuItem menuUndo;
		private System.Windows.Forms.ToolStripMenuItem menuRedo;
		private System.Windows.Forms.ToolStripMenuItem menuConvert;
		private System.Windows.Forms.ToolStripMenuItem menuPlay;
		private System.Windows.Forms.ToolStripMenuItem menuContinue;
		private System.Windows.Forms.ToolStripMenuItem menuPause;
		private System.Windows.Forms.ToolStripMenuItem menuAdvance;
		private System.Windows.Forms.ToolStripMenuItem menuStop;
		private Editing.HelpManager helpManager;
		private System.Windows.Forms.SplitContainer dataSplitter;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListView stackList;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Label label3;
	}
}