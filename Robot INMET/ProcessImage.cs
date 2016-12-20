using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Accusoft.ImagXpressSdk;

namespace Image
{
    public class ProcessImage
    {
        private Processor processor;
        private readonly ImagXpress imagXpress1;
        
        public enum ImageType
        {
            Jpg,
            Jpeg,
            Bmp,
            Tiff    
        }

        public ProcessImage (string imagemOrigem, string imagemDestino, ImageType imageType, bool deskew)
	    {
            this.imagXpress1 = new Accusoft.ImagXpressSdk.ImagXpress();
            this.imagXpress1.Licensing.UnlockRuntime(1908224503, 373713104, 1341088068, 5785);

            this.imagXpress1.InitializeLifetimeService();

            processor = new Processor(this.imagXpress1)
                            {
                                Image = ImageX.FromFile(this.imagXpress1, imagemOrigem)                                
                            };
            var red = 0;
            var blue = 0;
            var green = 0;
            
            GetHistogram(processor, out red, out blue, out green);

            var cores = Path.GetFileNameWithoutExtension(imagemOrigem) + ";" + red + ";" + blue + ";" + green;

            System.IO.File.AppendAllText(Path.GetDirectoryName(imagemOrigem)  + "\\files.txt", "\r\n" + cores);

            if (deskew)
                   processor.Deskew(DeskewType.PreserveCrop);

            if ((red > 1700000 & red < 2100000) & blue < 2100000)
                processor.Binarize(BinarizeMode.QuickText, 179, 190, 0, 1, 0, 210, BinarizeBlur.NoBlur);
            else if ((red > 1700000 & red < 2100000) & blue > 2120000)
                processor.Binarize(BinarizeMode.QuickText, 160, 180, 0, 1, 0, 215, BinarizeBlur.NoBlur);
            else if (red > 1400000 & red < 2100000)
                processor.Binarize(BinarizeMode.QuickText, 150, 180, 0, 1, 0, 210, BinarizeBlur.NoBlur);
            else if (red > 2100000)
                processor.Binarize(BinarizeMode.QuickText, 170, 185, 0, 1, 0, 215, BinarizeBlur.NoBlur);
            else
            {
                processor.Binarize(BinarizeMode.QuickText, 165, 180, 0, 1, 0, 180, BinarizeBlur.NoBlur);
            }
            SalvarImagemProcessada(processor, imagemDestino, imageType);
	    }

        private void SalvarImagemProcessada(Processor processadorImagem, string imageDestino, ImageType imageType)
        {
            var dir = new DirectoryInfo(Path.GetDirectoryName(imageDestino));
            if (!dir.Exists)
            {
                dir.Create();
            }
                    
            processadorImagem.Image.Save(imageDestino, SaveImageOptions(imageType));
        }

        public static SaveOptions SaveImageOptions(ImageType imageType)
        {
            var saveOptions = new SaveOptions();
            
            switch (imageType)
            {
                case ImageType.Tiff:
                    {
                        saveOptions.Format = ImageXFormat.Tiff;
                        saveOptions.Tiff.Compression = Compression.NoCompression;
                        break;
                    }
                case ImageType.Jpg:
                    {
                        saveOptions.Format = ImageXFormat.Jpeg;
                        saveOptions.Jpeg.Grayscale = true;
                        break;
                    }
                default:
                    {
                        saveOptions.Format = ImageXFormat.Tiff;
                        saveOptions.Tiff.Compression = Compression.NoCompression;
                        saveOptions.Tiff.MultiPage = false;
                        break;
                    }
            }
         
            return saveOptions;
        }

        private void GetHistogram(Processor processor, out int red, out int blue, out int green)
        {
            int[] redCounts;
            int[] greenCounts;
            int[] blueCounts;

            red = 0;
            blue = 0;
            green = 0;

            processor.RGBColorCount(out redCounts, out greenCounts, out blueCounts);

            var numeroMax = redCounts[0];
            foreach (var t in redCounts)
            {
                if (t > numeroMax)
                {
                    red = numeroMax = t;
                }
            }

            System.Diagnostics.Debug.Print("Red::" + numeroMax);

            numeroMax = blueCounts[0];
            foreach (var t in blueCounts)
            {
                if (t > numeroMax)
                {
                    blue = numeroMax = t;
                }
            }
            
            System.Diagnostics.Debug.Print("Blue::" + numeroMax);

            numeroMax = greenCounts[0];
            foreach (var t in greenCounts)
            {
                if (t > numeroMax)
                {
                    green = numeroMax = t;
                }
            }

            System.Diagnostics.Debug.Print("Green::" + numeroMax);           
            
        }
    }
}
