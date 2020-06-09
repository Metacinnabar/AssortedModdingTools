using System;
using System.Reflection;

namespace AssortedModdingTools.Helpers
{
	public static class EventHelper
	{
		public static void CallEvent<TEventType>(ref TEventType hook, TEventType value, string eventName, Type classType) where TEventType : MulticastDelegate
		{
			EventInfo eventInfo = classType.GetEvent(eventName);
			eventInfo.AddEventHandler(hook.Target, value);
		}

		public static void InvokeEvent<TClass>(TClass instance, string eventName, object[] eventParams = null)
		{
			MulticastDelegate eventDelagate = (MulticastDelegate)typeof(TClass).GetField(eventName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance);

			Delegate[] delegates = eventDelagate.GetInvocationList();
			object[] parameters = eventParams ?? new object[] { };

			foreach (Delegate dlg in delegates)
			{
				dlg.Method.Invoke(dlg.Target, parameters);
			}
		}
	}
}