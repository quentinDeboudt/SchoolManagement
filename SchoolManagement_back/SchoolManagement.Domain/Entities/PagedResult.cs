namespace SchoolManagement.Domain.Entities;

// public class PagedResult<T>
// {
//     public List<T> Items { get; set; }
//     public int TotalCount { get; set; }
// }
public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();  // Les éléments paginés
        public int TotalCount { get; set; }  // Le nombre total d'éléments non paginés

        public PagedResult()
        {
        }

        public PagedResult(List<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }