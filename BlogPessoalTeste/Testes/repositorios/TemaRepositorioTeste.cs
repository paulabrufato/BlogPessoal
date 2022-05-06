using System.Linq;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositorios;
using BlogPessoal.src.repositorios.implementacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoalTeste.Testes.repositorios
{
    [TestClass]
    public class TemaRepositorioTeste
    {
        private BlogPessoalContexto _contexto;
        private ITema _repositorio;

        [TestInitialize]
        public void ConfiguracaoInicial()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);
        }

        [TestMethod]
        public void CriarQuatroTemasNoBancoRetornaQuatroTemas2()
        {
            
            _repositorio.NovoTema(new NovoTemaDTO("C#"));
            _repositorio.NovoTema(new NovoTemaDTO("Java"));
            _repositorio.NovoTema(new NovoTemaDTO("Python"));
            _repositorio.NovoTema(new NovoTemaDTO("JavaScript"));

            
            Assert.AreEqual(4, _repositorio.PegarTodosTemas().Count);
        }

        [TestMethod]
        [DataRow(1)]
        public void PegarTemaPeloIdRetornaTema1(int id)
        {
            var tema = _repositorio.PegarTemaPeloId(id);
            
            Assert.AreEqual("C#", tema.Descricao);
        }
        [TestMethod]
        [DataRow("Java")]
        public void PegaTemaPelaDescricaoRetornadoisTemas(string descricao)
        {
            
            var temas = _repositorio.PegarTemasPelaDescricao(descricao);
            
           
            Assert.AreEqual(2, temas.Count);
        }
        [TestMethod]
        [DataRow(3)]
        public void AlterarTemaPythonRetornaTemaCobol(int id)
        {
            
            _repositorio.AtualizarTema(new AtualizarTemaDTO(id, "COBOL"));
            
            
            Assert.AreEqual("COBOL",
            _repositorio.PegarTemaPeloId(id).Descricao);
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public void DeletarTemasRetornaNulo(int id)
        {
            
            _repositorio.DeletarTema(id);
            
            
            Assert.IsNull(_repositorio.PegarTemaPeloId(id));
        }
    }
}
