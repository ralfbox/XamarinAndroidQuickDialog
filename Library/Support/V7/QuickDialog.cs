

using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using System;

namespace QuickDialogLibrary.Support.V7 {
    

    public class QuickDialog: DialogFragment {

        private AnswerEngineer AnswerEngineer;
        internal BundleQD BundleQD;
        public ControllerQD Controller {get; private set; }
        private bool ExistController;

        public override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            BundleQD = new BundleQD(Arguments);
            AnswerEngineer = new AnswerEngineer(this);

            if (BundleQD.HasController) {
                System.Type controllerType = BundleQD.Controller;
                if (controllerType != null) { 
                    Controller = System.Activator.CreateInstance(controllerType) as ControllerQD;
                    Controller._QuickDialog = this;
                }
            }
            ExistController = Controller != null;

        }

        public override Android.App.Dialog OnCreateDialog(Bundle savedInstanceState) {
    
            Cancelable = BundleQD.Cancellable;

            var ret = CreateDialogBuilder(savedInstanceState);
            return ret.Create();
        }


        protected AlertDialog.Builder CreateDialogBuilder(Bundle savedInstanceState) {
            BundleQD b = new BundleQD(Arguments);
            AlertDialog.Builder ret = new AlertDialog.Builder(Activity);

            if (b.HasTitle)
                ret.SetTitle(b.Title);
            if (b.HasMessage)
                ret.SetMessage(b.Message);
            if (b.HasPositiveBT)
                ret.SetPositiveButton(b.PositiveBT, AnswerEngineer.PositiveButtonClick);
            if (b.HasNegativeBT)
                ret.SetNegativeButton(b.NegativeBT, AnswerEngineer.NegativeClick);
            if (b.HasNeutralBT)
                ret.SetNeutralButton(b.NeutralBT, AnswerEngineer.NeutralButtonClick);

            ret.SetCancelable(b.Cancellable);
            if (ExistController)
                ret = Controller.CreatingAlertDialogBuilder(ret);
            return ret;
        }

        public override void OnResume() {
            base.OnResume();
            if (ExistController)
                Controller.OnResume();
        }

        public override void OnCancel(IDialogInterface dialog) {
            AnswerEngineer.OnCancel(dialog);
            base.OnCancel(dialog);
        }



        public class Builder {            
            
            private BundleQD args;
            protected Bundle Args {get { return args.Args; } }
            protected readonly Context Context;

            public Builder(Context context, int request) {
                args = new BundleQD(new Bundle());
                Context = context;
                args.Request = request;
                args.ResponseToFragment = false;
            }
            
            public Builder(Fragment fragment, int request) : this(fragment.Context, request){
                args.ResponseToFragment = true;
                if (fragment.Tag!= null)
                    args.ResponseFragmentTag = fragment.Tag;
                else
                    args.ResponseFragmentID = fragment.Id;
            }

            public Builder Title(string title)   { args.Title = title; return this; }
            public Builder Title(int title)      { return Title(str(title)); }

            public Builder Message(int message) { return Message(str(message)); }
            public Builder Message(string message) { args.Message = message; return this; }

            public Builder PositiveButton(int positiveButton) { return PositiveButton(str(positiveButton)); }
            public Builder PositiveButton(string positiveButton) { args.PositiveBT = positiveButton; return this; }

            public Builder NegativeButton(int negativeButton) { return NegativeButton(str(negativeButton)); }
            public Builder NegativeButton(string negativeButton) { args.NegativeBT = negativeButton; return this; }

            public Builder NeutralButton(int neutralButton) { return NeutralButton(str(neutralButton)); }
            public Builder NeutralButton(string neutralButton) { args.NeutralBT = neutralButton; return this; }

            public Builder Cancellable(bool cancellable) { args.Cancellable = cancellable; return this; }

            public Builder FinishActivityIfPositiveBTClicked() { args.FinishIfPositiveBTClicked = true; return this; }
            public Builder FinishActivityIfNegativeBTClicked() { args.FinishIfNegativeBTClicked = true; return this; }
            public Builder FinishActivityIfNeutralBTClicked() { args.FinishIfNeutralBTClicked = true; return this; }


            public Builder Controller(System.Type type) {
                if (!type.IsSubclassOf(typeof(ControllerQD)))
                    throw new System.ArgumentException("The type must subclass of ControllerQD");
                args.Controller = type;
                return this;
            }

            private string str(int resInt) {
                return Context.GetString(resInt);
            }

            public QuickDialog Build() {
                QuickDialog ret = new QuickDialog();
                ret.Arguments = Args;
                return ret;
            }

            public void Show(FragmentManager fragmentManager, string tag) {
                Build().Show(fragmentManager, tag);
            }
            
        }      
        
    }
}
