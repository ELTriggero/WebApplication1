using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    /// <summary>
    /// Descrizione di riepilogo per WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Per consentire la chiamata di questo servizio Web dallo script utilizzando ASP.NET AJAX, rimuovere il commento dalla riga seguente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        DataTable dtCountries = new DataTable();
        DbAccess objDbAccess = new DbAccess();
        DataTable dtUsers = new DataTable();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int Sum(int a, int b)
        {
            return a+b;
        }

        [WebMethod]
        public string Countries()
        {
            dtCountries.Columns.Add("CountryName");
            dtCountries.Columns.Add("Continent");

            dtCountries.Rows.Add("Pakistan", "Asia");
            dtCountries.Rows.Add("india", "Asia");
            dtCountries.Rows.Add("Germany", "Europe");
            dtCountries.Rows.Add("Srilanka", "Asia");
            dtCountries.Rows.Add("Australia", "Australia");
            dtCountries.Rows.Add("New Zeland", "Austrlia");

            return JsonConvert.SerializeObject(dtCountries);
        }

        [WebMethod]
        public string dataTablesForusers(string id)
        {
            string query = "Select * From Users Where ID = '" + id + "'";
            objDbAccess.readDatathroughAdapter(query, dtUsers);

            string result = JsonConvert.SerializeObject(dtUsers);
            return result;
        }
    }
}
