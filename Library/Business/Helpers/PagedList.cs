using Microsoft.EntityFrameworkCore;

namespace Business.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }

        public int TotalCount { get; private set; }

        public PagedList(List<T> items, int count, int pageId)
        {
            TotalCount = count;
            CurrentPage = pageId;

            AddRange(items);
        }

        public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> source, int pageId, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageId - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<T>(items, count, pageId);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageId, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageId - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageId);
        }

    }
}
