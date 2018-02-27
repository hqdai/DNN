using System;
using System.Collections.Generic;

namespace Hydros.Model
{
    public class DataTablesInfo
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }
    
    public class DataTables_AssetSummaryInfo : DataTablesInfo
    {
        public List<AssetSummaryInfo> data { get; set; }
    }

    #region "jQuery Datatables.js"
    public class JQDTParams
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public JQDTColumnSearch  /*Dictionary<string, string>*/ search { get; set; }
        public List<JQDTColumnOrder/*Dictionary<string, string>*/> order { get; set; }
        public List<JQDTColumn/*Dictionary<string, string>*/> columns { get; set; }

    }

    public enum JQDTColumnOrderDirection
    {
        asc, desc
    }

    public class JQDTColumnOrder
    {
        public int column { get; set; }
        public JQDTColumnOrderDirection dir { get; set; }
    }
    public class JQDTColumnSearch
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class JQDTColumn
    {
        public string data { get; set; }
        public string name { get; set; }
        public Boolean searchable { get; set; }
        public Boolean orderable { get; set; }
        public JQDTColumnSearch search { get; set; }
    }
    #endregion
}
