using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XF.Contatos.App_Code;
using Xamarin.Forms;
using Xamarin.Media;
using Android.Content;

[assembly: Dependency(typeof(XF.Contatos.Droid.MainActivity))]
namespace XF.Contatos.Droid
{
    [Activity(Label = "XF.Contatos", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ICamera
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public void CapturarFoto()
        {
            var context = Forms.Context as Activity;
            var captura = new MediaPicker(context);

            var intent = captura.GetTakePhotoUI(new StoreCameraMediaOptions
            {
                DefaultCamera = CameraDevice.Rear
            });
            context.StartActivityForResult(intent, 1);
        }        

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (resultCode == Result.Canceled) return;

            var mediaPath = await data.GetMediaFileExtraAsync(Forms.Context);
            System.Diagnostics.Debug.WriteLine(mediaPath.Path);

            MessagingCenter.Send<ICamera, string>(this, "cameraFoto", mediaPath.Path);
        }
    }
}

