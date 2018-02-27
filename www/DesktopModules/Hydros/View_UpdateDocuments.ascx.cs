using System;
using DotNetNuke;
using DotNetNuke.Entities.Modules;
using Hydros.Model;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Common;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DotNetNuke.Services.Exceptions;
using Hydros.Controller;
using System.IO;
using DotNetNuke.UI.Utilities;

namespace Hydros
{
    public partial class View_UpdateDocuments : PortalModuleBase
    {
        public string ItemID = "";
        public string AssetType = "";
        public string GroupID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ItemID = (Request.Params["ItemID"] == null) ? null : Request.Params["ItemID"].ToString();
                AssetType = (Request.Params["AssetType"] == null) ? null : Request.Params["AssetType"].ToString();

                if (!IsPostBack)
                {
                    try
                    {
                        GroupID = DatabaseController.Instance.Hydros_GroupID_Get(ItemID);
                    }
                    catch (Exception ex)
                    {
                        Exceptions.ProcessModuleLoadException(this, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

    }
}