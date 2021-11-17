using Newtonsoft.Json;
using System.IO;

namespace Seasail
{
    public class GlobalContext
    {
        public string AppId { get; set; } = "4cPBw4DdNqzfoceytwCKDoeLfsumH1dybtFWip1FsnNC";
        public string SdkKey { get; set; } = "ErGFTbsCmYGDabYixCiK1Baseuv2mKYRNMYUMgLLjLwa";

        public string BarcodePortName { get; set; } = "COM2";
        public int BarcodeBaudrate { get; set; } = 9600;

        public string MainboardSerialPort { get; set; } = "COM1";
        public int MainboardBaudrate { get; set; } = 9600;
        public int PassTimeout { get; set; } = 5000;
        public string EntranceDirection { get; set; } = "K1";


        public string ServerName { get; set; } = ".";

        public string DbName { get; set; } = "JXD";

        public string UserName { get; set; } = "sa";

        public string Password { get; set; } = "123456";
        public string ConnectString => $"Data Source={ServerName};Initial Catalog ={DbName}; User Id ={UserName}; Password={Password}";

        public string DBProvider { get; set; } = "SQLite";
        public string DBConnectionString => $"Data Source={ServerName};Initial Catalog ={DbName}; User Id ={UserName}; Password={Password}";
        public string SQLiteConnectionString => $"Data Source=JXD.db;";
        public string EntityAssembly { get; set; } = "Seasail.Entity.JXD";
        public static int SnowFlakeWorkerId { get; set; }
        public string Theme { get; set; } = "Light.Blue";
        public string Culture { get; set; } = "zh-CN";



        public int Id { get; set; } = 1;

        public string Mac { get; set; } = "AA-BB-CC-DD-EE-FF";

        public string IPAddress { get; set; } = "192.168.0.100";

        public int VersionCode { get; set; } = 1;

        public string VersionName { get; set; } = "1.0";

        public bool FaceEnabled { get; set; }

        public bool IdCardWithFaceVerification { get; set; }

        public string BackgroundImageSource { get; set; }
        public string TicketServerUrl { get; set; } = "http://pdev.cleartv.cn/checker/clear";
        public string DisplayName { get; set; } = "深圳东部华侨城·花仙谷";
        public int IDCardCheckTicketMode { get; set; } = 0;
        public int CheckSingleFaceEnabled { get; set; } = 1;
        public int FaceVarifyWaitingTime { get; set; } = 20;
        public string IDCardReaderConnectWay { get; set; } = "USB";
        public string IDCardReaderParameter1 { get; set; }
        public string IDCardReaderParameter2 { get; set; }

        #region Public

        public void Save(string path = "")
        {
            if (string.IsNullOrEmpty(path))
                path = "option.json";
            try
            {
                var json = JsonConvert.SerializeObject(this);
                File.WriteAllText(path, json);
            }
            catch
            {

            }
        }

        public static GlobalContext Load(string path = "")
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                    path = "option.json";
                if (File.Exists(path))
                {
                    var context = File.ReadAllText(path);
                    return JsonConvert.DeserializeObject<GlobalContext>(context);
                }
                return new GlobalContext();
            }
            catch
            {
                return null;
            }
        }

        #endregion


    }
}
