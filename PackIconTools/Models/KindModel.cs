using Caliburn.Micro;
using PackIconTools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PackIconTools.Models
{
    public class KindModel : PropertyChangedBase
    {
        public Type Type { get; set; }
        private Assembly _assembly;
        private string _kindName;
        public object Kind { get; private set; }

        public KindModel(Assembly value, string typeName, object kind)
        {
            this._assembly = value;
            Type = _assembly.GetType($"MahApps.Metro.IconPacks.PackIcon{typeName}");
            this._kindName = kind.ToString();
            Kind = kind;
            _imageSource = MahAppsPackIconHelper.CreateImageSource(kind, Brushes.Black);
        }

        public string KindName => _kindName;

        private ImageSource _imageSource;

        public ImageSource ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                NotifyOfPropertyChange(() => ImageSource);
            }
        }
    }
}
