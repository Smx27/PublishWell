using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace API.Extension;
/// <summary>
/// This is a class to handle caching 
/// </summary>
public static class DistributedCacheExtensions
{
	/// <summary>
	/// Generic class to ser records in Redis caching
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="cache"></param>
	/// <param name="recordID"></param>
	/// <param name="Data"></param>
	/// <param name="expireTime"></param>
	/// <param name="unusedExpireTime"></param>
	/// <returns>Task: if this is complete or not</returns>
	public static async Task SetRecordsAsync<T>(this IDistributedCache cache, string recordID, T Data, TimeSpan? expireTime = null, TimeSpan? unusedExpireTime = null)
	{
		//Setting options for Distributed cache 
		var options = new DistributedCacheEntryOptions
		{
			SlidingExpiration = unusedExpireTime,
			AbsoluteExpirationRelativeToNow = expireTime ?? TimeSpan.FromSeconds(120),
		};
		// Converting data into json
		var jsonData = JsonSerializer.Serialize(Data);
		// Setting up data into caching 
		await cache.SetStringAsync(recordID, jsonData, options);
	}
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="cache"></param>
	/// <param name="recordID"></param>
	/// <returns></returns>
	public static async Task<T> GetRecordsAsync<T>(this IDistributedCache cache, string recordID)
	{
		// fetching the data using recordID
		var jsonData = await cache.GetStringAsync(recordID);

		//Checking if it is null just return default value
		if(jsonData is null)
		{
			return default(T);
		}
		// Sending the object from Cache
		return JsonSerializer.Deserialize<T>(jsonData);
	}
}
