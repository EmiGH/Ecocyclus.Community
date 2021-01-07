using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace CSI.WebUI.Console.Common
{
    public partial class ImageViewer : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            if (!IsPostBack)
            {
                if (Request.QueryString["IdFile"] != null)
                {
                    Int64 _IdFile = Convert.ToInt64(Request.QueryString["IdFile"]);

                    try
                    {
                        Library.Objects.Auxiliaries.Files.File _file = ((Library.Operation)Session["Operation"]).File(_IdFile);
                        
                        String _fileName = _file.Name;
                        String _fileExtension = _fileName.Substring(_fileName.LastIndexOf(".") + 1);

                        Response.Clear();
                        Response.Cache.SetCacheability(HttpCacheability.Public);
                        Response.Cache.SetLastModified(DateTime.Now);
                        Response.Buffer = true;
                        Response.ContentType = _file.Type;
                        Response.AddHeader("Content-Length", _file.Stream.Length.ToString());
                        Response.AddHeader("Content-Type", _file.Type);
                        Response.AddHeader("Content-Disposition", "filename=" + DateTime.Now.ToString() + "." + _fileExtension);

                        Response.BinaryWrite(_file.Stream);
                    }
                    catch
                    {
                        //Si encuentra un error al cargar los Resources...por defecto muestra la imagen "Sin Imagen"
                        Response.BinaryWrite(File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/Images/NoImagesAvailable.gif"));
                    }
                }
                else
                    Response.BinaryWrite(File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/Images/NoImagesAvailable.gif"));
            }

        }
    }
}