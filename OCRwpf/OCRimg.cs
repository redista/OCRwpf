using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Win32;

namespace OCRwpf
{
    public class OCRimg
    {
        private static HttpClient req = new HttpClient();
        private static string APIendpoint = "https://api.ocr.space/parse/image";

        static OCRimg()
        {
            req.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //req.BaseAddress = new Uri(APIendpoint);
        }

        async public Task<string> MakeOCRreq(OpenFileDialog)
        {
            string param = String.Format($"file?apikey={APIkeys.OCRkey}&file=");

            var res = await req.GetStringAsync(APIendpoint + param).ConfigureAwait(false);

            return res.ToString();
        }
    }
}
