using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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
                    appInfo = await response.Content.ReadFromJsonAsync<AppInfo>();
                }
            }
            catch (HttpRequestException e)
            {
                // UNDONE: 通信失敗のエラー処理
            }

            return appInfo;
        }
    }
}
