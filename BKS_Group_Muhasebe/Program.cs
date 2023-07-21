using System;

namespace BKS_Group_Muhasebe
{
    internal class Program
    {
        static void Main()
        {
            BaslangicMenu();

        }
        static void Menuyedon()
        {
            Console.Write("\n\n\nAna Menüye Dönmek İçin Bir Tuşa Basınız ");
            Console.ReadKey();
            BaslangicMenu();
        }


        static void BaslangicMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Gelir Ekle\n2. Gider Ekle\n3. Kayıtları Listele\n4. Vergi Hesapla\n5. Çıkış");
            Console.Write("Seçiminiz:");
            string secim = Console.ReadLine();
            Console.Clear();

            if (secim == "1")
            {
                GelirEkle();
            }
            else if (secim == "2")
            {
                GiderEkle();
            }
            else if (secim == "3")
            {
                KayıtlariListele();
            }
            else if (secim == "4")
            {
                VergiHesapla();
            }
            else if (secim == "5")
            {
                Console.WriteLine("Çıkış Yapılıyor...");
            }
            else
            {
                Console.WriteLine("Geçersiz seçim. Tekrar deneyin.");
                BaslangicMenu();
            }
        }





        static void GelirEkle()
        {
            Console.WriteLine("Gelir Ekle");
            Console.Write("Ürün Adı: ");
            string urun = Console.ReadLine();

            Console.Write("Adet: ");
            double adet = double.Parse(Console.ReadLine());

            Console.Write("Fiyat: ");
            double fiyat = double.Parse(Console.ReadLine());

            Console.Write("KDV Oranı (%): ");
            double kdv = double.Parse(Console.ReadLine());

            string tarih = DateTime.Now.ToString("dd.MM.yyyy");
            string tip = "Gelir";

            using (StreamWriter muhasebe = new StreamWriter("muhasebe.txt", true))
            {
                muhasebe.WriteLine($",{tip},{urun},{adet},{fiyat},{kdv},{tarih}|");
            }

            Console.WriteLine("Gelir başarıyla eklendi!");
            Menuyedon();
        }



        static void GiderEkle()
        {
            Console.WriteLine("Gider Ekle");
            Console.Write("Ürün Adı: ");
            string urun = Console.ReadLine();

            Console.Write("Adet: ");
            double adet = double.Parse(Console.ReadLine());

            Console.Write("Fiyat: ");
            double fiyat = double.Parse(Console.ReadLine());

            Console.Write("KDV Oranı (%): ");
            double kdv = double.Parse(Console.ReadLine());

            string tarih = DateTime.Now.ToString("dd.MM.yyyy");
            string tip = "Gider";

            using (StreamWriter muhasebe = new StreamWriter("muhasebe.txt", true))
            {
                muhasebe.WriteLine($",{tip},{urun},{adet},{fiyat},{kdv},{tarih}|");
            }

            Console.WriteLine("Gider başarıyla eklendi!");
            Menuyedon();
        }


        static void KayıtlariListele()
        {
            Console.Clear();
            Console.WriteLine("1 -Gelir kayıtlarını listele");
            Console.WriteLine("2 - Gider kayıtlarını listele");
            ConsoleKeyInfo secim = Console.ReadKey();
            Console.WriteLine(" ");
            if (secim.Key == ConsoleKey.D1)
            {
                Listele("Gelir");
            }
            if (secim.Key == ConsoleKey.D2)
            {
                Listele("Gider");
            }
        }

