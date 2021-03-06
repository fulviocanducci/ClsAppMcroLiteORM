﻿using MicroLite.Mapping;
using MicroLite.Mapping.Attributes;
namespace CslAppMicroLiteORM.Models
{
    [Table("People")]
    public class People
    {
        [Column("Id")]
        [Identifier(IdentifierStrategy.DbGenerated)]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
