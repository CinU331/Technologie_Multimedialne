### Medication Tracker
- To build and run this project you will need __Microsoft Visual Studio__ with installed additional workload - __Mobile development with .NET__.  
- If you don't have the mentioned workload, you can download it with the __Visual Studio Installer__.  
- To run app on Android emulator you just need to open the project with Visual Studio and then select __Debug -> Start Without Debugging__ (Ctrl+F5). ( [More info about setting up the Android SDK](https://docs.microsoft.com/en-us/xamarin/android/get-started/installation/android-sdk?tabs=windows) )
- To pack the app in an APK file you have to open the project with Visual Studio, change the Solution Configuration to __Release__ and then __Build -> Archive...__ When the Archive Manager finish packing you only have to click __Distribute...__, add your keystore and choose a saving location. ( [More info about archiving apps](https://docs.microsoft.com/en-us/xamarin/android/deploy-test/release-prep/?tabs=windows#archive) )

### Warning &#9888;
The main feature, which is adding reminders __is not working any more__, because we stopped hosting our API.  
It was responible for sending the list of avaible medicines, so now when you try to add some reminder, you won't see any list of medicines to choose, so you can't add any new reminder.
