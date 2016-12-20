// ============================================
// 
// FlexImage.Binarize
// BinarizeFinancial.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Accusoft.ImagXpressSdk;
using FlexImage.Core;
using FlexImage.WebService;

#endregion

namespace FlexImage.Binarize
{
    public class BinarizeFinancial
    {
        private readonly ImagXpress imagXpress1;
        private readonly ImageXView imageXView1;
        private readonly Log log = GenericSingleton<Log>.GetInstance();
        private readonly Statistics oSt = new Statistics();
        private readonly Processor prc;
        private readonly WSBasicTable wsBasicTable = GenericSingleton<WSBasicTable>.GetInstance();

        private BinarizeBlur bBlur = BinarizeBlur.NoBlur;
        private BinarizeMode bMode = BinarizeMode.QuickText;

        public BinarizeFinancial()
        {
            this.imagXpress1 = new Accusoft.ImagXpressSdk.ImagXpress();
            this.imagXpress1.Licensing.UnlockRuntime(1908224503, 373713104, 1341088068, 5785);

            this.imagXpress1.InitializeLifetimeService();

            this.imageXView1 = new ImageXView();
            this.imageXView1.AutoImageDispose = true;

            this.imageXView1.Workspace = this.imagXpress1;
            this.imageXView1.InitializeLifetimeService();

            this.prc = new Processor(this.imagXpress1);
        }

        public void Dispose()
        {
            try
            {
                if (this.prc != null)
                    this.prc.Dispose();

                if (this.imageXView1 != null)
                    this.imageXView1.Dispose();

                if (this.imagXpress1 != null)
                    this.imagXpress1.Dispose();
            }
            catch (Exception e)
            {
                this.log.Error("Binarize: Dispose:" + e);
            }
        }

        public string CutEdges(string filename, SaveOptions opt)
        {
            this.log.Debug("CutEdges::" + filename);

            string newFileName = "";

            using (this.prc.Image = ImageX.FromFile(this.imagXpress1, filename))
            {
                var p = new Point(0, 0);
                float percentCrop = 0.90F;
                this.prc.CropBorder(percentCrop, CropType.CropBorderBlackColor);
                this.prc.Deskew();

                this.prc.Image.Save(filename.ToUpper().Replace(".JPG", "_CUT.JPG"), opt);
                    //saves the image                
            }
            this.DisposePRC();

            return newFileName;
        }


        public void BinarizeChqFront(string OriginalFilename, string BinarizedFileName)
        {
            this.log.Debug("BinarizeChqFront: " + System.IO.Path.GetFileName(OriginalFilename));

            int limSizeMin = 0;
            int limSizeMax = 0;

            double ctLow;
            double ctHigh;

            limSizeMin = 16000;
            limSizeMax = 25000;
            ctLow = 0.7;
            ctHigh = 0.4;

            this.oSt.calcHisto(OriginalFilename);

            //double p5 = oSt.percentile(5);
            double p10 = this.oSt.percentile(10);
            //double p15 = oSt.percentile(15);

            //Console.WriteLine("p5: " + p5);
            Console.WriteLine("p10: " + p10);
            //Console.WriteLine("p15: " + p15);

            //Cheque ITAU azul: 115, 157, 167   : Avg 180 : StdDev 32 : Low 158, High 168 - Dif: 10
            //Cheque HSBC1    : 108, 165, 207   : Avg 220 : StdDev 44 : Low 189, High 202 - Dif: 13
            //Cheque HSBC2    : 119, 182, 218   : Avg 224 : StdDev 41 : Low 194, High 207 - Dif: 13
            //ITAU Person.    : 89, 139, 197    : Avg 199 : StdDev 42 : Low 170, High 182 - Dif: 12
            //UBB Verde:      : 94, 147, 188    : Avg 208 : StdDev 46 : Low 175, High 190 - Dif: 15
            //UBB Bege:       : 122, 165, 176   : Avg 195 : StdDev 32 : Low 172, High 182 - Dif: 10

            var lowThreshold = (int) (this.oSt.Average - this.oSt.StdDev*ctLow);
            var highThreshold = (int) (this.oSt.Average - this.oSt.StdDev*ctHigh);

            int k = 170 - lowThreshold;
            if (k > 0)
            {
                lowThreshold += k;
                highThreshold += k;
            }

            int i = 0;
            bool ok = true;

            while (i <= 6)
            {
                ok = true;

                if (lowThreshold < 0)
                    lowThreshold = 0;

                if (highThreshold > 255)
                    highThreshold = 255;

                if (lowThreshold > highThreshold)
                {
                    lowThreshold = highThreshold;
                    i = 6;
                }

                i = i + 1;
                this.ProcessFileBIN(OriginalFilename, BinarizedFileName, lowThreshold, highThreshold, 3);

                var fTIF = new FileInfo(BinarizedFileName);
            
                long fileSize = fTIF.Length;

                if (fileSize > limSizeMax)
                {
                    ok = false;
                    lowThreshold = (int) (this.oSt.Average - this.oSt.StdDev*ctLow*(i + 1));
                    highThreshold = highThreshold + 10;
                }
                else
                {
                    if (fileSize < limSizeMin)
                    {
                        ok = false;
                        lowThreshold = lowThreshold + 10;
                    }
                }
            
                if (ok)
                    break;
            }
            return;
        }

