package mono;

import java.io.*;
import java.lang.String;
import java.util.Locale;
import java.util.HashSet;
import java.util.zip.*;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.res.AssetManager;
import android.util.Log;
import mono.android.Runtime;

public class MonoPackageManager {

	static Object lock = new Object ();
	static boolean initialized;

	static android.content.Context Context;

	public static void LoadApplication (Context context, ApplicationInfo runtimePackage, String[] apks)
	{
		synchronized (lock) {
			if (context instanceof android.app.Application) {
				Context = context;
			}
			if (!initialized) {
				android.content.IntentFilter timezoneChangedFilter  = new android.content.IntentFilter (
						android.content.Intent.ACTION_TIMEZONE_CHANGED
				);
				context.registerReceiver (new mono.android.app.NotifyTimeZoneChanges (), timezoneChangedFilter);
				
				System.loadLibrary("monodroid");
				Locale locale       = Locale.getDefault ();
				String language     = locale.getLanguage () + "-" + locale.getCountry ();
				String filesDir     = context.getFilesDir ().getAbsolutePath ();
				String cacheDir     = context.getCacheDir ().getAbsolutePath ();
				String dataDir      = getNativeLibraryPath (context);
				ClassLoader loader  = context.getClassLoader ();

				Runtime.init (
						language,
						apks,
						getNativeLibraryPath (runtimePackage),
						new String[]{
							filesDir,
							cacheDir,
							dataDir,
						},
						loader,
						new java.io.File (
							android.os.Environment.getExternalStorageDirectory (),
							"Android/data/" + context.getPackageName () + "/files/.__override__").getAbsolutePath (),
						MonoPackageManager_Resources.Assemblies,
						context.getPackageName ());
				
				mono.android.app.ApplicationRegistration.registerApplications ();
				
				initialized = true;
			}
		}
	}

	public static void setContext (Context context)
	{
		// Ignore; vestigial
	}

	static String getNativeLibraryPath (Context context)
	{
	    return getNativeLibraryPath (context.getApplicationInfo ());
	}

	static String getNativeLibraryPath (ApplicationInfo ainfo)
	{
		if (android.os.Build.VERSION.SDK_INT >= 9)
			return ainfo.nativeLibraryDir;
		return ainfo.dataDir + "/lib";
	}

	public static String[] getAssemblies ()
	{
		return MonoPackageManager_Resources.Assemblies;
	}

	public static String[] getDependencies ()
	{
		return MonoPackageManager_Resources.Dependencies;
	}

	public static String getApiPackageName ()
	{
		return MonoPackageManager_Resources.ApiPackageName;
	}
}

class MonoPackageManager_Resources {
	public static final String[] Assemblies = new String[]{
		/* We need to ensure that "MovieReminder.Droid.dll" comes first in this list. */
		"MovieReminder.Droid.dll",
		"System.Runtime.dll",
		"Xamarin.Android.Support.v4.dll",
		"Xamarin.Android.Support.Vector.Drawable.dll",
		"Xamarin.Android.Support.Animated.Vector.Drawable.dll",
		"Xamarin.Android.Support.v7.AppCompat.dll",
		"Xamarin.Android.Support.v7.RecyclerView.dll",
		"Xamarin.Android.Support.Design.dll",
		"Xamarin.Android.Support.v7.CardView.dll",
		"Xamarin.Android.Support.v7.MediaRouter.dll",
		"Microsoft.Practices.ServiceLocation.dll",
		"Microsoft.Practices.Unity.dll",
		"Prism.dll",
		"Prism.Forms.dll",
		"Prism.Unity.Forms.dll",
		"ModernHttpClient.dll",
		"OkHttp.dll",
		"Newtonsoft.Json.dll",
		"FormsViewGroup.dll",
		"Xamarin.Forms.Core.dll",
		"Xamarin.Forms.Platform.Android.dll",
		"Xamarin.Forms.Platform.dll",
		"Xamarin.Forms.Xaml.dll",
		"Xamarin.Forms.Pages.dll",
		"MovieReminder.dll",
		"System.Threading.dll",
		"System.Collections.dll",
		"System.Collections.Concurrent.dll",
		"System.Diagnostics.Debug.dll",
		"System.Reflection.dll",
		"System.Linq.dll",
		"System.Runtime.InteropServices.dll",
		"System.Runtime.Extensions.dll",
		"System.Reflection.Extensions.dll",
		"System.Linq.Expressions.dll",
		"System.Resources.ResourceManager.dll",
		"System.Globalization.dll",
		"System.Diagnostics.Tools.dll",
		"System.ObjectModel.dll",
		"System.Threading.Tasks.dll",
		"System.IO.dll",
		"System.Xml.XDocument.dll",
		"System.Runtime.Serialization.Primitives.dll",
		"System.Dynamic.Runtime.dll",
		"System.Text.RegularExpressions.dll",
		"System.Xml.ReaderWriter.dll",
		"System.Text.Encoding.dll",
		"System.Text.Encoding.Extensions.dll",
		"System.ComponentModel.dll",
		"MovieReminder_Models.dll",
		"Plugin.Calendars.Abstractions.dll",
		"Plugin.Settings.dll",
		"Plugin.Settings.Abstractions.dll",
	};
	public static final String[] Dependencies = new String[]{
	};
	public static final String ApiPackageName = "Mono.Android.Platform.ApiLevel_24";
}
