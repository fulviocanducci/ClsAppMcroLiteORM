using MicroLite.Mapping;
using MicroLite.Mapping.Attributes;
using System.ComponentModel.DataAnnotations;
namespace WebAppMicroLiteORM.Models
{
    [Table("People")]
    public class People
    {
        [Column("Id")]
        [Identifier(IdentifierStrategy.DbGenerated)]
        public int Id { get; set; }

        [Column("Name")]
        [Required(ErrorMessage = "Digite o nome completo")]
        public string Name { get; set; }
    }
}
