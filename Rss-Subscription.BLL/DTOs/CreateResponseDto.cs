using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rss_Subscription.BLL.DTOs
{
    public class CreateResponseDto
    {
        public CreateResponseDto()
        {

        }

        public CreateResponseDto(int id)
        {
            Id = id;
        }

        public CreateResponseDto(IList<int> ids)
        {
            Ids = ids;
        }

        [Required]
        public int Id { get; }

        public IList<int> Ids { get; set; }
    }
}
