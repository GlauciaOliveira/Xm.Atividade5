using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using XF.Contatos.iOS.App_Code;
using XF.Contatos.App_Code;
using Xamarin.Forms;

[assembly: Dependency(typeof(Telefone_Android))]
namespace XF.Contatos.iOS.App_Code
{
    public class Telefone_Android : ITelefone
    {
        public bool Ligar(string  numero)
        {
            return UIApplication.SharedApplication.OpenUrl(
                new NSUrl("tel:" + numero));
        }
    }
}