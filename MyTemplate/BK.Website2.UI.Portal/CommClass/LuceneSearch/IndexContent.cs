using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace My.Template.Model.LuceneSearch
{
    public class IndexContent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public LuceneEnum LuceneEnum { get; set; }
    }
}