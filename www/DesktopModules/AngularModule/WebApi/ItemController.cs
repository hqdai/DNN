using DotNetNuke.Security;
using DotNetNuke.Web.Api;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngularModule.Model;
using AngularModule.Controller;
using System;
using DotNetNuke.Common.Utilities;

namespace AngularModule.WebApi
{
    public class ItemController : DnnApiController
    {

        /// <summary>
        /// API that returns Hello world
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("test")]
        [AllowAnonymous]
        public HttpResponseMessage HelloWorld()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Hello World!");
        }

        /// <summary>
        /// API that creates a new item in the item list
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("new")] // /API/item/new
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public HttpResponseMessage AddItem(ItemInfo item)
        {
            try
            {
                item.CreatedByUserId = UserInfo.UserID;
                item.CreatedOnDate = DateTime.Now;
                item.LastModifiedByUserId = UserInfo.UserID;
                item.LastModifiedOnDate = DateTime.Now;
                int returnID = AppController.Instance.AddItem(item);
                return Request.CreateResponse(HttpStatusCode.OK, returnID);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// API that returns an item list
        /// </summary>
        [HttpPost, HttpGet]
        [AllowAnonymous]
        [ActionName("list")]  // /API/item/list
        //[DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public HttpResponseMessage GetModuleItems()
        {
            try
            {
                var itemList = AppController.Instance.ListItems().ToJson();
                return Request.CreateResponse(HttpStatusCode.OK, itemList);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// API that returns a single item
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [ActionName("byid")]  // /API/item/byid/{id}

        public HttpResponseMessage GetItem(int id)
        {
            try
            {
                var item = AppController.Instance.GetItem((id)).ToJson();
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// API that returns a single item
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [ActionName("byida")]  // /API/item/byid/{id}

        public HttpResponseMessage GetItem(int id, string name)
        {
            try
            {
                var item = AppController.Instance.GetItem((id)).ToJson();
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}