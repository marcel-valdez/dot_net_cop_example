namespace Game.Core
{
  using System;
  using System.Collections;
  using DependencyLocation;
  using Fasterflect;

  public static class RequestContext
  {

    /// <summary>
    /// Gets or sets the context getter.
    /// </summary>
    /// <value>
    /// The context getter.
    /// </value>
    public static Func<IDictionary> ContextGetter
    {
      get;
      set;
    }

    public static T Model<T>()
    {
      if (ContextGetter()["context"] == null)
      {
        object[] args = Dependency.Locator.GetConfiguration<object[]>("model-args");
        ContextGetter().Add("context", typeof(T).CreateInstance(args));
      }

      return (T)ContextGetter()["context"];
    }
  }
}
