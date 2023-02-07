namespace Rss_Subscription.BLL.DTOs.Pagination
{
    public class PaginationDto
    {
        public PaginationDto() : this(1, 20) { }

        public PaginationDto(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public static readonly PaginationDto Default = new PaginationDto();

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
