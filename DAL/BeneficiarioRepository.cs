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
        List<Beneficiario> lBeneficiario = new List<Beneficiario>();
        int cantidadLinea;
        public List<Beneficiario> CargarArchivo(string filename)
        {
            string linea;
            cantidadLinea = 1;

            FileStream SourceStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader Reader = new StreamReader(SourceStream);
            
            while ((linea = Reader.ReadLine()) != null)
            {
                Beneficiario beneficiario = MapearCarga(linea);
                lBeneficiario.Add(beneficiario);
                cantidadLinea ++;
            }

            return lBeneficiario;
        }
        public Beneficiario MapearCarga(string linea)
        {
            char delimitador = ';';
            string[] arrayBeneficiario = linea.Split(delimitador);
            Beneficiario beneficiario = new Beneficiario();

            beneficiario.CodigoProveedor = arrayBeneficiario[0];
            beneficiario.Cedula = arrayBeneficiario[1];
            beneficiario.NombreBeneficiario = arrayBeneficiario[2];
            beneficiario.Fecha = arrayBeneficiario[3];
            beneficiario.ValorAyuda = Convert.ToDouble(arrayBeneficiario[4]);

            return beneficiario;
        }
    }


}

