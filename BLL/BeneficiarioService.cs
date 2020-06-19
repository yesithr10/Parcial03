using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;
namespace BLL
{
    public class BeneficiarioService
    {
        private readonly ConnectionManager conexion;
        private readonly BeneficiarioRepository repositorio;
        public BeneficiarioService(string connectionString)
        {
            conexion = new ConnectionManager(connectionString);
            repositorio = new BeneficiarioRepository(conexion);
        }

        public RespuestaConsulta CargarArchivo(string FileStream)
        {
            RespuestaConsulta respuestaConsulta = new RespuestaConsulta();
            try
            {
                respuestaConsulta.Liquidacion = repositorio.CargarArchivo(FileStream);
                respuestaConsulta.Error = false;
                respuestaConsulta.Mensaje = "Cargado Correctamente";
                return respuestaConsulta;
            }
            catch (Exception)
            {
                respuestaConsulta.Error = true;
                respuestaConsulta.Mensaje = "No se cargaron los datos";
                return respuestaConsulta;
            }
            
        }
        public string Guardar(Beneficiario beneficiario)
        {
            try
            {
                conexion.Open();
                repositorio.Guardar(beneficiario);
                return $"Se guardaron los datos satisfactoriamente";
            }
            catch (Exception e)
            {
                return $"Error de la Aplicacion: {e.Message}";
            }
            finally { conexion.Close(); }
        }
        public string GuardarGlosas(Beneficiario beneficiario)
        {
            try
            {
                conexion.Open();
                repositorio.GuardarGlosas(beneficiario);
                return $"Se guardaron los datos satisfactoriamente";
            }
            catch (Exception e)
            {
                return $"Error de la Aplicacion: {e.Message}";
            }
            finally { conexion.Close(); }
        }
        public bool Cargar(string ruta, string codigoProveedor, string fecha)
        {

            try
            {
                conexion.Open();
                return repositorio.GuardarConValidacion(ruta, codigoProveedor, fecha);
            }
            catch (Exception e)
            {

                return false;
            }
            finally
            {
                conexion.Close();
            }


        }
    }


    


    public class RespuestaConsulta
    {
        public bool Error;
        public string Mensaje;
        public List<Beneficiario> Liquidacion;
    }
}
