﻿namespace cppjudge
{
    partial class mainWindowForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindowForm));
            this.compileFile = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.Button();
            this.statWindow = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.testBtn = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.testFolders = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // compileFile
            // 
            this.compileFile.Location = new System.Drawing.Point(139, 333);
            this.compileFile.Name = "compileFile";
            this.compileFile.Size = new System.Drawing.Size(116, 23);
            this.compileFile.TabIndex = 0;
            this.compileFile.Text = "Компилировать";
            this.compileFile.UseVisualStyleBackColor = true;
            this.compileFile.Click += new System.EventHandler(this.compileFile_Click);
            // 
            // openFile
            // 
            this.openFile.Location = new System.Drawing.Point(12, 333);
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(121, 23);
            this.openFile.TabIndex = 2;
            this.openFile.Text = "Выбрать файл";
            this.openFile.UseVisualStyleBackColor = true;
            this.openFile.Click += new System.EventHandler(this.openFile_Click);
            // 
            // statWindow
            // 
            this.statWindow.Location = new System.Drawing.Point(473, 12);
            this.statWindow.Multiline = true;
            this.statWindow.Name = "statWindow";
            this.statWindow.Size = new System.Drawing.Size(277, 342);
            this.statWindow.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(467, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 6;
            // 
            // testBtn
            // 
            this.testBtn.Location = new System.Drawing.Point(261, 333);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(125, 23);
            this.testBtn.TabIndex = 13;
            this.testBtn.Text = "Тестировать";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Click += new System.EventHandler(this.testBtn_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(392, 333);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(75, 23);
            this.btnConfig.TabIndex = 14;
            this.btnConfig.Text = "Настройки";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // testFolders
            // 
            this.testFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testFolders.ImageIndex = 0;
            this.testFolders.ImageList = this.imageList1;
            this.testFolders.Location = new System.Drawing.Point(0, 0);
            this.testFolders.Name = "testFolders";
            this.testFolders.SelectedImageIndex = 0;
            this.testFolders.Size = new System.Drawing.Size(146, 315);
            this.testFolders.TabIndex = 15;
            this.testFolders.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.testFolders_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1.png");
            this.imageList1.Images.SetKeyName(1, "w450h4001385925290Document.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.testFolders);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Size = new System.Drawing.Size(455, 315);
            this.splitContainer1.SplitterDistance = 146;
            this.splitContainer1.TabIndex = 16;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.type,
            this.lastModified});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HoverSelection = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(305, 315);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // name
            // 
            this.name.Text = "Имя";
            // 
            // type
            // 
            this.type.Text = "Тип";
            // 
            // lastModified
            // 
            this.lastModified.Text = "Дата изменения";
            // 
            // mainWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 365);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.testBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statWindow);
            this.Controls.Add(this.openFile);
            this.Controls.Add(this.compileFile);
            this.Name = "mainWindowForm";
            this.Text = "Тестер олимпиад";
            this.Load += new System.EventHandler(this.mainWindowForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button compileFile;
        private System.Windows.Forms.Button openFile;
        private System.Windows.Forms.TextBox statWindow;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button testBtn;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.TreeView testFolders;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ColumnHeader lastModified;
    }
}

