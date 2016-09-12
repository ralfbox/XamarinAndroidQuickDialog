
using Android.App;
using Android.Widget;
using Android.OS;
using QuickDialogLibrary.Support.V7;
using Android.Support.V7.App;
using QuickDialogLibrary.Listeners;

namespace Sample {


    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme")]
    public class MainActivity: AppCompatActivity {
        private const int QD_REQUEST_ALERT_1 = 3;
        private const int QD_ALERT_2 = 2;


        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);           
            SetContentView(Resource.Layout.Main);
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += delegate {
                new QuickDialog.Builder(this, QD_REQUEST_ALERT_1)
                    .Title("Alert 1")
                    .Message("Any massage")
                    .PositiveButton("Positive")
                    .NegativeButton("Negative")
                    .Show(SupportFragmentManager, "err");
            };

            Button button2 = FindViewById<Button>(Resource.Id.MyButton2);
            button2.Click += delegate {
                new QuickDialog.Builder(this, QD_ALERT_2)
                    .Title("Alert 2")
                    .Message("Any massege")
                    .PositiveButton("Positive")
                    .NegativeButton("Negative")
                    .NeutralButton("Neutral")
                    .Cancellable(false)
                    .Build()
                    .Show(SupportFragmentManager, "err");
            };

            if (savedInstanceState == null) {
                SupportFragmentManager.BeginTransaction().Replace(Resource.Id.Frame1, Fragment1.newInstance("Fragment 1")).Commit();
                SupportFragmentManager.BeginTransaction().Replace(Resource.Id.Frame2, Fragment1.newInstance("Fragment 2"), "qwerty").Commit();
            }
        }

        private void ShowText(string text) {
            Toast.MakeText(this, text, ToastLength.Long).Show();
        }


        [PositiveButtonQD(QD_REQUEST_ALERT_1)]
        private void AnyName_f_g_Dupa1() {
            ShowText("PositiveButton alert 1");
        }


        [NegativeButtonQD(QD_ALERT_2)]
        private void OnNegativeBTAlert2() {
            ShowText("NegativeButton alert 2");
        }

        [PositiveButtonQD(QD_ALERT_2)]
        private void OnPositiveBTAlert2() {
            ShowText("Positive alert 2");
        }


        [CancelQD(QD_REQUEST_ALERT_1)]
        private void OnCancelAlert1() {
            ShowText("cancel alert 1");
        }

        [NeutralButtonQD(QD_ALERT_2)]
        private void OnAlert2NeutralBT() {
            ShowText("Neutral button alert 2");
        }
    }
}

