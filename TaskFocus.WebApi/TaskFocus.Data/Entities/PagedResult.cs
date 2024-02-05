using System.Collections.Generic;

namespace TaskFocus.Data.Entities
{
    public class PagedResult<TEntity> where TEntity : BaseEntity
    {
        public ICollection<TEntity> Items { get; set; }

        public int TotalCount { get; set; }
    }
}