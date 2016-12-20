// ============================================
// 
// FlexImage.Binarize
// Statistics.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

#endregion

namespace FlexImage.Binarize
{
    public class Statistics
    {
        private double average;
        private double[] sortedData;
        private double stdDev;

        public double Average
        {
            get { return this.average; }
            set { this.average = value; }
        }

        public double Variance { get; set; }

        public double StdDev
        {
            get { return this.stdDev; }
            set { this.stdDev = value; }
        }

        public long Width { get; set; }

        public long Height { get; set; }

        public void calcHisto(string fileName)
        {
            Byte[] buffer = File.ReadAllBytes(fileName);
            var memStream = new MemoryStream(buffer);
            var bmpimg = (Bitmap) Image.FromStream(memStream);

            BitmapData data = bmpimg.LockBits(new Rectangle(0, 0, bmpimg.Width, bmpimg.Height), ImageLockMode.ReadOnly,
                                              PixelFormat.Format24bppRgb);

            int tam = data.Width*data.Height;
            var x = new double[tam];
            long cont = 0;

            unsafe
            {
                var ptr = (byte*) data.Scan0;

                int remain = data.Stride - data.Width*3;

                var histogram = new int[256];
                for (int i = 0; i < histogram.Length; i++)
                    histogram[i] = 0;

                for (int i = 0; i < data.Height; i++)
                {
                    for (int j = 0; j < data.Width; j++)
                    {
                        int mean = ptr[0] + ptr[1] + ptr[2];
                        mean /= 3;

                        histogram[mean]++;
                        ptr += 3;

                        x[cont] = mean;
                        cont++;
                    }

                    ptr += remain;
                }

                //drawHistogram(histogram, form);
            }

            this.average = this.GetAverage(x);
            //variance = GetVariance(x);
            this.stdDev = this.GetStdev(x);

            // calculate quartiles
            this.sortedData = new double[x.Length];
            x.CopyTo(this.sortedData, 0);
            Array.Sort(this.sortedData);

            x = null;
            buffer = null;
            memStream = null;
            bmpimg.UnlockBits(data);
            bmpimg.Dispose();
        }


        //public void drawHistogram(int[] histogram, Form1 form)
        //{

        //    Bitmap bmp = new Bitmap(histogram.Length + 10, 310);
        //    form.pictureBox2.Image = bmp;
        //    int keep = 0;

        //    BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

        //    unsafe
        //    {
        //        int remain = data.Stride - data.Width * 3;
        //        byte* ptr = (byte*)data.Scan0;

        //        for (int i = 0; i < data.Height; i++)
        //        {

        //            for (int j = 0; j < data.Width; j++)
        //            {
        //                ptr[0] = ptr[1] = ptr[2] = 150;
        //                ptr += 3;
        //            }
        //            ptr += remain;
        //        }

        //        int max = 0;
        //        for (int i = 0; i < histogram.Length; i++)
        //        {
        //            if (max < histogram[i])
        //                max = histogram[i];
        //        }

        //        for (int i = 0; i < histogram.Length; i++)
        //        {
        //            if (i == (int)average)
        //                continue;

        //            ptr = (byte*)data.Scan0;
        //            ptr += data.Stride * (305) + (i + 5) * 3;

        //            int length = (int)(1.0 * histogram[i] * 300 / max);

        //            for (int j = 0; j < length; j++)
        //            {
        //                ptr[0] = 255;
        //                ptr[1] = ptr[2] = 0;
        //                ptr -= data.Stride;
        //            }

        //        }

        //    }

        //    bmp.UnlockBits(data);
        //}

        //==========================================================================================================


        public double GetAverage(double[] data)
        {
            int len = data.Length;

            if (len == 0)
                throw new Exception("No data");

            double sum = 0;

            for (int i = 0; i < data.Length; i++)
                sum += data[i];

            return sum/len;
        }


        /// <summary>
        ///   Get variance
        /// </summary>
        public double GetVariance(double[] data)
        {
            int len = data.Length;

            // Get average
            double avg = this.GetAverage(data);

            double sum = 0;

            for (int i = 0; i < data.Length; i++)
                sum += Math.Pow((data[i] - avg), 2);

            return sum/len;
        }

        /// <summary>
        ///   Get standard deviation
        /// </summary>
        public double GetStdev(double[] data)
        {
            return Math.Sqrt(this.GetVariance(data));
        }

        /// <summary>
        ///   Get correlation
        /// </summary>
        public void GetCorrelation(double[] x, double[] y, ref double covXY, ref double pearson)
        {
            if (x.Length != y.Length)
                throw new Exception("Length of sources is different");

            double avgX = this.GetAverage(x);
            double stdevX = this.GetStdev(x);
            double avgY = this.GetAverage(y);
            double stdevY = this.GetStdev(y);

            int len = x.Length;

            for (int i = 0; i < len; i++)
                covXY += (x[i] - avgX)*(y[i] - avgY);

            covXY /= len;

            pearson = covXY/(stdevX*stdevY);
        }

        //=========================================================================================================

        /// <summary>
        ///   Calculate percentile of a sorted data set
        /// </summary>
        /// <param name="p"> percentile, value 0-100 </param>
        /// <returns> </returns>
        internal double percentile(double p)
        {
            // algo derived from Aczel pg 15 bottom
            if (p >= 100.0d)
                return this.sortedData[this.sortedData.Length - 1];

            double position = (this.sortedData.Length + 1)*p/100.0;
            double leftNumber = 0.0d, rightNumber = 0.0d;

            double n = p/100.0d*(this.sortedData.Length - 1) + 1.0d;

            if (position >= 1)
            {
                leftNumber = this.sortedData[(int) Math.Floor(n) - 1];
                rightNumber = this.sortedData[(int) Math.Floor(n)];
            }
            else
            {
                leftNumber = this.sortedData[0]; // first data
                rightNumber = this.sortedData[1]; // first data
            }

            if (leftNumber == rightNumber)
                return leftNumber;
            else
            {
                double part = n - Math.Floor(n);
                return leftNumber + part*(rightNumber - leftNumber);
            }
        }

        // end of internal function percentile
    }
}