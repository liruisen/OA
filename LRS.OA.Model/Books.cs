//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LRS.OA.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublisherId { get; set; }
        public System.DateTime PublishDate { get; set; }
        public string ISBN { get; set; }
        public int WordsCount { get; set; }
        public decimal UnitPrice { get; set; }
        public string ContentDescription { get; set; }
        public string AurhorDescription { get; set; }
        public string EditorComment { get; set; }
        public string TOC { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public int Clicks { get; set; }
    }
}
