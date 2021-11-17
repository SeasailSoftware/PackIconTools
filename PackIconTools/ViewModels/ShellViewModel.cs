using Caliburn.Micro;
using PackIconTools.Core;
using PackIconTools.Models;
using PackIconTools.Utils;
using Seasail.i18N;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Forms;
using Screen = Caliburn.Micro.Screen;

namespace PackIconTools.ViewModels
{
    [Export(typeof(IShell))]
    class ShellViewModel : Screen
    {
        public ITranslater Translater => IoC.Get<ITranslater>();
        public override string DisplayName => Translater.Trans(nameof(ShellViewModel));

        public Options Options => IoC.Get<Options>();

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            LoadAssemblies();
            return base.OnActivateAsync(cancellationToken);
        }

        private void LoadAssemblies()
        {

            foreach (var filePath in Directory.GetFileSystemEntries(AppDomain.CurrentDomain.BaseDirectory))
            {
                var file = new FileInfo(filePath);
                if (file.Name.StartsWith("MahApps.Metro"))
                {
                    var assembly = Assembly.LoadFrom(filePath);
                    Assemblies.Add(new AssemblyModel(assembly));
                }
            }
        }

        public ObservableCollection<AssemblyModel> Assemblies { get; set; } = new ObservableCollection<AssemblyModel>();


        private AssemblyModel _currentAssembly;
        public AssemblyModel CurrentAssembly
        {
            get => _currentAssembly;
            set
            {
                _currentAssembly = value;
                NotifyOfPropertyChange(() => CurrentAssembly);
            }
        }

        private KindModel _currentKind;
        public KindModel CurrentKind
        {
            get => _currentKind;
            set
            {
                _currentKind = value;
                NotifyOfPropertyChange(() => CurrentKind);
            }
        }

        public string SavePath
        {
            get => Options.SavePath;
            set
            {
                Options.SavePath = value;
                NotifyOfPropertyChange(() => SavePath);
            }
        }

        public System.Drawing.Imaging.ImageFormat[] OutputFormats => new System.Drawing.Imaging.ImageFormat[]
        {
           System.Drawing.Imaging.ImageFormat.Bmp,
           System.Drawing.Imaging.ImageFormat.Png,
           System.Drawing.Imaging.ImageFormat.Gif,
           System.Drawing.Imaging.ImageFormat.Jpeg,
           System.Drawing.Imaging.ImageFormat.Icon
        };

        public System.Drawing.Imaging.ImageFormat OutputFormat
        {
            get => Options.OutputFormat;
            set
            {
                Options.OutputFormat = value;
                NotifyOfPropertyChange(() => OutputFormat);
            }
        }
        public Color SelectedColor
        {
            get => Options.SelectedColor;
            set
            {
                Options.SelectedColor = value;
                NotifyOfPropertyChange(() => SelectedColor);
            }
        }

        public string[] Sizes => new string[] { "24 * 24", "36 * 36", "48 * 48", "64 * 64" };

        public string Size
        {
            get => Options.Size;
            set
            {
                Options.Size = value;
                NotifyOfPropertyChange(() => Size);
            }
        }

        public RelayCommand SelectSavePathCommand => new RelayCommand(x=>
        {
            using (FolderBrowserDialog openFileDialog = new FolderBrowserDialog())  //选择文件夹
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)//注意，此处一定要手动引入System.Window.Forms空间，否则你如果使用默认的DialogResult会发现没有OK属性
                {
                    SavePath = openFileDialog.SelectedPath;
                }
            }
        });


        public RelayCommand GenerateCommand => new RelayCommand(x =>
        {
            var brush = new SolidColorBrush(SelectedColor);
            var width = Convert.ToInt32(Size.Split(" ")[0]);
            var imageSource = MahAppsPackIconHelper.CreateImageSource(CurrentKind.Kind, brush);
            var image = (imageSource as DrawingImage).ToBitmap(width, width);
            if (image != null)
            {
                var fileName = $@"{SavePath}/{Guid.NewGuid()}.{OutputFormat.ImageSuffix()}";
                if(OutputFormat == System.Drawing.Imaging.ImageFormat.Icon)
                {
                    using (System.Drawing.Bitmap iconBm = new System.Drawing.Bitmap(image))
                    {
                        using (System.Drawing.Icon icon = System.Drawing.Icon.FromHandle(iconBm.GetHicon()))
                        {
                            using (var stream = new FileStream(fileName, FileMode.Create))
                            {
                                icon.Save(stream);
                            }
                        }
                    }
                }
                else
                {
                    image.Save($@"{SavePath}/{Guid.NewGuid()}.{OutputFormat.ImageSuffix()}", OutputFormat);
                }
            }

        }, y => CurrentKind != null && OutputFormat != null && !string.IsNullOrEmpty(Size) && !string.IsNullOrEmpty(SavePath));
    }
}
