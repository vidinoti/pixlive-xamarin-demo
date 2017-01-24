using System;
using UIKit;

using VdarSdkIos;

namespace PixLiveSdkDemoIos
{
	public partial class ViewController : VDARLiveAnnotationViewController, IRemoteControllerDelegate
	{
		protected ViewController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			VDARRemoteController.SharedInstance.Delegate = this;
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			VDARRemoteController.SharedInstance.Delegate = null;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		public void DidProgress(VDARRemoteController controller, float prc, bool isReady, string folder)
		{
			if (prc >= 1 || nfloat.IsNaN(prc) || nfloat.IsInfinity(prc))
			{
				SynchronizationLabel.Hidden = true;
				SynchronizationProgressView.Hidden = true;
			}
			else
			{
				SynchronizationLabel.Hidden = false;
				SynchronizationProgressView.Hidden = false;
				SynchronizationProgressView.Progress = prc;
			}
		}

	}
}
