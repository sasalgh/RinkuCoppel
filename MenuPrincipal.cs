using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RinkuCoppel
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();


            
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (ActiveForm != null)
            {
                
            }

            

            if (this.panelContenido.Controls.Count != 0)
                this.panelContenido.Controls.RemoveAt(0);

            
            //activeForm = childForm;
            //ActiveForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelContenido.Controls.Add(childForm);
            panelContenido.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormEmpleados(), sender);
        }
    }
}
