//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using Newtonsoft.Json;


namespace My.Template.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Videos : IBaseEntity
    {
        public int ID { get; set; }
        public string VName { get; set; }
        public string ThemPic { get; set; }
        public string MPEG4Url { get; set; }
        public string OggUrl { get; set; }
        public string WebMUrl { get; set; }
        public bool IsView { get; set; }
        public bool IsDelete { get; set; }
        public int Sequence { get; set; }
        public System.DateTime AddTime { get; set; }
    }
}
