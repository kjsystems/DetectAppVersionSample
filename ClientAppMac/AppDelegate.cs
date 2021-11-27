using System.Net.Http;
using System.Threading.Tasks;
using AppKit;
using Foundation;
using Newtonsoft.Json;

namespace ClientAppMac
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
            try
            {
                var client = new HttpClient();
                var path = "http://localhost:7071/api/GetVersion";

                // 最新バージョンを取得する
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var jsonText = await response.Content.ReadAsStringAsync();
                    var appInfo = JsonConvert.DeserializeObject<AppInfo>(jsonText);

                    if (appInfo?.Version != this.AppVerion)
                    {
                        using (var alert = new NSAlert())
                        {
                            alert.MessageText = $"最新バージョン（{appInfo?.Version}）があります";
                            alert.RunSheetModal(null);
                        }
                    }
                    else
                    {
                        // MessageBox.Show($"最新バージョンです");
                    }
                }
                else
                {
                    // MessageBox.Show($"最新バージョンを取得できません");
                }
            }
            catch
            {
                // MessageBox.Show($"最新バージョンを取得できません");
            }
        }
    }
}
