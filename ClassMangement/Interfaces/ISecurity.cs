namespace ClassMangement.Interfaces
{
	public interface ISecurity<T, Y>
	{
		string Generate(T user);
		T Authenticate(Y value);
		T GetCurrentUser();
	}
}
