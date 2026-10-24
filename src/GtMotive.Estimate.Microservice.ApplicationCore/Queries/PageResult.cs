using System;
using System.Collections.Generic;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Queries
{
    /// <summary>
    /// Page of results with paging metadata.
    /// </summary>
    /// <typeparam name="T">Item type.</typeparam>
    /// <param name="items">Items of the page.</param>
    /// <param name="page">Page number (1-based).</param>
    /// <param name="pageSize">Page size.</param>
    /// <param name="totalCount">Total number of items.</param>
    public sealed class PageResult<T>(IReadOnlyCollection<T> items, int page, int pageSize, int totalCount)
    {
        /// <summary>
        /// Gets the items of the page.
        /// </summary>
        public IReadOnlyCollection<T> Items { get; } = items;

        /// <summary>
        /// Gets the page number (1-based).
        /// </summary>
        public int Page { get; } = page;

        /// <summary>
        /// Gets the page size.
        /// </summary>
        public int PageSize { get; } = pageSize;

        /// <summary>
        /// Gets the total number of items.
        /// </summary>
        public int TotalCount { get; } = totalCount;

        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
