
#region

using System;
using System.Collections.Generic;
using System.Xml;
using FlexImage.Core;
using FlexImage.OCR;
using FlexImage.WebService;

#endregion

namespace INMET
{
    public partial class ClassifyINMET
    {
        public ABBYY _oAbbyy;

        private readonly WSBasicTable _wsBasicTable = GenericSingleton<WSBasicTable>.GetInstance();
        private readonly WSRepository _wsRepository = GenericSingleton<WSRepository>.GetInstance();
        private readonly Log _log = GenericSingleton<Log>.GetInstance();

        //Livro M21A
        string[] templatesM1A = {
                    "livroM1A.fcdot"                    
                };
        
        //921Caderneta
        string[] templates921 = {
                    "921Detalhe.fcdot",
                    "921Capa.fcdot", 
                    "921ContraCapa.fcdot", 
                    "921Equipamentos.fcdot"
                };
        
        //921ACaderneta
        string[] templates921A = {
                    "921ADetalhe.fcdot",
                    "921ACapa.fcdot", 
                    "921AContraCapa.fcdot", 
                    "921AEquipamentos.fcdot"
                };
        
        //921BJan1971Caderneta
        string[] templates921BJan1971 = {
                    "921BJan1971Detalhe.fcdot",
                    "921BJan1971Capa.fcdot", 
                    "921BJan1971ContraCapa.fcdot", 
                    "921BJan1971Equipamentos.fcdot"
                };
        
        //921BJun1972Caderneta
        string[] templates921BJun1972 = {
                    "921BJun1972Detalhe.fcdot",
                    "921BJun1972Capa.fcdot", 
                    "921BJun1972ContraCapa.fcdot", 
                    "921BJun1972Equipamentos.fcdot"
                };
        
        //921BJun1973Caderneta
        string[] templates921BJun1973 = {
                    "921BJun1973Detalhe.fcdot",
                    "921BJun1973Capa.fcdot", 
                    "921BJun1973ContraCapa.fcdot", 
                    "921BJun1973Equipamentos.fcdot"
                };

        //--------------

        //921BJun1974Caderneta
        string[] templates921BJun1974 = {
                    "921BJun1974Detalhe.fcdot",
                    "921BJun1974Capa.fcdot", 
                    "921BJun1974ContraCapa.fcdot", 
                    "921BJun1974Equipamentos.fcdot"
                };

        //921BJun1975Caderneta
        string[] templates921BJun1975 = {
                    "921BJun1975Detalhe.fcdot",
                    "921BJun1975Capa.fcdot", 
                    "921BJun1975ContraCapa.fcdot", 
                    "921BJun1975Equipamentos.fcdot"
                };


        //921CJul1976Caderneta
        string[] templates921CJul1976 = {
                    "921CJul1976Detalhe.fcdot",
                    "921CJul1976Capa.fcdot", 
                    "921CJul1976ContraCapa.fcdot", 
                    "921CJul1976Equipamentos.fcdot"
                };

        //--------------

        //921CAbr1977Caderneta
        string[] templates921CAbr1977 = {
                    "921CAbr1977Detalhe.fcdot",
                    "921CAbr1977Capa.fcdot", 
                    "921CAbr1977ContraCapa.fcdot", 
                    "921CAbr1977Equipamentos.fcdot"
                };
        
        //921CAbr1978Caderneta
        string[] templates921CAbr1978 = {
                    "921CAbr1978Detalhe.fcdot",
                    "921CAbr1978Capa.fcdot", 
                    "921CAbr1978ContraCapa.fcdot", 
                    "921CAbr1978Equipamentos.fcdot"
                };
        
        //922Caderneta
        string[] templates922 = {
                    "922Detalhe.fcdot",
                    "922Capa.fcdot", 
                    "922ContraCapa.fcdot", 
                    "922Equipamentos.fcdot"
                };

        //922Jul1979Caderneta
        string[] templates922Jul1979 = {
                    "922Jul1979Detalhe.fcdot",
                    "922Jul1979Capa.fcdot", 
                    "922Jul1979ContraCapa.fcdot", 
                    "922Jul1979Equipamentos.fcdot"
                };
        
        //922Jul1980Caderneta
        string[] templates922Jul1980 = {
                    "922Jul1980Detalhe.fcdot",
                    "922Jul1980Capa.fcdot", 
                    "922Jul1980ContraCapa.fcdot", 
                    "922Jul1980Equipamentos.fcdot"
                };
        
        //922Jan1983Caderneta
        string[] templates922Jan1983 = {
                    "922Jan1983Detalhe.fcdot",
                    "922Jan1983Capa.fcdot", 
                    "922Jan1983ContraCapa.fcdot", 
                    "922Jan1983Equipamentos.fcdot"
                };
        
        string[] templates922AJan1987 = {
                    "922AJan1987Detalhe.fcdot",
                    "922AJan1987Detalhe2.fcdot",
                    "922AJan1987Capa.fcdot", 
                    "922AJan1987ContraCapa.fcdot", 
                    "922AJan1987Equipamentos.fcdot"
                };
        
        string[] templates922AJan1986 ={
                    "922AJan1986Detalhe.fcdot",
                    "922AJan1986Capa.fcdot", 
                    "922AJan1986ContraCapa.fcdot", 
                    "922AJan1986Equipamentos.fcdot"
                };

        string[] templates922AJan1992={
                    "922AJan1992Detalhe.fcdot",
                    "922AJan1992Capa.fcdot", 
                    "922AJan1992ContraCapa.fcdot", 
                    "922AJan1992Equipamentos.fcdot"
                };
        
