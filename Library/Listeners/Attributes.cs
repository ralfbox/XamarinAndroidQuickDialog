

namespace QuickDialogLibrary.Listeners {

    public abstract class AttributeQuickDialog: System.Attribute {
        public readonly int Request;

        public AttributeQuickDialog(int request) {
            Request = request;
        }
    }



    public class PositiveButtonQD: AttributeQuickDialog {
        public PositiveButtonQD(int request) : base(request) { }
    }

    public class NegativeButtonQD: AttributeQuickDialog {
        public NegativeButtonQD(int request) : base(request) { }
    }

    public class NeutralButtonQD: AttributeQuickDialog {
        public NeutralButtonQD(int request) : base(request) { }
    }

    public class CancelQD: AttributeQuickDialog {
        public CancelQD(int request) : base(request) { }
    }

}