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

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
            await LoadAssemblies();
        }

        private Task LoadAssemblies()
        {
            return Task.Run(() =>
            {
                foreach (var filePath in Directory.GetFileSystemEntries(AppDomain.CurrentDomain.BaseDirectory))
                {
                    var file = new FileInfo(filePath);
                    if (file.Name.StartsWith("MahApps.Metro"))
                    {
                        var assembly = Assembly.LoadFrom(filePath);
                        App.Current.Dispatcher.Invoke(new System.Action(() =>
                        {
                            Assemblies.Add(new AssemblyModel(assembly));
                        }));
                    }
                }
            });
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



        public RelayCommand GenerateCommand => new RelayCommand(x =>
        {
            var dlg = new SaveFileDialog() { Filter = "Bmp File (*.bmp)|*.bmp|Png File(*.png)|*.png|Jpeg File(*.jpg)|*.jpg|Icon File(*.ico)|*.ico" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var brush = new SolidColorBrush(SelectedColor);
                var width = Convert.ToInt32(Size.Split(" ")[0]);
                var imageSource = MahAppsPackIconHelper.CreateImageSource(CurrentKind.Kind, brush);

                var image = (imageSource as DrawingImage).ToBitmap(width, width);
                if (image != null)
                {
                    //image.Save(dlg.FileName);
                    //IconGenerator.ConvertImageToIcon(dlg.FileName, new System.Drawing.Size(64, 64));
                    var fileName = dlg.FileName;
                    IconGenerator.Save(image, dlg.FileName, new System.Drawing.Size(width, width));
                }
            }

        }, y => CurrentKind != null);

        public RelayCommand ExportToXamlCommand => new RelayCommand(x =>
        {
            var dlg = new SaveFileDialog() { Filter = "Xaml File (*.xaml)|*.xaml" };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try
            {
                var buffer = new StringBuilder();
                buffer.AppendLine("<ResourceDictionary xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">");
                buffer.AppendLine("<!--  Path  Begin-->");
                foreach (var kind in CurrentAssembly.Kinds)
                {
                    buffer.AppendLine($"      <Geometry x:Key=\"Path_{CurrentAssembly.TypeName}_{kind.KindName}\">{kind.PathData}</Geometry>");
                }
                buffer.AppendLine("<!--  Path  End-->");
                buffer.AppendLine("</ResourceDictionary>");
                System.IO.File.WriteAllText(dlg.FileName, buffer.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Seasail.Logging.LogManager.Error(ex);
            }

        }, y => CurrentAssembly != null);
    }
}
