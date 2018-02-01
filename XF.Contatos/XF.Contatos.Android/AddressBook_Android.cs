using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using XF.Contatos.Model;
using Xamarin.Contacts;
using Xamarin.Forms;
using System.Threading.Tasks;
using XF.Contatos.Droid;
using XF.Contatos.ViewModel;

using Xamarin.Forms;
using Xamarin.Media;
using System.IO;
using Android.Graphics;
using XF.Contatos.View;

[assembly: Dependency(typeof(AddressBook_Android))]
namespace XF.Contatos.Droid
{
    public class AddressBook_Android : IContato
    {
        public async void GetMobileContacts(ContatoViewModel vm)
        {
            var context = Forms.Context as Activity;
            var book = new Xamarin.Contacts.AddressBook(context);
            if (await book.RequestPermission())
            {
                foreach (Contact contact in book)
                {
                    SetContato(contact, vm);
                }
            }
            else
            {
                AlertDialog.Builder messageUI = new AlertDialog.Builder(context);
                messageUI.SetMessage("Permissão negada. Habite acesso a lista de contatos");
                messageUI.SetTitle("Autorização");
                messageUI.Create().Show();
            }
        }

        void SetContato(Contact paramContato, ContatoViewModel vm)
        {
            var image = paramContato.GetThumbnail();
            ImageSource imgSource = null;
            if (image != null)
            {
                byte[] imgFile = new byte[image.Width * image.Height * 4];
                MemoryStream stream = new MemoryStream(imgFile);
                image.Compress(Bitmap.CompressFormat.Png, 100, stream);
                stream.Flush();
                imgSource = ImageSource.FromStream(()
                    => new MemoryStream(imgFile));
            }
            else
                imgSource = ImageSource.FromFile("contacts.png");

            var contato = new Contato()
            {
                Nome = paramContato.FirstName,
                SobreNome = paramContato.LastName,
                DisplayName = paramContato.DisplayName,
                Foto = imgSource                
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
                        telefone.Tipo = Model.TipoTel.Home;
                        break;
                    case Xamarin.Contacts.PhoneType.HomeFax:
                        telefone.Tipo = Model.TipoTel.HomeFax;
                        break;
                    case Xamarin.Contacts.PhoneType.Work:
                        telefone.Tipo = Model.TipoTel.Work;
                        break;
                    case Xamarin.Contacts.PhoneType.WorkFax:
                        telefone.Tipo = Model.TipoTel.WorkFax;
                        break;
                    case Xamarin.Contacts.PhoneType.Pager:
                        telefone.Tipo = Model.TipoTel.Pager;
                        break;
                    case Xamarin.Contacts.PhoneType.Mobile:
                        telefone.Tipo = Model.TipoTel.Mobile;
                        break;
                    case Xamarin.Contacts.PhoneType.Other:
                        telefone.Tipo = Model.TipoTel.Other;
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