using System.Linq;
using BlogPessoal.src.data;
using BlogPessoal.src.modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BlogPessoalTeste.Testes.data
{
    [TestClass]
    public class BlogPessoalContextoTeste
    {
        private BlogPessoalContexto _contexto;

        [TestInitialize]
        public void inicio()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal")
            .Options;
            _contexto = new BlogPessoalContexto(opt);

        }

        [TestMethod]
        public void InserirNovoUsuarioNoBancoRetornarUsuario()
        {
            UsuarioModelo usuario = new UsuarioModelo();
            usuario.Nome = "Ana Paula";
            usuario.Email = "paulabrufato.pb@gmail.com";
            usuario.Senha = "123abc";
            usuario.Foto = "Link da foto";

            _contexto.Usuarios.Add(usuario); // adiciona usuário
           
            _contexto.SaveChanges(); // comita criação

            Assert.IsNotNull(_contexto.Usuarios.FirstOrDefault(u => u.Email == "paulabrufato.pb@gmail.com"));
        }
    }
}
