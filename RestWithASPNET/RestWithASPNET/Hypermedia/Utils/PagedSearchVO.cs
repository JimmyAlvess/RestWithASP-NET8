using Microsoft.AspNetCore.Identity;
using RestWithASPNET.Hypermedia.Abstract;

namespace RestWithASPNET.Hypermedia.Utils
{
    public class PagedSearchVO<T> where T : ISupportsHyperMedia 
    {
        public PagedSearchVO(int currenPage, int pageSize, string sortFields, string sortDirections)
        {
            CurrenPage = currenPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }
        public PagedSearchVO(int currenPage, int pgeSize, string sortFields, string sortDirections, Dictionary<string, object> filters, List<T> list)
        {
            CurrenPage = currenPage;
            PageSize = pgeSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
            Filters = filters;
        }
        public PagedSearchVO(int currenPage, string sortFields, string sortDirections)
         : this(currenPage, 10, sortFields, sortDirections)
        {
        }
        public PagedSearchVO()
        {
        }

        public int CurrenPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResult { get; set; }
        public string SortFields { get; set; }
        public string SortDirections { get; set; }

        public Dictionary<string, Object> Filters { get; set; }
        public List<T> List { get; set; }

        public int GetCurrentPage()
        {
            return CurrenPage == 0 ? 2 : CurrenPage;
        }
        public int GetPageSize()
        {
            return PageSize == 0 ? 10 : PageSize;
        }
    }
}
