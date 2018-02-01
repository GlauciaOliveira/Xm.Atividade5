using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using XF.Contatos.Model;
using Xamarin.Contacts;
using Xamarin.Forms;
using XF.Contatos.iOS;
using XF.Contatos.ViewModel;
using System.IO;

[assembly: Dependency(typeof(AddressBook_iOS))]
namespace XF.Contatos.iOS
{
    class AddressBook_iOS : IContato
    {
        public object Bitmap { get; private set; }

        public async void GetMobileContacts(ContatoViewModel vm)
        {
            var book = new Xamarin.Contacts.AddressBook();
            if (await book.RequestPermission())
            {
                foreach (Contact contact in book)
                {
                    SetContato(contact, vm);
                }
            }
            else
            {
                var message = "Permissão negada. Habite acesso a lista de contatos";
                UIAlertView avAlert = new UIAlertView("Autorização", message, null, "OK");
                avAlert.Show();
            }
        }

        void SetContato(Contact paramContato, ContatoViewModel vm)
        {
            var contato = new Contato()
            {
                Nome = paramContato.FirstName,
                SobreNome = paramContato.LastName,
                DisplayName = paramContato.DisplayName
            };

            foreach (var item in paramContato.Phones)
            {
                var telefone = new Telefone()
                {
                    Descricao = item.Label,
                    Numero = item.Number
                };
                switch (item.Type)
                {
                    case Xamarin.Contacts.PhoneType.Home:
                        telefone.Tipo = Model.PhoneType.Home;
                        break;
                    case Xamarin.Contacts.PhoneType.HomeFax:
                        telefone.Tipo = Model.PhoneType.HomeFax;
                        break;
                    case Xamarin.Contacts.PhoneType.Work:
                        telefone.Tipo = Model.PhoneType.Work;
                        break;
                    case Xamarin.Contacts.PhoneType.WorkFax:
                        telefone.Tipo = Model.PhoneType.WorkFax;
                        break;
                    case Xamarin.Contacts.PhoneType.Pager:
                        telefone.Tipo = Model.PhoneType.Pager;
                        break;
                    case Xamarin.Contacts.PhoneType.Mobile:
                        telefone.Tipo = Model.PhoneType.Mobile;
                        break;
                    case Xamarin.Contacts.PhoneType.Other:
                        telefone.Tipo = Model.PhoneType.Other;
                        break;
                    default:
                        break;
                }
                contato.Telefones.Add(telefone);
            }
            vm.Contatos.Add(contato);
        }
    }
}