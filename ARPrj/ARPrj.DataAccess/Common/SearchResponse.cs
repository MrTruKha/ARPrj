using System.Collections.Generic;

namespace ARPrj.DataAccess.Common
{
    public class SearchResponse<T>
    {
        public List<T> Records { get; set; }
        public int TotalRecords { get; set; }
        public SearchResponse()
        {
            Records = new List<T>();
            TotalRecords = 0;
        }
    }

    public class BooleanResponse
    {
        public BooleanResponse()
        {
            IsSuccess = false;
            Messages = new List<string>();
        }
        public bool IsSuccess { get; set; }
        public List<string> Messages { get; set; }

        public string MessageDisplay
        {
            get
            {
                return Messages?.Count > 0 ? string.Join(", ", Messages) : "";
            }
        }
    }
}
