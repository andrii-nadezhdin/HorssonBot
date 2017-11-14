using System.Collections.Concurrent;

namespace App.Core
{
	public class BotState
	{
		public static readonly BotState Instance = new BotState();

		private readonly ConcurrentDictionary<string, object> _workingChannels = new ConcurrentDictionary<string, object>();

		public void Set(string channelId, string parameter, object value)
		{
			_workingChannels.AddOrUpdate($"{channelId}.{parameter}", value, (key, oldValue) => value);
		}

		public T Get<T>(string channelId, string parameter, T defaultValue)
		{
			var value = _workingChannels.GetOrAdd($"{channelId}.{parameter}", defaultValue);
			return (T)value;
		}

		public void Clear(string channelId, string parameter)
		{
			_workingChannels.TryRemove($"{channelId}.{parameter}", out _);
		}

	}
}