public static class GSStructUtility
{
	public static void FillArrayNewInstance<T>(T[] array) where T : class, new()
	{
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new T();
		}
	}
}
