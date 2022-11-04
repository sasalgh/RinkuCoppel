using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Desarrollador: L.I. Sandro Sarlis López
 * Fecha 04/11/2022
 * Se utiliza para ordenar el área de trabajo del Sistema y Desplegar el Menu de opciones del Sistema.
 */

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
            //****** Rutina que abre un formulario de la opción seleccionada dentro del panel derecho de este formulario
            
            if (this.panelContenido.Controls.Count != 0)
                this.panelContenido.Controls.RemoveAt(0);

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
            //****** Envia el Formulario de la opción para que sea mostrado en el panel derecho
            OpenChildForm(new FormEmpleados(), sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //****** Envia el Formulario de la opción para que sea mostrado en el panel derecho
            OpenChildForm(new FormEntregas(), sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //****** Envia el Formulario de la opción para que sea mostrado en el panel derecho
            OpenChildForm(new FormReporte(), sender);
        }
    }
}
