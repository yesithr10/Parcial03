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
        BeneficiarioRepository beneficiarioRepository;
        Beneficiario beneficiario;
        public RespuestaConsulta CargarArchivo(string FileStream)
        {
            RespuestaConsulta respuestaConsulta = new RespuestaConsulta();
            try
            {
                respuestaConsulta.Liquidacion = beneficiarioRepository.CargarArchivo(FileStream);
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
    }
    
    public class RespuestaConsulta
    {
        public bool Error;
        public string Mensaje;
        public List<Beneficiario> Liquidacion;
    }
}
