namespace WinFormTest
{
    partial class ChildForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new ContextMenuStrip(components);
            listBox1 = new ListBox();
            helloToolStripMenuItem = new ToolStripMenuItem();
            hiToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { helloToolStripMenuItem, hiToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(211, 80);
            // 
            // listBox1
            // 
            listBox1.ContextMenuStrip = contextMenuStrip1;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(308, 159);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(150, 104);
            listBox1.TabIndex = 1;
            // 
            // helloToolStripMenuItem
            // 
            helloToolStripMenuItem.Name = "helloToolStripMenuItem";
            helloToolStripMenuItem.Size = new Size(210, 24);
            helloToolStripMenuItem.Text = "Hello";
            // 
            // hiToolStripMenuItem
            // 
            hiToolStripMenuItem.Name = "hiToolStripMenuItem";
            hiToolStripMenuItem.Size = new Size(210, 24);
            hiToolStripMenuItem.Text = "Hi";
            // 
            // ChildForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listBox1);
            Name = "ChildForm";
            Text = "ChildForm";
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private ListBox listBox1;
        private ToolStripMenuItem helloToolStripMenuItem;
        private ToolStripMenuItem hiToolStripMenuItem;
    }
}