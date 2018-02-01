using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Contatos.ViewModel;

namespace XF.Contatos.Model
{ 
    public class Contato
    {
        public Contato()
        {
            Telefones = new List<Telefone>();
        }

        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string DisplayName { get; set; }
        public List<Telefone> Telefones { get; set; }
        public string Numero
        {
            get
            {
                if (Telefones != null)
                    return Telefones.SingleOrDefault().Numero;

                return string.Empty;
            }
        }

        public ImageSource Foto { get; set; }
    }
}
