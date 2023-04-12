using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

using System.Xml.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly string cadenaSql;

        public HomeController(IConfiguration _config)
        {
            cadenaSql = _config.GetConnectionString("CadenaSQL");
        }

        public IActionResult Index()
        {

            return View();
        }

        //reference models
        //reference System.Xml.Linq;
        [HttpPost]
        public JsonResult GuardarVenta([FromBody] Venta body) {

            XElement venta = new XElement("Venta",
                new XElement("NumeroDocumento", body.NumeroDocumento),
                new XElement("RazonSocial", body.RazonSocial),
                new XElement("Total", body.Total)
            );

            XElement oDetalleVenta = new XElement("Detalle_Venta");
            foreach (Detalle_Venta item in body.lstDetalleVenta)
            {
                oDetalleVenta.Add(new XElement("Item",
                    new XElement("Producto", item.Producto),
                    new XElement("Precio", item.Precio),
                    new XElement("Cantidad", item.Cantidad),
                    new XElement("Total", item.Total)
                    ));
            }
            venta.Add(oDetalleVenta);

            using (SqlConnection oconexion = new SqlConnection(cadenaSql))
            {
                oconexion.Open();
                SqlCommand cmd = new SqlCommand("sp_GuardarVenta", oconexion);
                cmd.Parameters.Add("venta_xml", SqlDbType.Xml).Value = venta.ToString();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }

            return Json(new {respuesta = true });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}