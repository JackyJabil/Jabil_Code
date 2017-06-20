using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OEE_DASHBOARD
{
    public partial class ShowmachineOEE : System.Web.UI.Page
    {
        public string  id = "1";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    id = Request.QueryString["id"].ToUpper().Replace("D", "");
                }
            }

        }
    }
}