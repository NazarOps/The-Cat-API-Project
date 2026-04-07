using System.Collections.Generic;
using System.Collections;

namespace Cat_API_Project.DTO
{
    public class PagedResultDTO<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public List<T> Items { get; set; } = new(); //generic wrapper for different dtos
    }
}
