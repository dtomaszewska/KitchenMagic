namespace KitchenMagic.Common
{
	public static class Configuration
	{
		public const string GoogleClientID = "719031588943-iv0fceo2tiqenac31bms17eos47mspq8.apps.googleusercontent.com";
		public const string GoogleClientSecret = "nq1STeDNMzsjE9iKZ7-Qv1_K";
		public const string GoogleAuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
		public const string GoogleAppDataAccessEndpoint = "https://www.googleapis.com/auth/drive.appfolder";

		public static string AccessToken { get; set; }

		public const string ApplicationName = "Kitchen Magic";
		public const string WPFCredentialPath = "Kitchen Magic";
		public const string DriveAplicationDirectory = "KitchenMagic";
		public const string DriveAplicationDb = "recipiesDb.sqlite";

	}
}
