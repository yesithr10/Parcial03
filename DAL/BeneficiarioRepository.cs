using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private readonly SqlConnection _connection;

        public BeneficiarioRepository(ConnectionManager connection)
        {
            _connection = connection._conexion;
        }

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

        public void Guardar(Beneficiario beneficiario)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"Insert Into BeneficiarioParcial3 (CodigoProveedor,Cedula,NombreBeneficiario,Fecha,ValorAyuda) 
                                        values (@CodigoProveedor,@Cedula,@NombreBeneficiario,@Fecha,@ValorAyuda)";
                command.Parameters.AddWithValue("@CodigoProveedor", beneficiario.CodigoProveedor);
                command.Parameters.AddWithValue("@Cedula", beneficiario.Cedula);
                command.Parameters.AddWithValue("@NombreBeneficiario", beneficiario.NombreBeneficiario);
                command.Parameters.AddWithValue("@Fecha", beneficiario.Fecha);
                command.Parameters.AddWithValue("@ValorAyuda", beneficiario.ValorAyuda);
                var filas = command.ExecuteNonQuery();
            }
        }
        public void GuardarGlosas(Beneficiario beneficiario)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"Insert Into NoBeneficiariosParcial3 (CodigoProveedor,Cedula,NombreBeneficiario,Fecha,ValorAyuda) 
                                        values (@CodigoProveedor,@Cedula,@NombreBeneficiario,@Fecha,@ValorAyuda)";
                command.Parameters.AddWithValue("@CodigoProveedor", beneficiario.CodigoProveedor);
                command.Parameters.AddWithValue("@Cedula", beneficiario.Cedula);
                command.Parameters.AddWithValue("@NombreBeneficiario", beneficiario.NombreBeneficiario);
                command.Parameters.AddWithValue("@Fecha", beneficiario.Fecha);
                command.Parameters.AddWithValue("@ValorAyuda", beneficiario.ValorAyuda);
                var filas = command.ExecuteNonQuery();
            }
        }


        public bool GuardarConValidacion(string ruta, string codigoProveedor, string fecha)
        {
            FileStream origen = new FileStream(ruta, FileMode.OpenOrCreate);
            StreamReader reader = new StreamReader(origen);
            string linea = string.Empty;

            while ((linea = reader.ReadLine()) != null)
            {
                Beneficiario beneficiario = MapearCarga(linea);

                if ((beneficiario.CodigoProveedor == codigoProveedor) && (beneficiario.Fecha == fecha) && (beneficiario.ValorAyuda >= 200000) && beneficiario.ValorAyuda <= 500000)
                {
                    Guardar(beneficiario);                   
                }
                else
                {
                    GuardarGlosas(beneficiario);
                }

            }

            reader.Close();
            origen.Close();
            return true;
        }

    }


}

