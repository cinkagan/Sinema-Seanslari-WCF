using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SinemaSeanslari
{
    // NOT: "Sinema" sınıf adını kodda, svc'de ve yapılandırma dosyasında birlikte değiştirmek için "Yeniden Düzenle" menüsündeki "Yeniden Adlandır" komutunu kullanabilirsiniz.
    // NOT: Bu hizmeti test etmek üzere WCF Test İstemcisi'ni başlatmak için lütfen Çözüm Gezgini'nde Sinema.svc'yi veya Sinema.svc.cs'yi seçin ve hata ayıklamaya başlayın.
    public class Sinema : ISinema
    {
        public List<tur> GetData(string link)
        {
            try
            {
                // Sinema bilgilerini tutacağımız liste
                List<tur> sinema = new List<tur>();

                // sinema salonunun linki
                Uri url = new Uri(link);
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string html = client.DownloadString(url);
                HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
                dokuman.LoadHtml(html);

                // sırasıyla sinemadaki filmin adı,puanı
                HtmlNodeCollection basliklar = dokuman.DocumentNode.SelectNodes("//*[contains(@class,'grid8 bestof cinema-detail shadow')]/*[contains(@class,'bestof-detail')]/h3/a");
                HtmlNodeCollection puanlar = dokuman.DocumentNode.SelectNodes("//*[contains(@class,'puan-durum')]/*[@id='rating']");

                var key = -1;
                foreach (HtmlNode baslik in basliklar)
                {
                    key++;
                    //ListBox ekle
                    HtmlNodeCollection seanslar = dokuman.DocumentNode.SelectNodes("//*[contains(@class,'left-content')]/div[" + (key + 2) + "]/*[contains(@class,'grid6 fr')]/*[contains(@class,'select-seans selected-seans')]");

                    string veri = baslik.InnerText;
                    veri = veri.Replace("'", " ");


                    // Console.WriteLine("İsim : "+ veri.Trim());
                    //  Console.WriteLine("Puan : "+puanlar[key].InnerText);
                    ArrayList seansListesi = new ArrayList();
                    foreach (var item in seanslar)
                    {
                        seansListesi.Add(item.InnerText.Replace(" ", string.Empty).Trim());
                        //  Console.WriteLine("Seans : " + item.InnerText.Replace(" ", string.Empty).Trim());
                    }

                    sinema.Add(new tur()
                    {
                        isim = veri.Trim(),
                        puan = puanlar[key].InnerText,
                        seans = seansListesi
                    });
                }

                return sinema;
            }
            catch (Exception hata)
            {
                return new List<tur>();
                Console.WriteLine(hata.ToString());
            }
        }
    }
}