        public void BinarizeChqBack(string OriginalFilename, string BinarizedFileName)
        {
            this.log.Debug("BinarizeChqBack: " + System.IO.Path.GetFileName(OriginalFilename));

            using (this.prc.Image = ImageX.FromFile(this.imagXpress1, OriginalFilename))
            {
                this.prc.EnableArea = false;

                this.prc.AutoBinarize2();
                this.prc.DocumentDespeckle(3, 3);
                this.prc.DocumentBorderCrop();

                var saveOptions = new SaveOptions(); //creates the saveoptions object
                saveOptions.Format = ImageXFormat.Tiff; //tells IX to save to the jpeg image format
                saveOptions.Tiff.Compression = Compression.Group4;
                this.prc.Image.Save(BinarizedFileName, saveOptions); //saves the image
            }
            this.DisposePRC();
        }

        private string PreProcess(string OriginalFileName)
        {
            string preProcessFileBINname = "";

            preProcessFileBINname = OriginalFileName.ToUpper().Replace(".JPG", "_PRE.JPG");

            using (this.prc.Image = ImageX.FromFile(this.imagXpress1, OriginalFileName))
            {
                this.prc.EnableArea = false;
                var cropRectangle = new Rectangle(0, 0, this.prc.Image.Width, this.prc.Image.Height);
                this.prc.CropBorder((float) 0.9, CropType.CropBorderBlackColor);
                var saveOptions = new SaveOptions(); //creates the saveoptions object
                saveOptions.Format = ImageXFormat.Jpeg; //tells IX to save to the jpeg image format
                this.prc.Image.Save(preProcessFileBINname, saveOptions); //saves the image
            }
            this.DisposePRC();

            return preProcessFileBINname;
        }

        public int BinarizeFileFIN(string OriginalFileName, string BinarizedFileName, int rotate180)
        {
            this.log.Debug("BinarizeFile:" + OriginalFileName);

            try
            {
                int lowThreshold = 130; //145
                int highThreshold = 154;

                this.oSt.calcHisto(OriginalFileName);

                //double p5 = oSt.percentile(5);
                double p10 = this.oSt.percentile(10);
                //double p15 = oSt.percentile(15);

                //Console.WriteLine("p5: " + p5);
                Console.WriteLine("p10: " + p10);
                //Console.WriteLine("p15: " + p15);

                if (p10 < 50)
                {
                    //Indica que a imagem qtd excessiva de pontos pretos, indicando
                    //provavelmente área que necessita de CROP
                    OriginalFileName = this.PreProcess(OriginalFileName);
                    this.oSt.calcHisto(OriginalFileName);
                }

                lowThreshold = (int) (this.oSt.Average - this.oSt.StdDev);
                highThreshold = (int) (this.oSt.Average);

                if (lowThreshold < 0)
                    lowThreshold = 0;
                if (highThreshold > 255)
                    highThreshold = 255;
                if (highThreshold < lowThreshold)
                    highThreshold = lowThreshold;


                highThreshold = lowThreshold;


                this.ProcessFileBIN(OriginalFileName, BinarizedFileName, lowThreshold, highThreshold, 5);
            }
            catch (Exception e)
            {
                this.log.Error("Binarize:BinarizeFile:" + e);
                this.log.Debug("Trying to FastBinarize");
                try
                {
                    this.FastBinarizeFIN(OriginalFileName, BinarizedFileName);
                }
                catch (Exception e1)
                {
                    this.log.Error("FastBinarizeError[2]:" + e1);
                }
            }

            return 0;
        }

        public void ProcessFileBIN(string OriginalFileName, string BinarizedFileName, int hsLowThresh, int hsHighThresh,
                                   short despeckleSize)
        {
            using (this.prc.Image = ImageX.FromFile(this.imagXpress1, OriginalFileName))
            {
                this.prc.EnableArea = false;
                this.prc.Binarize(this.bMode, hsLowThresh, hsHighThresh, 0, 3, 48, 50, BinarizeBlur.NoBlur);

                if (despeckleSize != 0)
                    this.prc.DocumentDespeckle(despeckleSize, despeckleSize);

                this.prc.DocumentDeskew(2, 80, Color.White, true, 80);
                this.prc.DocumentBorderCrop();

                var saveOptions = new SaveOptions(); //creates the saveoptions object
                saveOptions.Format = ImageXFormat.Tiff; //tells IX to save to the jpeg image format
                saveOptions.Tiff.Compression = Compression.Group4;
                this.prc.Image.Save(BinarizedFileName, saveOptions); //saves the image
            }
            this.DisposePRC();
        }

