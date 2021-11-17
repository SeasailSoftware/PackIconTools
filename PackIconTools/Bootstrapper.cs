using Caliburn.Micro;
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using PackIconTools.Core;
using PackIconTools.i18N;
using Seasail.Extensions;
using Seasail.i18N;
using Seasail.IO;
using Seasail.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace PackIconTools
{
    public class Bootstrapper : BootstrapperBase
    {
        private CompositionContainer _container;
        private Options _options;
        private ITranslater _translater;
        //初始化
        public Bootstrapper()
        {
            Initialize();
        }

        //重写Configure
        protected override void Configure()
        {
            var aggregateCatalog = new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x))
     .OfType<ComposablePartCatalog>());

            _container = new CompositionContainer(aggregateCatalog);
            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(_container);
            //初始化配置文件
            InitializeConfig();
            batch.AddExportedValue(_options);
            //batch.AddExportedValue<AppSettings>(_config);
            //batch.AddExportedValue<DeviceService>(new DeviceService());
            //batch.AddExportedValue<Seasail.Core.Control.Views.Dialog.MessageBoxView>
            //初始化语言
            InitializeCulture();
            batch.AddExportedValue(_translater);
            _container.Compose(batch);
        }


        protected override object GetInstance(Type service, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
            var exports = _container.GetExportedValues<object>(contract);
            Seasail.Data.Check.NotNull(exports, $"Could not locate any instances of contract {contract}.");
            return exports.First();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetExportedValues<object>(
                AttributedModelServices.GetContractName(service));
        }


        protected async override void OnStartup(object sender, StartupEventArgs e)
        {
            //加载启动动画
            LoadSplashScreen();

            //加载程序集
            LoadAssemblys();
            // 自定义视图、视图模型查找
            ViewLocator.LocateTypeForModelType = LocateTypeForModelType;

            // 初始化自定义的值替换
            InitSpecialValues();

            // 解决控件时间显示不是本地格式的问题
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            // 初始化显示主题
            InitializeTheme();

            //初始化数据库连接
            InitializeDbConnection();

            //设置显示主界面
            await DisplayRootViewFor<IShell>();
        }



        private void LoadAssemblys()
        {
            var finder = new DirectoryAssemblyFinder(DirectoryHelper.RootPath());
            var assemblys = finder.FindAll();
            if (!assemblys.IsNullOrEmpty())
            {
                foreach (var assembly in assemblys?.Where(p => p.FullName.StartsWith("Seasail")))
                {
                    AssemblySource.Instance.AddIfNotExist(assembly);
                }
            }
            //var assembly = Assembly.LoadFrom("TNH.Control.dll");
            //AssemblySource.Instance.AddIfNotExist(assembly);
        }

        private void InitializeDbConnection()
        {
            //var dlg = new Microsoft.Data.ConnectionUI.DataConnectionDialog();
            //var config = new DataConnectionConfiguration();

            //// 加载连接设置对话框的的配置
            //config.LoadConfiguration(dlg);


            //string providerName = _config.SystemConfig.DBProvider ?? "System.Data.SQLite";
            //string connectionString=_config.SystemConfig.DBConnectionString?? string.Format("Data Source={0}",System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "colors.db"));

            //var provider = dlg.UnspecifiedDataSource.Providers.FirstOrDefault(x => x.Name == providerName);
            //if (provider != null)
            //{
            //    var dataSource = dlg.DataSources.FirstOrDefault(x => x.DefaultProvider.Name == provider.Name);
            //    dlg.SelectedDataSource = dataSource;
            //    if (dataSource.Providers.Count > 1)
            //    {
            //        dlg.SelectedDataProvider = provider;
            //    }
            //}

            //dlg.ConnectionString = connectionString;


            //// 显示对话框
            //if (Microsoft.Data.ConnectionUI.DataConnectionDialog.Show(dlg) == DialogResult.OK)
            //{
            //    //// 存储对话框的配置
            //    //config.SaveConfiguration(dlg);

            //    //// 存储配置
            //    //if (cs == null)
            //    //{
            //    //    cs = new ConnectionStringSettings("DefaultDbConnection",
            //    //        dlg.ConnectionString, dlg.SelectedDataProvider.Name);
            //    //    AppConfig.Configuration.ConnectionStrings.ConnectionStrings.Add(cs);
            //    //}
            //    //else
            //    //{
            //    //    cs.ConnectionString = dlg.ConnectionString;
            //    //    cs.ProviderName = dlg.SelectedDataProvider.Name;
            //    //}

            //    //AppConfig.Configuration.Save(ConfigurationSaveMode.Modified);

            //    //ColorDb?.Dispose();
            //    //ColorDb = ColorDbContext.CreateDbContext(ConnectionHelper.CreateDbConnection(cs.ProviderName, cs.ConnectionString));
            //    //ColorDb.Database.Log = s => Trace.TraceInformation(s);
            //    //return true;
            //}
        }

        /// <summary>
        /// 加载开机界面
        /// </summary>
        private void LoadSplashScreen()
        {
            //在资源文件中定义了SplashScreen，不再需要手动启动开机动画
#if !DEBUG
            //string splashScreenPngPath = "Resources/SplashScreen.bmp";
            //SplashScreen s = new SplashScreen(splashScreenPngPath);
            //s.Show(true);
#endif
        }
        private void InitializeTheme()
        {
            Theme theme = ThemeManager.Current.Themes.FirstOrDefault(p => p.Name == _options.Theme);
            if (theme != null && theme != ThemeManager.Current.DetectTheme())
                ThemeManager.Current.ChangeTheme(Application.Current, theme.Name);
        }

        /// <summary>
        /// 在这里添加我自已的Caliburn.Micro绑定变量
        /// </summary>
        private void InitSpecialValues()
        {
            MessageBinder.SpecialValues.Add("$clickedItem",
                c => (c.EventArgs as ItemClickEventArgs)?.ClickedItem);
        }

        // 定位视图类型，支持派生类继承父视图
        private static Type LocateTypeForModelType(Type modelType, DependencyObject displayLocation, object context)
        {
            var viewTypeName = modelType.FullName;

            if (Caliburn.Micro.View.InDesignMode)
            {
                viewTypeName = ViewLocator.ModifyModelTypeAtDesignTime(viewTypeName);
            }

            viewTypeName = viewTypeName.Substring(0, viewTypeName.IndexOf('`') < 0
                ? viewTypeName.Length
                : viewTypeName.IndexOf('`'));

            var viewTypeList = ViewLocator.TransformName(viewTypeName, context);
            var viewType = AssemblySource.FindTypeByNames(viewTypeList);
            if (viewType == null)
            {
                Trace.TraceWarning("View not found. Searched: {0}.", string.Join(", ", viewTypeList.ToArray()));

                if (modelType.BaseType != null)
                {
                    return ViewLocator.LocateTypeForModelType(modelType.BaseType, displayLocation, context);
                }
            }

            return viewType;
        }

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }
        private void InitializeCulture()
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            if (!string.IsNullOrEmpty(_options.Culture))
            {
                ci = CultureInfo.GetCultureInfo(_options.Culture);
            }
            Utils.LocalUtil.SwitchCulture(ci);
            _translater = new Translater();
        }

        /// <summary>
        /// 初始化配置文件
        /// </summary>
        private void InitializeConfig()
        {
            _options = Options.Load();
            if (_options == null)
                _options = new Options();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            _options.Save();
            base.OnExit(sender, e);
        }

        protected override async void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            await Task.Run(() =>
            {
                Seasail.Logging.LogManager.Error(e.Exception);
                Application.Current.Shutdown(-1);
            });
        }
    }
}
