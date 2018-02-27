using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Hydros.Model;
using System.IO;

namespace Hydros.Controller
{
    public class CloudiaryController
    {
        string Cloudname = "mrdzen";
        string APIKey = "924589665466888";
        string APISecret = "_orGXLDJpFw2Hrv_LpqvpWMe8H4";

        /*
        string Cloudname = "magnussen-home";
        string APIKey = "476398767755447";
        string APISecret = "o0qrMWr8cwIEtQbNKjcwsd-Tqhw";*/

        private static CloudiaryController _instance;
        public static CloudiaryController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CloudiaryController();
                }
                return _instance;
            }
        }

        public ImageUploadResult UploadPhoto(string filePath, string GroupID)
        {
            Account acc = new Account(Cloudname, APIKey, APISecret);
            Cloudinary cloudinary = new Cloudinary(acc);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(filePath),
                UseFilename = true,
                Folder = GroupID
            };
            //uploadParams.PublicId = "dzen";
            //uploadParams.Tags = "dzen,alo,123";
            var uploadResult = cloudinary.Upload(uploadParams);

            //Add to Database
            CloudinaryInfo objInfo = new CloudinaryInfo();
            objInfo.PublicID = uploadResult.PublicId;
            objInfo.Version = uploadResult.Version;
            objInfo.Format = uploadResult.Format;
            objInfo.ResoureType = uploadResult.ResourceType;
            objInfo.URI = uploadResult.Uri.AbsoluteUri;
            objInfo.SecureURI = uploadResult.SecureUri.AbsoluteUri;
            objInfo.Tags = string.Join(",",uploadResult.Tags);
            objInfo.Bytes = uploadResult.Length;
            objInfo.CreatedAt = uploadResult.CreatedAt;
            DatabaseController.Instance.Hydros_Cloudinary_Add(objInfo);

            return uploadResult;
        }

        public RawUploadResult UploadDocument(string filePath, string GroupID)
        {
            Account acc = new Account(Cloudname, APIKey, APISecret);
            Cloudinary cloudinary = new Cloudinary(acc);

            var uploadParams = new RawUploadParams()
            {
                File = new FileDescription(filePath),
                UseFilename = true,
                Folder = GroupID
            };
            //uploadParams.PublicId = "dzen";
            //uploadParams.Tags = "dzen,alo,123";
            var uploadResult = cloudinary.Upload(uploadParams);

            //Add to Database
            CloudinaryInfo objInfo = new CloudinaryInfo();
            objInfo.PublicID = uploadResult.PublicId;
            objInfo.Version = uploadResult.Version;
            //objInfo.Format = uploadResult.Format;
            objInfo.Format = Path.GetExtension(filePath).Replace(".","");
            objInfo.ResoureType = uploadResult.ResourceType;
            objInfo.URI = uploadResult.Uri.AbsoluteUri;
            objInfo.SecureURI = uploadResult.SecureUri.AbsoluteUri;
            objInfo.Tags = string.Join(",", uploadResult.Tags);
            objInfo.Bytes = uploadResult.Length;
            objInfo.CreatedAt = uploadResult.CreatedAt;
            DatabaseController.Instance.Hydros_Cloudinary_Add(objInfo);

            return uploadResult;
        }

    }
}
