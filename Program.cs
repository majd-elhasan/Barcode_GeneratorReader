using System;
using IronBarCode;

namespace Barcode_App
{
    class Program
    {
        public static  string barcodes_Dir_path = Directory.GetCurrentDirectory()+"\\Barcodes";
        static void Main(string[] args)
        {
            Directory.CreateDirectory(barcodes_Dir_path);
            welcome();

            void GenerateBarcode(string TextToBe_Barcode, string Output_BarcodeName){
                var MyBarCode = BarcodeWriter.CreateBarcode(TextToBe_Barcode, BarcodeEncoding.Code128);
                MyBarCode.SaveAsImage(barcodes_Dir_path+"\\"+ Output_BarcodeName);
            }
            
            void ReadBarcode(string Barcode_FileName){
                BarcodeResult Result = BarcodeReader.QuicklyReadOneBarcode(barcodes_Dir_path+"\\"+Barcode_FileName);
                if (Result != null){
                    Console.WriteLine("Barkodun değeri :  \n"  + Result.Text);   
                } 
            }

            void GenerateQRcode(string TextToBe_QRcode, string Output_QRcodeName){
                var MyQRCode = QRCodeWriter.CreateQrCode(TextToBe_QRcode,200,QRCodeWriter.QrErrorCorrectionLevel.Low,0);
                MyQRCode.SaveAsImage(barcodes_Dir_path+"\\"+Output_QRcodeName);
            }
            // I didnt include GenerateQRcode() Method because I cant read it from the console .
            

            void Barkod_olustur(){
                GenerateBarcode(Text_input(),input_fileName());
            }
            void Barkod_oku(){
                string[] barcodeFileNames = listingBarcodesDirectory();
                string  Barkod_isimi = "";
                Console.WriteLine("barcode klasöründeki dosyaları göstermek için 1'i ");
                Console.WriteLine("barcode klasöründeki dosya ismini girmek için 2'i  tuşlayınız.");
                string input = Console.ReadLine();
                if (input !="1" && input !="2"){Console.WriteLine("1 ve 2 dışında bir veri girildi !");listingBarcodesDirectory();}
                else 
                switch (input)
                {
                    case "1":
                        dosyalari_goster();
                    break;

                    case "2":
                        dosya_ismini_gir(ref Barkod_isimi);
                    break;
                }
                ReadBarcode(Barkod_isimi);

                void dosyalari_goster(){
                    foreach (string item in barcodeFileNames)
                    {
                        Console.WriteLine(item);
                    }   
                    dosya_ismini_gir(ref Barkod_isimi);
                }
                void dosya_ismini_gir(ref  string Barkod_isimi){
                    Console.WriteLine("Barkod dosaysının ismini (dosya tipi ile beraber) giriniz.");
                    string fileName = Console.ReadLine();
                    int i =0;
                    foreach (string item in barcodeFileNames)
                    {
                        if (fileName != item) i++;
                    }
                    Barkod_isimi = fileName;
                    if (i == barcodeFileNames.Length) {Console.WriteLine("{0} dosya adı {1} klsöründe bulunamadı",input,barcodes_Dir_path); dosya_ismini_gir(ref Barkod_isimi);}
                }

            }
            string[] listingBarcodesDirectory(){
                string[] barcodePaths = Directory.GetFiles(barcodes_Dir_path);
                string[] barcodeFileNames =new string[barcodePaths.Length];
                for (int i = 0; i < barcodePaths.Length; i++)
                {
                    barcodeFileNames[i] = barcodePaths[i].Replace(barcodes_Dir_path+"\\","");
                }
                return barcodeFileNames;
            }
            
            string Text_input()
            {
               Retry:
               Console.WriteLine("Barkod'a dönüştürmek istediğiniz yazıyı giriniz.(Maximum karakter sayısı 40)");
               string text =Console.ReadLine();
                    if (text.Length > 40) goto Retry;
               return text ; 
            }  
            string  input_fileName(){
                string fileName ="";
                string[] fileTypes= new string[]{".jpeg",".png",".jpg",".tiff",".bmp",".gif"};
                try_again:
                
                    Console.WriteLine("Barkod dosyasının ismi ve formatını (.jpeg .png .jpg .tiff .bmp .gif )  giriniz.");
                    fileName = Console.ReadLine();
                    int i = 0;
                    foreach (string item in fileTypes)
                    {
                        if(!fileName.EndsWith(item)){i++;}
                        else {break;}
                    }
                    if(i == fileTypes.Length) goto try_again;
                
                return fileName;
            } 
            void welcome(){
                Console.WriteLine("yapmak istediğiniz işlemi seçiniz.");
                Console.WriteLine("1.  Barkod oluştur");
                Console.WriteLine("2.  Barkod oku");
                string input = Console.ReadLine();
                if (input !="1" && input !="2"){Console.WriteLine("1 ve 2 dışında bir veri girildi !"); welcome();}
                else 
                switch (input)
                {
                    case "1":
                        Barkod_olustur();
                    break;

                    case "2":
                        Barkod_oku();
                    break;
                }
            }        
        }
    }
}