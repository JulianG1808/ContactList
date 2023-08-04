using ProyectoCrud.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace ProyectoCrud.Controllers
{
    public class ContactController : Controller
    {
        //cadena de conexion para conectarse a SQL Server
        //                     este metodo nos conecta con el archivo, web.config
        private static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();

        private static List<Contact> olista = new List<Contact>();


        // GET: Contact
        public ActionResult Inicio()
        {
            
            olista = new List<Contact>();

            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM CONTACTO", oconexion);
                cmd.CommandType = CommandType.Text;
                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contact newContact = new Contact();

                        newContact.IdContacto = Convert.ToInt32(dr["IdContacto"]);
                        newContact.Nombres = dr["Nombres"].ToString();
                        newContact.Apellidos = dr["Apellidos"].ToString();
                        newContact.Telefono = dr["Telefono"].ToString();
                        newContact.Correo = dr["Correo"].ToString();

                        olista.Add(newContact);
                    }
                }
            }
                
            return View(olista);
        }
    }
}