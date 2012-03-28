using System.IO.IsolatedStorage;

namespace Garifzyanov.Toolkit.IsolatedStorage
{
	public class IsolatedStorageKeeper<T>
	{
		private readonly string _tokenKey;

		public IsolatedStorageKeeper(string tokenKey)
		{
			_tokenKey = tokenKey;
		}

		public bool IsTokenExist()
		{
			var settings = IsolatedStorageSettings.ApplicationSettings;
			return settings.Contains(_tokenKey);
		}

		public T Get()
		{
			var settings = IsolatedStorageSettings.ApplicationSettings;
			T token;
			settings.TryGetValue(_tokenKey, out token);
			return token;
		}

		public void Save(T token)
		{
			var settings = IsolatedStorageSettings.ApplicationSettings;
			settings[_tokenKey] = token;
			settings.Save();
		}

		public void RemoveToken()
		{
			var settings = IsolatedStorageSettings.ApplicationSettings;
			settings.Remove(_tokenKey);
			settings.Save();
		}
	}
}
