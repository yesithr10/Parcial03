using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entity;
namespace DAL
{
    public class BeneficiarioRepository
    {
        
        public List<Beneficiario> CargarArchivo(string FileStream)
        {

            List<Beneficiario> lBeneficiario = new List<Beneficiario>();
            string linea;

            TextReader lector;
            lector = new StreamReader(FileStream);

            while ((linea = lector.ReadLine()) != null)
            {
                Beneficiario beneficiario = new Beneficiario();
                char delimitador = ';';
                string[] arrayPersona = linea.Split(delimitador);

                beneficiario.CodigoProveedor = arrayPersona[0];
                beneficiario.Cedula = arrayPersona[1];
                beneficiario.NombreBeneficiario = arrayPersona[2];
                beneficiario.Fecha = arrayPersona[3];
                beneficiario.ValorAyuda = Convert.ToDouble(arrayPersona[4]);
                lBeneficiario.Add(beneficiario);
            }
            lector.Close();
            return lBeneficiario;
        }
    }


}

