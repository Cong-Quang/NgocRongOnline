using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
public abstract class ThreadAction<T> where T : ThreadAction<T>, new()
{
    public static T gI { get; } = new();
    public bool IsActing => this.threadAction?.IsAlive == true;
    //// <summary>
    /// Thread sử dụng để thực thi hành động.
    /// </summary>
    protected Thread threadAction;
    /// <summary>
    /// Hành động cần thực hiện.
    /// </summary>
    protected abstract void action();
    /// <summary>
    /// Thực thi hành động bằng thread của instance.
    /// </summary>
    public void performAction()
    {
        if (this.IsActing)
            this.threadAction.Abort();

       this.executeAction();
    }
    /// <summary>
    /// Sử dụng thread của instance để thực thi hành động.
    /// </summary>
    /// <param name="isReturn">Dùng check return của action khi khác thread.</param>
    protected void executeAction()
    {
        // Không thực hiện hành động trong luồng khác
        if (Thread.CurrentThread != this.threadAction)
        {
            this.threadAction = new Thread(this.executeAction)
            {
                IsBackground = true
            };
            this.threadAction.Start();
            return;
        }
        this.action();
    }
}