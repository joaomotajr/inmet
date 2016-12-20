// ============================================
// 
// FlexImage.OCR
// ABBYY.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.IO;
using System.Runtime.InteropServices;
using FCEngine;
using FlexImage.Core;
using System.Collections;
using System.Collections.Generic;
using FlexImage.WebService;
using System.Drawing;
using System.Linq;

#endregion

namespace FlexImage.OCR
{
    public class ABBYY
    {
        private  IEngine engine;
        private readonly Log log = GenericSingleton<Log>.GetInstance();
        private readonly IFlexiCaptureProcessor processor;
        private IFlexiCaptureProcessor processorCfl;

        private readonly WSBasicTable _wsBasicTable = GenericSingleton<WSBasicTable>.GetInstance();
        private readonly WsProcessingNf _wsCancelNF = GenericSingleton<WsProcessingNf>.GetInstance();
        
        [DllImport(FceConfig.DllPath, CharSet = CharSet.Unicode), PreserveSig]
        private static extern int InitializeEngine(string devSN, string reserved1, string reserved2, out IEngine engine);

        [DllImport(FceConfig.DllPath, CharSet = CharSet.Unicode), PreserveSig]
        private static extern int DeinitializeEngine();

        public ABBYY(string SN, string templateDir, string[] templates)
        {
            try
            {
                this.log.Debug("Loading FlexiCapture Engine...");

                this.engine = this.loadEngine(SN);

                this.log.Debug("Creating and configuring the FlexiCapture Processor");
                this.processor = this.engine.CreateFlexiCaptureProcessor();                

                foreach (string templateName in templates)
                    this.AddToTemplateList(templateDir + templateName);
            }
            catch (Exception e)
            {
                this.log.Error("ABBYY Constructor Error: " + e.Message);
             
            }
        }

        public IEngine loadEngine(string SN)
        {
            IEngine engine =null;

            if (!System.IO.File.Exists(FceConfig.DllPath))
            {
                Alert.Exclamation("FceConfig.DllPath: [" + FceConfig.DllPath + "] not found" );
                return engine;
            }

            try
            {
                this.log.Debug("LoadEngine:Begin");
                int hResult = InitializeEngine(SN, null, null, out engine);
                Marshal.ThrowExceptionForHR(hResult);
                this.log.Debug("LoadEngine:End");
            }
            catch (Exception e)
            {
                Alert.Exclamation("ABBYY Error LoadEngine: " + e.ToString());
            }
            
            return engine;
        }
                      


