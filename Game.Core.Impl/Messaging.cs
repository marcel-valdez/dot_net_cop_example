namespace Game.Core.Impl
{
  using System;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  using DependencyLocation;
  using Game.Core;

  internal class Messaging : IMessaging, IDisposable
  {
    HandlerMap handlers;
    MessageMap messages;
    bool dispose = false;
    readonly AutoResetEvent semaphore;
    Container<Action> pendingTasks;

    public Messaging()
    {
      this.handlers = new HandlerMap();
      this.messages = new MessageMap();
      this.semaphore = new AutoResetEvent(false);
      this.pendingTasks = new Container<Action>();
      Task.Factory.StartNew(Publisher);
    }

    #region IMessaging Members
    private void Publisher()
    {
      int maxTasks = 8;//Task.Factory.Scheduler.MaximumConcurrencyLevel;
      Task[] workers = InitWorkers(maxTasks);

      while (!this.dispose)
      {
        if (!pendingTasks.HasElements)
        {
          semaphore.WaitOne(2000);
        }

        if (pendingTasks.HasElements)
        {
          Container<Action> cpPendingTasks = GetCopyOfPendingTasks();
          PublishMessages(workers, cpPendingTasks);
        }
      }
    }

    /// <summary>
    /// Initializes the workers.
    /// </summary>
    /// <param name="workerCount">The worker count.</param>
    /// <returns>A worker array</returns>
    private static Task[] InitWorkers(int workerCount)
    {
      Task[] workers = new Task[workerCount];
      for (int i = 0; i < workerCount; i++)
      {
        workers[i] = Task.Factory.StartNew(() =>
        {
        });
      }

      return workers;
    }

    /// <summary>
    /// Publishes the messages.
    /// </summary>
    /// <param name="workers">The workers.</param>
    /// <param name="cpPendingTasks">The pending tasks.</param>
    private static void PublishMessages(Task[] workers, Container<Action> pendingTasks)
    {
      // Ejecuta las tareas usando el máximo valor de concurrencia posible
      while (pendingTasks.HasElements)
      {
        int index = Task.WaitAny(workers);
        Action task = pendingTasks.Pop();
        workers[index].ContinueWith(_ => task());
      }
    }

    /// <summary>
    /// Gets the copy of pending tasks.
    /// </summary>
    /// <returns>The copy of pending tasks</returns>
    private Container<Action> GetCopyOfPendingTasks()
    {
      Container<Action> cpPendingTasks;
      lock (this.pendingTasks)
      {
        cpPendingTasks = this.pendingTasks.GetCopy();
        this.pendingTasks.Clear();
      }

      return cpPendingTasks;
    }

    /// <summary>
    /// Subscribes to the specified channel.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <param name="channel">The channel.</param>
    /// <param name="handler">The handler.</param>
    /// <param name="filter">The filter.</param>
    public void Subscribe<TMessage>(object channel, Action<object, TMessage> handler, Predicate<TMessage> filter)
    {
      this.handlers.AddHandler(channel, handler, filter);
    }

    /// <summary>
    /// Subscribes to the specified channel.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <param name="channel">The channel.</param>
    /// <param name="handler">The handler.</param>
    public void Subscribe<TMessage>(object channel, Action<object, TMessage> handler)
    {
      this.handlers.AddHandler(channel, handler, _ => true);
    }

    /// <summary>
    /// Unsubscribes from the specified channel.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <param name="channel">The channel.</param>
    /// <param name="handler">The handler.</param>
    public void Unsubscribe<TMessage>(object channel, Action<object, TMessage> handler)
    {
      this.handlers.Remove(channel, handler);
    }

    /// <summary>
    /// Publishes to the specified channel.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <param name="channel">The channel.</param>
    /// <param name="message">The message.</param>
    public void Publish<TMessage>(object channel, TMessage message)
    {
      Tuple<Action<object, TMessage>, Predicate<TMessage>>[]
          cpHandlers = this.handlers.GetHandlers<TMessage>(channel).ToArray();

      Action task = () => TaskAction<TMessage>(channel, message, cpHandlers);

      lock (this.pendingTasks)
      {
        this.pendingTasks.Add(task);
      }

      this.semaphore.Set();
    }

    /// <summary>
    /// Makes the task item.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <param name="channel">The channel.</param>
    /// <param name="message">The message.</param>
    /// <param name="cpHandlers">The copy of the handlers.</param>
    private static void TaskAction<TMessage>(object channel, TMessage message, Tuple<Action<object, TMessage>, Predicate<TMessage>>[] cpHandlers)
    {
      foreach (var handler in cpHandlers)
      {
        try
        {
          if (handler.Item2(message))
          {
            handler.Item1(channel, message);
          }
        }
        catch (Exception ex)
        {
          Dependency.Locator.GetSingleton<ILog>()
              .AddToLog(ex);
        }
      }
    }

    #endregion

    #region IDisposable Members
    public void Dispose()
    {
      this.dispose = true;
    }
    #endregion
  }
}