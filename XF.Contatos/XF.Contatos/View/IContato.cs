using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF.Contatos.ViewModel;

namespace XF.Contatos.View
{
    public interface IContato
    {
        void GetMobileContacts(ContatoViewModel vm);
    }
}
