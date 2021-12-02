using ClientApp.Core;
using System.Threading.Tasks;
using System.Windows;

namespace ClientApp.Wpf
{

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

            // アプリ起動時にバージョンをチェックする
            ContentRendered += async (s, e) =>
            {
                await CheckVersion();
            };
        }

        async Task CheckVersion()
        {
            // バージョンを取得する
            var appInfo = await GetVersionService.GetVersion();

            // 最新バージョンが返ってくる
            // バックエンドが起動してない場合は戻り値はnull
            if (appInfo!=null && appInfo.Version != this.AppVerion)
            {
                MessageBox.Show($"最新バージョン（{appInfo?.Version}）があります");
            }
        }
    }
}
