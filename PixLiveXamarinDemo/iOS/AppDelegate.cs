using Foundation;
using UIKit;

using CoreFoundation;
using System;
using VdarSdkIos;

namespace PixLiveSdkDemoIos
{

	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{

		// PixLive Maker License key associated with your account (get it from https://armanager.vidinoti.com)
		private const string PixLiveLicenseKey = "qv8db1pcnzc444ysnqtj";

		// flag indicating if a synchronization is in progress
		private bool syncing = false;

		public override UIWindow Window
		{
			get;
			set;
		}

		/**
		 * Downloads the contents from the PixLive Maker server
		 */
		private void sync()
		{
			if (syncing)
			{
				// A sync is already in progress, abort.
				return;
			}
			syncing = true;

			// Synchronize not before the SDK has been loaded
			NSOperationQueue queue = VDARSDKController.SharedInstance.AfterLoadingQueue;
			queue.AddOperation(() =>
			{
				DispatchQueue.MainQueue.DispatchAsync(() =>
				{
					// By default synchronize without tags (i.e. with all contents).
					VDARPrior[] priors = { };

					// Uncomment the following for syncing with tags
					// VDARPrior[] priors = { VDARTagPrior.TagWithName("tag1") };

					VDARRemoteController.SharedInstance.SyncRemoteModelsAsynchronouslyWithPriors(priors, (result, error) =>
					{
						Console.WriteLine("Following models have been synced: " + result);
						if (error != null)
						{
							Console.WriteLine("Error while syncing: " + error);
						}
						Console.WriteLine("Sync done");
						syncing = false;
					});
				});
			});
		}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			Console.WriteLine("PixLive SDK Version: " + VDARSDKController.VDARSDKVersion);

			// Get folder where the contents will be downloaded
			string modelDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/models";

			// Start the PixLive SDK
			VDARSDKController.StartSDK(modelDir, PixLiveLicenseKey);

			// Initialize the camera image source
			VDARSDKController.SharedInstance.ImageSender = new VDARCameraImageSource();

			return true;
		}

		public override void OnResignActivation(UIApplication application)
		{
			VDARSDKController.SharedInstance.Save();
		}

		public override void DidEnterBackground(UIApplication application)
		{
			VDARSDKController.SharedInstance.Save();
		}

		public override void WillEnterForeground(UIApplication application)
		{

		}

		public override void OnActivated(UIApplication application)
		{
			// Synchronize the contents when the application comes in the foreground
			sync();
		}

		public override void WillTerminate(UIApplication application)
		{

		}
	}
}

