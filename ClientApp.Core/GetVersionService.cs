using System.Net.Http;
#if NET48
using Newtonsoft.Json;
#else
using System.Net.Http.Json;
#endif
using System.Threading.Tasks;

namespace ClientApp.Core
{
    public class GetVersionService
    {
        const string GetVersionUrl = Constant.ManUrl + "/api/GetVersion";

        public static async Task<AppInfo> GetVersion()
        {
            var client = new HttpClient();
            var url = GetVersionUrl;

            AppInfo appInfo = null;

            try
            {
                // 最新バージョンを取得する
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
#if NET48
                    var jsonText = await response.Content.ReadAsStringAsync();
                    appInfo = JsonConvert.DeserializeObject<AppInfo>(jsonText);
#else
                    appInfo = await response.Content.ReadFromJsonAsync<AppInfo>();
#endif
                }
            }
            catch (HttpRequestException)
            {
                // UNDONE: 通信失敗のエラー処理
            }

            return appInfo;
        }
    }
}
