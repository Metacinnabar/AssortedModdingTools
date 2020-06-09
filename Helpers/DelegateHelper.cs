using System;

namespace AssortedModdingTools.Helpers
{
	public static class DelegateHelper
	{
		public static void AddToDelegate(ref Action hook, Action value)
		{
			if (hook == null)
				hook = value;
			else
				hook += value;
		}

		public static void AddToDelegate<T1>(ref Action<T1> hook, Action<T1> value)
		{
			if (hook == null)
				hook = value;
			else
				hook += value;
		}

		public static void AddToDelegate<TResult>(ref Func<TResult> hook, Func<TResult> value)
		{
			Func<TResult> source = value as Func<TResult>;

			if (hook == null)
				hook = source;
			else
				hook += source;
		}

		public static void AddToDelegate<T1, TResult>(ref Func<T1, TResult> hook, Func<T1, TResult> value)
		{
			Func<T1, TResult> source = value as Func<T1, TResult>;

			if (hook == null)
				hook = source;
			else
				hook += source;
		}

		public static void AddToDelegate<T1, T2, TResult>(ref Func<T1, T2, TResult> hook, Func<T1, T2, TResult> value)
		{
			Func<T1, T2, TResult> source = value as Func<T1, T2, TResult>;

			if (hook == null)
				hook = source;
			else
				hook += source;
		}
	}
}