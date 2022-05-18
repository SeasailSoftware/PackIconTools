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

        public KindModel(int index, Assembly value, string typeName, object kind)
        {
            _index = index;
            this._assembly = value;
            Type = _assembly.GetType($"MahApps.Metro.IconPacks.PackIcon{typeName}");
            this._kindName = kind.ToString();
            Kind = kind;
            _imageSource = MahAppsPackIconHelper.CreateImageSource(kind, Brushes.Black);
            _pathData = MahAppsPackIconHelper.GetPathData(kind);
        }

        private string _pathData;
        public string PathData
        {
            get => _pathData;
            set
            {
                _pathData = value;
                NotifyOfPropertyChange(() => PathData);
            }
        }

        private int _index;
        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                NotifyOfPropertyChange(() => Index);
            }
        }

        public string KindName
        {
            get => _kindName;
            set
            {
                _kindName = value;
                NotifyOfPropertyChange(() => KindName);
            }
        }

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
