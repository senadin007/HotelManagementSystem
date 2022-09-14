using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Models
{
    public class PaginatedList<T>
    {
        public IReadOnlyCollection<T> Items { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
        public int NextPage { get; }
        public int PreviousPage { get; }
        public int PageSize { get; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, int nextPage, int previousPage)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
            NextPage = nextPage;
            PreviousPage = previousPage;
            PageSize = pageSize;
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();

            var lastPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(count) / pageSize));

            var nextPage = pageIndex >= 1 && pageIndex < lastPage
                ? pageIndex + 1
                : 0;

            var previousPage = pageIndex > 1
                ? pageIndex - 1
                : 1;

            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageIndex, pageSize, nextPage, previousPage);
        }
        public static async Task<PaginatedList<T>> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();

            var lastPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(count) / pageSize));

            var nextPage = pageIndex >= 1 && pageIndex < lastPage
                ? pageIndex + 1
                : 0;

            var previousPage = pageIndex > 1
                ? pageIndex - 1
                : 1;

            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>(items, count, pageIndex, pageSize, nextPage, previousPage);
        }
        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();

            var lastPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(count) / pageSize));

            var nextPage = pageIndex >= 1 && pageIndex < lastPage
                ? pageIndex + 1
                : 0;

            var previousPage = pageIndex > 1
                ? pageIndex - 1
                : 1;

            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>(items, count, pageIndex, pageSize, nextPage, previousPage);
        }
    }
}
