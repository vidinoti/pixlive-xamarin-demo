using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using Com.Vidinoti.Android.Vdarsdk;
using Com.Vidinoti.Android.Vdarsdk.Camera;
using System.Collections.Generic;
using Java.Lang;
using Java.Util;
using System;
using Android.Views;
using Java.Util.Concurrent.Atomic;

namespace PixLiveSdkDemo
{

	[Activity(Label = "PixLiveSdkDemo", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity, IVDARSDKControllerEventReceiver, IVDARRemoteControllerListener
	{

		private const string PixLiveMakerLicenseKey = "qv8db1pcnzc444ysnqtj";

		private const string Tag = "MainActivity";
		private VDARAnnotationView annotationView;
		private DeviceCameraImageSender imageSender;

		private View progressView;
		private ProgressBar progressBar;

		private AtomicBoolean syncInProgress = new AtomicBoolean(false);

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Start the SDK
			string arContentFileDir = ApplicationContext.FilesDir.AbsolutePath + "/arcontent";
			VDARSDKController sdk = VDARSDKController.StartSDK(this, arContentFileDir, PixLiveMakerLicenseKey);
			sdk.SetActivity(this);
			sdk.RegisterEventReceiver(this);
			// Setup the camera for the PixLive SDK
			imageSender = new DeviceCameraImageSender();
			Log.Debug(Tag, "Camera image sender: " + imageSender);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Retrieve and initialize the annotation view
			annotationView = (VDARAnnotationView) FindViewById(Resource.Id.myArView);
			annotationView.DarkScreenMode = false;
			annotationView.AnimationSpeed = 0.0f;

			progressView = FindViewById(Resource.Id.myProgressView);
			progressBar = (ProgressBar) FindViewById(Resource.Id.myProgressBar);

		}

		private void sync()
		{
			// Allow a single synchronization at a time
			if (syncInProgress.CompareAndSet(false, true))
			{
				List<VDARPrior> list = new List<VDARPrior>();
				// By default, it synchronizes with all contents
				// Uncomment the following for synchronizing with a given tag
				//list.Add(new VDARTagPrior("Tag1"));

				VDARSDKController.Instance.AddNewAfterLoadingTask(new Runnable(() => {
					VDARRemoteController.Instance.SyncRemoteContextsAsynchronouslyWithPriors(list, new SyncObserver(syncInProgress));
				}));
			}
		}

		private void displayProgress(int percent)
		{
			progressBar.Progress = percent;
			if (percent < 100)
			{
				progressView.Visibility = ViewStates.Visible;
			}
			else
			{
				progressView.Visibility = ViewStates.Gone;
			}
		}

		protected override void OnResume()
		{
			base.OnResume();
			// Initialize
			VDARSDKController.Instance.SetActivity(this);
			annotationView.OnResume();
			VDARRemoteController.Instance.AddProgressListener(this);

			sync();
		}

		protected override void OnPause()
		{
			base.OnPause();
			annotationView.OnPause();
			VDARRemoteController.Instance.RemoveProgressListener(this);
		}

		public void OnAnnotationsHidden()
		{
			Log.Info(Tag, "OnAnnotationsHidden");
		}

		public void OnCodesRecognized(IList<VDARCode> p0)
		{
			Log.Info(Tag, "OnCodesRecognized");
		}

		public void OnEnterContext(VDARContext p0)
		{
			Log.Info(Tag, "OnEnterContext");
		}

		public void OnExitContext(VDARContext p0)
		{
			Log.Info(Tag, "OnExitContext");
		}

		public void OnFatalError(string p0)
		{
			Log.Info(Tag, "OnFatalError");
		}

		public void OnPresentAnnotations()
		{
			Log.Info(Tag, "OnPresentAnnotations");
		}

		public void OnRequireSynchronization(IList<VDARPrior> p0)
		{
			Log.Info(Tag, "OnRequireSynchronization");
		}

		public void OnTrackingStarted(int p0, int p1)
		{
			Log.Info(Tag, "OnTrackingStarted");
		}

		public void OnSyncProgress(VDARRemoteController p0, float progress, bool p2, string p3)
		{
			displayProgress((int) progress);
		}
	}

	class SyncObserver : Java.Lang.Object, Java.Util.IObserver
	{
		private AtomicBoolean syncInProgress;

		public SyncObserver(AtomicBoolean syncInProgress)
		{
			this.syncInProgress = syncInProgress;
		}

		public void Update(Observable o, Java.Lang.Object arg)
		{
			VDARRemoteController.ObserverUpdateInfo info = (VDARRemoteController.ObserverUpdateInfo)arg;
			if (info.IsCompleted)
			{
				Log.Info("Sync", "Synced " + info.FetchedContexts.Count + " models.");
				syncInProgress.Set(false);
			}

		}
	}
}

