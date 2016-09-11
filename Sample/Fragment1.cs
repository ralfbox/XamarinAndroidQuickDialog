using Android.OS;
using Android.Renderscripts;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using QuickDialogLibrary.Listeners;
using QuickDialogLibrary.Support.V7;
using System;

namespace Sample {


    public class Fragment1: Fragment {

        private string Name;

        public const int QD_REQUEST_ALERT_1 = 1;

        public override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            Name = Arguments.GetString("name");            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            View v = inflater.Inflate(Resource.Layout.FragmentView, container, false);
            v.FindViewById(Resource.Id.MyButton).Click += delegate {
                new QuickDialog.Builder(this, QD_REQUEST_ALERT_1)
                        .Title("fragmentName: " + Name)
                        .PositiveButton("positive")
                        .NegativeButton("negative")
                        .Controller(typeof(ControllerAlert))
                        .Build().Show(FragmentManager, "err");
            };
            
            return v;
        }

        [PositiveButtonQD(QD_REQUEST_ALERT_1)]
        private void OnPositiveButtonClick(ControllerAlert controller, QuickDialog qd) {
            ShowText("Positive button clicked: " + controller.EditText.Text + "\n" + qd.ToString() + "\n\n");
        }

        [NegativeButtonQD(QD_REQUEST_ALERT_1)]
        private void OnNegativeButtonClick() {
            try {
                System.Type t = System.Type.GetType("Sample.Fragment1");
                var o = Activator.CreateInstance(t);

                ShowText("Negative button clicked\n" + o.ToString());
            }catch (Exception ee) {
                ShowText("Err: " + ee.Message);
            }
        }

        [CancelQD(QD_REQUEST_ALERT_1)]
        private void OnCancell() {
            ShowText("Dialog cancelled");
        }

        private void ShowText(string text) {
            Toast.MakeText(Context, text + "\nFragmentName: " + Name, ToastLength.Long).Show();
        }


        public static Fragment1 newInstance(string name) {
            Bundle b = new Bundle();
            b.PutString("name", name);
            Fragment1 ret = new Fragment1();
            ret.Arguments = b;
            return ret;
        }
    }


    public class ControllerAlert : ControllerQD {
        public EditText EditText { get; private set; }

        public override AlertDialog.Builder CreatingAlertDialogBuilder(AlertDialog.Builder builder) {
            View v = LayoutInflater.Inflate(Resource.Layout.get_text_alert, null);
            EditText = v.FindViewById<EditText>(Resource.Id.ed1);
            builder.SetView(v);            
            return builder;
        }

    }
}