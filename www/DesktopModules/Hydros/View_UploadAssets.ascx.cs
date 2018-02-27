using System;
using DotNetNuke;
using DotNetNuke.Entities.Modules;
using Hydros.Model;
using Telerik.Web.UI;
using Hydros.Controller;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;

namespace Hydros
{
    public partial class View_UploadAssets : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btUpload_Click(object sender, EventArgs e)
        {

            AssetInfo objInfo = new AssetInfo();
            CloudinaryInfo objCloudinaryInfo = new CloudinaryInfo();
            AssetController objCtrl = new AssetController();
            DateTime CurrentDate = DateTime.Now;

            foreach (UploadedFile postedFile in radAUpload.UploadedFiles)
            {
                var filePath = Server.MapPath("~/Portals/UploadedAssets/" + postedFile.FileName);
                postedFile.SaveAs(filePath);
                //UPload to Cloudiary
                string returnID = CloudiaryController.Instance.UploadPhoto(filePath, txtGroupID.Text).PublicId;

                //Add Asset to Database
                objInfo.AssetID = returnID;
                objInfo.AssetType = sltAccessType.Value;
                objInfo.CreatedBy = UserId;
                objInfo.DateAdded = CurrentDate;
                objInfo.DateModified = CurrentDate;
                objInfo.ItemID = txtItemID.Text;
                objInfo.ItemType = sltItemType.Value;
                objInfo.ModifiedBy = UserId;

                objInfo.IsDeleted = 0;
                objInfo.IsPrimary = 0;
                objInfo.Order = 0;

                objCtrl.AddItem(objInfo);
            }
            Response.Redirect(Globals.NavigateURL("Update", "mid=" + this.ModuleId + 
                "&ItemID=" + txtItemID.Text + 
                "&ItemType=" + sltItemType.Value +
                "&AssetType=" + sltAccessType.Value +
                "&popUp=true"));

        }
    }
}