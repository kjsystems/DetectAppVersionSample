using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace ClientApp
{
    public class AppInfo
    {
        public string Version { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 自分自身のバージョン
        /// </summary>
        string AppVerion => "1.0.0";

        public MainWindow()
        {
            InitializeComponent();
            ContentRendered += async (s, e) =>
            {
                await CheckVersion();
            };
        }

        async Task CheckVersion()
        {
            try
            {
                var client = new HttpClient();
                var path = "http://localhost:7071/api/GetVersion";

                // 最新バージョンを取得する
                AppInfo? appInfo = null;
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    appInfo = await response.Content.ReadFromJsonAsync<AppInfo>();
                    if (appInfo?.Version != this.AppVerion)
                    {
                        MessageBox.Show($"最新バージョン（{appInfo?.Version}）があります");
                    }
                    else
                    {
                        MessageBox.Show($"最新バージョンです");
                    }
                }
                else
                {
                    MessageBox.Show($"最新バージョンを取得できません");
                }
            }
            catch
            {
                MessageBox.Show($"最新バージョンを取得できません");
            }
        }
    }
}
