using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Models
{
    public class Categoria : IValidatableObject //para implementar um atributo personalizado
    {
        public Categoria()
        {
            Produtos = new Collection<Produto>(); // É uma boa pratica inicilalizar uma coleção
        }
        [Key]
        public int CategoriaId { get; set; }
        [Required]
        [MaxLength(80)]
        public string Nome { get; set; }
        [Required]
        [MaxLength(300)]
        public string ImagemUrl { get; set; }
        public ICollection<Produto> Produtos { get; set;}   //Atributo Propriedade de navegação (Relação entre entidades)

        //para implementar um atributo personalizado
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.Nome))
            {
                var primeiraLetra = this.Nome[0].ToString();
                if (primeiraLetra != primeiraLetra.ToUpper())
                {
                    yield return new ValidationResult("A primeira letra do nome da categoria deve ser maiúscula", new[] { nameof(this.Nome) });
                }
            }
        }
    }
}
