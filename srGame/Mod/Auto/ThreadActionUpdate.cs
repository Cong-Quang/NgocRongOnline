using System.Threading;

/// <summary>
/// Lớp abstract để quản lý các hành động thực thi bằng thread với khả năng tự cập nhật.
/// </summary>
/// <typeparam name="T">Kiểu của lớp kế thừa.</typeparam>
public abstract class ThreadActionUpdate<T> : ThreadAction<T> where T : ThreadActionUpdate<T>, new()
{
    /// <summary>
    /// Trạng thái của hành động.
    /// </summary>
    public new bool IsActing { get; set; }

    /// <summary>
    /// Thời gian chờ giữa các lần cập nhật.
    /// </summary>
    public abstract int Interval { get; }

    /// <summary>
    /// Thực thi hành động cập nhật.
    /// </summary>
    protected override void Action()
    {
        while (IsActing)
        {
            Update();
            Thread.Sleep(Interval);
        }
    }

    /// <summary>
    /// Phương thức cập nhật hành động.
    /// </summary>
    protected abstract void Update();

    /// <summary>
    /// Chuyển đổi trạng thái của hành động.
    /// </summary>
    /// <param name="isActing">Trạng thái mới của hành động. Nếu giá trị là null, trạng thái của hành động sẽ đảo ngược.</param>
    public void Toggle(bool? isActing = null)
    {
        if (isActing == null)
            IsActing = !IsActing;
        else
            IsActing = (bool)isActing;

        if (IsActing)
            PerformAction();
        else
        {
            if (base.IsActing)
                threadAction.Abort();
        }
    }
}
