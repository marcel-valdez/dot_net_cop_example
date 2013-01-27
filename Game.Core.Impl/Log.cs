using FilePath = System.IO.Path;
namespace Game.Core.Impl
{
  using System;
  using System.IO;
  using System.Reflection;

  internal class Log : ILog
  {
    private const int MAX_LOG_SIZE = 1024 * 1024 * 5;//bytes (5 Mb)
    private string path = ".";
    private readonly static object LogLock = new object();

    #region Properties
    public string Path
    {
      get
      {
        return this.path;
      }
      set
      {
        this.path = value;
      }
    }

    public bool DebugOn
    {
      get;
      set;
    }
    #endregion

    #region Interface Methods
    /// <summary>
    /// Adds to log.
    /// </summary>
    /// <param name="exc">The exc.</param>
    /// <returns></returns>
    public bool AddToLog(Exception exc)
    {
      string caller = Assembly.GetCallingAssembly().GetName().Name;
      if (exc.TargetSite != null)
      {
        caller += "." + exc.TargetSite.Name;
      }

      return this.AddToLog(caller + ": ", exc);
    }

    /// <summary>
    /// Adds Exception and its inner exceptions to the log.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="exc">The exception.</param>
    /// <returns>true if successful.</returns>
    public bool AddToLog(string message, Exception exc)
    {
      string msg = String.Format(
          "{0}{1}{2}{1}{3}",
          message,
          Environment.NewLine,
          exc.Message,
          exc.StackTrace);

      Exception inner = exc.InnerException;
      while (inner != null)
      {
        msg += String.Format("{0}{1}{0}{2}", Environment.NewLine, inner.Message, inner.StackTrace);
        inner = inner.InnerException;
      }

      return this.AddToLog(msg);
    }

    /// <summary>
    /// Adds to log.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns></returns>
    public bool AddToLog(string message)
    {
      if (this.DebugOn && Environment.UserInteractive)
      {
        Console.Out.WriteLine(message);
      }

      if (message == null)
      {
        message = "Error desconocido.";
      }

      bool success = true;
      string filename = "Game.log";
      string line = String.Format("\r\n{0} {1}", DateTime.Now, message);
      string filepath;

      if (AppDomain.CurrentDomain != null && AppDomain.CurrentDomain.BaseDirectory != null)
      {
        filepath = FilePath.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
      }
      else
      {
        filepath = @"";
      }


      if (this.Path != ".")
      {
        var dirinfo = new DirectoryInfo(this.Path);
        if (dirinfo.Exists)
        {
          filepath = string.Format("{0}\\", this.Path);
        }
      }

      Exception debugException = null;
      try
      {

        lock (Log.LogLock)
        {
          var info = new FileInfo(filepath + filename);          
          if (!info.Exists)
          {
            info.Create().Close();
          }

          // Se limita el tamaño del archivo a MAX_LOG_SIZE
          if (info.Length + line.Length > MAX_LOG_SIZE)
          {
            long needsRemoved = (info.Length + line.Length) - MAX_LOG_SIZE;
            using (FileStream fs = info.Open(FileMode.Open))
            {
              Log.ChopStreamStart(fs, needsRemoved);
            }
          }
          
          using (FileStream xmlFile = new FileStream(FilePath.Combine(filepath, filename), FileMode.Append, FileAccess.Write))
          {
            using (StreamWriter writer = new StreamWriter(xmlFile))
            {
              writer.Write(line);
            }
          }
        }
      }
      catch (Exception e)
      {
        debugException = new Exception(String.Format("Unable to create or open log file {0}", filename), e);
      }

      if (this.DebugOn)
      {
        if (debugException != null)
          throw debugException;
      }

      return success;
    }
    #endregion

    /// <summary>
    /// Chops off the first bytes of a stream, changes the stream's Length and leaves the Position at the
    /// end of the stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="count">The count.</param>
    /// <returns>The number of bytes chopped off</returns>
    static int ChopStreamStart(Stream stream, long count)
    {
      long oldlength = stream.Length;
      long newlength = oldlength - count;

      if (newlength <= 0)
      {
        newlength = 0;
      }
      else
      {
        byte[] data = new byte[newlength];
        stream.Position = count;
        newlength = stream.Read(data, 0, data.Length);
        stream.Position = 0;
        stream.Write(data, 0, data.Length);
      }
      stream.SetLength(newlength);

      return (int)(oldlength - newlength);
    }
  }

}
