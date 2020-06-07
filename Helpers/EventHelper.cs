using System;
using System.Reflection;

namespace AssortedModdingTools.Extensions
{
	public static class EventHelper
	{
		public static void CallEvent<TEventType>(ref TEventType hook, TEventType value, string eventName, Type classType) where TEventType : MulticastDelegate
		{
			EventInfo eventInfo = classType.GetEvent(eventName);
			eventInfo.AddEventHandler(hook.Target, value);
		}
	}
}