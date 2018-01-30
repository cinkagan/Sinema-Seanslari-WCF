# Sinema-Seanslari-WCF
Bu servis yardımıyla sinemalar.com dan istediğiniz salona ait sinema filmleri bilgilerini güncel olarak alabilirsiniz

Servis'imizi projeye dahil ettikten sonra gerekli bağlantı için aşağıdaki kodları yazıyoruz
```
 using (Sinema.SinemaClient sinema=new Sinema.SinemaClient())
            {
                foreach (var item in sinema.GetData("https://www.sinemalar.com/sinemasalonu/1813/eskisehir-ozdilek-cinetime"))
                {
                    listBox1.Items.Add(item.isim);
                    listBox2.Items.Add(item.puan);
                    
                }
}
```

Kodları açıklamak gerekirse
```
Sinema.SinemaClient sinema=new Sinema.SinemaClient()
```
ile servise bağlantı sağlıyoruz.


Verileri çekmek için sinema.GetData(sinemalar.com'daki salon linki)

```
sinema.GetData("https://www.sinemalar.com/sinemasalonu/1813/eskisehir-ozdilek-cinetime")
```


Servisten elde edeceğiniz veriler aşağıda
 ```
 public class tur
    {
        public string isim { get; set; }
        public string puan { get; set; }
        public ArrayList seans { get; set; }
    }
```