        public void FastBinarizeFIN(string OriginalFileName, string BinarizedFileName)
        {
            if (File.Exists(OriginalFileName))
            {
                using (this.prc.Image = ImageX.FromFile(this.imagXpress1, OriginalFileName))
                {
                    this.prc.EnableArea = false;
                    this.prc.AutoBinarize();

                    int low = this.prc.BinarizeLowThreshold;
                    int high = this.prc.BinarizeHighThreshold;

                    //Verifica se imagem ficou muito Clara (apenas imagens Frente)
                    //Vide imagens de Fax
                    if (OriginalFileName.Contains("F.JPG"))
                    {
                        if ((high - low) < 10)
                        {
                            if (low > 160)
                            {
                                this.log.Debug(">>>> Rebinarizing... - High/Low=" + high.ToString() + "/" +
                                               low.ToString());
                                this.BinarizeFileFIN(OriginalFileName, BinarizedFileName, 0);
                                this.DisposePRC();
                                return;
                            }
                        }
                    }

                    if (this.wsBasicTable.GetParameters("BINARIZE_DESPECKLE", "1") == "1")
                        this.prc.DocumentDespeckle(4, 4);

                    double MinimumAngle = 5;
                    short MinimumConfidence = 75;
                    short Quality = 10;

                    if (this.wsBasicTable.GetParameters("BINARIZE_DESPECKLE2", "0") == "1")                    
                        this.prc.DocumentDespeckle(20, 20);


                    if (this.wsBasicTable.GetParameters("BINARIZE_DESKEW", "1") == "1")
                        this.prc.DocumentDeskew(MinimumAngle, MinimumConfidence, Color.White, false, Quality);

                    if (this.wsBasicTable.GetParameters("BINARIZE_BORDERCROP", "0") == "1")
                        this.prc.DocumentBorderCrop();

                    var saveOptions = new SaveOptions(); //creates the saveoptions object
                    saveOptions.Format = ImageXFormat.Tiff; //tells IX to save to the jpeg image format
                    saveOptions.Tiff.Compression = Compression.Group4;
                    this.prc.Image.Save(BinarizedFileName, saveOptions); //saves the image
                }
                this.DisposePRC();
            }
            else
                this.log.Error("FastBinarize::File doesnt exist");
        }

        private void DisposePRC()
        {
            // Dispose the ImageX object if the ImageXView has one
            try
            {
                if (this.prc.Image != null)
                {
                    this.prc.Image.Dispose();
                    this.prc.Image = null;
                }
                if (this.imageXView1.Image != null)
                {
                    this.imageXView1.Image.Dispose();
                    this.imageXView1.Image = null;
                }
            }
            catch (Exception e)
            {
                this.log.Error("DisposePRC:" + e);
            }
        }

        public void RotateFile(string BinarizedFileName, int degrees)
        {
            using (this.prc.Image = ImageX.FromFile(this.imagXpress1, BinarizedFileName))
            {
                this.prc.Rotate(degrees);
                var saveOptions = new SaveOptions(); //creates the saveoptions object
                saveOptions.Format = ImageXFormat.Tiff;
                saveOptions.Tiff.Compression = Compression.Group4;
                this.prc.Image.Save(BinarizedFileName, saveOptions); //saves the image
            }
            this.DisposePRC();
        }

        public string CheckRotation(string OriginalFileName)
        {
            string NewFileName = "";

            using (this.prc.Image = ImageX.FromFile(this.imagXpress1, OriginalFileName))
            {
                if (this.prc.Image.Width >= 1300)
                {
                    NewFileName = OriginalFileName.ToUpper().Replace(".JPG", "_ROT.JPG");
                    this.prc.Rotate(90);
                    var saveOptions = new SaveOptions(); //creates the saveoptions object
                    saveOptions.Format = ImageXFormat.Jpeg;
                    saveOptions.Tiff.Compression = Compression.Jpeg;
                    this.prc.Image.Save(NewFileName, saveOptions); //saves the image
                }
            }
            this.DisposePRC();

            return NewFileName;
        }

