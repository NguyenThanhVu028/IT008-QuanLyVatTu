namespace WinFormTest
{
    partial class MyForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        //Methods
        private void ClickEvent(object sender, EventArgs e)
        {
            MessageBox.Show("Are you sure you want to close this application?");
            this.Close();
        }
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            filesToolStripMenuItem = new ToolStripMenuItem();
            newFormToolStripMenuItem = new ToolStripMenuItem();
            helloToolStripMenuItem = new ToolStripMenuItem();
            domainUpDown1 = new DomainUpDown();
            comboBox1 = new ComboBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { filesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // filesToolStripMenuItem
            // 
            filesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newFormToolStripMenuItem });
            filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            filesToolStripMenuItem.Size = new Size(52, 24);
            filesToolStripMenuItem.Text = "Files";
            // 
            // newFormToolStripMenuItem
            // 
            newFormToolStripMenuItem.Name = "newFormToolStripMenuItem";
            newFormToolStripMenuItem.Size = new Size(160, 26);
            newFormToolStripMenuItem.Text = "New Form";
            newFormToolStripMenuItem.Click += ClickOnNewForm;
            // 
            // helloToolStripMenuItem
            // 
            helloToolStripMenuItem.Name = "helloToolStripMenuItem";
            helloToolStripMenuItem.Size = new Size(111, 24);
            helloToolStripMenuItem.Text = "hello";
            // 
            // domainUpDown1
            // 
            domainUpDown1.Location = new Point(149, 45);
            domainUpDown1.Name = "domainUpDown1";
            domainUpDown1.Size = new Size(150, 27);
            domainUpDown1.TabIndex = 3;
            domainUpDown1.Text = "domainUpDown1";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(149, 93);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 4;
            // 
            // MyForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(comboBox1);
            Controls.Add(domainUpDown1);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "MyForm";
            Text = "Test";
            WindowState = FormWindowState.Maximized;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private MenuStrip menuStrip1;
        private ToolStripMenuItem filesToolStripMenuItem;
        private ToolStripMenuItem newFormToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip2;
        private ContextMenuStrip contextMenuStrip3;
        private ToolStripMenuItem helloToolStripMenuItem;
        private DomainUpDown domainUpDown1;
        private ComboBox comboBox1;
    }
}
