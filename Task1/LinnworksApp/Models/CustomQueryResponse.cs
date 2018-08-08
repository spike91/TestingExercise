using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_TestApp2.Models
{
    public class CustomQueryResponse<T>
    {
        public class Column {
            public int Index { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public int TotalResults { get; set; }
        public List<Column> Columns { get; set; }
        public List<T> Results { get; set; }
    }
}