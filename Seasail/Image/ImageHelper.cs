using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seasail.Image
{
    public static class ImageHelper
    {
        public static string ImageToBase64String(System.Drawing.Image img)
        {
            //var fileName = $@"{Environment.CurrentDirectory}\{Guid.NewGuid()}.jpg";
            //image.Save(fileName, image.RawFormat);
            try
            {
                using (var ms = new MemoryStream())
                {
                    img.Save(ms, img.RawFormat);
                    byte[] bytes = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(bytes, 0, bytes.Length);
                    return System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(bytes, Base64FormattingOptions.None));
                }
            }
            catch (Exception ex)
            {
                Logging.LogManager.Error(ex);
                return string.Empty;
            }

        }


    }
}
