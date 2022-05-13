using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.modelos
{
    /// <summary>
    /// <para>Resumo: Classe responsável por representar tb_tema no banco.</para>
    /// <para>Resumo: Criado por: Paula Brufato</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    [Table("tb_temas")]
    public class TemaModelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Descricao { get; set; } 

        [JsonIgnore]
        public List<PostagemModelo> PostagensRelacionadas { get; set; }         
    }
}