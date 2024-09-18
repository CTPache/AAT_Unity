public class OffsetedArray<T> where T : class
{
	private T[] array_;

	private int offset_;

	public T this[int index]
	{
		get
		{
			return array_[offset_ + index];
		}
	}

	public OffsetedArray(T[] array, int offset)
	{
		array_ = array;
		offset_ = offset;
	}
}
