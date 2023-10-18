using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restful_crud_dapper.Models;

public class Language
{
    [Key]
    public int language_id { get; set; }

    public string Name { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? last_update { get; set; }

}
