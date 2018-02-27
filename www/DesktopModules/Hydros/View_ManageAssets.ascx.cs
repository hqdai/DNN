using System;
using DotNetNuke;
using DotNetNuke.Entities.Modules;
using Hydros.Model;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Common;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace Hydros
{
    public partial class View_ManageAssets : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
               
            }
            
        }

        //public string OpenPopUp(int Height, int Width)
        //{
        //    string url = "return " + UrlUtils.PopUpUrl(
        //            Globals.NavigateURL("Update", "mid=" + this.ModuleId + "&ItemID=B1428-01&ItemType=Item&AssetType=Photo"),
        //            this,
        //            PortalSettings,
        //            true,
        //            false,
        //            (Height),
        //            (Width),
        //            false,
        //            "");
        //    return url;
        //}
    }
}