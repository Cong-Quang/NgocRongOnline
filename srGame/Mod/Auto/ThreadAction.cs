using System.Threading;

/// <summary>
/// Lớp abstract để quản lý các hành động thực thi bằng thread.
/// </summary>
/// <typeparam name="T">Kiểu của lớp kế thừa.</typeparam>
public abstract class ThreadAction<T> where T : ThreadAction<T>, new()
{
    /// <summary>
    /// Singleton instance của lớp kế thừa.
    /// </summary>
    public static T gI { get; } = new T();

    /// <summary>
    /// Xác định xem hành động đang được thực thi hay không.
    /// </summary>
    public bool IsActing => threadAction?.IsAlive == true;

    /// <summary>
    /// Thread sử dụng để thực thi hành động.
    /// </summary>
    protected Thread threadAction;

    /// <summary>
    /// Phương thức trừu tượng để định nghĩa hành động cụ thể.
    /// </summary>
    protected abstract void Action();

    /// <summary>
    /// Thực thi hành động bằng thread của instance.
    /// </summary>
    public void PerformAction()
    {
        if (IsActing)
            threadAction.Abort();

        ExecuteAction();
    }

    /// <summary>
    /// Sử dụng thread của instance để thực thi hành động.
    /// </summary>
    protected void ExecuteAction()
    {
        // Nếu đang không ở trong thread của instance, tạo một thread mới và thực thi hành động.
        if (Thread.CurrentThread != threadAction)
        {
            threadAction = new Thread(ExecuteAction)
            {
                IsBackground = true
            };
            threadAction.Start();
            return;
        }

        // Thực thi hành động cụ thể.
        Action();
    }
}
