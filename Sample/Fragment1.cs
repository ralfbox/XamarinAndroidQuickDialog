using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using QuickDialogLibrary.Listeners;
using QuickDialogLibrary.Support.V7;
using System;

namespace Sample {


    public class Fragment1: Fragment {

        public const int QD_REQUEST_ALERT_1 = 1;

        private string Name;
        private int CounterPositive = 0;
        private int CounterNegative = 0;

        private Button bt;


        public override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            Name = Arguments.GetString("name");            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            View v = inflater.Inflate(Resource.Layout.FragmentView, container, false);
            (bt = v.FindViewById<Button>(Resource.Id.MyButton)).Click += OnButtonClick;

            if(savedInstanceState != null) {
                CounterPositive = savedInstanceState.GetInt("pos");
                CounterNegative = savedInstanceState.GetInt("neg");
            }
            ReloadTextButton();
            return v;
        }

        private void ReloadTextButton() {
            bt.Text = "Positive: " + CounterPositive + ";   Negative: " + CounterNegative;
        }

        public override void OnSaveInstanceState(Bundle outState) {
            base.OnSaveInstanceState(outState);
            outState.PutInt("pos", CounterPositive);
            outState.PutInt("neg", CounterNegative);
        }



        private void OnButtonClick(object sender, EventArgs e) {
            //Create AlertDialog
             new QuickDialog.Builder(this, QD_REQUEST_ALERT_1)
                        .Title("fragmentName: " + Name)
                        .PositiveButton("positive")
                        .NegativeButton("negative")
                        .Controller(typeof(ControllerAlert))
                        .Show(FragmentManager, "err");
        }

//Method for positive button
        [PositiveButtonQD(QD_REQUEST_ALERT_1)]
        public void OnPositiveButtonClick(ControllerAlert controller, QuickDialog qd) {
            CounterPositive++;
            ReloadTextButton();
            string text = controller.EditText.Text;
            if (text.Length > 0)
                ShowText("Writen text: " + controller.EditText.Text);
        }

//Mehtod for negative button 
        [NegativeButtonQD(QD_REQUEST_ALERT_1)]
        public void OnNegativeButtonClick() {
            CounterNegative++;
            ReloadTextButton();
        }

//Method for cancel dialog
        [CancelQD(QD_REQUEST_ALERT_1)]
        private void OnCancell() {
            ShowText("Dialog cancelled");
        }







        private void ShowText(string text) {
            Toast.MakeText(Activity, text, ToastLength.Long).Show();
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