namespace WinFormTest
{
    public partial class MyForm : Form
    {
        public MyForm()
        {
            InitializeComponent();
        }

        private void ClickOnNewForm(object sender, EventArgs e)
        {
            ChildForm Child = new ChildForm();
            Child.MdiParent = this;
            Child.Show();
        }
    }
}
