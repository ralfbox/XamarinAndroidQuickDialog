
using Android.App;
using Android.Content;
using Android.OS;

namespace QuickDialogLibrary {

    internal class BundleQD{
        
        private readonly static string ARG_TITLE = "title";
        private readonly static string ARG_MESSAGE = "message";
        private readonly static string ARG_POSITIVE_BT = "positive";
        private readonly static string ARG_NEGATIVE_BT = "negative";
        private readonly static string ARG_NEUTRAL_BT = "neutral";
        private readonly static string ARG_IS_CANCLELLABLE = "cancellable";
        private readonly static string ARG_REQUEST = "request";

        private readonly static string ARG_RESPONSE_TO_FRAGMENT = "reponse-to-framgnet";
        private readonly static string ARG_FRAGMENT_RESPONSE_TAG = "fragment-response-tag";
        private readonly static string ARG_FRAGMENT_RESPONSE_ID = "fragment-response-id";

        private readonly static string ARG_CONTROLLER = "controller-namespace.name";


        public readonly Bundle Args;

        public BundleQD(Bundle args) {
            this.Args = args;
        }


        public bool HasTitle {get { return Contains(ARG_TITLE); } }
        public string Title {
            set { SetStr(ARG_TITLE, value); }
            get { return GetStr(ARG_TITLE); }
        }

        public bool HasMessage { get { return Contains(ARG_MESSAGE); } }
        public string Message {
            get { return Args.GetString(ARG_MESSAGE); }
            set { Args.PutString(ARG_MESSAGE, value); }
        }

        public bool HasPositiveBT {get { return Contains(ARG_POSITIVE_BT); } }
        public string PositiveBT {
            get { return GetStr(ARG_POSITIVE_BT); }
            set { SetStr(ARG_POSITIVE_BT, value); }
        }

        public bool HasNegativeBT {get { return Contains(ARG_NEGATIVE_BT); } }
        public string NegativeBT {
            get { return GetStr(ARG_NEGATIVE_BT); }
            set { SetStr(ARG_NEGATIVE_BT, value); }
        }

        public bool HasNeutralBT {get { return Contains(ARG_NEUTRAL_BT); } }
        public string NeutralBT {
            get { return GetStr(ARG_NEUTRAL_BT); }
            set { SetStr(ARG_NEUTRAL_BT, value); }
        }

        public bool Cancellable {
            get { return Args.GetBoolean(ARG_IS_CANCLELLABLE, true); }
            set { Args.PutBoolean(ARG_IS_CANCLELLABLE, value); }
        }

        public int Request {
            get { return Args.GetInt(ARG_REQUEST); }
            set { Args.PutInt(ARG_REQUEST, value); }
        }

        public bool HasResponseFragmentID {get { return Contains(ARG_FRAGMENT_RESPONSE_ID); } }
        public int ResponseFragmentID {
            get { return Args.GetInt(ARG_FRAGMENT_RESPONSE_ID); }
            set { Args.PutInt(ARG_FRAGMENT_RESPONSE_ID, value); }
        }

        public bool HasResponseFragmentTag { get { return Contains(ARG_FRAGMENT_RESPONSE_TAG); } }
        public string ResponseFragmentTag {
            get { return GetStr(ARG_FRAGMENT_RESPONSE_TAG); }
            set { SetStr(ARG_FRAGMENT_RESPONSE_TAG, value); }
        }

        public bool ResponseToFragment {
            get { return Args.GetBoolean(ARG_RESPONSE_TO_FRAGMENT, false); }
            set { Args.PutBoolean(ARG_RESPONSE_TO_FRAGMENT, value); }
        }

        public bool HasController { get {return Contains(ARG_CONTROLLER); } }
        public System.Type Controller {
            get {
                string name = GetStr(ARG_CONTROLLER);
                return System.Type.GetType(name);
            }
            set { SetStr(ARG_CONTROLLER, value.AssemblyQualifiedName); }
        }

        private void SetStr(string key, string value) {
            Args.PutString(key, value);
        }

        private string GetStr(string key) {
            return Args.GetString(key);
        }


        private bool Contains(string key) {
            return Args.ContainsKey(key);
        }

    }


}