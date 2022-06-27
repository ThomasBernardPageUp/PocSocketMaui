using System;
using Newtonsoft.Json;

namespace PocSocketMaui.Commons.Extensions
{
	public static class ObjectExtension
	{
		public static bool IsNull(this object obj)
		{
			return obj is null;
		}

		public static bool IsNotNull(this object obj)
		{
			return !obj.IsNull();
		}

		public static string ToJsonString(this object obj)
		{
			if (obj.IsNotNull())
				return JsonConvert.SerializeObject(obj);

			return string.Empty;
		}
	}
}

