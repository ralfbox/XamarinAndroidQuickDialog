using System;

using Android.Content;
using QuickDialogLibrary.Listeners;
using System.Reflection;
using Android.Util;
using QuickDialogLibrary.Core;
using System.Collections.Generic;

namespace QuickDialogLibrary.Support.V7 {
    internal class AnswerEngineer {
        private readonly MethodSearcherQDDetails Details;
        private readonly QuickDialog QuickDialog;


        public AnswerEngineer(QuickDialog builder) {
            this.QuickDialog = builder;
            object responseToObj = GetObjectForResponse(builder);
            int request = builder.BundleQD.Request;
            Details = new MethodSearcherQDDetails(responseToObj, request);
        }

        private object GetObjectForResponse(QuickDialog qd) {
            BundleQD args = qd.BundleQD;
            if (!args.ResponseToFragment)
                return qd.Activity;
            if (args.HasResponseFragmentTag)
                return qd.FragmentManager.FindFragmentByTag(args.ResponseFragmentTag);
            return qd.FragmentManager.FindFragmentById(args.ResponseFragmentID);
        }




        public void NeutralButtonClick(object sender, DialogClickEventArgs e) {
            InvokeMethod<NeutralButtonQD>();
        }

        public void NegativeClick(object sender, DialogClickEventArgs e) {
            InvokeMethod<NegativeButtonQD>();
        }

        public void PositiveButtonClick(object sender, DialogClickEventArgs e) {
            InvokeMethod<PositiveButtonQD>();
        }

        public void OnCancel(IDialogInterface dialog) {
            InvokeMethod<CancelQD>();
        }

        private void InvokeMethod<T>() where T : AttributeQuickDialog {
            MethodInfo method = new QDMethodSearcher<T>(Details).Execute();
            if (method != null) {
                try {
                    method.Invoke(Details.O, BuildResult(method));
                } catch (Exception e) {
                    Log.Error("QuickDialog", "Cannot invoke method: " + method.Name + "   " + e.Message, e.Message);
                }
            }
        }

        

        private object[] BuildResult(MethodInfo method) {
            ParameterInfo[] parameters = method.GetParameters();


            if (parameters.Length > 0) {
                List<object> ret = new List<object>();
                foreach (ParameterInfo parameter in parameters) {
                    Type parameterType = parameter.ParameterType;

                    if (parameter.ParameterType == QuickDialog.Controller.GetType()) {
                        ret.Add(QuickDialog.Controller);

                    } else if (parameterType == QuickDialog.GetType() ||
                        QuickDialog.GetType().IsSubclassOf(parameterType) ){
                        ret.Add(QuickDialog);
                    }
                }
                return ret.ToArray();
            }
            return null;
        }
    }

}