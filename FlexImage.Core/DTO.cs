// ============================================
// 
// FlexImage.Core
// DTO.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System.Drawing;

#endregion

namespace FlexImage.Core
{
    public class ModuleDTO
    {
        public long id { get; set; }
        public string alias { get; set; }
        public string name { get; set; }
        public string tooltip { get; set; }
        public string icon { get; set; }
        public string action { get; set; }
    }

    public class QueueDTO
    {
        public string FileName { get; set; }
        public int Sequence { get; set; }
        public string QueueFileName { get; set; }
        public long UsrId { get; set; }
        public long StationId { get; set; }
        public long SiteId { get; set; }
        public long BatchId { get; set; }
        public long WorkflowId { get; set; }
        public long TypeId { get; set; }
        public long VirtualFolderId { get; set; }
        public int Line { get; set; }
        public bool WorkflowDaily { get; set; }
        public string WorkflowTags { get; set; }
        public string Micr { get; set; }
        public string Barcode { get; set; }
        public long BoxId { get; set; }
        public string ScanTags { get; set; }
        public string XmlData { get; set; }
        public string RecognData { get; set; }
    }

    public class BarcodeDTO
    {
        public string Barcode { get; set; }
        public Rectangle Rect { get; set; }
        public string Type { get; set; }
    }
}