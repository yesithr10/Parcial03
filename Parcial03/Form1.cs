using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Entity;
namespace Parcial03
{
    public partial class Form1 : Form
    {
        BeneficiarioService beneficiarioService;
        public Form1()
        {           
            InitializeComponent();
            beneficiarioService = new BeneficiarioService(ConfigConnection.ConnectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargarArchivos();
        }
        public void CargarArchivos()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Cargar Informe";
            openFileDialog.InitialDirectory = @"c:/document";
            openFileDialog.DefaultExt = ".pdf";

            string filename = "";

           

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog.FileName;
                string ruta = openFileDialog.FileName;
                string codigoProvedor = cmbEntidad.Text;
                string fecha = FechaDatetime.Value.ToString("dd/MM/yyyy");

                if (filename != "")
                {
                    RespuestaConsulta respuestaConsulta = beneficiarioService.CargarArchivo(filename);
                    MessageBox.Show(respuestaConsulta.Mensaje, "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    foreach (var item in respuestaConsulta.Liquidacion)
                    {
                        if(beneficiarioService.Cargar(ruta, codigoProvedor, fecha).Equals(true))
                        {
                        beneficiarioService.Guardar(item);
                        }
                        else
                        {
                        beneficiarioService.GuardarGlosas(item);
                        }
                        
                    }
                }
                else
                {

                    MessageBox.Show("No hay datos para generar el reporte", "Generar Pdf", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                }
            }
        }
        

    }
}
