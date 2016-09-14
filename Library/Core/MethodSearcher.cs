using QuickDialogLibrary.Support.V7;
using System;
using System.Reflection;

namespace QuickDialogLibrary.Core {
    

    abstract class MethodSearcherEngineer<T> where T:Attribute {
        private object O;

        public MethodSearcherEngineer(object o) {
            O = o;
        }

        public MethodInfo Execute() {
            Type type = O.GetType();
            MethodInfo[] methosds = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            foreach (MethodInfo method in methosds) {
                T attr = method.GetCustomAttribute<T>();
                if (attr != null && CheckValuesAttribute(attr))
                    return method;
                    
            }
            return null;
        }

        internal abstract bool CheckValuesAttribute(T attr);

    }

    internal class QDMethodSearcher<T>: MethodSearcherEngineer<T> where T:AttributeQuickDialog{
        int Request;
        public QDMethodSearcher(MethodSearcherQDDetails details) : base(details.O) {
            Request = details.Request;
        }

        internal override bool CheckValuesAttribute(T attr) {
            return attr.Request == Request;
        }

    }

    internal class MethodSearcherQDDetails {
        public object O { get; }
        public int Request { get; }

        public MethodSearcherQDDetails(object o, int request) {
            O = o;
            Request = request;
        }
    }

    //abstract class QDMethodSearcher: MethodSearcherEngineer {
    //    private readonly int Request;

    //    public QDMethodSearcher(object o, int request) : base(o) {
    //        Request = request;
    //    }

    //    protected override bool IsThisMethode(MethodInfo method) {
    //        AttributeQuickDialog aqd = GetAttributeQuickDialog(method);
    //        return aqd != null && aqd.Request == Request;
    //    }

    //    protected abstract AttributeQuickDialog GetAttributeQuickDialog(MethodInfo method);
    //}




    //class PositiveBTMethodSearcher: QDMethodSearcher {
    //    public PositiveBTMethodSearcher(object o, int request) : base(o, request ) { }

    //    protected override AttributeQuickDialog GetAttributeQuickDialog(MethodInfo method) {
    //        return method.GetCustomAttribute<PositiveButtonQD>();
    //    }
    //}

    //class NegativeBTMethodSearcher: QDMethodSearcher {
    //    public NegativeBTMethodSearcher(object o, int request) : base(o, request) { }

    //    protected override AttributeQuickDialog GetAttributeQuickDialog(MethodInfo method) {
    //        return method.GetCustomAttribute<NegativeButtonQD>();
    //    }
    //}

    //class NeutralBTMethodSearcher: QDMethodSearcher {
    //    public NeutralBTMethodSearcher(object o, int request) : base(o, request) { }

    //    protected override AttributeQuickDialog GetAttributeQuickDialog(MethodInfo method) {
    //        return method.GetCustomAttribute<NeutralButtonQD>();
    //    }
    //}

    //class CancelBTMethodSearcher: QDMethodSearcher {
    //    public CancelBTMethodSearcher(object o, int request) : base(o, request) { }

    //    protected override AttributeQuickDialog GetAttributeQuickDialog(MethodInfo method) {
    //        return method.GetCustomAttribute<CancelQD>();
    //    }
    //}
}