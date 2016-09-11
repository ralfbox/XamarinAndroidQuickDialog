using Android.Support.V7.App;
using Android.Views;

namespace QuickDialogLibrary.Support.V7 {

    public class ControllerQD {

        internal QuickDialog _QuickDialog { set { QuickDialog = value; } }
        protected QuickDialog QuickDialog {get; private set;}
        protected Android.App.Activity Activity {get { return QuickDialog.Activity; } }
        protected LayoutInflater LayoutInflater {get { return Activity.LayoutInflater; } }
            
        /// <summary>
        /// Constructor must be empty!
        /// </summary>                    
        public ControllerQD() { }


        public virtual AlertDialog.Builder CreatingAlertDialogBuilder(AlertDialog.Builder builder) {
            return builder;
        }

        public void OnResume() {

        }

    }
}