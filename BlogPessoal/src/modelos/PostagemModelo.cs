using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.src.modelos
{
    /// <summary>
    /// <para>Resumo: Classe responsável por representar tb_postagem no banco.</para>
    /// <para>Resumo: Criado por: Paula Brufato</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    [Table("tb_postagens")]
    public class PostagemModelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        
        [Required, StringLength(30)]
        public string Titulo { get; set; } 

        [Required, StringLength(30)]
        public string Descricao { get; set; } 

        public string Foto { get; set; }

        [ForeignKey("fk_usuario")]
        public UsuarioModelo Criador { get; set; }

        [ForeignKey("fk_tema")]
        public TemaModelo Tema { get; set; }
    }
}