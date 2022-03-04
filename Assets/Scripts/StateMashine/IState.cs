public interface IState
{
    public void Enter(Context context);
    public void LateUpdate(Context context);
    public void Update(Context context);
    public void Exit(Context context);
}