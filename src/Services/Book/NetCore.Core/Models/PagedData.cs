using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Core.Models
{
    public class PagedData<TEntity>
    {
        private IEnumerable<TEntity> _items;
        private int _total;

        public PagedData(IEnumerable<TEntity> items, int total)
        {
            this._items = items;
            this._total = total;
        }

        public IEnumerable<TEntity> Items { get { return _items; } }
        public int Total { get { return _total; } }
    }
}
