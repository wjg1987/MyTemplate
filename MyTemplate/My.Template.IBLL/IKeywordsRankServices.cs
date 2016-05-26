using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Template.Model;

namespace My.Template.IBLL
{
    public partial interface IKeywordsRankService : IBaseServices<KeywordsRank>
    {
        bool DeleteAllKeyWords();
        bool InsertKeyWords();
        List<string> GetSearchWord(string str);
    }
}
