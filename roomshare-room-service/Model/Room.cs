using roomshare_room_service.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace roomshare_room_service.Model
{
    [Table("room")]
    public class Room : BaseEntity
    {
        [Column("room_key")]
        [Required]
        public Guid RoomKey { get; set; }

        [Column("name")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Column("description")]
        [StringLength(1000)]
        public string? Description { get; set; }

        [Column("locator_key")]
        [Required]
        public Guid LocatorKey { get; set; }

        [Column("address")]
        [Required]
        [StringLength(200)]
        public string? Address { get; set; }

        [Column("cep")]
        [Required]
        public long CEP { get; set; }
    }
}
