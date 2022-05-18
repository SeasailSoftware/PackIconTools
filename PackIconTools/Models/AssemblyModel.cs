using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PackIconTools.Models
{
    public class AssemblyModel : PropertyChangedBase
    {
        public string TypeName { get; private set; }
        public AssemblyModel(Assembly assembly)
        {
            _assembly = assembly;
            InitializeKinds();
            _name = assembly.ManifestModule.Name;
        }

        private void InitializeKinds()
        {
            var items = _assembly.ManifestModule.Name.Replace(".dll", "").Split('.');
            if (items == null || items.Length < 4) return;
            TypeName = items[3];
            items[3] = $"PackIcon{items[3]}Kind";
            var kind = string.Join('.', items);
            var t = _assembly.GetType(kind);
            if (t == null) return;
            var instance = Activator.CreateInstance(t);
            var values = Enum.GetValues(t);
            var index = 1;
            foreach (var item in values)
            {

                //var kindType = $"MahApps.Metro.IconPacks.{items[3]}.{item}";
                Kinds.Add(new KindModel(index++, _assembly, TypeName, item));
            }
        }


        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private Assembly _assembly;
        public Assembly Assembly
        {
            get => _assembly;
            set
            {
                _assembly = value;
                NotifyOfPropertyChange(() => Assembly);
            }
        }



        public ObservableCollection<KindModel> Kinds { get; set; } = new ObservableCollection<KindModel>();
    }
}
