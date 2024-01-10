using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Threading;
using System.Net.Mail;
using System.Globalization;

namespace Kitap_Film_Arama_Motoru_Nesne_Tabanlı
{
    public class Item

    {

        public string Tür { get; set; }
        public string İsim { get; set; }
        public string Genre { get; set; }
        public int Yıl { get; set; }
        public int Fiyat { get; set; }

        //burada eşyaların ortak yönlerini alıyoruz
    }

    class Kitap : Item
    {
        public string Yazar { get; set; } // burada kitap için yazar bilgisi alıyoruz
    }

    class Film : Item
    {
        public string Yönetmen { get; set; } // burada yönetmen bilgisini alıyoruz
    }

    class Oyun : Item
    {
        public string Yapımcı { get; set; } // burada yapımcı bilgisini alıyoruz
    }

    interface IKütüphane<T> // burada tüm itemlerin ortak fonksiyonlarını tanımlamak için bir arayüz oluşturuyoruz
    {
        void ItemSil(string isim, string tür, string genre, int yıl, string yapan); // eklenen itemi silecek
        void ItemEkle(T t); // item ekleyecek
        void ItemListele(string aranangenre); // eklenen itemi aranan türe göre listeleyecek    
        void ItemListele(int arananyıl); // eklenen türü aranan yıla göre listeleyecek
        void İsimItemListele(string aranankitap); // eklenen türü isme göre listeleyecek
        void HepsiniListele(); // tüm türü listeleyecek
        bool VarMıKontrol(string isim, string yapan); // listenin içinde o item var mı diye kontrol edecek
    }

    class KütüphaneKitap : IKütüphane<Kitap>
    {

        private List<Kitap> KitapList;




        public KütüphaneKitap()
        {
            KitapList = new List<Kitap>();
        }

        public bool VarMıKontrol(string isim, string yazar)
        {
            foreach (Kitap kitap in KitapList)
            {
                if (kitap.İsim == isim && kitap.Yazar == yazar)
                {
                    return true; // Kitap listesinde bulundu
                }
            }
            return false; // Kitap listesinde bulunamadı
        }
    
    public void ItemEkle(Kitap kitap)
        {
            Console.Clear();
            var kontrolk = KitapList.FirstOrDefault(k => k.İsim == kitap.İsim && k.Tür == kitap.Tür && k.Genre == kitap.Genre && k.Yıl == kitap.Yıl && k.Fiyat == kitap.Fiyat); // burda tüm ögelerine göre kontrol ediyor ve bir değer atıyor

            if (kontrolk == null)
            {
                KitapList.Add(kitap);
                Console.WriteLine($"{kitap.Tür} başarıyla eklendi: {kitap.İsim}");
            }
            else
            {
                Console.WriteLine("Bu kitap zaten kütüphanede mevcut.");
            }
            Thread.Sleep(1500);
            Console.Clear();
        }
        public void ItemListele(int arananyil)
        {
            Console.Clear();
            bool kitapFlag = false;

            if (!KitapList.Any()) // kitap listesi boş mu diye kontrol ediliyor
            {
                Console.WriteLine("Kütüphanede hiç kitap yok.");
            }
            else
            {
                Console.WriteLine($"{arananyil} Yılındaki Kitaplar:");
                foreach (Kitap kitap in KitapList)
                {
                    if (kitap.Yıl == arananyil) //yıl kontrol ediliyor
                    {
                        Console.WriteLine($"İsim : {kitap.İsim} - Yazar : {kitap.Yazar} - Tür : {kitap.Genre} - Fiyat : {kitap.Fiyat}");
                        kitapFlag = true; // eğer denkleşen bir durum var ise flagi true yapıyor ve aşağıdaki if çalışmıyor
                    }
                }
            }

            if (!kitapFlag && KitapList.Any())
            {
                Console.WriteLine($"Kütüphanede {arananyil} yılında kitap bulunamadı.");
            }
        }


        public void HepsiniListele()
        {
            if (!KitapList.Any())
            {
                Console.WriteLine("Kütüphanede hiç kitap yok.");
            }
            else
            {
                Console.WriteLine("Kitaplar:");



                foreach (Kitap kitap in KitapList)
                {
                    Console.WriteLine($"İsim : {kitap.İsim} - Yazar : {kitap.Yazar} - Tür : {kitap.Genre} - Yıl : {kitap.Yıl} - Fiyat : {kitap.Fiyat} ");
                }
            }
        }
        public void İsimItemListele(string aranankitap)
        {
            Console.Clear();
            bool kitapFlag = false;

            if (!KitapList.Any())
            {
                Console.WriteLine("Kütüphanede hiç kitap yok.");
            }
            else
            {
                Console.WriteLine($"{aranankitap} İsimli Kitaplar:");
                foreach (Kitap kitap in KitapList)
                {
                    if (string.Equals(kitap.İsim, aranankitap, StringComparison.OrdinalIgnoreCase)) // burada harfe duyarlı olmaması için bu şekilde denkliyor
                    {
                        Console.WriteLine($"İsim : {kitap.İsim} - Yazar : {kitap.Yazar} - Tür : {kitap.Genre} - Yıl : {kitap.Yıl} - Fiyat: {kitap.Fiyat} ");
                        kitapFlag = true;
                    }
                }
            }

            if (!kitapFlag && KitapList.Any())
            {
                Console.WriteLine($"Kütüphanede {aranankitap} isimli kitap bulunamadı.");
            }
        }
        public void ItemListele(string aranangenre)
        {

            bool kitapflg = false;
            if (KitapList == null || !KitapList.Any())
            {
                Console.WriteLine("Kütüphanede hiç kitap yok.");
            }
            else if (aranangenre == "Hepsi")
            {
                Console.WriteLine("Kitaplar:");



                foreach (Kitap kitap in KitapList)
                {
                    Console.WriteLine($"İsim : {kitap.İsim} - Yazar : {kitap.Yazar} - Tür : {kitap.Genre} - Yıl : {kitap.Yıl} - Fiyat : {kitap.Fiyat} ");
                }
            }
            else
            {
                Console.WriteLine($"{aranangenre} türündeki kitaplar:");
                foreach (Kitap kitap in KitapList)
                {
                    if (kitap.Genre == aranangenre)
                    {
                        kitapflg = true;
                        Console.WriteLine($"İsim : {kitap.İsim} - Yazar : {kitap.Yazar} - Yıl : {kitap.Yıl} - Fiyat : {kitap.Fiyat} ");
                    }
                }
            }
            if (!kitapflg && KitapList.Any() && (aranangenre != "Hepsi"))
            {
                Console.WriteLine($"Kütüphanede {aranangenre} türünde kitap bulunamadı.");
            }
        }



        public void ItemSil(string isim, string tür, string genre, int yıl, string yazar)
        {
            Console.Clear();
            Kitap silinecek = KitapList.Find(kitap => string.Equals(kitap.İsim, isim, StringComparison.OrdinalIgnoreCase) && string.Equals(kitap.Tür, tür, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(kitap.Genre, genre, StringComparison.OrdinalIgnoreCase) && kitap.Yıl == yıl && string.Equals(kitap.Yazar, yazar, StringComparison.OrdinalIgnoreCase));

            if (silinecek != null)
            {
                KitapList.Remove(silinecek);
                Console.WriteLine($"{tür} başarıyla silindi: {isim}");
            }
            else
            {
                Console.WriteLine($"Silinecek {tür} bulunamadı: {isim}");
            }

        }

    }
        class KütüphaneFilm : IKütüphane<Film>
    {
        private List<Film> FilmList;

        public KütüphaneFilm()
        {
            FilmList = new List<Film>();
        }

        public bool VarMıKontrol(string isim, string yönetmen)
        {
            foreach (Film film in FilmList)
            {
                if (film.İsim == isim && film.Yönetmen== yönetmen)
                {
                    return true; 
                }
            }
            return false; 
        }

        public void ItemEkle(Film film)
        {
            Console.Clear();
            var kontrol = FilmList.FirstOrDefault(f => f.İsim == film.İsim && f.Tür == film.Tür && f.Genre == film.Genre && f.Yıl == film.Yıl && f.Fiyat == film.Fiyat);

            if (kontrol == null)
            {
                FilmList.Add(film);
                Console.WriteLine($"{film.Tür} başarıyla eklendi: {film.İsim}");
            }
            else
            {
                Console.WriteLine("Bu film zaten kütüphanede mevcut.");
            }
            Thread.Sleep(1500);
            Console.Clear();
        }
        public void ItemListele(int arananyıl)
        {
            Console.Clear();
            bool filmFlag = false;

            if (!FilmList.Any())
            {
                Console.WriteLine("Kütüphanede hiç film yok.");
            }
            else
            {
                Console.WriteLine($"{arananyıl} Yılındaki Filmler:");
                foreach (Film film in FilmList)
                {
                    if (film.Tür == "Film" && film.Yıl == arananyıl)
                    {
                        Console.WriteLine($"İsim : {film.İsim} - Yönetmen : {film.Yönetmen} - Tür : {film.Genre} - Fiyat : {film.Fiyat} TL");
                        filmFlag = true;
                    }
                }
            }

            if (!filmFlag && FilmList.Any())
            {
                Console.WriteLine($"Kütüphanede {arananyıl} yılında film bulunamadı.");
            }
        }

        public void HepsiniListele()
        {
            if (!FilmList.Any())
            {
                Console.WriteLine("Kütüphanede hiç film yok.");
            }
            else
            {
                Console.WriteLine("Filmler:");

                foreach (Film film in FilmList)
                {

                    Console.WriteLine($"İsim : {film.İsim} - Yönetmen : {film.Yönetmen} - Tür : {film.Genre} - Yıl : {film.Yıl} - Fiyat : {film.Fiyat} TL ");


                }
            }
        
    }
        public void İsimItemListele(string arananfilm)
        {
            Console.Clear();
            bool kitapFlag = false;

            if (!FilmList.Any())
            {
                Console.WriteLine("Kütüphanede hiç film yok.");
            }
            else
            {
                Console.WriteLine($"{arananfilm} İsimli Filmler:");
                foreach (Film film in FilmList)
                {
                    if (string.Equals(film.İsim, arananfilm, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"İsim : {film.İsim} - Yönetmen : {film.Yönetmen} - Tür : {film.Genre} - Yıl : {film.Yıl} - Fiyat : {film.Fiyat} Tl ");
                        kitapFlag = true;
                    }
                }
            }

            if (!kitapFlag && FilmList.Any())
            {
                Console.WriteLine($"Kütüphanede {arananfilm} isimli film bulunamadı.");
            }
        }



