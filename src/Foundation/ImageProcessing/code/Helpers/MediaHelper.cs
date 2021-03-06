using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace VRBYOD.Foundation.ImageProcessing.Helpers
{
    public static class MediaHelper
    {

        public static string CalculateMd5(MediaItem media)
        {
            byte[] hash;
            using (var md5 = MD5.Create())
            {
                using (var stream = media.GetMediaStream())
                {
                    hash = md5.ComputeHash(stream);
                }
            }
            return BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);
        }

        public static bool RemoveFile(string Path)
        {
            if (!File.Exists(Path))
                return false;
            File.Delete(Path);
            return true;
        }
    }
}