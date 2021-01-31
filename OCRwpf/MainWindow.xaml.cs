using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OCRwpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog odlg { get; set; }
        Image img { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            odlg = new OpenFileDialog();
            odlg.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*jpg; *jpeg; *.png";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if((bool)odlg.ShowDialog())
            {
                Uri fileURI = new Uri(odlg.FileName);

                img = new Image();
                img.Source = new BitmapImage(fileURI);

                file_img.Source = img.Source;
            }
        }

        private async void ocr_btn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(odlg.FileName))
                return;

            var s = await OCRreq.MakeOCRreq(odlg);
            if (s == "0")
                return;

            OCRresult content = JsonSerializer.Deserialize<OCRresult>(s);

            foreach(OCRresult.Parsedresult parse in content.ParsedResults)
            {
                imgtext_tblk.Text += parse.ParsedText;
            }
        }
    }
}