        public string ProcessImage(string fileName, string exportType, ref string fileNamePreProcessed, string alias, string startupPath, bool isCapa, bool binarize, long batchnfid)
        {
            string pathImage = "";
            string exportFileName = "";

            this.log.Debug("ProcessImage:: " + fileName);

            try
            {               
                
                processor.SetUseFirstMatchedDocumentDefinition(true);

                //-------------------------------------------------------------------

                var x = engine.CreateImageProcessingTools();

                var image = x.OpenImageFile(fileName).OpenImagePage(0);
                
                IImage image90 = null;
                IImage image3 = null;

                var language = engine.PredefinedLanguages.FindLanguage("PortugueseBrazilian");
                             
                if(isCapa)
                {
                    Size size = Files.GetDimensions(fileName);

                    //Cadernetas Horizontais
                    if (alias.Contains("922") || alias.Contains("923"))
                    {
                        if (size.Width < size.Height)
                        {
                            fileName = RotateAndSaveImage(true, fileName);
                            this.log.Debug("Documento rotacionado: " + fileName);
                        }
                    }

                    this.log.Debug("BeforeAddImage:4");

                    //image3 = x.OpenImageFile(fileName).OpenImagePage(0);

                    if (binarize)
                    {
                        var imageBin = x.Binarize(image, BinarizationModeEnum.BM_Version9, false, false);
                        processor.AddImage(imageBin);
                    }
                    else
                    {
                        //Documento Capa add a imagem direto do diretório, minimiza falhas de reconhecimento
                        processor.AddImageFile(fileName);
                    }
                }
                else
                {                   
                    
                    if (x.DetectOrientationByText(image, language) == RotationTypeEnum.RT_Clockwise)
                    {
                        image90 = x.RotateImageByRotationType(image, RotationTypeEnum.RT_Clockwise);
                        processor.AddImage(image90);
                    }
                    //else if (x.DetectOrientationByText(x.Binarize(image, BinarizationModeEnum.BM_Default, false, false, 0), language) == RotationTypeEnum.RT_Clockwise)
                    //{                        
                    //    image90 = x.RotateImageByRotationType(image, RotationTypeEnum.RT_Clockwise);
                    //    processor.AddImage(image90);
                    //}
                    //else if (x.DetectOrientationByText(x.Binarize(image, BinarizationModeEnum.BM_Version9, false, false, 0), language) == RotationTypeEnum.RT_Clockwise)
                    //{                        
                    //    image90 = x.RotateImageByRotationType(image, RotationTypeEnum.RT_Clockwise);
                    //    processor.AddImage(image90);
                    //}
                    else
                    {
                        //Se não houve rotate add imagem direto do sistema de arquivos
                        processor.AddImageFile(fileName);
                    }
                    
                }

                this.log.Debug("Before RecognizeNextDocument");
                IDocument document = processor.RecognizeNextDocument();

                if (document != null && document.DocumentDefinition != null)
                {
                    
                    IFileExportParams exportParams = this.engine.CreateFileExportParams();

                    exportParams.FileFormat = FileExportFormatEnum.FEF_XML;
                    exportParams.ExportFieldNames = true;
                    exportParams.XMLParams.ExportFieldPosition = true;
                    exportFileName = System.IO.Path.GetDirectoryName(fileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(fileName) + ".xml";

                    this.log.Debug("BeforeExportDocumentEx");
                    this.processor.ExportDocumentEx(document, System.IO.Path.GetDirectoryName(fileName), System.IO.Path.GetFileNameWithoutExtension(fileName) + ".xml", exportParams);

                    //Salva versão corrigida da imagem
                    this.log.Debug("Before Saving PreProcessed:" + fileNamePreProcessed);

                    if (System.IO.Path.GetExtension(fileName).ToUpper().Contains("JPG") && !binarize)
                    {
                        this.log.Debug("Color Image");
                        document.Pages[0].Image.ColorImage.WriteToFile(fileNamePreProcessed, ImageFileFormatEnum.IFF_Jpg, null, ImageCompressionTypeEnum.ICT_Jpeg, null);
                    }
                    else
                    {
                        this.log.Debug("B&W Image");
                        document.Pages[0].Image.BlackWhiteImage.WriteToFile(fileNamePreProcessed, ImageFileFormatEnum.IFF_Tif, null, ImageCompressionTypeEnum.ICT_CcittGroup4, null);
                    }
                }
                else
                {
                    if (isCapa && binarize != true)
                        exportFileName = "RETRY";
                    else if (isCapa && binarize == true)
                        exportFileName = "";

                        this.log.Debug("**** Not recognized");
                    string destDir = startupPath + "\\ERR\\" + alias + "\\" + batchnfid.ToString() ;

                    if (!System.IO.Directory.Exists(destDir))
                        System.IO.Directory.CreateDirectory(destDir);

                    string destFileName = destDir + "\\" + System.IO.Path.GetFileName(fileName);

                    System.IO.File.Copy(fileName, destFileName, true);
                }
            }
            catch (Exception e)
            {
                log.Error("ProcessImage Error: " + e.Message);
                System.Windows.Forms.MessageBox.Show("ProcessImage:" + e.Message);
            }
            
            return exportFileName;
        }

        public string RecognizeType(long docId, long batchNfId, string filename, string alias, int batchNfTotalDocs, long workflowId, bool isCapa)
        {
            string ret = "";
            
            var rfilename = "";

            if (isCapa)
                 rfilename= RotateAndSaveImage(true, filename);
            else 
                rfilename = filename;
                        
            if (processorCfl == null)
            {
                processorCfl = engine.CreateFlexiCaptureProcessor();

                string aux = Files.GetWorkDir() + "\\CFL\\CapaClassify.cfl";

                if (!File.Exists(aux))
                {
                    Alert.Exclamation("Arquivo CapaClassify.cfl não encontrado!");
                    return ret;
                }

                this.processorCfl.AddClassificationTreeFile(aux);
            }
            else
            {
                processorCfl.ResetProcessing();
            }

            processorCfl.AddImageFile(rfilename);

            var result = processorCfl.ClassifyNextPage();

            if (result != null && result.PageType == PageTypeEnum.PT_MeetsDocumentDefinition)
            {
                var names = result.GetClassNames();
                ret = names.Item(0).ToString();
            }
                        
            return ret;
        }


        private string RotateAndSaveImage(bool clockWise, String input)
        {
            var newOutPut = Path.GetDirectoryName(input) + "\\" + Path.GetFileNameWithoutExtension(input) + "_ROT" + Path.GetExtension(input);

            if (File.Exists(newOutPut))
                File.Delete(newOutPut);

            using (var img = System.Drawing.Image.FromFile(input))
            {
                if (clockWise)
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                else
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);

                img.Save(newOutPut, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            return newOutPut;
        }

        //public void unloadEngine()
        //{
        //    engine = null;
        //    int hResult = DeinitializeEngine();
        //    Marshal.ThrowExceptionForHR(hResult);
        //}

        public void AddToTemplateList(string filename)
        {
            if (!System.IO.File.Exists(filename))
            {
                Alert.Exclamation("FlexImage ABBYY Engine: Template not found: " + filename);
                return;
            }
            else
            {
                this.log.Debug("AddTemplateList::" + filename);
                this.processor.AddDocumentDefinitionFile(filename);                
            }
        }

        

    }    
}