using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wow_dashboard.Models.BlizzardData
{

    // TODO - Rename these properties, trim the fat

    public class JournalEncounterSearchResult
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int maxPageSize { get; set; }
        public int pageCount { get; set; }
        public Result[] results { get; set; }
    }

    public class Result
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public Name name { get; set; }
        public int id { get; set; }
    }

    public class Name
    {
        public string en_US { get; set; }
    }

}
