using System.Threading;
public abstract class ThreadActionUpdate<T> : ThreadAction<T> where T : ThreadActionUpdate<T>, new()
{
    private bool isActing;

    public new bool IsActing => isActing;

    /// <summary>
    /// Thời gian nghỉ giữa các lần thực thi.
    /// </summary>
    public abstract int Interval { get; }

    protected override void action()
    {
        while (isActing)
        {
            update();
            Thread.Sleep(Interval);
        }
    } 
    /// <summary>
    /// Hành động thực hiện.
    /// </summary>
    protected abstract void update();
    /// <summary>
    /// Chuyển đổi trạng thái hành động
    /// </summary>
    /// <param name="isActing">Trạng thái hành động muốn chuyển đổi, nếu null thì sẽ đổi qua lại giữa bật và tắt</param>
    public void toggle(bool? isActing = null)
    {
        if (isActing == null)
        {
            isActing = !this.isActing;
        }
        if (this.isActing = (bool)isActing)
        {
            this.performAction();
        }
        else
        {
            if (base.IsActing)
            {
                this.threadAction.Abort();
            }
        }
    }
}