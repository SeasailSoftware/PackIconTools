using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PackIconTools
{
    public class Options
    {
        public string SavePath { get; set; }
        public string Theme { get; set; } = "Light.Blue";
        public string Culture { get; set; } = "zh-CN";
        public ImageFormat OutputFormat { get;  set; }
        public Color SelectedColor { get;  set; }
        public string Size { get;  set; }

        internal static Options Load(string path = "")
        {
            if (string.IsNullOrEmpty(path))
                path = "option.json";
            if (File.Exists(path))
            {
                var context = File.ReadAllText(path);
                var options = JsonConvert.DeserializeObject<Options>(context);
                return options;
            }
            return null;
        }

        internal void Save(string path = "")
        {
            if (string.IsNullOrEmpty(path))
                path = "option.json";
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(path, json);
        }
    }
}
