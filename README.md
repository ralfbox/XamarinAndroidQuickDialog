# Xamarin AndroidQuickDialog
Andorid library to the creation of dialogs. With this library can be created very quickly while keeping the code clean.

## Motivation
In the operating system Andorid creating the alert dialog that have support for screen rotation is very tiring. To create this we need to create a new static class based on DialogFragment and in it AlertDialog. When we get an answer to the Activity once again we have to write quite a lot of code. We need to call getActivity (), then cast it to the appropriate class, and then call the appropriate method. An even greater problem is when we want to answer to fragment. It is very sluggish!
So I decided to write a library that makes it easy to create Alert dialog with support for screen rotation and returning responses to the Activity or Fragment.

## Usage
Install `Xamarin.Android.Support.v7.AppCompat` from `NuGet Package Manager`  
(`Tools -> NuGet Package Manager -> Manage NuGet Packages For Solution...`)  

Download `QuickDialogLibrary.dll` and add to your solution.  
(`Solution Explorer -> your solution -> References (click right mouse) -> Add Reference -> Browse... -> select quickDialogLibrary.dll`)

  
```Cs
using Android.Support.V4.App;
using Android.Support.V7.App;
using QuickDialogLibrary.Listeners;
using QuickDialogLibrary.Support.V7;

namespace Sample{
	public class MainActivity extends AppCompatActivity {	
        	private const int QD_REQUEST_ALERT_1 = 1;
		(...)
	
	//Create dialog
		private void createDialog(){
			new QuickDialog.Builder(this, QD_REQUEST_ALERT_1)
				.Title("Alert 1")
				.Message("Any massage")
				.PositiveButton("Positive")
				.NegativeButton("Negative")
				.Build()
				.Show(SupportFragmentManager, "err");
		}
		
	//create method for positive button click
		[PositiveButtonQD(QD_REQUEST_ALERT_1)]
		private void AnyName_f_g_Dupa1() {
				ShowText("PositiveButton alert 1");
		}
		
	//create method for negative button click
		[NegativeButtonQD(QD_REQUEST_ALERT_1)]
        private void OnNegativeClickAlert1() {
            ShowText("cancel alert 1");
        }
        
        private void ShowText(string text) {
            Toast.MakeText(this, text, ToastLength.Long).Show();
        }
	}
}
```


## License
Copyright (c) 2016, Rafa³ Pude³ko

You should but you don't have to mention it in application UI with string **"Used QuickQialog (c) 2016, Rafa³ Pude³ko"** (e.g. in "About" section).

Licensed under the [BSD 3-clause](http://www.opensource.org/licenses/BSD-3-Clause)