namespace HotelManagementSystem.Models
{
    public class PaginationVM
    {
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
        public int NextPage { get; }
        public int PreviousPage { get; }
        public int PageSize { get; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginationVM(int pageindex, int totalpages, int totalcount, int nextpage, int previouspage, int pagesize)
        {
            PageIndex = pageindex;
            TotalPages = totalpages;
            TotalCount = totalcount;
            NextPage = nextpage;
            PreviousPage = previouspage;
            PageSize = pagesize;
        }
    }
}