        private void DilateCheck(string BinarizedFileName)
        {
            using (this.prc.Image = ImageX.FromFile(this.imagXpress1, BinarizedFileName))
            {
                this.prc.DocumentBorderCrop();
                var MergeTL = new PointF(0, (float) (this.prc.Image.ImageXData.Height*0.2));
                var MergeSize = new SizeF(this.prc.Image.ImageXData.Width,
                                          (float) (this.prc.Image.ImageXData.Height*0.6));
                var MergeRegion = new RectangleF(MergeTL, MergeSize);
                this.prc.SetArea(MergeRegion);
                this.prc.EnableArea = true;
                this.prc.DocumentDilate(1, EnhancementDirection.Right);
                this.prc.DocumentDespeckle(6, 6);

                var saveOptions = new SaveOptions(); //creates the saveoptions object
                saveOptions.Format = ImageXFormat.Tiff;
                saveOptions.Tiff.Compression = Compression.Group4;
                this.prc.Image.Save(BinarizedFileName, saveOptions); //saves the image
            }
            this.DisposePRC();
        }

        public void CutChqSignature(string fileIn, string fileOut1)
        {
            var saveOptions = new SaveOptions(); //creates the saveoptions object
            saveOptions.Format = ImageXFormat.Pdf;
            saveOptions.Pdf.Compression = Compression.Group4;

            using (this.prc.Image = ImageX.FromFile(this.imagXpress1, fileIn))
            {
                int w = this.prc.Image.Width;
                int h = this.prc.Image.Height;

                var cropRec1 = new Rectangle(Convert.ToInt32(w/3), Convert.ToInt32(h/2), Convert.ToInt32(w),
                                             Convert.ToInt32(h/4));
                this.prc.Crop(cropRec1);
                this.prc.Image.Save(fileOut1, saveOptions); //saves the image             
            }
            this.DisposePRC();
        }

        public IntPtr CutChqCMC7(string fileIn, string fileOut)
        {
            var saveOptions = new SaveOptions(); //creates the saveoptions object
            saveOptions.Format = ImageXFormat.Tiff;
            saveOptions.Pdf.Compression = Compression.Group4;

            IntPtr dibHandle;

            using (this.prc.Image = ImageX.FromFile(this.imagXpress1, fileIn))
            {
                int w = this.prc.Image.Width;
                int h = this.prc.Image.Height;

                var cropRec1 = new Rectangle(Convert.ToInt32(0), Convert.ToInt32(h / 10 * 8), Convert.ToInt32(w / 10 * 8), Convert.ToInt32(h/10*9));
                this.prc.Crop(cropRec1);

                this.prc.Image.Save(fileOut, saveOptions); //saves the image             

                ImageX img = ImageX.FromFile(imagXpress1, fileOut);
                dibHandle = img.ToHdib(true);
                
            }
            this.DisposePRC();

            return dibHandle;
        }

        public void RemoveBarcodeHSBC(List<BarcodeDTO> bcList, string binarizedFileName)
        {
            string fileAux = binarizedFileName;

            foreach (BarcodeDTO bc in bcList)
            {
                if ((bc.Barcode.Length == 14))
                {
                    if (bc.Barcode.Substring(0, 3) == "399")
                    {
                        var saveOptions = new SaveOptions(); //creates the saveoptions object
                        saveOptions.Format = ImageXFormat.Tiff;
                        saveOptions.Tiff.Compression = Compression.Group4;

                        using (this.prc.Image = ImageX.FromFile(this.imagXpress1, fileAux))
                        {
                            int x1 = bc.Rect.X - 10;
                            int y1 = bc.Rect.Y - 10;
                            int x2 = bc.Rect.X + bc.Rect.Width + 10;
                            int y2 = bc.Rect.Y + bc.Rect.Height + 10;

                            this.prc.SetRegion(RectangleF.FromLTRB(x1, y1, x2, y2), RegionType.Rectangle);
                            this.prc.EnableRegion = true;
                            this.prc.ReplaceColors(Color.Black, Color.White, Color.White);

                            this.prc.Image.Save(fileAux, saveOptions); //saves the image
                        }
                        this.DisposePRC();
                    }
                }
            }
        }

        public void Test()
        {
            var di = new DirectoryInfo(@"C:\Simul\FLEXDOC\");

            FileInfo[] files = di.GetFiles();

            foreach (FileInfo file in files)
            {
                if (file.Extension.ToUpper() == ".JPG")
                {
                    //this.FastBinarizeFIN(file.FullName, file.FullName.Replace(".JPG", "_1.TIF"));
                    this.BinarizeFileFIN(file.FullName, file.FullName.Replace(".JPG", "_1.TIF"), 0);
                    //this.BinarizeChqBack(file.FullName, file.FullName.Replace(".JPG", "_3.TIF"));
                }
            }

            Alert.Information("Test:BinFin:Ok");
        }
    }
}