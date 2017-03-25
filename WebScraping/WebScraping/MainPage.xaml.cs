using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WebScraping
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            var str = "https://en.wikipedia.org/wiki/" + InputFilmText.Text.Replace(" ", "_") + "_(film)";
            var httpclient = new HttpClient();
            var url_string = await httpclient.GetStringAsync(new Uri(str));
            HtmlDocument htmldocument = new HtmlDocument();
            htmldocument.LoadHtml(url_string);


            var node1 = htmldocument.DocumentNode.Descendants("table").Where(d => d.GetAttributeValue("class", "") == "infobox vevent").First().Descendants("tr").ToArray();
            int count = 0;
            foreach (var item in node1)
            {
                var node3 = item.Descendants("th").Where(d => d.InnerText == InputFieldText.Text).ToArray();
                try
                {
                    if (node3[0].InnerText == InputFieldText.Text)
                        break;
                }
                catch (Exception)
                {
                    count++;
                }
            }
            var node4 = node1[count].Descendants("td").ToArray();
            ResultTextBlock.Text = node4[0].InnerText;

        }
    }
}
