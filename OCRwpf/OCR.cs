using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Win32;
using System.IO;

namespace OCRwpf
{
    public static class OCRreq
    {
        private static HttpClient req = new HttpClient();
        private static string APIendpoint = "https://api.ocr.space/parse/image";

        static async public Task<string> MakeOCRreq(OpenFileDialog o)
        {
            try
            {
                string param = String.Format($"file?apikey={APIkeys.OCRkey}&file=");

                MultipartFormDataContent form = new MultipartFormDataContent();
                form.Add(new StringContent(APIkeys.OCRkey), "apikey");
                form.Add(new StringContent("2"), "ocrengine");
                form.Add(new StringContent("true"), "scale");
                form.Add(new StringContent("true"), "istable");

                byte[] imagedata = File.ReadAllBytes(o.FileName);
                form.Add(new ByteArrayContent(imagedata, 0, imagedata.Length), "image", "image.jpg");

                HttpResponseMessage res = await req.PostAsync(APIendpoint, form);

                var str = await res.Content.ReadAsStringAsync();
                return str;
            }
            catch(Exception e)
            {
                return "0";
            }
        }
    }

    public class OCRresult
    {
        public Parsedresult[] ParsedResults { get; set; }
        public int OCRExitCode { get; set; }
        public bool IsErroredOnProcessing { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }

        public class Parsedresult
        {
            public object FileParseExitCode { get; set; }
            public string ParsedText { get; set; }
            public string ErrorMessage { get; set; }
            public string ErrorDetails { get; set; }
        }
    }
}