        static void Listele(string tip)
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-10} {4,-20} {5,-15} {6,-15}", " İşlem Tipi", " Ürün ", "Adet", "B.Fiyat ", "KDV", "Toplam Tutar", "Tarih");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            double adet, b_Fiyat, kdv, t_tutar;
            using StreamReader reader = new StreamReader("muhasebe.txt");
            {
                string[] Satirlar = reader.ReadToEnd().Split("|");
                foreach (var Satir in Satirlar)
                {
                    if (string.IsNullOrEmpty(Satir))
                    {
                        continue;
                    }


                    if (tip == "Gelir")
                    {

                        List<string> sutunlar = new List<string>(Satir.Split(','));
                        if (sutunlar.Contains("Gelir"))
                        {
                            adet = int.Parse(sutunlar[3]);
                            b_Fiyat = int.Parse(sutunlar[4]);
                            kdv = int.Parse(sutunlar[5]);
                            t_tutar = Math.Round(adet * b_Fiyat * ((100 + kdv) / 100));
                            Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-10} {4,-20} {5,-15} {6,-15}", $"{sutunlar[1]}", $"{sutunlar[2]}", $"{sutunlar[3]}", $"{sutunlar[4]}", $"{sutunlar[5]}", $"{t_tutar}", $"{sutunlar[6]}");

                        }

                    }

                    if (tip == "Gider")
                    {
                        List<string> sutunlar = new List<string>(Satir.Split(','));
                        if (sutunlar.Contains("Gider"))
                        {
                            adet = int.Parse(sutunlar[3]);
                            b_Fiyat = int.Parse(sutunlar[4]);
                            kdv = int.Parse(sutunlar[5]);
                            t_tutar = Math.Round(adet * b_Fiyat * ((100 + kdv) / 100));
                            Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-10} {4,-20} {5,-15} {6,-15}", $"{sutunlar[1]}", $"{sutunlar[2]}", $"{sutunlar[3]}", $"{sutunlar[4]}", $"{sutunlar[5]}", $"{t_tutar}", $"{sutunlar[6]}");
                        
                        }
                    }
                }


            }

            Menuyedon();


        }

        static void VergiHesapla()
        {
            double adet, b_Fiyat, kdv, t_tutar,gelirvergisi;
            double topgelir = 0;
            double topgider = 0; 
            double topkdvgelir = 0;
            double topkdvgider = 0;
            using StreamReader reader = new StreamReader("muhasebe.txt");
            {
                string[] Satirlar = reader.ReadToEnd().Split("|");
                foreach (var Satir in Satirlar)
                {
                    if (string.IsNullOrEmpty(Satir))
                    {
                        continue;
                    }
                    List<string> sutunlar = new List<string>(Satir.Split(','));
                    if (sutunlar.Contains("Gelir"))
                    {
                        adet = int.Parse(sutunlar[3]);
                        b_Fiyat = int.Parse(sutunlar[4]);
                        kdv = int.Parse(sutunlar[5]);
                        t_tutar = Math.Round(adet * b_Fiyat * ((100 + kdv) / 100));
                        topgelir += t_tutar; 
                        topkdvgelir += t_tutar-(adet*b_Fiyat);
                    }
                    if (sutunlar.Contains("Gider"))
                    {
                        adet = int.Parse(sutunlar[3]);
                        b_Fiyat = int.Parse(sutunlar[4]);
                        kdv = int.Parse(sutunlar[5]);
                        t_tutar = Math.Round(adet * b_Fiyat * ((100 + kdv) / 100));
                        topgider += t_tutar;
                        topkdvgider += t_tutar - (adet * b_Fiyat);
                    }
                }
               

            }
            gelirvergisi = (topgelir - topgider) * 0.2;
            Console.WriteLine("");
            Console.WriteLine($"Toplam Gelir  : {topgelir} TL");
            Console.WriteLine($"Toplam Gider  : {topgider} TL");
            Console.WriteLine($"Matrah        : {topgelir - topgider} TL");
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine($"Gelir Vergisi : {gelirvergisi} TL");
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine($"KDV           : {(topkdvgelir - topkdvgider)} TL");
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine($"Toplam Vergi  : {(gelirvergisi+topkdvgelir - topkdvgider)} TL");
            Console.WriteLine("----------------------------------------------------------------------");
            Menuyedon();
        }
    }
}