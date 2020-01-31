package mono;

import java.io.*;
import java.lang.String;
import java.util.Locale;
import java.util.HashSet;
import java.util.zip.*;
import java.util.Arrays;
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

				Locale locale       = Locale.getDefault ();
				String language     = locale.getLanguage () + "-" + locale.getCountry ();
				String filesDir     = context.getFilesDir ().getAbsolutePath ();
				String cacheDir     = context.getCacheDir ().getAbsolutePath ();
				String dataDir      = getNativeLibraryPath (context);
				ClassLoader loader  = context.getClassLoader ();
				java.io.File external0 = android.os.Environment.getExternalStorageDirectory ();
				String externalDir = new java.io.File (
							external0,
							"Android/data/" + context.getPackageName () + "/files/.__override__").getAbsolutePath ();
				String externalLegacyDir = new java.io.File (
							external0,
							"../legacy/Android/data/" + context.getPackageName () + "/files/.__override__").getAbsolutePath ();

				System.loadLibrary("monodroid");

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
						new String[] {
							externalDir,
							externalLegacyDir
						},
						MonoPackageManager_Resources.Assemblies,
						context.getPackageName (),
						android.os.Build.VERSION.SDK_INT,
						mono.android.app.XamarinAndroidEnvironmentVariables.Variables);
				
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
		/* We need to ensure that "RailTraceMobile.Android.dll" comes first in this list. */
		"RailTraceMobile.Android.dll",
		"CsvTextFieldParser.dll",
		"Fabulous.Core.dll",
		"Fabulous.CustomControls.dll",
		"Fabulous.Maps.dll",
		"FormsViewGroup.dll",
		"FSharp.Core.dll",
		"Google.Api.CommonProtos.dll",
		"Google.Api.Gax.dll",
		"Google.Api.Gax.Grpc.dll",
		"Google.Apis.Auth.dll",
		"Google.Apis.Auth.PlatformServices.dll",
		"Google.Apis.Core.dll",
		"Google.Apis.dll",
		"Google.Cloud.Speech.V1.dll",
		"Google.LongRunning.dll",
		"Google.Protobuf.dll",
		"Grpc.Auth.dll",
		"Grpc.Core.dll",
		"Microcharts.dll",
		"Microcharts.Droid.dll",
		"Microcharts.Forms.dll",
		"Microsoft.AppCenter.Analytics.Android.Bindings.dll",
		"Microsoft.AppCenter.Analytics.dll",
		"Microsoft.AppCenter.Android.Bindings.dll",
		"Microsoft.AppCenter.Crashes.Android.Bindings.dll",
		"Microsoft.AppCenter.Crashes.dll",
		"Microsoft.AppCenter.dll",
		"Microsoft.AppCenter.Push.Android.Bindings.dll",
		"Microsoft.AppCenter.Push.dll",
		"Microsoft.Azure.Mobile.Client.dll",
		"Microsoft.CognitiveServices.Speech.csharp.dll",
		"Microsoft.Extensions.Caching.Abstractions.dll",
		"Microsoft.Extensions.Caching.Memory.dll",
		"Microsoft.Extensions.DependencyInjection.Abstractions.dll",
		"Microsoft.Extensions.Options.dll",
		"Microsoft.Extensions.PlatformAbstractions.dll",
		"Microsoft.Extensions.Primitives.dll",
		"Microsoft.ProjectOxford.Face.dll",
		"Newtonsoft.Json.dll",
		"OxyPlot.dll",
		"OxyPlot.Xamarin.Android.dll",
		"OxyPlot.Xamarin.Forms.dll",
		"OxyPlot.Xamarin.Forms.Platform.Android.dll",
		"PCLCrypto.dll",
		"PCLStorage.Abstractions.dll",
		"PCLStorage.dll",
		"PInvoke.BCrypt.dll",
		"PInvoke.Kernel32.dll",
		"PInvoke.NCrypt.dll",
		"PInvoke.Windows.Core.dll",
		"Plugin.CurrentActivity.dll",
		"Plugin.Media.dll",
		"Plugin.Messaging.Abstractions.dll",
		"Plugin.Messaging.dll",
		"Plugin.Permissions.dll",
		"SharpNL.dll",
		"SharpSimpleNLG.dll",
		"SkiaSharp.dll",
		"SkiaSharp.Views.Android.dll",
		"SkiaSharp.Views.Forms.dll",
		"SQLite-net.dll",
		"SQLitePCLRaw.batteries_green.dll",
		"SQLitePCLRaw.batteries_v2.dll",
		"SQLitePCLRaw.core.dll",
		"SQLitePCLRaw.lib.e_sqlite3.dll",
		"SQLitePCLRaw.provider.e_sqlite3.dll",
		"System.Interactive.Async.dll",
		"System.Runtime.CompilerServices.Unsafe.dll",
		"System.Runtime.Loader.dll",
		"System.Security.AccessControl.dll",
		"System.Security.Permissions.dll",
		"System.Security.Principal.Windows.dll",
		"Universal.Common.dll",
		"Universal.Common.Net.Http.dll",
		"Universal.Common.Reflection.dll",
		"Universal.Common.Serialization.dll",
		"Universal.Microsoft.CognitiveServices.dll",
		"Universal.Microsoft.CognitiveServices.SpeakerRecognition.dll",
		"Validation.dll",
		"Xamarin.Android.Arch.Core.Common.dll",
		"Xamarin.Android.Arch.Lifecycle.Common.dll",
		"Xamarin.Android.Arch.Lifecycle.Runtime.dll",
		"Xamarin.Android.Support.Animated.Vector.Drawable.dll",
		"Xamarin.Android.Support.Annotations.dll",
		"Xamarin.Android.Support.Compat.dll",
		"Xamarin.Android.Support.Core.UI.dll",
		"Xamarin.Android.Support.Core.Utils.dll",
		"Xamarin.Android.Support.CustomTabs.dll",
		"Xamarin.Android.Support.Design.dll",
		"Xamarin.Android.Support.Fragment.dll",
		"Xamarin.Android.Support.Media.Compat.dll",
		"Xamarin.Android.Support.Transition.dll",
		"Xamarin.Android.Support.v4.dll",
		"Xamarin.Android.Support.v7.AppCompat.dll",
		"Xamarin.Android.Support.v7.CardView.dll",
		"Xamarin.Android.Support.v7.MediaRouter.dll",
		"Xamarin.Android.Support.v7.Palette.dll",
		"Xamarin.Android.Support.v7.RecyclerView.dll",
		"Xamarin.Android.Support.Vector.Drawable.dll",
		"Xamarin.Essentials.dll",
		"Xamarin.Firebase.Common.dll",
		"Xamarin.Firebase.Iid.dll",
		"Xamarin.Firebase.Messaging.dll",
		"Xamarin.Forms.Core.dll",
		"Xamarin.Forms.Maps.Android.dll",
		"Xamarin.Forms.Maps.dll",
		"Xamarin.Forms.Platform.Android.dll",
		"Xamarin.Forms.Platform.dll",
		"Xamarin.Forms.Xaml.dll",
		"Xamarin.GooglePlayServices.Base.dll",
		"Xamarin.GooglePlayServices.Basement.dll",
		"Xamarin.GooglePlayServices.Maps.dll",
		"Xamarin.GooglePlayServices.Tasks.dll",
	};
	public static final String[] Dependencies = new String[]{
	};
	public static final String ApiPackageName = "Mono.Android.Platform.ApiLevel_27";
}
