// ============================================
// 
// FlexImage.Binarize
// BinarizeNF.cs
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
    public class BinarizeNF
    {
        private readonly ImagXpress _imagXpress1;
        private readonly ImageXView _imageXView1;
        private readonly Log _log = GenericSingleton<Log>.GetInstance();
        private readonly Processor _prc;
        private readonly WSBasicTable _wsBasicTable = GenericSingleton<WSBasicTable>.GetInstance();

        private BinarizeBlur bBlur = BinarizeBlur.NoBlur;
        private BinarizeMode bMode = BinarizeMode.QuickText;

        public BinarizeNF()
        {
            this._imagXpress1 = new Accusoft.ImagXpressSdk.ImagXpress();
            this._imagXpress1.Licensing.UnlockRuntime(1908224503, 373713104, 1341088068, 5785);

            this._imagXpress1.InitializeLifetimeService();

            this._imageXView1 = new ImageXView();
            this._imageXView1.AutoImageDispose = true;

            this._imageXView1.Workspace = this._imagXpress1;
            this._imageXView1.InitializeLifetimeService();

            this._prc = new Processor(this._imagXpress1);
        }

        public void Dispose()
        {
            try
            {
                if (this._prc != null)
                    this._prc.Dispose();

                if (this._imageXView1 != null)
                    this._imageXView1.Dispose();

                if (this._imagXpress1 != null)
                    this._imagXpress1.Dispose();
            }
            catch (Exception e)
            {
                this._log.Error("Binarize: Dispose:" + e);
            }
        }

        public string DeskewJpg(long docId, string originalFileName)
        {
            long id = 1;

            string destPath = Files.GetFormattedPath(docId.ToString());
            string newPath = Path.Combine(Path.Combine(Files.GetWorkDir(), "WORK\\"), destPath);
            Directory.CreateDirectory(newPath);
            string newFilename = newPath + docId + "F.JPG";

            if (File.Exists(originalFileName))
            {
                using (this._prc.Image = ImageX.FromFile(this._imagXpress1, originalFileName))
                {
                    this._prc.EnableArea = false;
                    this._prc.Deskew();
                    

                    var saveOptions = new SaveOptions(); //creates the saveoptions object
                    saveOptions.Format = ImageXFormat.Jpeg; //tells IX to save to the jpeg image format
                    this._prc.Image.Save(newFilename, saveOptions); //saves the image
                }
                this.DisposePrc();
            }
            else
            {
                newFilename = "";
                this._log.Error("DeskewJpg::File doesnt exist");
            }

            return newFilename;

        }
        
        public void BinarizeProvas(string originalFileName, string binarizedFileName)
        {
            if (File.Exists(originalFileName))
            {
                using (this._prc.Image = ImageX.FromFile(this._imagXpress1, originalFileName))
                {
                    this._prc.EnableArea = false;

                    Color fromColor = System.Drawing.Color.FromArgb(252,216,219);
                    Color toColor = System.Drawing.Color.FromArgb(208,15,30);

                    Color replacementColor = Color.White;
                    this._prc.ReplaceColors(toColor, fromColor, replacementColor);

                    //this._prc.AdjustColorBalance(0, 100, 100, -100);

                    var saveOptions = new SaveOptions(); //creates the saveoptions object
                    saveOptions.Format = ImageXFormat.Jpeg; //tells IX to save to the jpeg image format
                    
                    double MinimumAngle = 5;
                    short MinimumConfidence = 75;
                    short Quality = 10;

                    this._prc.AutoBinarize();

                    this._prc.DocumentBorderCrop();
                    this._prc.Despeckle();
                    
                    saveOptions.Format = ImageXFormat.Tiff; //tells IX to save to the jpeg image format
                    saveOptions.Tiff.Compression = Compression.Group4;
                    this._prc.Image.Save(binarizedFileName, saveOptions); //saves the image

                }
                this.DisposePrc();
            }
            else
                this._log.Error("BinarizeProvas::File doesnt exist");
        }

        public string CleanUp(string filename)
        {
            string newFileName = "";
            using (this._prc.Image = ImageX.FromFile(this._imagXpress1, filename))
            {
                double MinimumAngle = 1;
                short MinimumConfidence = 30;
                short Quality = 100;

                this._prc.DocumentDeskew(MinimumAngle, MinimumConfidence, Color.White, false, Quality);

                this._prc.DocumentLineRemoval(100, 5, 15, 5, 14);
                this._prc.DocumentLineRemoval(100, 2, 10, 5, 14);

                this._prc.Deskew();

                var saveOptions = new SaveOptions(); //creates the saveoptions object
                saveOptions.Format = ImageXFormat.Tiff;
                saveOptions.Tiff.Compression = Compression.Group4;

                newFileName = filename.Replace(".TIF", "_CLEANED.TIF");
                this._prc.Image.Save(newFileName, saveOptions); //saves the image
            }

            this.DisposePrc();
            return newFileName;
        }

        public string CutEdges(string filename, SaveOptions opt)
        {
            this._log.Debug("CutEdges::" + filename);

            string newFileName = filename.ToUpper().Replace(".JPG", "_CUT.JPG");

            try
            {

                using (this._prc.Image = ImageX.FromFile(this._imagXpress1, filename))
                {
                    var p = new Point(0, 0);
                    float percentCrop = 0.90F;
                    this._prc.CropBorder(percentCrop, CropType.CropBorderBlackColor);
                    this._prc.Deskew();

                    this._prc.Image.Save(newFileName, opt); //saves the image
                }

            }
            catch (Exception e)
            {                
                System.IO.File.Copy(filename, newFileName);
            }

            this.DisposePrc();

            return newFileName;
        }

        private string PreProcess(string originalFileName)
        {
            string preProcessFilename = "";
            preProcessFilename = originalFileName.ToUpper().Replace(".JPG", "_PRE.JPG");

            using (this._prc.Image = ImageX.FromFile(this._imagXpress1, originalFileName))
            {
                this._prc.EnableArea = false;

                var cropRectangle = new Rectangle(0, 0, this._prc.Image.Width, this._prc.Image.Height);

                this._prc.CropBorder((float) 0.9, CropType.CropBorderBlackColor);

                var saveOptions = new SaveOptions(); //creates the saveoptions object
                saveOptions.Format = ImageXFormat.Jpeg; //tells IX to save to the jpeg image format
                this._prc.Image.Save(preProcessFilename, saveOptions); //saves the image
            }
            this.DisposePrc();

            return preProcessFilename;
        }

        public int BinarizeFile(string originalFileName, string binarizedFileName, int rotate180)
        {
            this._log.Debug("BinarizeFile:" + originalFileName);

            try
            {
                int lowThreshold = 145;
                int highThreshold = 154;

                if (binarizedFileName == "")
                    binarizedFileName = originalFileName.ToUpper().Replace(".JPG", ".TIF");

                var oSt = new Statistics();
                oSt.calcHisto(originalFileName);

                lowThreshold = (int) (oSt.Average - oSt.StdDev*1.2);
                highThreshold = (int) (oSt.Average + oSt.StdDev*1.2);

                if (lowThreshold < 0)
                    lowThreshold = 0;
                if (highThreshold > 255)
                    highThreshold = 255;
                if (highThreshold < lowThreshold)
                    highThreshold = lowThreshold;

                this.ProcessFile(originalFileName, binarizedFileName, lowThreshold, highThreshold);
            }
            catch (Exception e)
            {
                this._log.Error("Binarize:BinarizeFile:" + e);
                this._log.Debug("Trying to FastBinarize");
                this.FastBinarize(originalFileName, binarizedFileName);
            }

            return 0;
        }

        public void ProcessFile(string originalFileName, string binarizedFileName, int hsLowThresh, int hsHighThresh)
        {
            using (this._prc.Image = ImageX.FromFile(this._imagXpress1, originalFileName))
            {
                this._prc.EnableArea = false;

                this._prc.Binarize(this.bMode, hsLowThresh, hsHighThresh, 0, 3, 48, 50, this.bBlur);

                this._prc.DocumentDespeckle(4, 4);

                this._prc.Deskew();

                this._prc.Image.UndoEnabled = true;
                int height1 = this._prc.Image.Height;
                this._prc.DocumentBorderCrop();
                int height2 = this._prc.Image.Height;
                if (height2 < 560)
                    this._prc.Image.Undo();

                var saveOptions = new SaveOptions(); //creates the saveoptions object
                saveOptions.Format = ImageXFormat.Tiff; //tells IX to save to the jpeg image format
                saveOptions.Tiff.Compression = Compression.Group4;
                this._prc.Image.Save(binarizedFileName, saveOptions); //saves the image
            }
            this.DisposePrc();
        }

        public void FastBinarize(string originalFileName, string binarizedFileName)
        {
            if (File.Exists(originalFileName))
            {
                using (this._prc.Image = ImageX.FromFile(this._imagXpress1, originalFileName))
                {
                    this._prc.EnableArea = false;
                    this._prc.AutoBinarize();

                    int low = this._prc.BinarizeLowThreshold;
                    int high = this._prc.BinarizeHighThreshold;

                    //Verifica se imagem ficou muito Clara (apenas imagens Frente)
                    //Vide imagens de Fax
                    if (originalFileName.Contains("F.JPG"))
                    {
                        if ((high - low) < 10)
                        {
                            if (low > 160)
                            {
                                this._log.Debug(">>>> Rebinarizing... - High/Low=" + high.ToString() + "/" +
                                                low.ToString());
                                this.BinarizeFile(originalFileName, binarizedFileName, 0);
                                this.DisposePrc();
                                return;
                            }
                        }
                    }

                    if (this._wsBasicTable.GetParameters("BINARIZE_DESPECKLE", "1") == "1")
                        this._prc.DocumentDespeckle(2, 2);

                    double MinimumAngle = 5;
                    short MinimumConfidence = 75;
                    short Quality = 10;


                    if (this._wsBasicTable.GetParameters("BINARIZE_DESKEW", "1") == "1")
                        this._prc.DocumentDeskew(MinimumAngle, MinimumConfidence, Color.White, false, Quality);

                    if (this._wsBasicTable.GetParameters("BINARIZE_BORDERCROP", "0") == "1")
                        this._prc.DocumentBorderCrop();

                    if (this._wsBasicTable.GetParameters("BINARIZE_BLOBREMOVAL", "1") == "1")
                    {
                        var area = new Rectangle(0, 0, this._prc.Image.Width, this._prc.Image.Height);
                        this._prc.DocumentBlobRemoval(area, 1300, 1800, 93);
                    }

                    var saveOptions = new SaveOptions(); //creates the saveoptions object
                    saveOptions.Format = ImageXFormat.Tiff; //tells IX to save to the jpeg image format
                    saveOptions.Tiff.Compression = Compression.Group4;
                    this._prc.Image.Save(binarizedFileName, saveOptions); //saves the image
                }
                this.DisposePrc();
            }
            else
                this._log.Error("FastBinarize::File doesnt exist::" + originalFileName);
        }
             
        public void FastBinarize2(string originalFileName, string binarizedFileName)
        {
            if (File.Exists(originalFileName))
            {
                using (this._prc.Image = ImageX.FromFile(this._imagXpress1, originalFileName))
                {
                    this._prc.EnableArea = false;
                    this._prc.AutoBinarize2();

                    int low = this._prc.BinarizeLowThreshold;
                    int high = this._prc.BinarizeHighThreshold;

                    this._prc.DocumentDespeckle(2, 2);

                    double MinimumAngle = 5;
                    short MinimumConfidence = 75;
                    short Quality = 10;

                    this._prc.DocumentDeskew(MinimumAngle, MinimumConfidence, Color.White, false, Quality);
                    this._prc.DocumentBorderCrop();

                    var saveOptions = new SaveOptions(); //creates the saveoptions object
                    saveOptions.Format = ImageXFormat.Tiff; //tells IX to save to the jpeg image format
                    saveOptions.Tiff.Compression = Compression.Group4;
                    this._prc.Image.Save(binarizedFileName, saveOptions); //saves the image
                }
                this.DisposePrc();
            }
            else
                this._log.Error("FastBinarize::File doesnt exist");
        }

        private void DisposePrc()
        {
            // Dispose the ImageX object if the ImageXView has one
            try
            {
                if (this._prc.Image != null)
                {
                    this._prc.Image.Dispose();
                    this._prc.Image = null;
                }
            }
            catch (Exception e)
            {
                this._log.Error("DisposePRC:" + e);
            }
        }

        public string GenerateThumbnail(string originalFileName)
        {
            string thumbFilename = "";
            using (this._prc.Image = ImageX.FromFile(this._imagXpress1, originalFileName))
            {
                var newSize = new Size();

                double w = this._prc.Image.ImageXData.Width;
                double h = this._prc.Image.ImageXData.Height;

                int fator = 10;

                newSize.Width = Convert.ToInt32(this._prc.Image.ImageXData.Width/fator);
                newSize.Height = Convert.ToInt32(this._prc.Image.ImageXData.Height/fator);
                this._prc.Resize(newSize, ResizeType.Quality, true, true);

                var saveOptions = new SaveOptions(); //creates the saveoptions object
                saveOptions.Format = ImageXFormat.Jpeg; //tells IX to save to the jpeg image format
                //saveOptions.Jpeg.Grayscale = false;
                //saveOptions.Jpeg.Luminance = 24;
                //saveOptions.Jpeg.Chrominance = 30;

                thumbFilename = originalFileName.ToUpper().Replace("F.JPG", "T.JPG");
                this._prc.Image.Save(thumbFilename, saveOptions); //saves the image                

                this._log.Debug("Thumbnail Generated: " + thumbFilename);
            }
            this.DisposePrc();

            return thumbFilename;
        }

        public void RotateFile(string binarizedFileName, int degrees)
        {
            using (this._prc.Image = ImageX.FromFile(this._imagXpress1, binarizedFileName))
            {
                this._prc.Rotate(degrees);
                var saveOptions = new SaveOptions(); //creates the saveoptions object
                saveOptions.Format = ImageXFormat.Tiff;
                saveOptions.Tiff.Compression = Compression.Group4;
                this._prc.Image.Save(binarizedFileName, saveOptions); //saves the image
            }
            this.DisposePrc();
        }

        public string CheckRotation(string originalFileName)
        {
            string newFileName = "";

            using (this._prc.Image = ImageX.FromFile(this._imagXpress1, originalFileName))
            {
                if (this._prc.Image.Width >= 1300)
                {
                    newFileName = originalFileName.ToUpper().Replace(".JPG", "_ROT.JPG");
                    this._prc.Rotate(90);
                    var saveOptions = new SaveOptions(); //creates the saveoptions object
                    saveOptions.Format = ImageXFormat.Jpeg;
                    saveOptions.Tiff.Compression = Compression.Jpeg;
                    this._prc.Image.Save(newFileName, saveOptions); //saves the image
                }
            }
            this.DisposePrc();

            return newFileName;
        }


        public bool ProcessMultiPagePDF_TIF(List<QueueDTO> objList, string pdfFile)
        {
            this._log.Debug("Starting ProcessMultiPage: " + pdfFile);

            //Gera um único PDF
            var soOpts = new SaveOptions();
            soOpts.Format = ImageXFormat.Pdf;
            soOpts.Pdf.Compression = Compression.Group4;
            soOpts.Pdf.MultiPage = true;

            string filename = "";
            foreach (QueueDTO item in objList)
            {
                filename = Path.Combine(Path.GetDirectoryName(pdfFile),
                                        Path.GetFileName(item.FileName.Replace(".JPG", ".TIF")));

                this._log.Debug("ProcessMultiPage: " + filename);

                using (this._imageXView1.Image = ImageX.FromFile(this._imagXpress1, filename))
                {
                    this._imageXView1.Image.Page = 1;
                    this._imageXView1.Image.Save(pdfFile, soOpts);
                }
                this.DisposePrc();
            }

            soOpts = null;

            this._log.Debug("Binarize::ProcessMultiPage: Processed ok : " + pdfFile);
            return true;
        }

        public bool ProcessMultiPagePDF_JPG(List<QueueDTO> objList, string pdfFile)
        {
            this._log.Debug("Starting ProcessMultiPage PDF_JPG: " + pdfFile);

            //Gera um único PDF
            var soOpts = new SaveOptions();
            soOpts.Format = ImageXFormat.Pdf;
            soOpts.Pdf.Compression = Compression.Jpeg;
            soOpts.Pdf.MultiPage = true;

            string filename = "";
            foreach (QueueDTO item in objList)
            {
                filename = Path.Combine(Path.GetDirectoryName(pdfFile), Path.GetFileName(item.FileName));

                this._log.Debug("ProcessMultiPage PDF_JPG: " + filename);

                using (this._imageXView1.Image = ImageX.FromFile(this._imagXpress1, filename))
                {
                    this._imageXView1.Image.Page = 1;
                    this._imageXView1.Image.Save(pdfFile, soOpts);
                }
                this.DisposePrc();
            }

            soOpts = null;

            this._log.Debug("Binarize::ProcessMultiPage PDF_JPG: Processed ok : " + pdfFile);
            return true;
        }


        public string CreatePdFfromTif(string dir)
        {
            string pdfFile = dir + "\\RESULT.PDF";
            this._log.Debug("Starting CreatePDFfromTIF: " + pdfFile);

            try
            {
                File.Delete(pdfFile);
            }
            catch (Exception e)
            {
                this._log.Error("Process CreatePDFfromTIF:: " + e);
            }

            //Gera um único PDF
            var soOpts = new SaveOptions();
            soOpts.Format = ImageXFormat.Pdf;
            soOpts.Pdf.Compression = Compression.Group4;
            soOpts.Pdf.MultiPage = true;

            // Builds an array of all the files
            var di = new DirectoryInfo(dir);
            FileInfo[] files = di.GetFiles("*.TIF");

            // Sorts the FileInfo[] array
            Array.Sort(files, new CustomCompare());

            foreach (FileInfo filename in files)
            {
                this._log.Debug("CreatePDFfromTIF: " + filename);

                if (filename.Length > 3*1024) //3Kb
                {
                    using (this._imageXView1.Image = ImageX.FromFile(this._imagXpress1, filename.FullName))
                    {
                        this._imageXView1.Image.Page = 1;
                        this._imageXView1.Image.Save(pdfFile, soOpts);
                    }
                    this.DisposePrc();
                }
                else
                    this._log.Info("Binarize::CreatePDFFromTIF: BlankPage: " + filename);
            }

            return pdfFile;
        }

        private void DoSplit(string fileIn, string fileOut1, string fileOut2, bool isFront)
        {
            var saveOptions = new SaveOptions(); //creates the saveoptions object
            saveOptions.Format = ImageXFormat.Jpeg;
            saveOptions.Jpeg.Grayscale = false;

            using (this._prc.Image = ImageX.FromFile(this._imagXpress1, fileIn))
            {
                this._prc.Rotate(90);
                var cropRec1 = new Rectangle(0, 0, this._prc.Image.Width/2, this._prc.Image.Height);
                this._prc.Crop(cropRec1);
                if (isFront)
                    this._prc.Rotate(180);
                this._prc.Image.Save(fileOut1, saveOptions); //saves the image
            }
            this.DisposePrc();

            using (this._prc.Image = ImageX.FromFile(this._imagXpress1, fileIn))
            {
                this._prc.Rotate(90);
                var cropRec2 = new Rectangle(this._prc.Image.Width/2, 0, this._prc.Image.Width/2, this._prc.Image.Height);
                this._prc.Crop(cropRec2);
                if (isFront)
                    this._prc.Rotate(180);
                this._prc.Image.Save(fileOut2, saveOptions); //saves the image
            }
            this.DisposePrc();
        }

        public bool ProcessBooklet(List<QueueDTO> objList, string pdfFileName, string tagWorkflow, string scanTags)
        {
            bool ret = false;

            if (scanTags.Contains("FLATBED"))
                ret = this.ProcessBookletSimplex(objList, pdfFileName, tagWorkflow);
            else
                ret = this.ProcessBookletDuplex(objList, pdfFileName, tagWorkflow);

            return ret;
        }

        public bool ProcessBookletSimplex(List<QueueDTO> objList, string pdfFileName, string tagWorkflow)
        {
            if (objList.Count == 0)
                return false;

            string initDir = "";
            string fileOut1 = "";
            string fileOut2 = "";

            this._log.Debug("ProcessBookletSimplex:" + tagWorkflow);

            int rotate = 0;
            string ext = Path.GetExtension(objList[0].FileName);
            var filesIn = new string[objList.Count];
            int i = 0;
            int tipo = 0;

            var opt = new SaveOptions();
            if (ext == ".JPG")
            {
                opt.Format = ImageXFormat.Jpeg;
                tipo = 0;
            }
            else
            {
                opt.Format = ImageXFormat.Tiff;
                opt.Tiff.Compression = Compression.Group4;
                tipo = 1;
            }

            foreach (QueueDTO item in objList)
            {
                filesIn[i] = Path.Combine(Path.GetDirectoryName(pdfFileName), Path.GetFileName(item.FileName));
                if (tipo == 0)
                    this.CutEdges(filesIn[i], opt);
                i++;
            }

            initDir = Path.GetDirectoryName(filesIn[0]);
            string[] files;
            if (tipo == 0)
                files = Directory.GetFiles(initDir, "*_CUT" + ext, SearchOption.TopDirectoryOnly);
            else
                files = Directory.GetFiles(initDir, "*" + ext, SearchOption.TopDirectoryOnly);

            try
            {
                if (File.Exists(pdfFileName))
                    File.Delete(pdfFileName);
            }
            catch (Exception e)
            {
                this._log.Error("ProcessBookSimplex:" + e);
            }

            if (!this.JoinFiles(files, pdfFileName, tipo, true))
                return false;

            return true;
        }

        public bool ProcessBookletDuplex(List<QueueDTO> objList, string pdfFileName, string tagWorkflow)
        {
            string initDir = "";
            string fileOut1 = "";
            string fileOut2 = "";

            this._log.Debug("ProcessBooklet:" + tagWorkflow);

            var filesIn = new string[999];
            int i = 0;

            foreach (QueueDTO item in objList)
            {
                filesIn[i] = Path.Combine(Path.GetDirectoryName(pdfFileName), Path.GetFileName(item.FileName));
                i++;
            }

            initDir = Path.GetDirectoryName(filesIn[0]) + "\\SPLIT\\";

            try
            {
                //System.IO.Directory.Delete(initDir,true);
            }
            catch
            {
            }

            Directory.CreateDirectory(initDir);

            int totPages = i*2;

            int f1 = 1;
            int f2 = totPages;

            bool isFront = true;

            for (int j = 0; j < i; j++)
            {
                string fileaux = initDir + Path.GetFileNameWithoutExtension(filesIn[j]);

                fileOut1 = initDir + f1.ToString("0000") + Path.GetExtension(filesIn[j]);
                fileOut2 = initDir + f2.ToString("0000") + Path.GetExtension(filesIn[j]);

                this.DoSplit(filesIn[j], fileOut1, fileOut2, isFront);

                isFront = (!isFront);
                f1 = f1 + 1;
                f2 = f2 - 1;

                Console.WriteLine("Splitting:" + filesIn[j]);
            }

            string[] files = Directory.GetFiles(initDir, "*.JPG", SearchOption.TopDirectoryOnly);

            if (!this.JoinFiles(files, pdfFileName, 0, false))
                return false;

            return true;
        }

        public bool JoinFiles(string[] list, string pdfFileName, int format, bool cutEdges)
        {
            //Gera um único PDF com as páginas não-excluídas

            var soOpts = new SaveOptions();
            soOpts.Format = ImageXFormat.Pdf;

            if (format == 0)
            {
                soOpts.Pdf.Compression = Compression.Jpeg;
                soOpts.Jpeg.Chrominance = 40;
                soOpts.Jpeg.Luminance = 90;
                soOpts.Jpeg.SubSampling = SubSampling.SubSampling411;
                soOpts.Pdf.MultiPage = true;
            }
            else
            {
                soOpts.Pdf.Compression = Compression.Group4;
                soOpts.Tiff.Compression = Compression.Group4;
                soOpts.Pdf.MultiPage = true;
            }

            try
            {
                File.Delete(pdfFileName);
            }
            catch
            {
            }

            foreach (string file in list)
            {
                using (this._imageXView1.Image = ImageX.FromFile(this._imagXpress1, file))
                {
                    this._imageXView1.Image.Page = 1;
                    this._imageXView1.Image.Save(pdfFileName, soOpts);
                }
                this.DisposePrc();

                Console.WriteLine("Joinning PDF: " + file);
            }
            return true;
        }

        public void BinarizeInmet(string originalFileName, string binarizedFileName)
        {
            this._log.Debug("BinarizeINMET:" + originalFileName);

            if (File.Exists(originalFileName))
            {
                using (this._prc.Image = ImageX.FromFile(this._imagXpress1, originalFileName))
                {
                    this._prc.EnableArea = false;

                    this._prc.AutoBinarize();

                    //int i = this._prc.Image.ModcaTags.Count;
                    
                    var saveOptions = new SaveOptions(); //creates the saveoptions object
                    
                    
                    saveOptions.Format = ImageXFormat.Tiff; //tells IX to save to the jpeg image format
                    saveOptions.Tiff.Compression = Compression.Group4;
                    
                    this._prc.Image.Save(binarizedFileName, saveOptions); //saves the image
                }
                this.DisposePrc();
            }
            else
                this._log.Error("FastBinarizeINMET::File doesnt exist");
        }



        #region Nested type: CustomCompare

        private class CustomCompare : IComparer<FileInfo>
        {
            #region IComparer<FileInfo> Members

            public int Compare(FileInfo x, FileInfo y)
            {
                return String.CompareOrdinal(x.Name, y.Name);
            }

            #endregion
        }

        #endregion
    }
}