        public void ItemListele(string aranangenre)
        {
            
            bool filmflg = false;
            if (!FilmList.Any())
            {
                Console.WriteLine("Kütüphanede hiç film yok.");
            }
            else if (aranangenre == "Hepsi")
            {
                Console.WriteLine("Filmler:");

                foreach (Film film in FilmList)
                {
                    
                        Console.WriteLine($"İsim : {film.İsim} - Yönetmen : {film.Yönetmen} - Tür : {film.Genre} - Yıl : {film.Yıl} - Fiyat : {film.Fiyat} TL ");

                    
                }
            }
            else
            {
                Console.WriteLine($"{aranangenre} türündeki filmler:");

                foreach (Film film in FilmList)
                {
                    if (film.Genre == aranangenre)
                    {
                        filmflg = true;
                        Console.WriteLine($"İsim : {film.İsim} - Yönetmen : {film.Yönetmen} - Yıl {film.Yıl} - Fiyat : {film.Fiyat} Tl ");
                    }
                }
            }
            if (!filmflg && FilmList.Any() && (aranangenre != "Hepsi"))
            {
                Console.WriteLine($"Kütüphanede {aranangenre} türünde kitap bulunamadı.");
            }
        }


        public void ItemSil(string isim, string tür, string genre, int yıl, string yönetmen)
        {
            Console.Clear();
            Film silinecek = FilmList.Find(film => string.Equals(film.İsim, isim, StringComparison.OrdinalIgnoreCase) && string.Equals(film.Tür, tür, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(film.Genre, genre, StringComparison.OrdinalIgnoreCase) && film.Yıl == yıl && string.Equals(film.Yönetmen, yönetmen, StringComparison.OrdinalIgnoreCase));

            if (silinecek != null)
            {
                FilmList.Remove(silinecek);
                Console.WriteLine($"{tür} başarıyla silindi: {isim}");
            }
            else
            {
                Console.WriteLine($"Silinecek {tür} bulunamadı: {isim}");
            }
        }




    }
    class KütüphaneOyun : IKütüphane<Oyun>
    {
        private List<Oyun> OyunList;

        public KütüphaneOyun()
        {
            OyunList = new List<Oyun>();
        }

        public bool VarMıKontrol(string isim, string yapımcı)
        {
            foreach (Oyun oyun in OyunList)
            {
                if (oyun.İsim == isim && oyun.Yapımcı== yapımcı)
                {
                    return true; 
                }
            }
            return false; 
        }

        public void ItemEkle(Oyun oyun)
        {
            Console.Clear();
            var kontrolo = OyunList.FirstOrDefault(o => o.İsim == oyun.İsim && o.Tür == oyun.Tür && o.Genre == oyun.Genre && o.Yıl == oyun.Yıl && o.Fiyat == oyun.Fiyat);

            if (kontrolo == null)
            {
                OyunList.Add(oyun);
                Console.WriteLine($"{oyun.Tür} başarıyla eklendi: {oyun.İsim}");
            }
            else
            {
                Console.WriteLine("Bu oyun zaten kütüphanede mevcut.");
            }
            Thread.Sleep(1500);
            Console.Clear();
        }

        public void ItemListele(int arananyıl)
        {
            Console.Clear();
            bool oyunflg = false;

            if (!OyunList.Any())
            {
                Console.WriteLine("Kütüphanede hiç oyun yok.");
            }
            else
            {
                Console.WriteLine($"{arananyıl} Yılındaki Oyunlar:");
                foreach (Oyun oyun in OyunList)
                {
                    if (oyun.Tür == "Oyun" && oyun.Yıl == arananyıl)
                    {
                        Console.WriteLine($"İsim : {oyun.İsim} - Yapımcı : {oyun.Yapımcı} - Tür : {oyun.Genre} - Yıl : {oyun.Yıl} - Fiyat : {oyun.Fiyat} TL  ");
                        oyunflg = true;
                    }
                }
            }

            if (!oyunflg && OyunList.Any())
            {
                Console.WriteLine($"Kütüphanede {arananyıl} yılında oyun bulunamadı.");
            }
        }

        public void HepsiniListele()
        {
            if (!OyunList.Any())
            {
                Console.WriteLine("Kütüphanede hiç oyun yok.");
            }
            else
            {
                Console.WriteLine("Oyunlar:");
                foreach (Oyun oyun in OyunList)
                {
                    if (oyun.Tür == "Oyun")
                    {
                        Console.WriteLine($"İsim : {oyun.İsim} - Yapımcı : {oyun.Yapımcı} - Tür : {oyun.Genre} - Yıl : {oyun.Yıl} - Fiyat : {oyun.Fiyat} TL  ");
                    }
                }
            }
        }
        public void İsimItemListele(string arananoyun)
        {
            Console.Clear();
            bool kitapFlag = false;

            if (!OyunList.Any())
            {
                Console.WriteLine("Kütüphanede hiç oyun yok.");
            }
            else
            {
                Console.WriteLine($"{arananoyun} İsimli Kitaplar:");
                foreach (Oyun oyun in OyunList)
                {
                    if (string.Equals(oyun.İsim, arananoyun, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"İsim : {oyun.İsim} - Yapımcı : {oyun.Yapımcı} - Tür : {oyun.Genre} - Yıl : {oyun.Yıl} - Fiyat : {oyun.Fiyat} TL  ");
                        kitapFlag = true;
                    }
                }
            }

            if (!kitapFlag && OyunList.Any())
            {
                Console.WriteLine($"Kütüphanede {arananoyun} isimli oyun bulunamadı.");
            }
        }

