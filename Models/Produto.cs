using APICatalogo.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "O nome é obrigatírio")]
        [MaxLength(80)]
        [PrimeiraLetraMaiuscula]    // Atributo personalizado
        public string Nome { get; set; }
        [Required]
        [StringLength(80, MinimumLength = 10, ErrorMessage = "A descrição deve ter no máximo {1} e no mínimo {2} caracteres.")]
        public string Descricao { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Preco { get; set; }
        [Required]
        [MaxLength(500)]
        public string ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public Categoria Categoria { get; set; }    //Atributo Propriedade de navegação (Relação entre entidades)
        public int CategoriaId { get; set; }    //Atributo Propriedade de navegação (Relação entre entidades)
    }
}
