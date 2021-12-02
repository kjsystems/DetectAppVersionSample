using System.Threading.Tasks;
using AppKit;
using ClientApp.Core;
using Foundation;

namespace ClientApp.Mac
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        public class AppInfo
        {
            public string Version { get; set; }
        }

        /// <summary>
        /// 自分自身のバージョン
        /// </summary>
        string AppVerion => "1.0.0";

        public AppDelegate()
        {
        }

        public override async void DidFinishLaunching(NSNotification notification)
        {
            await CheckVersion();
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }

        async Task CheckVersion()
        {
            // バージョンを取得する
            var appInfo = await GetVersionService.GetVersion();

            // 最新バージョンが返ってくる
            // バックエンドが起動してない場合は戻り値はnull
            if (appInfo!=null && appInfo.Version != this.AppVerion)
            {
                        using (var alert = new NSAlert())
                        {
                            alert.MessageText = $"最新バージョン（{appInfo?.Version}）があります";
                            alert.RunSheetModal(null);
                        }
            }
        }
    }
}
