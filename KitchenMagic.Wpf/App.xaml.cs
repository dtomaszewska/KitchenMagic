using KitchenMagic.Wpf.MvxApp;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Wpf.Views;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

namespace KitchenMagic.Wpf
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{

		bool _setupComplete;

		void DoSetup()
		{
			var presenter = new MvxSimpleWpfViewPresenter(MainWindow);

			var setup = new Setup(Dispatcher, presenter);
			setup.Initialize();

			var start = Mvx.Resolve<IMvxAppStart>();
			start.Start();

			_setupComplete = true;
		}


		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			if (!_setupComplete)
				DoSetup();
			SelectCulture(CultureInfo.InstalledUICulture.Name);
		}

		public static void SelectCulture(string culture)
		{
			if (String.IsNullOrEmpty(culture))
				return;

			//Copy all MergedDictionarys into a auxiliar list.
			var dictionaryList = Application.Current.Resources.MergedDictionaries.ToList();

			//Search for the specified culture.
			string requestedCulture = $"Resources/StringResources.{culture}.xaml";
			var resourceDictionary = dictionaryList.
				FirstOrDefault(d => d.Source.OriginalString == requestedCulture);

			if (resourceDictionary == null)
			{
				//If not found, select our default language.
				requestedCulture = "StringResources.xaml";
				resourceDictionary = dictionaryList.
					FirstOrDefault(d => d.Source.OriginalString == requestedCulture);
			}

			//If we have the requested resource, remove it from the list and place at the end.
			//Then this language will be our string table to use.
			if (resourceDictionary != null)
			{
				Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
				Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
			}

			//Inform the threads of the new culture.
			Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
		}
	}
}
