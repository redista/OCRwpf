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

namespace OCRwpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        OpenFileDialog odlg;
        Image img;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            odlg = new OpenFileDialog();
            odlg.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*jpg; *jpeg; *.png";

            img = new Image();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if((bool)odlg.ShowDialog())
            {
                Uri fileURI = new Uri(odlg.FileName);
                img.Source = new BitmapImage(fileURI);

                file_img.Source = img.Source;
            }
        }

        private async void ocr_btn_Click(object sender, RoutedEventArgs e)
        {
            OCRimg ocr = new OCRimg();

            var s = await ocr.MakeOCRreq();

            imgtext_tblk.Text = s;
        }
    }
}
