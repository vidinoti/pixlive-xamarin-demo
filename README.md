# pixlive-xamarin-demo
Xamarin demo application (Android + iOS) using PixLive SDK for Augmented Reality

## How to get started

1. Copy and paste the PixLive SDK dlls (`VdarSdkIos.dll`, `VdarSdkAndroid.dll`, `AndroidDependencyPdfium.dll`, `AndroidDependencyPdfViewer.dll`) into the folder `PixLiveSDK_Xamarin`. If you don't have the DLL files, contact support@vidinoti.com.
2. Open the solution `PixLiveXamarinDemo` with Xamarin Studio.
3. The solution contains an Android and iOS project. You can simply run any of the two projects.
4. Scan the below demo image.

![Demo image](demo-image.jpg)

## Integrate the application with PixLive Maker

1. Log in or sign up to [PixLive Maker](https://armanager.vidinoti.com)
2. Retrieve your license key in the section "PixLive SDK > My licenses". This license key is used for linking your application with your PixLive Maker account. Use it when starting the SDK.
    - iOS: see the variable `PixLiveLicenseKey` in `PixLiveXamarinDemo/iOS/AppDelegate.cs`
    - Android: see `PixLiveMakerLicenseKey` in `PixLiveXamarinDemo/Droid/MainActivity.cs`
3. Add your application in PixLive Maker. If you don't do that, you won't be able to synchronize correctly.
    - In PixLive Maker, open the page "My Applications" under the section "PixLive SDK"
    - Add an entry for each of your application (both Android and iOS). The app identifier must match your application id (Android = package name, iOS = bundleId).
4. Once you have done the above operations, you are ready to use the SDK as demonstrated in the example projects.