        string[] templates922AJan1993={
                    "922AJan1993Detalhe.fcdot",
                    "922AJan1993Capa.fcdot", 
                    "922AJan1993ContraCapa.fcdot", 
                    "922AJan1993Equipamentos.fcdot"
                };

        string[] templates922Dez1981={
                    "922Dez1981Detalhe.fcdot",
                    "922Dez1981Capa.fcdot", 
                    "922Dez1981ContraCapa.fcdot", 
                    "922Dez1981Equipamentos.fcdot"
                };
  
        string[] templates923Jan1987 ={
                    "923Jan1987Detalhe.fcdot",
                    "923Jan1987Capa.fcdot", 
                    "923Jan1987ContraCapa.fcdot", 
                    "923Jan1987Equipamentos.fcdot"
                };

        string[] templates922BNov1994 =  {
                    "922BNov1994Detalhe.fcdot",
                    "922BNov1994Capa.fcdot", 
                    "922BNov1994ContraCapa.fcdot", 
                    "922BNov1994Equipamentos.fcdot"
                };
        
        string[] templates922CAbr1998 =  {
                    "922CAbr1998Detalhe.fcdot",
                    "922CAbr1998Capa.fcdot", 
                    "922CAbr1998ContraCapa.fcdot", 
                    "922CAbr1998Equipamentos.fcdot"
                };

        string[] templates922CRev2001 =  {
                    "922CRev2001Detalhe.fcdot",
                    "922CRev2001Capa.fcdot", 
                    "922CRev2001ContraCapa.fcdot", 
                    "922CRev2001Equipamentos.fcdot"
                };

        string[] templates922CRev2002 =  {
                    "922CRev2002Detalhe.fcdot",
                    "922CRev2002Capa.fcdot", 
                    "922CRev2002ContraCapa.fcdot", 
                    "922CRev2002Equipamentos.fcdot"
                };

        string[] templates922CRev2005 =  {
                    "922CRev2005Detalhe.fcdot",
                    "922CRev2005Capa.fcdot", 
                    "922CRev2005ContraCapa.fcdot", 
                    "922CRev2005Equipamentos.fcdot"
                };

        public ClassifyINMET(string SN, string alias)
        {
            this._log.Debug("ClassifyINMET -  SN:" + SN + " / Alias:" + alias);

            try
            {

                string[] templates=null;
                string templateDir = Files.GetWorkDir() + "\\TEMPLATES_ABBYY\\" + alias + "\\";
                if (!System.IO.Directory.Exists(templateDir))
                    System.IO.Directory.CreateDirectory(templateDir);

                if (alias == "livroM1A")
                    templates = templatesM1A;

                if (alias == "921Caderneta")
                    templates = templates921;

                if (alias == "921ACaderneta")
                    templates = templates921A;

                if (alias == "921BJan1971Caderneta")
                    templates = templates921BJan1971;

                if (alias == "921BJun1972Caderneta")
                    templates = templates921BJun1972;

                if (alias == "921BJun1973Caderneta")
                    templates = templates921BJun1973;


                if (alias == "921BJun1974Caderneta")
                    templates = templates921BJun1974;

                if (alias == "921BJun1975Caderneta")
                    templates = templates921BJun1975;

                if (alias == "921CJul1976Caderneta")
                    templates = templates921CJul1976;


                if (alias == "921CAbr1977Caderneta")
                    templates = templates921CAbr1977;

                if (alias == "921CAbr1978Caderneta")
                    templates = templates921CAbr1978;

                if (alias == "922Caderneta")
                    templates = templates922;

                if (alias == "922Jul1979Caderneta")
                    templates = templates922Jul1979;

                if (alias == "922Jul1980Caderneta")
                    templates = templates922Jul1980;

                if (alias == "922Jan1983Caderneta")
                    templates = templates922Jan1983;
        
                if (alias == "922AJan1987Caderneta")
                    templates = templates922AJan1987;

                if (alias == "922AJan1986Caderneta")
                    templates = templates922AJan1986;

                if (alias == "922AJan1992Caderneta")
                    templates = templates922AJan1992;

                if (alias == "922AJan1993Caderneta")
                    templates = templates922AJan1993;

                if (alias == "922Dez1981Caderneta")
                    templates = templates922Dez1981;

                if (alias == "923Jan1987Caderneta")
                    templates = templates923Jan1987;

                if (alias == "922BNov1994Caderneta")
                    templates = templates922BNov1994;

                if (alias == "922CAbr1998Caderneta")
                    templates = templates922CAbr1998;                               
                
                if (alias == "922CRev2001Caderneta")
                    templates = templates922CRev2001;

                if (alias == "922CRev2002Caderneta")
                    templates = templates922CRev2002;

                if (alias == "922CRev2005Caderneta")
                    templates = templates922CRev2005;

                
                if (templates == null)
                {
                    this._log.Error("ERROR: Templates not matched!");
                }

                foreach (string filename in templates)
                {
                    string localFilename = templateDir + filename;

                    if (!System.IO.File.Exists(templateDir + filename))
                    {
                        string aux = "Arquivos FCDOT não encontrados: [" + templateDir + filename + "]";
                        this._log.Debug(aux);
                        Alert.Exclamation(aux);
                    }
                }

                this._log.Debug("Initializing ABBYY -  SN:" + SN + " / TemplateDir:" + templateDir);
                this._oAbbyy = new ABBYY(SN, templateDir, templates);
                this._log.Debug("ABBYY initialized!");
            }
            catch (Exception e)
            {
                this._log.Error("ERROR:: Initializing ABBYY:" + e.Message);                
            }         
        }
    }
}
