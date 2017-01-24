// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace PixLiveSdkDemoIos
{
	[Register("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode("iOS Designer", "1.0")]
		UIKit.UILabel SynchronizationLabel { get; set; }

		[Outlet]
		[GeneratedCode("iOS Designer", "1.0")]
		UIKit.UIProgressView SynchronizationProgressView { get; set; }

		void ReleaseDesignerOutlets()
		{
			if (SynchronizationLabel != null)
			{
				SynchronizationLabel.Dispose();
				SynchronizationLabel = null;
			}

			if (SynchronizationProgressView != null)
			{
				SynchronizationProgressView.Dispose();
				SynchronizationProgressView = null;
			}
		}
	}
}