using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.Repository
{
    class Class1
    {
    }
    public class Peoples
    {
        public Peoples()
        {

        }
        public Peoples(int id, string nome, int quantidade)
        {
            Id = id;
            Nome = nome;
            Quantidade = quantidade;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
    }


    public class Tipo
    {
        public int TipoId { get; set; }
        public int Quantidade { get; set; }
    }

}
