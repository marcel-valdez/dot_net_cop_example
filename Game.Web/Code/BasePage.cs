using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using DependencyLocation;
using Fasterflect;
using Game.Core;

namespace Game
{
  public abstract class BasePage : Page
  {
    protected T LazyLoad<T>(Expression<Func<T>> getter)
    {
      var propGetter = (MemberExpression)getter.Body;
      string memberName = propGetter.Member.Name;
      Func<T> compiled = getter.Compile();
      if (compiled() == null)
      {
        lock (this)
        {
          if (compiled() == null)
          {
            T value = Dependency.Locator.Create<T>(HttpContext.Current.Session.SessionID);
            this.SetFieldValue(memberName, value);
          }
        }
      }

      return compiled();
    }

    protected static void Log(string message, params object[] args)
    {
      Dependency.Locator.GetSingleton<ILog>().AddToLog(string.Format(message, args));
    }

    private PageCycle _CycleState = PageCycle.None;

    protected PageCycle CycleState
    {
      get
      {
        return _CycleState;
      }
    }

    protected override void OnPreInit(EventArgs e)
    {
      _CycleState = PageCycle.PreInit;
      base.OnPreInit(e);
    }

    protected override void OnInit(EventArgs e)
    {
      _CycleState = PageCycle.Init;
      base.OnInit(e);
    }

    protected override void OnInitComplete(EventArgs e)
    {
      _CycleState = PageCycle.InitComplete;
      base.OnInitComplete(e);
    }

    protected override void OnPreLoad(EventArgs e)
    {
      _CycleState = PageCycle.PreLoad;
      base.OnPreLoad(e);
    }

    protected override void OnLoad(EventArgs e)
    {
      _CycleState = PageCycle.Load;
      base.OnLoad(e);
    }

    protected override void OnLoadComplete(EventArgs e)
    {
      _CycleState = PageCycle.LoadComplete;
      base.OnLoadComplete(e);
    }

    protected override void OnPreRender(EventArgs e)
    {
      _CycleState = PageCycle.PreRender;
      base.OnPreRender(e);
    }

    protected override void OnPreRenderComplete(EventArgs e)
    {
      _CycleState = PageCycle.PreRender;
      base.OnPreRenderComplete(e);
    }

    protected override void OnError(EventArgs e)
    {
      _CycleState = PageCycle.Error;
      base.OnError(e);
    }

    protected override void OnUnload(EventArgs e)
    {
      _CycleState = PageCycle.Unload;
      base.OnUnload(e);
    }
  }

  public enum PageCycle
  {
    None = -1,
    PreInit = 0,
    Init = 1,
    InitComplete = 2,
    PreLoad = 3,
    Load = 4,
    LoadComplete = 5,
    PreRender = 6,
    RenderComplete = 7,
    Error = 8,
    Unload = 9
  }
}