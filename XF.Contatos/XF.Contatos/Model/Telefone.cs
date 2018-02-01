using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF.Contatos.Model
{
    public class Telefone
    {
        public Telefone() { }

        public TipoTel Tipo { get; set; }
        public string Descricao { get; set; }
        public string Numero { get; set; }
    }
}
