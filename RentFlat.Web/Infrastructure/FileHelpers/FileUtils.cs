﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace RentFlat.Web.Infrastructure.FileHelpers
{
    public class FileUtils
    {
        public static readonly string UPLOAD_PATH = "~/Content/Uploads/";
        public static string UploadFile(HttpPostedFileBase file)
        {
            string filename = null;

            if (file != null && file.ContentLength > 0)
            {
                filename = GetFileName(file.FileName);
                string path = Path.Combine(HostingEnvironment.MapPath(UPLOAD_PATH), filename);
                file.SaveAs(path);
            }

            return filename;
        }

        public static string UploadFile(HttpPostedFileBase file, string fileFolder)
        {
            string filename = null;

            if (file != null && file.ContentLength > 0)
            {
                filename = GetFileName(file.FileName);
                string path = Path.Combine(HostingEnvironment.MapPath(UPLOAD_PATH + fileFolder), filename);
                file.SaveAs(path);
            }

            return filename;
        }

        public static string UploadFile(HttpPostedFileBase file, string username, string flatId)
        {
            string filename = null;

            if (file != null && file.ContentLength > 0)
            {
                filename = GetFileName(file.FileName);
                string uploadDirectoryPath = Path.Combine(HostingEnvironment.MapPath(UPLOAD_PATH), username, flatId);

                if (!Directory.Exists(uploadDirectoryPath))
                {
                    Directory.CreateDirectory(uploadDirectoryPath);
                }
                string path = Path.Combine(HostingEnvironment.MapPath(UPLOAD_PATH), username, flatId, filename);

                file.SaveAs(path);
            }

            return filename;
        }

        public static string GetFileName(string uploadedFileName)
        {
            string filename = Guid.NewGuid() + Path.GetExtension(uploadedFileName);
            return filename;
        }
    }
}