        public void ItemListele(string aranangenre)
        {

            bool oyunflg = false;
            if (!OyunList.Any())
            {
                Console.WriteLine("Kütüphanede hiç oyun yok.");
            }
            else
            {
                if (aranangenre == "Hepsi")
                {
                    Console.WriteLine("Oyunlar:");
                    foreach (Oyun oyun in OyunList)
                    {
                        if (oyun.Tür == "Oyun")
                        {
                            Console.WriteLine($"İsim : {oyun.İsim} - Yapımcı : {oyun.Yapımcı} - Tür : {oyun.Genre} - Yıl : {oyun.Yıl} - Fiyat : {oyun.Fiyat} TL  ");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"{aranangenre}:");
                    foreach (Oyun oyun in OyunList)
                    {
                        if (oyun.Genre == aranangenre && oyun.Tür == "Oyun")
                        {
                            oyunflg = true;
                            Console.WriteLine($"İsim : {oyun.İsim} - Yapımcı : {oyun.Yapımcı} - Yıl : {oyun.Yıl} - Fiyat : {oyun.Fiyat} TL ");
                        }
                    }
                }
            }
            if (!oyunflg && (aranangenre != "Hepsi") && OyunList.Any())
            {
                Console.WriteLine($"Kütüphanede {aranangenre} türünde kitap bulunamadı.");
            }
        }




        public void ItemSil(string isim, string tür, string genre, int yıl, string yapımcı)
        {
            Console.Clear();

            bool silindi = false;

            foreach (Oyun oyun in OyunList)
            {
                if (String.Compare(oyun.İsim, isim, StringComparison.OrdinalIgnoreCase) == 0 && String.Compare(oyun.Tür, tür, StringComparison.OrdinalIgnoreCase) == 0 &&
                    String.Compare(oyun.Genre, genre, StringComparison.OrdinalIgnoreCase) == 0 && oyun.Yıl == yıl && String.Compare(oyun.Yapımcı, yapımcı, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    OyunList.Remove(oyun);
                    Console.WriteLine($"{tür} başarıyla silindi: {isim}");
                    silindi = true;
                    break;
                }
            }

            if (!silindi)
            {
                Console.WriteLine($"Silinecek {tür} bulunamadı: {isim}");
            }
        }


    }

        internal class Program
    {


        static void Main(string[] args)
        {
            KütüphaneKitap kutuphanekitap = new KütüphaneKitap(); 
            KütüphaneFilm kutuphanefilm = new KütüphaneFilm();
            KütüphaneOyun kutuphaneoyun = new KütüphaneOyun();
            //tüm kütüphaneler için bir nesne tanımladık
            int secim;
            int kategoriSecim;
            bool girisbasarili;
            int kategoriNumara;
            int girenSecim;
            

            do
            {
                girisbasarili = false; //giriş başarılıyı false olarak alıyoruz daha sonrasında şifre durumuna göre true olacak ve sisteme girilecek
                İlkmenü:
                Console.Clear();
                Console.WriteLine("\n1- Kullanıcı girişi.\n2- Yönetici girişi\n3- Çıkış");

                string girenInput = Console.ReadLine();

                if (!Kontrol(girenInput, out girenSecim)) // aşağıdaki kontrol fonksiyonuna göre tekrardan soracak veya devam edecek
                {
                    continue; 
                }

               


                switch (girenSecim)
                {
                    case 1:
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("\nNe yapmak istersiniz:\n1- Kitaplar\n2- Filmler\n3- Oyunlar\n4- Tüm Ürünleri Listele\n5- Giriş Ekranına Dönüş\n6- Çıkış");
                           string kategoriSeçim = Console.ReadLine();

                            

                            if (!Kontrol(kategoriSeçim, out kategoriNumara))
                            {
                                continue;
                            }

                            switch (kategoriNumara)
                            {
                                case 1:
                                    Console.Clear();

                                    do
                                    {

                                        Console.WriteLine("-----------------------------------------------");
                                        Console.WriteLine("\nKitaplar için yapmak istediğiniz işlemi seçin:\n1- Kitapları Listele\n2- Kitap Satın Al\n3- Ödeme Yöntemleri\n4- Ana Menüye Dön\n5- Çıkış");
                                        string secimInput = Console.ReadLine();

                                        if (!Kontrol(secimInput, out secim))
                                        {
                                            continue;
                                        }
                                        switch (secim)
                                        {

                                            case 1:


                                            aramabaşlangıç: // continue durumunda bir önceki menüye attığı için buraya bir başlangıç noktası koyuyoruz ve olası durumlarda goto ile buraya yolluyoruz
                                                Console.Clear();
                                                Console.WriteLine("1- Yıla göre arama\n2- İsme göre arama\n3- Türe göre arama\n4- Hepsini Listele\n5- Ana Menüye Dön");
                                                string aramasecim = Console.ReadLine();

                                                if (!Kontrol(aramasecim, out int aramakarar))
                                                {
                                                    goto aramabaşlangıç;
                                                }

                                                Console.Clear();
                                                switch (aramakarar)
                                                {
                                                    case 1:
                                                        Console.Clear();
                                                        Console.WriteLine("Aramak istediğiniz Yılı giriniz");
                                                        string arananyıl = Console.ReadLine();
                                                        if (!Kontrol(arananyıl, out int aranantarih))
                                                        {
                                                            goto aramabaşlangıç;
                                                        }
                                                        kutuphanekitap.ItemListele(aranantarih);
                                                        break;

                                                    case 2:
                                                        Console.Clear();
                                                        Console.WriteLine("Aramak istediğiniz kitabın ismini yazınız");
                                                        string arananKitap = Console.ReadLine();
                                                        kutuphanekitap.İsimItemListele(arananKitap);

                                                        break;

                                                    case 3:
                                                        Console.Clear();
                                                        Console.WriteLine("Hangi Kategoriyi Listelemek İstiyorsunuz?");
                                                        for (int i = 1; i <= 15; i++)
                                                        {
                                                            Console.WriteLine($"{i}- {GetKitapTuru(i)}"); // burada aşağıdaki fonksiyon ile türleri yazdırıyoruz
                                                        }
                                                        string listekitapTuruInput = Console.ReadLine();
                                                        int listekitapTuru; //listekitaptürüne bir sayı atanıyor

                                                        if (!int.TryParse(listekitapTuruInput, out listekitapTuru) || listekitapTuru < 1 || listekitapTuru > 15)
                                                        {
                                                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                            Thread.Sleep(1500);
                                                            goto aramabaşlangıç;
                                                        }


                                                        else
                                                        {
                                                            Console.Clear();
                                                            string aranan = GetKitapTuru(listekitapTuru); // bu sayıya göre aşağıdaki fonksiyondan string bir değer alıyor
                                                            kutuphanekitap.ItemListele(aranan);
                                                        }

                                                        break;
                                                    case 4: kutuphanekitap.HepsiniListele(); break;
                                                    case 5:
                                                        Console.Clear();

                                                        Console.WriteLine("Ana menüye dönülüyor...");
                                                        Thread.Sleep(1500);
                                                        Console.Clear();
                                                        break;

                                                    default:
                                                        Console.WriteLine("Tanımlanamayan Hareket");
                                                        break;

                                                }
                                                break;
                                            case 2:
                                                Console.Clear();
                                            satınbaşlangıç:

                                                Console.WriteLine("Ödeme türünü seçiniz.\n1-Kredi Kartı\n2-Havale\n3-Ana Menüye Dön");
                                                string satınsecim = Console.ReadLine();

                                                if (!Kontrol(satınsecim, out int satınkarar))
                                                {
                                                    goto satınbaşlangıç;
                                                }

                                                Console.Clear();
                                                switch (satınkarar) {
                                                    case 1:

                                                        Console.WriteLine("Satın almak istediğiniz kitabın adını girin.");
                                                        string satınkitap = Console.ReadLine();
                                                        Console.WriteLine("Satın almak istediğiniz kitabın yazarını girin");
                                                        string satınyazar = Console.ReadLine();
                                                        if (!kutuphanekitap.VarMıKontrol(satınkitap, satınyazar)) // burada var mı diye kontrol edilip var ise diğer aşamalar başlıyor
                                                        {
                                                            Console.WriteLine("Aradığınız kitap kütüphanede bulunmamaktadır.");
                                                            Thread.Sleep(1500);
                                                            Console.Clear();
                                                            goto satınbaşlangıç;
                                                        }
                                                        Console.WriteLine("Mail adresinizi giriniz:");
                                                        string kişimail = Console.ReadLine();

                                                        Console.WriteLine("Gönderilecek adresi giriniz:");
                                                        string kişiadres = Console.ReadLine(); // kişiye mail gönderilebilmesi için mail alınıyor

                                                    hatalıhane:
                                                        Console.WriteLine("16 haneli kredi kartınızı boşluk koymadan giriniz:");
                                                        string kişihane = Console.ReadLine();

                                                        if (kişihane.Length != 16) // uzunluk 16 değil ise devam etmiyor
                                                        {
                                                            Console.WriteLine("Lütfen geçerli bir kart numarası giriniz.");
                                                            Thread.Sleep(1500);
                                                            goto hatalıhane;
                                                        }
                                                        Console.WriteLine("Lütfen CVV kodunuzu giriniz");
                                                        string kişicvv = Console.ReadLine();

                                                        Console.WriteLine("Tarihi giriniz (AA/YY formatında):");
                                                        string kişitarih = Console.ReadLine();

                                                        DateTime tarih;
                                                        if (DateTime.TryParseExact(kişitarih, "MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tarih)) // burada yazılan bilginin tarih formatına uygun olup olmadığı kontrol ediliyor
                                                        {
                                                            string smtpServer = "smtp.gmail.com"; // SMTP sunucu adresi
                                                            int smtpPort = 587; // SMTP port numarası (genellikle 587 veya 465)
                                                            string smtpUsername = "ardacanb7@gmail.com"; // E-posta adresiniz
                                                            string smtpPassword = ""; // E-posta şifreniz

                                                            // Gönderilecek e-posta bilgileri isteğe göre kullanıcıdan da alınabilir
                                                            string toEmailAddress = kişimail; // yukarıda kişiden alan mail buraya yerleşiyor
                                                            string subject = "Kitap Siparişiniz hk.";
                                                            string body = $"Sayın Müşterimiz,\n\nSiparişiniz başarıyla alınmıştır. İşte detaylar:\n\nAdres: {kişiadres}\n\nSiparişiniz en kısa sürede işleme alınacak ve adresinize gönderilecektir.\n\nSipariş verdiğiniz kitap : {satınkitap} - {satınyazar }\n\nSiparişlerinizin detayları en kısa sürede size iletilecektir.\n\nTeşekkür ederiz.";



                                                            // MailMessage sınıfı ile e-posta oluşturma
                                                            MailMessage mail = new MailMessage(smtpUsername, toEmailAddress, subject, body);

                                                            // SmtpClient sınıfı ile e-posta gönderimi
                                                            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                                                            {
                                                                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                                                                smtpClient.EnableSsl = true; // SSL kullanılacaksa true, kullanılmayacaksa false

                                                                try
                                                                {
                                                                    smtpClient.Send(mail);
                                                                    Console.WriteLine("E-posta başarıyla gönderildi.");
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    Console.WriteLine("E-posta gönderiminde hata oluştu: " + ex.ToString());
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Geçersiz tarih formatı. Lütfen AA.YY biçiminde girin.");
                                                        }
                                                        break;

                                                    case 2:
                                                        Console.WriteLine("Satın almak istediğiniz kitabın adını girin.");
                                                        string satınkitaphavale = Console.ReadLine();
                                                        Console.WriteLine("Satın almak istediğiniz kitabın yazarını girin");
                                                        string satınyazarhavale = Console.ReadLine();
                                                        if (!kutuphanekitap.VarMıKontrol(satınkitaphavale, satınyazarhavale))
                                                        {
                                                            Console.WriteLine("Aradığınız kitap kütüphanede bulunmamaktadır.");
                                                            Thread.Sleep(1500);
                                                            Console.Clear();
                                                            goto satınbaşlangıç;
                                                        }
                                                        Console.WriteLine("IBAN NUMARASI : TR123456789012345678901234\n Havale işlemini tamamladıktan sonra bir tuşa basınız.");
                                                        Console.ReadKey();
                                                        Console.Clear();
                                                        Console.WriteLine("(Havale İşlemi)\n\nSayın Müşterimiz,\n\nHavale işleminizi gerçekleştirdiğiniz için teşekkür ederiz\nİşleminiz en kısa sürede işleme alınacak ve size bilgilendirme maili gönderilecektir.\n\nTeşekkür ederiz.");
                                                        break;
                                                    case 3: Console.WriteLine("Ana Menüye dönülüyor.");
                                                            Thread.Sleep(1500);
                                                            continue;
                                                    default:
                                                        Console.Clear();

                                                        Console.WriteLine("Tanımlanamayan hareket.");
                                                        Thread.Sleep(1500);
                                                        goto satınbaşlangıç;

                                                }
                                                break;
                                                

                                            case 3:
                                                yöntembaşlangıç:
                                                Console.Clear() ;
                                                Console.WriteLine("Ödeme Yöntemleri Menüsü:");
                                                Console.WriteLine("1. Havale");
                                                Console.WriteLine("2. Kredi Kartı");

                                                Console.WriteLine("\nLütfen ödeme yöntemini seçin.:");
                                                string ödemeseçim = Console.ReadLine();
                                                if (!Kontrol(ödemeseçim, out int ödemeseçimi))
                                                {
                                                    continue;
                                                }

                                                switch (ödemeseçimi)
                                                {
                                                    case 1:
                                                        HavaleAnlatım();  // aşağıdaki fonksiyon ile havaleyi anlatıyor
                                                        
                                                        break;
                                                    case 2:
                                                        KrediKartıAnlatım(); // aşağıdaki fonksiyon ile kredi kartı yöntemini anlatıyor
                                                        break;
                                                    default:
                                                        Console.WriteLine("Geçersiz giriş. Lütfen bir sayı seçin.");
                                                        Thread.Sleep(1500);
                                                        goto yöntembaşlangıç;
                                                }

                                                break;
                                            case 4: 
                                                Console.Clear();

                                                Console.WriteLine("Ana menüye dönülüyor...");
                                                Thread.Sleep(1500);
                                                break;
                                            case 5:
                                                Console.Clear();

                                                Console.WriteLine("Uygulama kapatılıyor...");
                                                Thread.Sleep(1500);
                                                Environment.Exit(0);
                                                break;
                                            default:
                                                Console.Clear();

                                                Console.WriteLine("Tanımlanamayan hareket.");
                                                Thread.Sleep(1500);
                                                goto aramabaşlangıç;
                                                
                                        }
                                    } while (secim != 4);
                                    break;

                                case 2:
                                    Console.Clear();

                                    do
                                    {

                                        Console.WriteLine("-----------------------------------------------");
                                        Console.WriteLine("\nFilmler için yapmak istediğiniz işlemi seçin:\n1- Filmleri Listele\n2- Film Satın Al\n3- Ödeme Yöntemleri\n4- Ana Menüye Dön\n5- Çıkış");
                                        string secimInput = Console.ReadLine();

                                        if (!Kontrol(secimInput, out secim))
                                        {
                                            continue;
                                        }
                                        switch (secim)
                                        {

                                            case 1:


                                                filmilkmenü:
                                                Console.Clear();
                                                Console.WriteLine("1- Yıla göre arama\n2- İsme göre arama\n3- Türe göre arama\n4- Hepsini Listele\n5- Ana Menüye Dön");
                                                string aramasecim = Console.ReadLine();

                                                if (!Kontrol(aramasecim, out int aramakarar))
                                                {
                                                    goto filmilkmenü;
                                                }

                                                Console.Clear();
                                                switch (aramakarar)
                                                {
                                                    case 1:
                                                        Console.WriteLine("Aramak istediğiniz Yılı giriniz");
                                                        string arananyıl = Console.ReadLine();
                                                        if (!Kontrol(arananyıl, out int aranantarih))
                                                        {
                                                            goto filmilkmenü;
                                                        }
                                                        kutuphanefilm.ItemListele(aranantarih);
                                                        break;

                                                    case 2:
                                                        Console.WriteLine("Aramak istediğiniz filmin ismini yazınız");
                                                        string arananFilm = Console.ReadLine();
                                                        kutuphanefilm.İsimItemListele(arananFilm);
                                                       
                                                        break;

                                                    case 3:
                                                        Console.Clear();
                                                        Console.WriteLine("Hangi Kategoriyi Listelemek İstiyorsunuz?"); 
                                                        for (int i = 1; i <= 15; i++)
                                                        {
                                                            Console.WriteLine($"{i}- {GetFilmTuru(i)}");
                                                        }
                                                        string listefilmTuruInput = Console.ReadLine();


                                                        if (!int.TryParse(listefilmTuruInput, out int listefilmTuru) || listefilmTuru < 1 || listefilmTuru > 15)
                                                        {
                                                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                            goto filmilkmenü;
                                                        }


                                                        else
                                                        {
                                                            Console.Clear();
                                                            string aranan = GetFilmTuru(listefilmTuru);
                                                            kutuphanefilm.ItemListele(aranan);
                                                        }

                                                        break;

                                                    case 4:
                                                        kutuphanefilm.HepsiniListele();
                                                        break;

                                                    case 5:
                                                        Console.Clear();

                                                        Console.WriteLine("Ana menüye dönülüyor...");
                                                        Thread.Sleep(1500);
                                                        Console.Clear();
                                                        break;

                                                    default:
                                                        Console.WriteLine("Tanımlanamayan Hareket");
                                                        goto filmilkmenü;
                                                        

                                                }

                                                break;
                                            case 2:

                                                Console.Clear();
                                            satınbaşlangıçfilm:

                                                Console.WriteLine("Ödeme türünü seçiniz.\n1-Kredi Kartı\n2-Havale\n3-Ana Menüye Dön");
                                                string satınsecim = Console.ReadLine();

                                                if (!Kontrol(satınsecim, out int satınkarar))
                                                {
                                                    goto satınbaşlangıçfilm;
                                                }

                                                Console.Clear();
                                                switch (satınkarar)
                                                {
                                                    case 1:

                                                        Console.WriteLine("Satın almak istediğiniz filmin adını girin.");
                                                        string satınfilm = Console.ReadLine();
                                                        Console.WriteLine("Satın almak istediğiniz filmin yönetmenini girin");
                                                        string satınyönetmen = Console.ReadLine();
                                                        if (!kutuphanefilm.VarMıKontrol(satınfilm, satınyönetmen))
                                                        {
                                                            Console.WriteLine("Aradığınız film kütüphanede bulunmamaktadır.");
                                                            Thread.Sleep(1500);
                                                            Console.Clear();
                                                            goto satınbaşlangıçfilm;
                                                        }
                                                        Console.WriteLine("Mail adresinizi giriniz:");
                                                        string kişimail = Console.ReadLine();

                                                        Console.WriteLine("Gönderilecek adresi giriniz:");
                                                        string kişiadres = Console.ReadLine();

                                                    hatalıhanefilm:
                                                        Console.WriteLine("16 haneli kredi kartınızı boşluk koymadan giriniz:");
                                                        string kişihane = Console.ReadLine();

                                                        if (kişihane.Length != 16)
                                                        {
                                                            Console.WriteLine("Lütfen geçerli bir kart numarası giriniz.");
                                                            Thread.Sleep(1500);
                                                            goto hatalıhanefilm;
                                                        }
                                                        Console.WriteLine("Lütfen CVV kodunuzu giriniz");
                                                        string kişicvv = Console.ReadLine();

                                                        Console.WriteLine("Tarihi giriniz (AA/YY formatında):");
                                                        string kişitarih = Console.ReadLine();

                                                        DateTime tarih;
                                                        if (DateTime.TryParseExact(kişitarih, "MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tarih))
                                                        {
                                                            string smtpServer = "smtp.gmail.com"; // SMTP sunucu adresi
                                                            int smtpPort = 587; // SMTP port numarası (genellikle 587 veya 465)
                                                            string smtpUsername = "ardacanb7@gmail.com"; // E-posta adresiniz
                                                            string smtpPassword = ""; // E-posta şifreniz

                                                            // Gönderilecek e-posta bilgileri isteğe göre kullanıcıdan da alınabilir
                                                            string toEmailAddress = kişimail;
                                                            string subject = "Film Siparişiniz hk.";
                                                            string body = $"Sayın Müşterimiz,\n\nSiparişiniz başarıyla alınmıştır. İşte detaylar:\n\nAdres: {kişiadres}\n\nSiparişiniz en kısa sürede işleme alınacak ve adresinize gönderilecektir.\n\nSipariş verdiğiniz film : {satınfilm} - {satınyönetmen}\n\nSiparişlerinizin detayları en kısa sürede size iletilecektir.\n\nTeşekkür ederiz.";



                                                            // MailMessage sınıfı ile e-posta oluşturma
                                                            MailMessage mail = new MailMessage(smtpUsername, toEmailAddress, subject, body);

                                                            // SmtpClient sınıfı ile e-posta gönderimi
                                                            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                                                            {
                                                                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                                                                smtpClient.EnableSsl = true; // SSL kullanılacaksa true, kullanılmayacaksa false

                                                                try
                                                                {
                                                                    smtpClient.Send(mail);
                                                                    Console.WriteLine("E-posta başarıyla gönderildi.");
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    Console.WriteLine("E-posta gönderiminde hata oluştu: " + ex.ToString());
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Geçersiz tarih formatı. Lütfen AA.YY biçiminde girin.");
                                                        }
                                                        break;

                                                    case 2:
                                                        Console.WriteLine("Satın almak istediğiniz kitabın adını girin.");
                                                        string satınfilmhavale = Console.ReadLine();
                                                        Console.WriteLine("Satın almak istediğiniz kitabın yazarını girin");
                                                        string satınyönetmenhavale = Console.ReadLine();
                                                        if (!kutuphanefilm.VarMıKontrol(satınfilmhavale, satınyönetmenhavale))
                                                        {
                                                            Console.WriteLine("Aradığınız kitap kütüphanede bulunmamaktadır.");
                                                            Thread.Sleep(1500);
                                                            Console.Clear();
                                                            goto satınbaşlangıçfilm;
                                                        }
                                                        Console.WriteLine("IBAN NUMARASI : TR123456789012345678901234\n Havale işlemini tamamladıktan sonra bir tuşa basınız.");
                                                        Console.ReadKey();
                                                        Console.Clear();
                                                        Console.WriteLine("(Havale İşlemi)\n\nSayın Müşterimiz,\n\nHavale işleminizi gerçekleştirdiğiniz için teşekkür ederiz\nİşleminiz en kısa sürede işleme alınacak ve size bilgilendirme maili gönderilecektir.\n\nTeşekkür ederiz.");
                                                        break;
                                                    case 3:
                                                        Console.WriteLine("Ana Menüye dönülüyor.");
                                                        Thread.Sleep(1500);
                                                        continue;
                                                    default:
                                                        Console.Clear();

                                                        Console.WriteLine("Tanımlanamayan hareket.");
                                                        Thread.Sleep(1500);
                                                        goto satınbaşlangıçfilm;

                                                }
                                                break;





                                            case 3:
                                            yöntembaşlangıçfilm:
                                                Console.Clear();
                                                Console.WriteLine("Ödeme Yöntemleri Menüsü:");
                                                Console.WriteLine("1. Havale");
                                                Console.WriteLine("2. Kredi Kartı");

                                                Console.WriteLine("\nLütfen ödeme yöntemini seçin.:");
                                                string ödemeseçim = Console.ReadLine();
                                                if (!Kontrol(ödemeseçim, out int ödemeseçimi))
                                                {
                                                    continue;
                                                }

                                                switch (ödemeseçimi)
                                                {
                                                    case 1:
                                                        HavaleAnlatım();

                                                        break;
                                                    case 2:
                                                        KrediKartıAnlatım();
                                                        break;
                                                    default:
                                                        Console.WriteLine("Geçersiz giriş. Lütfen bir sayı seçin.");
                                                        Thread.Sleep(1500);
                                                        goto yöntembaşlangıçfilm;
                                                }

                                                break;
                                            case 4:
                                                Console.Clear();

                                                Console.WriteLine("Ana menüye dönülüyor...");
                                                Thread.Sleep(1500);
                                                Console.Clear();
                                                break;
                                            case 5:
                                                Console.Clear();

                                                Console.WriteLine("Uygulama kapatılıyor...");
                                                Thread.Sleep(1500);
                                                Environment.Exit(0);
                                                break;
                                            default:
                                                Console.Clear();

                                                Console.WriteLine("Tanımlanamayan hareket.");
                                                Thread.Sleep(1500);
                                                break;
                                        }
                                    } while (secim != 4);
                                    break;
                                    
                                case 3:
                                    Console.Clear();

                                    do
                                    {

                                        Console.WriteLine("-----------------------------------------------");
                                        Console.WriteLine("\nOyunlar için yapmak istediğiniz işlemi seçin:\n1- Oyunları Listele\n2- Oyun Satın Al\n3- Ödeme Yöntemleri\n4- Ana Menüye Dön\n5- Çıkış");
                                        string secimInput = Console.ReadLine();

                                        if (!Kontrol(secimInput, out secim))
                                        {
                                            continue;
                                        }
                                        switch (secim)
                                        {

                                            case 1:


                                                oyunilkmenü:
                                                Console.Clear();
                                                Console.WriteLine("1- Yıla göre arama\n2- İsme göre arama\n3- Türe göre arama\n4- Hepsini Listele\n5- Ana Menüye Dön");
                                                string aramasecim = Console.ReadLine();

                                                if (!Kontrol(aramasecim, out int aramakarar))
                                                {
                                                    goto oyunilkmenü;
                                                }

                                                Console.Clear();
                                                switch (aramakarar)
                                                {
                                                    case 1:
                                                        Console.WriteLine("Aramak istediğiniz Yılı giriniz");
                                                        string arananyıl = Console.ReadLine();
                                                        if (!Kontrol(arananyıl, out int aranantarih))
                                                        {
                                                            goto oyunilkmenü;
                                                        }
                                                        kutuphaneoyun.ItemListele(aranantarih);
                                                        break;

                                                    case 2:
                                                        Console.WriteLine("Aramak istediğiniz kitabın ismini yazınız");
                                                        string arananOyun = Console.ReadLine();
                                                        kutuphaneoyun.İsimItemListele(arananOyun);
                                                      
                                                        break;

                                                    case 3:
                                                        Console.Clear();
                                                        Console.WriteLine("Hangi Kategoriyi Listelemek İstiyorsunuz?"); 
                                                        for (int i = 1; i <= 15; i++)
                                                        {
                                                            Console.WriteLine($"{i}- {GetOyunTuru(i)}");
                                                        }
                                                        string listeOyunTuruInput = Console.ReadLine();


                                                        if (!int.TryParse(listeOyunTuruInput, out int listeOyunTuru) || listeOyunTuru < 1 || listeOyunTuru > 15)
                                                        {
                                                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                            goto oyunilkmenü;
                                                        }


                                                        else
                                                        {
                                                            Console.Clear();
                                                            string aranan = GetOyunTuru(listeOyunTuru);
                                                            kutuphaneoyun.ItemListele(aranan);
                                                        }

                                                        break;
                                                    case 4:
                                                        kutuphaneoyun.HepsiniListele();
                                                        break;
                                                    case 5:
                                                        Console.Clear();

                                                        Console.WriteLine("Ana menüye dönülüyor...");
                                                        Thread.Sleep(1500);
                                                        break;

                                                    default:
                                                        Console.WriteLine("Tanımlanamayan Hareket");
                                                        goto oyunilkmenü;
                                                        

                                                }









                                                break;
                                            case 2:
                                                Console.Clear();
                                            satınbaşlangıçoyun:

                                                Console.WriteLine("Ödeme türünü seçiniz.\n1-Kredi Kartı\n2-Havale\n3-Ana Menüye Dön");
                                                string satınsecim = Console.ReadLine();

                                                if (!Kontrol(satınsecim, out int satınkarar))
                                                {
                                                    goto satınbaşlangıçoyun;
                                                }

                                                Console.Clear();
                                                switch (satınkarar)
                                                {
                                                    case 1:

                                                        Console.WriteLine("Satın almak istediğiniz kitabın adını girin.");
                                                        string satınoyun = Console.ReadLine();
                                                        Console.WriteLine("Satın almak istediğiniz kitabın yazarını girin");
                                                        string satınyapımcı = Console.ReadLine();
                                                        if (!kutuphaneoyun.VarMıKontrol(satınoyun, satınyapımcı))
                                                        {
                                                            Console.WriteLine("Aradığınız kitap kütüphanede bulunmamaktadır.");
                                                            Thread.Sleep(1500);
                                                            Console.Clear();
                                                            goto satınbaşlangıçoyun;
                                                        }
                                                        Console.WriteLine("Mail adresinizi giriniz:");
                                                        string kişimail = Console.ReadLine();

                                                        Console.WriteLine("Gönderilecek adresi giriniz:");
                                                        string kişiadres = Console.ReadLine();

                                                    hatalıhaneoyun:
                                                        Console.WriteLine("16 haneli kredi kartınızı boşluk koymadan giriniz:");
                                                        string kişihane = Console.ReadLine();

                                                        if (kişihane.Length != 16)
                                                        {
                                                            Console.WriteLine("Lütfen geçerli bir kart numarası giriniz.");
                                                            Thread.Sleep(1500);
                                                            goto hatalıhaneoyun;
                                                        }
                                                        Console.WriteLine("Lütfen CVV kodunuzu giriniz");
                                                        string kişicvv = Console.ReadLine();

                                                        Console.WriteLine("Tarihi giriniz (AA/YY formatında):");
                                                        string kişitarih = Console.ReadLine();

                                                        DateTime tarih;
                                                        if (DateTime.TryParseExact(kişitarih, "MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tarih))
                                                        {
                                                            string smtpServer = "smtp.gmail.com"; // SMTP sunucu adresi
                                                            int smtpPort = 587; // SMTP port numarası (genellikle 587 veya 465)
                                                            string smtpUsername = "ardacanb7@gmail.com"; // E-posta adresiniz
                                                            string smtpPassword = ""; // E-posta şifreniz

                                                            // Gönderilecek e-posta bilgileri isteğe göre kullanıcıdan da alınabilir
                                                            string toEmailAddress = kişimail;
                                                            string subject = "Oyun Siparişiniz hk.";
                                                            string body = $"Sayın Müşterimiz,\n\nSiparişiniz başarıyla alınmıştır. İşte detaylar:\n\nAdres: {kişiadres}\n\nSiparişiniz en kısa sürede işleme alınacak ve adresinize gönderilecektir.\n\nSipariş verdiğiniz oyun : {satınoyun} - {satınyapımcı}\n\nSiparişlerinizin detayları en kısa sürede size iletilecektir.\n\nTeşekkür ederiz.";



                                                            // MailMessage sınıfı ile e-posta oluşturma
                                                            MailMessage mail = new MailMessage(smtpUsername, toEmailAddress, subject, body);

                                                            // SmtpClient sınıfı ile e-posta gönderimi
                                                            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                                                            {
                                                                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                                                                smtpClient.EnableSsl = true; // SSL kullanılacaksa true, kullanılmayacaksa false

                                                                try
                                                                {
                                                                    smtpClient.Send(mail);
                                                                    Console.WriteLine("E-posta başarıyla gönderildi.");
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    Console.WriteLine("E-posta gönderiminde hata oluştu: " + ex.ToString());
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Geçersiz tarih formatı. Lütfen AA.YY biçiminde girin.");
                                                        }
                                                        break;

                                                    case 2:
                                                        Console.WriteLine("Satın almak istediğiniz kitabın adını girin.");
                                                        string satınoyunhavale = Console.ReadLine();
                                                        Console.WriteLine("Satın almak istediğiniz kitabın yazarını girin");
                                                        string satınyapımcıhavale = Console.ReadLine();
                                                        if (!kutuphaneoyun.VarMıKontrol(satınoyunhavale, satınyapımcıhavale))
                                                        {
                                                            Console.WriteLine("Aradığınız kitap kütüphanede bulunmamaktadır.");
                                                            Thread.Sleep(1500);
                                                            Console.Clear();
                                                            goto satınbaşlangıçoyun;
                                                        }
                                                        Console.WriteLine("IBAN NUMARASI : TR123456789012345678901234\n Havale işlemini tamamladıktan sonra bir tuşa basınız.");
                                                        Console.ReadKey();
                                                        Console.Clear();
                                                        Console.WriteLine("(Havale İşlemi)\n\nSayın Müşterimiz,\n\nHavale işleminizi gerçekleştirdiğiniz için teşekkür ederiz\nİşleminiz en kısa sürede işleme alınacak ve size bilgilendirme maili gönderilecektir.\n\nTeşekkür ederiz.");
                                                        break;
                                                    case 3:
                                                        Console.WriteLine("Ana Menüye dönülüyor.");
                                                        Thread.Sleep(1500);
                                                        continue;
                                                    default:
                                                        Console.Clear();

                                                        Console.WriteLine("Tanımlanamayan hareket.");
                                                        Thread.Sleep(1500);
                                                        goto satınbaşlangıçoyun;

                                                }
                                                break;





                                            case 3:
                                            yöntembaşlangıçoyun:
                                                Console.Clear();
                                                Console.WriteLine("Ödeme Yöntemleri Menüsü:");
                                                Console.WriteLine("1. Havale");
                                                Console.WriteLine("2. Kredi Kartı");

                                                Console.WriteLine("\nLütfen ödeme yöntemini seçin.:");
                                                string ödemeseçim = Console.ReadLine();
                                                if (!Kontrol(ödemeseçim, out int ödemeseçimi))
                                                {
                                                    continue;
                                                }

                                                switch (ödemeseçimi)
                                                {
                                                    case 1:
                                                        HavaleAnlatım();

                                                        break;
                                                    case 2:
                                                        KrediKartıAnlatım();
                                                        break;
                                                    default:
                                                        Console.WriteLine("Geçersiz seçim. Lütfen 1 veya 2 girin.");
                                                        Thread.Sleep(1500);
                                                        goto yöntembaşlangıçoyun;
                                                }

                                                break;
                                            case 4:
                                                Console.Clear();

                                                Console.WriteLine("Ana menüye dönülüyor...");
                                                Thread.Sleep(1500);
                                                Console.Clear();
                                                break;
                                            case 5:
                                                Console.Clear();

                                                Console.WriteLine("Uygulama kapatılıyor...");
                                                Thread.Sleep(1500);
                                                Environment.Exit(0);
                                                break;
                                            default:
                                                Console.Clear();

                                                Console.WriteLine("Tanımlanamayan hareket.");
                                                Thread.Sleep(1500);
                                                break;
                                                
                                        }
                                    } while (secim != 4);
                                    break;
                                    
                                case 4:
                                    Console.Clear();
                                    Console.WriteLine("Tüm Kitaplar:");
                                    kutuphanekitap.ItemListele("Hepsi");

                                    Console.WriteLine("\nTüm Filmler:");
                                    kutuphanefilm.ItemListele("Hepsi");

                                    Console.WriteLine("\nTüm Oyunlar:");
                                    kutuphaneoyun.ItemListele("Hepsi");
                                    Console.ReadKey();
                                    Console.WriteLine("Devam etmek için bir tuşa basınız.");
                                    break;
                                    
                                case 5:
                                    Console.Clear();
                                    Console.WriteLine("Giriş ekranına dönülüyor...");
                                    Thread.Sleep(1500);
                                    break;
                                    
                                case 6:
                                    Console.Clear();

                                    Console.WriteLine("Uygulama kapatılıyor...");
                                    Thread.Sleep(1500);
                                    Environment.Exit(0);
                                    break;
                                default:
                                    Console.WriteLine("Tanımlanamayan Hareket.");
                                    Thread.Sleep(1500);
                                    break;
                            }
                        } while (kategoriNumara != 5);


                        break;
                    case 2:
                        Console.WriteLine("Lütfen Kullanıcı adı giriniz.");
                        string kullaniciadi = Console.ReadLine();
                        if (Kullanicikontrol(kullaniciadi) == false) // kullanıcı adının doğru olup olmadığı kontrol ediliyor
                        {
                            Console.WriteLine("Kullanıcı adı yanlış tekrar deneyin.");
                            Thread.Sleep(1500);
                            continue;
                        }
                        else if (Kullanicikontrol(kullaniciadi) == true) 
                        {
                            Console.WriteLine("Lütfen şifre girin.");
                            string şifre = gizlisifre();
                            if(Sifrekontrol(şifre) == false ) // şifrenin doğru olup olmadığı kontrol ediliyor
                            {
                                Console.WriteLine("Şifre yanlış lütfen tekrar deneyin.");
                                Thread.Sleep(1500);
                                continue;

                            }
                            else
                            {
                                girisbasarili = true;
                do
                {
                    Console.Clear();
                    Console.WriteLine("\nNe yapmak istersiniz:\n1- Kitaplar\n2- Filmler\n3- Oyunlar\n4- Tüm Ürünleri Listele\n5- Giriş Ekranına Dönüş\n6- Çıkış");
                    string kategoriInput = Console.ReadLine();

                    if (!int.TryParse(kategoriInput, out kategoriSecim) && kategoriSecim <1 && kategoriSecim > 3)
                    {
                        Console.WriteLine("Geçersiz giriş. Lütfen bir sayı seçin.");
                        Thread.Sleep(1500);
                        Console.Clear() ;

                        continue;

                    }
                    switch (kategoriSecim)
                    {
                        case 1:
                            Console.Clear();

                            do
                            {

                                Console.WriteLine("-----------------------------------------------");
                                Console.WriteLine("\nKitaplar için yapmak istediğiniz işlemi seçin:\n1- Kitap Ekle\n2- Kitap Sil\n3- Kitapları Listele\n4- Ana Menüye Dön\n5- Çıkış");
                                string secimInput = Console.ReadLine();

                                if (!int.TryParse(secimInput, out secim))
                                {
                                    Console.WriteLine("Geçersiz giriş. Lütfen bir sayı seçin.");
                                    Thread.Sleep(1500);
                                    Console.Clear();
                                    continue;
                                }
                                switch (secim)
                                {
                                    case 1:
                                        Console.Clear();

                                        Console.WriteLine("Eklenecek kitabın adını girin:");
                                        string kitapAdi = Console.ReadLine();
                                        Console.WriteLine("Yazarın adını girin:");
                                        string yazarAdi = Console.ReadLine();
                                        Console.WriteLine("Eklenecek kitabın fiyatını girin (Sadece sayı):");
                                        bool kitapfiyat = int.TryParse(Console.ReadLine(), out int kitapFiyatı);
                                        if (!kitapfiyat)
                                        {
                                        Console.Clear();
                                        Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                        Thread.Sleep(1500);
                                        continue;
                                        }
                                        Console.WriteLine("Yayınlanma tarihini giriniz");
                                        
                                        bool yayıntarihi= int.TryParse(Console.ReadLine(), out int yayıntarih);
                                                        if (!yayıntarihi)
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                                Thread.Sleep(1500);
                                                            continue;
                                                        }
                                                        Console.WriteLine("Kitap türünü seçiniz:");
                                        for (int i = 1; i <= 14; i++)
                                        {
                                            Console.WriteLine($"{i}- {GetKitapTuru(i)}");
                                        }
                                                        int kitaptur;
                                                        bool validInput = int.TryParse(Console.ReadLine(), out kitaptur);

                                                        if (!validInput || kitaptur < 1 || kitaptur > 14)
                                                        {
                                                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                            Thread.Sleep(1500);
                                                            Console.Clear();
                                                        }
                                                        else
                                                        {
                                                            string genreAdi = GetKitapTuru(kitaptur);
                                                            Kitap yeniKitap = new Kitap { Tür = "Kitap", İsim = kitapAdi, Yazar = yazarAdi, Genre = genreAdi, Yıl = yayıntarih, Fiyat = kitapFiyatı };
                                                            kutuphanekitap.ItemEkle(yeniKitap);
                                                        }

                                                        break;
                                    case 2:
                                        Console.Clear();
                                        
                                        Console.WriteLine("Silinecek kitabın adını girin:");
                                        string silinecekKitapAdi = Console.ReadLine();
                                        Console.WriteLine("Silinecek kitabın yazarını girin:");
                                        string silinecekKitapYazar = Console.ReadLine();
                                        Console.WriteLine("Silinecek kitabın yılını girin:");
                                        string kitapyıl = Console.ReadLine();
                                                        if (!int.TryParse(kitapyıl, out int silinecekKitapYıl))
                                                        {
                                                            Console.WriteLine("Lütfen geçerli bir yıl giriniz");
                                                            continue;

                                                        }
                                                        {

                                                        }

                                                        for (int i = 1; i <= 14; i++)
                                                        {
                                                            Console.WriteLine($"{i}- {GetKitapTuru(i)}");
                                                        }
                                                        string secilenInput = Console.ReadLine();
                                                        int secilenk;

                                                        if (!Kontrol(secilenInput, out secilenk))
                                                        {
                                                            continue;
                                                        }

                                                        else
                                                        {
                                                            string silinecekKitapGenre = GetKitapTuru(secilenk);
                                                            kutuphanekitap.ItemSil(silinecekKitapAdi, "Kitap", silinecekKitapGenre, silinecekKitapYıl, silinecekKitapYazar);
                                                        }

                                                        break;
                                    case 3:
                                                    aramabaşlangıç:
                                                        Console.Clear();
                                                        Console.WriteLine("1- Yıla göre arama\n2- İsme göre arama\n3- Türe göre arama\n4- Hepsini Listele\n5- Ana Menüye Dön");
                                                        string aramasecim = Console.ReadLine();

                                                        if (!Kontrol(aramasecim, out int aramakarar))
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                            Thread.Sleep(1500);
                                                            goto aramabaşlangıç;
                                                        }

                                                        
                                                        switch (aramakarar)
                                                        {
                                                            case 1:
                                                                Console.Clear();
                                                                Console.WriteLine("Aramak istediğiniz Yılı giriniz");
                                                                string arananyıl = Console.ReadLine();
                                                                if (!Kontrol(arananyıl, out int aranantarih))
                                                                {
                                                                    goto aramabaşlangıç;
                                                                }
                                                                kutuphanekitap.ItemListele(aranantarih);
                                                                break;

                                                            case 2:
                                                                Console.Clear();
                                                                Console.WriteLine("Aramak istediğiniz kitabın ismini yazınız");
                                                                string arananKitap = Console.ReadLine();
                                                                kutuphanekitap.İsimItemListele(arananKitap);
                                                             
                                                                break;

                                                            case 3:
                                                                Console.Clear();
                                                                Console.WriteLine("Hangi Kategoriyi Listelemek İstiyorsunuz?"); 
                                                                for (int i = 1; i <= 15; i++)
                                                                {
                                                                    Console.WriteLine($"{i}- {GetKitapTuru(i)}");
                                                                }
                                                                string listekitapTuruInput = Console.ReadLine();
                                                                int listekitapTuru;

                                                                if (!int.TryParse(listekitapTuruInput, out listekitapTuru) || listekitapTuru < 1 || listekitapTuru > 15)
                                                                {
                                                                    Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                                    Thread.Sleep(1500);
                                                                    goto aramabaşlangıç;
                                                                }


                                                                else
                                                                {
                                                                    Console.Clear();
                                                                    string aranan = GetKitapTuru(listekitapTuru);
                                                                    kutuphanekitap.ItemListele(aranan);
                                                                }

                                                                break;
                                                            case 4: 
                                                                kutuphanekitap.HepsiniListele();
                                                                break;
                                                            case 5:
                                                                Console.Clear();

                                                                Console.WriteLine("Ana menüye dönülüyor...");
                                                                Thread.Sleep(1500);
                                                                Console.Clear();
                                                                break;

                                                            default:
                                                                Console.Clear();
                                                                Console.WriteLine("Tanımlanamayan Hareket");
                                                                goto aramabaşlangıç;

                                                        }
                                                        break;
                                    case 4:
                                        Console.Clear();

                                        Console.WriteLine("Ana menüye dönülüyor...");
                                        Thread.Sleep(1500);
                                                        Console.Clear();
                                                        break;
                                    case 5:
                                        Console.Clear();

                                        Console.WriteLine("Uygulama kapatılıyor...");
                                        Thread.Sleep(1500);
                                        Environment.Exit(0);
                                        break;
                                    default:
                                        Console.Clear();

                                        Console.WriteLine("Tanımlanamayan hareket.");
                                        Thread.Sleep(1500);
                                                        goto İlkmenü;
                                        
                                }
                            } while (secim != 4);
                            break;
                        case 2:
                            Console.Clear();

                            do
                            {
                                Console.WriteLine("-----------------------------------------------");
                                Console.WriteLine("\nFilmler için yapmak istediğiniz işlemi seçin:\n1- Film Ekle\n2- Film Sil\n3- Filmleri Listele\n4- Ana Menüye Dön\n5- Çıkış");
                                                string secimInput = Console.ReadLine();
                                                
                                                
                                                if (!Kontrol(secimInput, out secim))
                                                {
                                                    continue;
                                                }

                                                switch (secim)
                                {
                                    case 1:
                                        Console.Clear();

                                        Console.WriteLine("Eklenecek filmin adını girin:");
                                        string filmAdi = Console.ReadLine();
                                        Console.WriteLine("Yönetmenin adını girin:");
                                        string yonetmenAdi = Console.ReadLine();
                                                        Console.WriteLine("Eklenecek filmin fiyatını girin (Sadece sayı):");
                                                        bool filmfiyat = int.TryParse(Console.ReadLine(), out int filmfiyatı);
                                                        if (!filmfiyat)
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                            Thread.Sleep(1500);
                                                            continue;
                                                        }
                                                        Console.WriteLine("Çıkış yılı girin.");
                                                        bool yayıntarihi = int.TryParse(Console.ReadLine(), out int yayıntarih);
                                                        if (!yayıntarihi)
                                                        {
                                                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                            Thread.Sleep(1500);
                                                            continue;
                                                        }
                                                        Console.WriteLine("Film türünü seçiniz:");
                                        for (int i = 1; i <= 14; i++)
                                        {
                                            Console.WriteLine($"{i}- {GetFilmTuru(i)}");
                                        }

                                                        string filmturInput = Console.ReadLine();
                                                        int filmtur;

                                                        if (!int.TryParse(filmturInput, out filmtur) || (filmtur < 1 || filmtur > 14))
                                                        {
                                                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                        
                                                            Thread.Sleep(1500);
                                                            Console.Clear();
                                                        }
                                                        else
                                                        {
                                                            Console.Clear();
                                                            string genreAdi = GetFilmTuru(filmtur);
                                                            Film yeniFilm = new Film { Tür = "Film", İsim = filmAdi, Yönetmen = yonetmenAdi, Genre = genreAdi, Yıl = yayıntarih, Fiyat = filmfiyatı };
                                                            kutuphanefilm.ItemEkle(yeniFilm);
                                                        }




                                                        break;
                                    case 2:
                                        Console.Clear();

                                        Console.WriteLine("Silinecek filmin adını girin:");
                                        string silinecekFilmAdi = Console.ReadLine();
                                        Console.WriteLine("Silinecek filmin yönetmenini girin:"); 
                                        string silinecekFilmYönetmen = Console.ReadLine();
                                                        

                                                        Console.WriteLine("Silinecek filmin yılını giriniz");

                                                        bool siltarihi = int.TryParse(Console.ReadLine(), out int siltarih);
                                                        if (!siltarihi)
                                                        {
                                                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                            Thread.Sleep(1500);
                                                            continue;
                                                        }
                                                        for (int i = 1; i <= 14; i++)
                                                        {
                                                            Console.WriteLine($"{i}- {GetFilmTuru(i)}");
                                                        }
                                                        string secilenFilmInput = Console.ReadLine();
                                                        int secilenF;

                                                        if (!Kontrol(secilenFilmInput, out secilenF))
                                                        {
                                                            continue;
                                                        }
                                                        else
                                                        {
                                                            string silinecekFilmGenre = GetFilmTuru(secilenF);
                                                            kutuphanefilm.ItemSil(silinecekFilmAdi, "Film", silinecekFilmGenre, siltarih, silinecekFilmYönetmen );
                                                        }

                                                        break;
                                    case 3:
                                                    filmilkmenüy:
                                                        Console.Clear();
                                                        Console.WriteLine("1- Yıla göre arama\n2- İsme göre arama\n3- Türe göre arama\n4- Hepsini Listele\n5- Ana Menüye Dön");
                                                        string aramasecim = Console.ReadLine();

                                                        if (!Kontrol(aramasecim, out int aramakarar))
                                                        {

                                                            goto filmilkmenüy;
                                                        }

                                                        Console.Clear();
                                                        switch (aramakarar)
                                                        {
                                                            case 1:
                                                                Console.WriteLine("Aramak istediğiniz Yılı giriniz");
                                                                string arananyıl = Console.ReadLine();
                                                                if (!Kontrol(arananyıl, out int aranantarih))
                                                                {
                                                                    goto filmilkmenüy;
                                                                }
                                                                kutuphanefilm.ItemListele(aranantarih);
                                                                break;

                                                            case 2:
                                                                Console.WriteLine("Aramak istediğiniz filmin ismini yazınız");
                                                                string arananFilm = Console.ReadLine();
                                                                kutuphanefilm.İsimItemListele(arananFilm);
                                                              
                                                                break;

                                                            case 3:
                                                                Console.Clear();
                                                                Console.WriteLine("Hangi Kategoriyi Listelemek İstiyorsunuz?");
                                                                for (int i = 1; i <= 15; i++)
                                                                {
                                                                    Console.WriteLine($"{i}- {GetFilmTuru(i)}");
                                                                }
                                                                string listefilmTuruInput = Console.ReadLine();


                                                                if (!int.TryParse(listefilmTuruInput, out int listefilmTuru) || listefilmTuru < 1 || listefilmTuru > 15)
                                                                {
                                                                    Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                                    goto filmilkmenüy;
                                                                }


                                                                else
                                                                {
                                                                    Console.Clear();
                                                                    string aranan = GetFilmTuru(listefilmTuru);
                                                                    kutuphanefilm.ItemListele(aranan);
                                                                }

                                                                break;

                                                                case 4:
                                                                kutuphanefilm.HepsiniListele();
                                                                break;
                                                            case 5:
                                                                Console.Clear();

                                                                Console.WriteLine("Ana menüye dönülüyor...");
                                                                Thread.Sleep(1500);
                                                                Console.Clear() ;
                                                                break;

                                                            default:
                                                                Console.WriteLine("Tanımlanamayan Hareket");
                                                                goto filmilkmenüy;

                                                        }
                                                        break;
                                                    case 4:
                                        Console.Clear();

                                        Console.WriteLine("Ana menüye dönülüyor...");
                                        Thread.Sleep(1500);
                                                        Console.Clear();
                                        break;
                                    case 5:
                                        Console.Clear();

                                        Console.WriteLine("Uygulama kapatılıyor...");
                                        Thread.Sleep(1500);
                                        Environment.Exit(0);
                                        break;
                                    default:
                                        Console.Clear();

                                        Console.WriteLine("Tanımlanamayan hareket.");
                                        Thread.Sleep(1500);
                                        break;
                                }
                            } while (secim != 4);
                            break;
                        case 3:
                            Console.Clear();

                            do
                            {
                                Console.WriteLine("-----------------------------------------------");
                                Console.WriteLine("\nOyunlar için yapmak istediğiniz işlemi seçin:\n1- Oyun Ekle\n2- Oyun Sil\n3- Oyunları Listele\n4- Ana Menüye Dön\n5- Çıkış");
                                string secimInput= Console.ReadLine();
                                                if (!Kontrol(secimInput, out secim))
                                                {
                                                    
                                                    continue;
                                                }

                                                switch (secim)
                                {
                                    case 1:
                                        Console.Clear();

                                        Console.WriteLine("Eklenecek oyunun adını girin:");
                                        string oyunAdi = Console.ReadLine();
                                        Console.WriteLine("Yapımcının adını girin:");
                                        string yapimciAdi = Console.ReadLine();
                                                        Console.WriteLine("Eklenecek oyunun fiyatını girin (Sadece sayı):");
                                                        bool oyunfiyat = int.TryParse(Console.ReadLine(), out int oyunfiyatı);
                                                        if (!oyunfiyat)
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                            Thread.Sleep(1500);
                                                            continue;
                                                        }
                                                        Console.WriteLine("Oyunun çıkış tarihini girin:");
                                                        bool yayıntarihi = int.TryParse(Console.ReadLine(), out int yayıntarih);
                                                        if (!yayıntarihi)
                                                        {
                                                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                            Thread.Sleep(1500);
                                                            continue;
                                                        }
                                                        Console.WriteLine("Oyun türünü seçiniz:");
                                                       
                                                        for (int i = 1; i <= 14; i++)
                                        {
                                            Console.WriteLine($"{i}- {GetOyunTuru(i)}");
                                        }
                                                        string oyunTuruInput = Console.ReadLine();
                                                        int oyunTuru;

                                                        if (!int.TryParse(oyunTuruInput, out oyunTuru) || (oyunTuru < 1 || oyunTuru > 14))
                                                        {
                                                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                            
                                                            Thread.Sleep(1500);
                                                            Console.Clear() ;
                                                        }
                                                        else
                                                        {
                                                            string genreAdi = GetOyunTuru(oyunTuru);
                                                            Oyun yeniOyun = new Oyun { Tür = "Oyun", İsim = oyunAdi, Yapımcı = yapimciAdi, Genre = genreAdi, Yıl = yayıntarih, Fiyat = oyunfiyatı };
                                                            kutuphaneoyun.ItemEkle(yeniOyun);
                                                        }

                                                        break;
                                    case 2:
                                        Console.Clear();

                                        Console.WriteLine("Silinecek oyunun adını girin:");
                                        string silinecekOyunAdi = Console.ReadLine();
                                        Console.WriteLine("Silinecek oyunun yapımcısını girin:");
                                        string silinecekOyunYapımcı = Console.ReadLine();
                                                        Console.WriteLine("Silinecek oyunun yılını girin");
                                                        bool siltarihi = int.TryParse(Console.ReadLine(), out int siltarih);
                                                        if (!siltarihi)
                                                        {
                                                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                            Thread.Sleep(1500);
                                                            continue;
                                                        }
                                                        for (int i = 1; i <= 14; i++)
                                                        {
                                                            Console.WriteLine($"{i}- {GetOyunTuru(i)}");
                                                        }
                                                        int oyunTuru2;

                                                        if (!int.TryParse(Console.ReadLine(), out oyunTuru2) || oyunTuru2 < 1 || oyunTuru2 > 14)
                                                        {
                                                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                            
                                                            Thread.Sleep(1500);
                                                            Console.Clear() ;

                                                        }
                                                        else
                                                        {
                                                            string genreAdi = GetOyunTuru(oyunTuru2);
                                                            kutuphaneoyun.ItemSil(silinecekOyunAdi, "Oyun", genreAdi, siltarih, silinecekOyunYapımcı);
                                                        }

                                                        break;
                                    case 3:
                                                    oyunilkmenüy:
                                                        Console.Clear();
                                                        Console.WriteLine("1- Yıla göre arama\n2- İsme göre arama\n3- Türe göre arama\n4- Hepsini Listele\n5- Ana Menüye Dön");
                                                        string aramasecim = Console.ReadLine();

                                                        if (!Kontrol(aramasecim, out int aramakarar))
                                                        {
                                                            goto oyunilkmenüy;
                                                        }

                                                        Console.Clear();
                                                        switch (aramakarar)
                                                        {
                                                            case 1:
                                                                Console.WriteLine("Aramak istediğiniz Yılı giriniz");
                                                                string arananyıl = Console.ReadLine();
                                                                if (!Kontrol(arananyıl, out int aranantarih))
                                                                {
                                                                    goto oyunilkmenüy;
                                                                }
                                                                kutuphaneoyun.ItemListele(aranantarih);
                                                                break;

                                                            case 2:
                                                                Console.WriteLine("Aramak istediğiniz kitabın ismini yazınız");
                                                                string arananOyun = Console.ReadLine();
                                                                kutuphaneoyun.İsimItemListele(arananOyun);
                                                        
                                                                break;

                                                            case 3:
                                                                Console.Clear();
                                                                Console.WriteLine("Hangi Kategoriyi Listelemek İstiyorsunuz?"); 
                                                                for (int i = 1; i <= 15; i++)
                                                                {
                                                                    Console.WriteLine($"{i}- {GetOyunTuru(i)}");
                                                                }
                                                                string listeOyunTuruInput = Console.ReadLine();


                                                                if (!int.TryParse(listeOyunTuruInput, out int listeOyunTuru) || listeOyunTuru < 1 || listeOyunTuru > 15)
                                                                {
                                                                    Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                                                    goto oyunilkmenüy;
                                                                }


                                                                else
                                                                {
                                                                    Console.Clear();
                                                                    string aranan = GetOyunTuru(listeOyunTuru);
                                                                    kutuphaneoyun.ItemListele(aranan);
                                                                }

                                                                break;

                                                            case 4:
                                                                kutuphaneoyun.HepsiniListele();
                                                                break;

                                                            case 5:
                                                                Console.Clear();

                                                                Console.WriteLine("Ana menüye dönülüyor...");
                                                                Thread.Sleep(1500);
                                                                Console.Clear();
                                                                break;

                                                            default:
                                                                Console.WriteLine("Tanımlanamayan Hareket");
                                                                goto oyunilkmenüy;

                                                        }
                                                        break;
                                                    case 4:
                                        Console.Clear();

                                        Console.WriteLine("Ana menüye dönülüyor...");
                                        Thread.Sleep(1500);
                                                        Console.Clear();
                                        break;
                                    case 5:
                                        Console.Clear();

                                        Console.WriteLine("Uygulama kapatılıyor...");
                                        Thread.Sleep(1500);
                                        Environment.Exit(0);
                                        break;
                                    default:
                                        Console.Clear();

                                        Console.WriteLine("Tanımlanamayan hareket.");
                                        Thread.Sleep(1500);
                                        break;
                                }
                            } while (secim != 4);
                            break;
                        case 4:

                            Console.Clear();
                            Console.WriteLine("Tüm Kitaplar:");
                            kutuphanekitap.ItemListele("Hepsi");

                            Console.WriteLine("\nTüm Filmler:");
                            kutuphanefilm.ItemListele("Hepsi");

                            Console.WriteLine("\nTüm Oyunlar:");
                            kutuphaneoyun.ItemListele("Hepsi");
                            Console.WriteLine("Devam etmek için bir tuşa basınız.");
                            Console.ReadKey();
                            
                            break;

                        case 5:
                            Console.Clear();

                            Console.WriteLine("Giriş ekranına dönülüyor...");
                            Thread.Sleep(1500);
                                            goto İlkmenü;    
                        case 6:
                            Console.Clear();

                            Console.WriteLine("Uygulama kapatılıyor.");
                            Thread.Sleep(1500);
                            Environment.Exit(0);
                            break;
                        default:
                            Console.Clear();

                            Console.WriteLine("Tanımlanamayan hareket.");
                            Thread.Sleep(1500);
                            break;
                    }
                } while (kategoriSecim != 5);

                            }
                              }
                        break;
                    case 3:
                        Console.WriteLine("Uygulama kapatılıyor.");
                        Thread.Sleep(1500);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Yanlış değer");
                        goto İlkmenü;
                            }
            } while (!girisbasarili); 
        }
        static string GetOyunTuru(int secim) // burada oyun türünün yazdırılabilmesi ve seçilebilmesi için fonksiyon oluşturuluyor
        {
            switch (secim)
            {
                case 1:
                    return "Aksiyon";
                case 2:
                    return "Macera";
                case 3:
                    return "Rol Yapma (RPG)";
                case 4:
                    return "Strateji";
                case 5:
                    return "Simülasyon";
                case 6:
                    return "Bulmaca";
                case 7:
                    return "Yarış";
                case 8:
                    return "Spor";
                case 9:
                    return "Platform";
                case 10:
                    return "Ağırbaşlı (Sandbox)";
                case 11:
                    return "Korku";
                case 12:
                    return "Nişancı (Shooter)";
                case 13:
                    return "Hayatta Kalma";
                case 14:
                    return "Bilgi Yarışması";
                case 15:
                    return "Hepsi";
                default:
                    return "Tanımsız";
            }
        }
        static string GetFilmTuru(int secim)
        {
            switch (secim)
            {
                case 1:
                    return "Aksiyon";
                case 2:
                    return "Macera";
                case 3:
                    return "Drama";
                case 4:
                    return "Komedi";
                case 5:
                    return "Bilim Kurgu";
                case 6:
                    return "Korku";
                case 7:
                    return "Romantik";
                case 8:
                    return "Belgesel";
                case 9:
                    return "Gerilim";
                case 10:
                    return "Anime";
                case 11:
                    return "Fantastik";
                case 12:
                    return "Aile";
                case 13:
                    return "Tarih";
                case 14:
                    return "Müzikal";
                case 15:
                    return "Hepsi";
                default:
                    return "Tanımsız";
            }
        }
        static string GetKitapTuru(int secim)
        {
            switch (secim)
            {
                case 1:
                    return "Roman";
                case 2:
                    return "Bilim Kurgu";
                case 3:
                    return "Korku";
                case 4:
                    return "Macera";
                case 5:
                    return "Polisiye";
                case 6:
                    return "Biyografi";
                case 7:
                    return "Tarih";
                case 8:
                    return "Din";
                case 9:
                    return "Felsefe";
                case 10:
                    return "Sanatsal";
                case 11:
                    return "Çocuk Kitabı";
                case 12:
                    return "Eğitim";
                case 13:
                    return "Referans";
                case 14:
                    return "Kılavuz";
                case 15:
                    return "Hepsi";
                default:
                    return "Tanımsız";
            }
            
        }
        static bool Kullanicikontrol(string kullaniciadi)
        {
            string dogrukullaniciadi = "ardacan24";
            return  kullaniciadi == dogrukullaniciadi ;
        } // kontrol fonksiyonu
        static bool Sifrekontrol(string sifre)
        {
            string dogrusifre = "ardarda1212123";
            return sifre == dogrusifre;
        } // kontrol fonksiyonu
        static string gizlisifre() // şifrenin gizli olması sağlanıyor
        {
            string sifre = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                
                if (key.Key != ConsoleKey.Enter)
                {
                    
                    sifre += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); 

            return sifre;
        }
        public static bool Kontrol(string input, out int output)
        {
            if (int.TryParse(input, out output))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Geçersiz giriş. Lütfen bir sayı seçin.");
                Thread.Sleep(1500);
                Console.Clear();
                return false;
            }
        }// değer olarak sayı istendiğinde yazı girilirse programın kapanmasını önlüyor.

        static void HavaleAnlatım()
        {
            Console.Clear();
            Console.WriteLine("\nHavale Ödeme Yöntemi:\nHavale yöntemi ile ödeme yapmak için banka hesabınıza ihtiyacınız vardır.\nAlışveriş tutarını banka hesabınızdan gönderirsiniz.\nİşlem süresi bankalar arası değişebilir.\nişlem tamamlandığında size bir bilgilendirme maili gelecektir.");
            Console.WriteLine("Bir tuşa basınız.");
            Console.ReadKey();

        } // havale yöntemi anlatılıyor

        static void KrediKartıAnlatım()
        {
            Console.Clear();
            Console.WriteLine("\nKredi Kartı Ödeme Yöntemi:\nKredi kartı ile ödeme yapmak için mail adresi, kredi kartı numarası, son kullanma tarihi ve CVV bilgilerinizi girmeniz ve daha sonrasında mail adresinize gelen doğrulama kodunu girmeniz gerekir.\nAlışveriş tutarı kredi kartınızdan çekilir ve ödeme anında gerçekleşir.");
            Console.WriteLine("Bir tuşa basınız.");
            Console.ReadKey();
        } // kredi kartı yöntemi anlatılıyor
    }

}

