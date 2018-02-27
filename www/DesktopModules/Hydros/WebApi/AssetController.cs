using DotNetNuke.Security;
using DotNetNuke.Web.Api;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hydros.Model;
using Hydros.Controller;
using System;
using DotNetNuke.Common.Utilities;
using DotNetNuke;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http.Formatting;
using System.IO;

namespace Hydros.WebApi
{
    public class AssetController : DnnApiController
    {
        [HttpGet]
        [ActionName("test")]
        [AllowAnonymous]
        public HttpResponseMessage HelloWorld()
        {
            Hydros.Model.AssetController AS = new Model.AssetController();
            IEnumerable<AssetInfo> info = AS.GetByItem("B1428-01");

            return Request.CreateResponse(HttpStatusCode.OK, info.ToJson());
        }

        [HttpGet]
        [ActionName("PhotoGetByItem")]
        [AllowAnonymous]
        public HttpResponseMessage Photo_GetByItem(string ItemID)
        {
            try
            { 
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(DatabaseController.Instance.Hydros_Assets_Photo_GetByItem(ItemID).ToJson(), 
                    Encoding.UTF8, 
                    "application/json");
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        [ActionName("DocumentGetByItem")]
        [AllowAnonymous]
        public HttpResponseMessage Document_GetByItem(string ItemID)
        {
            try
            {
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(DatabaseController.Instance.Hydros_Assets_Document_GetByItem(ItemID).ToJson(),
                    Encoding.UTF8,
                    "application/json");
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpPost]
        [ActionName("UploadFile")] // /API/Asset/UploadFile
        [AllowAnonymous]
        public HttpResponseMessage AddItem()
        {
            try
            {
                AssetInfo objInfo = new AssetInfo();
                CloudinaryInfo objCloudinaryInfo = new CloudinaryInfo();
                AssetController objCtrl = new AssetController();
                DateTime CurrentDate = DateTime.Now;

                var httpRequest = HttpContext.Current.Request;
                string ItemID = httpRequest["ItemID"];
                string GroupID = httpRequest["GroupID"];
                string ItemType = httpRequest["ItemType"];
                string AssetType = httpRequest["AssetType"];

                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        var filePath = HttpContext.Current.Server.MapPath("~/Portals/UploadedAssets/" + postedFile.FileName);
                        postedFile.SaveAs(filePath);

                        //Upload to Cloudiary
                        string returnID = "";
                        if (Path.GetExtension(filePath) == "jpg" || Path.GetExtension(filePath) == "jpeg")
                        {
                            returnID = CloudiaryController.Instance.UploadPhoto(filePath, GroupID).PublicId;
                        }
                        else
                        {
                            returnID = CloudiaryController.Instance.UploadDocument(filePath, GroupID).PublicId;
                        }


                        //Add Asset to Database
                        objInfo.AssetID = returnID;
                        objInfo.AssetType = AssetType; //Photo,Video,Document
                        objInfo.CreatedBy = UserInfo.UserID;
                        objInfo.DateAdded = CurrentDate;
                        objInfo.DateModified = CurrentDate;
                        objInfo.ItemID = ItemID;
                        objInfo.ItemType = ItemType; //LineArt,CameraShot,Document.StudySheet ..., Video.Youtue ... Vimeo
                        objInfo.ModifiedBy = UserInfo.UserID;

                        objInfo.IsDeleted = 0;
                        objInfo.IsPrimary = 0;
                        objInfo.Order = 0;

                        DatabaseController.Instance.Hydros_Assets_Add(objInfo);

                    }

                }
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        #region "Unused"
        //[HttpPost]
        //[ActionName("New")] // /API/Asset/new
        //[AllowAnonymous]
        //public HttpResponseMessage AddItem(AssetInfo item)
        //{
        //    try
        //    {
        //        item.CreatedBy = UserInfo.UserID;
        //        item.DateAdded = DateTime.Now;
        //        item.ModifiedBy = UserInfo.UserID;
        //        item.DateModified = DateTime.Now;
        //        DatabaseController.Instance.Hydros_Assets_Add(item);
        //        return Request.CreateResponse(HttpStatusCode.Created);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        //[HttpPost]
        //[ActionName("Update")] // /API/Asset/Update
        //[AllowAnonymous]
        //public HttpResponseMessage Update(AssetInfo item)
        //{
        //    try
        //    {
        //        item.ModifiedBy = UserInfo.UserID;
        //        item.DateModified = DateTime.Now;
        //        DatabaseController.Instance.Hydros_Assets_Update(item);
        //        return Request.CreateResponse(HttpStatusCode.Created);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
        #endregion

        [HttpPost]
        [ActionName("UpdatePrimary")] // /API/Asset/UpdatePrimary
        [AllowAnonymous]
        public HttpResponseMessage UpdatePrimary()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                string AssetID = httpRequest["AssetID"];
                string ItemID = httpRequest["ItemID"];
                string AssetType = httpRequest["AssetType"];
                string ItemType = httpRequest["ItemType"];

                AssetInfo item = new AssetInfo();
                item.AssetID = AssetID;
                item.ItemID = ItemID;
                item.ItemType = ItemType;
                item.AssetType = AssetType;
                item.ModifiedBy = UserInfo.UserID;
                item.DateModified = DateTime.Now;
                
                DatabaseController.Instance.Hydros_Assets_Update_Primary(item);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete] 
        //API/Asset/Delete?AssetID=@AssetID
        [AllowAnonymous]
        public IHttpActionResult Delete(string AssetID)
        {
            try
            {
                DatabaseController.Instance.Hydros_Assets_Delete(AssetID, UserInfo.UserID);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        [ActionName("Summary")] // /API/Asset/Summary
        [AllowAnonymous]
        public HttpResponseMessage Summary(JQDTParams dtparams)
        {
            try
            {
                //Request DataTables Variable from Client
                int end = dtparams.length + dtparams.start;
                int start = (dtparams.start > 0)? dtparams.start + 1:dtparams.start;
                int draw = dtparams.draw;
                string search = dtparams.search.value;
                int colsort = dtparams.order[0].column;
                string dirsort = dtparams.order[0].dir.ToString();

                var data = DatabaseController.Instance.Hydros_Assets_Summary(search, colsort, dirsort, start, end).ToList();
                int recordsFiltered = DatabaseController.Instance.Hydros_Assets_Summary_CountFilteredRow(search, colsort, dirsort, start, end);
                int recordsTotal = DatabaseController.Instance.Hydros_Assets_Summary_TotalRow();
                DataTables_AssetSummaryInfo returnTable = new DataTables_AssetSummaryInfo();
                
                returnTable.draw = draw;
                returnTable.recordsTotal = recordsTotal;
                returnTable.recordsFiltered = recordsFiltered;
                returnTable.data = data;

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(returnTable.ToJson(), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
