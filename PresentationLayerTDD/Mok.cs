namespace Game.Presentation.Tests
{
    using System;
using System.Collections.Generic;
using System.Threading;
using DependencyLocation;
using Game.Logic;
using Moq;

    public sealed class Mok
    {
        private const string LOCK_NAME = "locator-lock";
        public static readonly Mok Helper = new Mok();

        private static readonly Semaphore LocatorLock = new Semaphore(1, 1, LOCK_NAME);

        public void AcquireLock()
        {
            using (var writer = System.IO.File.AppendText(@"d:/temp/test.log"))
            {
                writer.WriteLine("Acquiring lock.");
            }

            //LocatorLock.WaitOne();

            using (var writer = System.IO.File.AppendText(@"d:/temp/test.log"))
            {
                writer.WriteLine("Acquired lock.");
            }
        }

        public void ReleaseLock()
        {
            using (var writer = System.IO.File.AppendText(@"d:/temp/test.log"))
            {
                writer.WriteLine("Releasing lock.");
            }

            //LocatorLock.Release(1);
        }

        public IOperationResult<T> CreateResultWith<T>(ResultValue result, T data, string message = "")
        {
            Mock<IOperationResult<T>> moq = new Mock<IOperationResult<T>>();
            moq.SetupGet(r => r.Result)
                .Returns(result);
            moq.SetupGet(r => r.ResultData)
                .Returns(data);
            moq.SetupGet(r => r.Message)
                .Returns(message);

            return moq.Object;
        }

        public IOperationResult<T> CreateResultWith<T>(ResultValue result, Func<T> data, string message = "")
        {
            Mock<IOperationResult<T>> moq = new Mock<IOperationResult<T>>();
            moq.SetupGet(r => r.Result)
                .Returns(result);
            moq.SetupGet(r => r.ResultData)
                .Returns(data);
            moq.SetupGet(r => r.Message)
                .Returns(message);

            return moq.Object;
        }

        public Mock<IAccountManager> SetupAccountManager(ResultValue defaultResult = ResultValue.Fail, ResultValue sessionsResult = ResultValue.Success, params Tuple<IRoomUser, string>[] roomUsers)
        {

            Mock<IAccountManager> mgr = null;
            foreach (var tuple in roomUsers)
            {
                Mock<IUserSession> moqSession = CreateSession(tuple.Item1, tuple.Item2);
                mgr = SetupAccountManager(defaultResult, sessionsResult, moqSession.Object);
            }

            return mgr;
        }

        private List<string> addedSessions = new List<string>();
        public Mock<IAccountManager> SetupAccountManager(ResultValue defaultResult = ResultValue.Fail, ResultValue sessionsResult = ResultValue.Success, params IUserSession[] sessions)
        {
            IAccountManager AccMgr = null;
            try
            {
                AccMgr = Locate<IAccountManager>();
            }
            catch (Exception) { }

            Mock<IAccountManager> moq;
            if (AccMgr == null)
            {
                moq = new Mock<IAccountManager>();
                Inject(moq.Object);
            }
            else
            {
                moq = Mock.Get(AccMgr);
            }

            foreach (var session in sessions)
            {
                AddSession(moq, session, sessionsResult);
            }

            moq.Setup(mgr => mgr.GetUserSession(
                                 It.Is<string>(s => !this.addedSessions.Contains(s))))
                .Returns(() =>
                {
                    return CreateResultWith(defaultResult, default(IUserSession));
                });

            return moq;
        }

        public Mok AddSession(Mock<IAccountManager> moq, IUserSession session, ResultValue sessionsResult = ResultValue.Success)
        {
            this.addedSessions.Add(session.SessionId);
            moq.Setup(mgr => mgr.GetUserSession(It.Is<string>(s => s == session.SessionId)))
                               .Returns(() =>
                               {
                                   return CreateResultWith(sessionsResult, session);
                               });
            return Helper;
        }

        public Mock<IUserSession> CreateSession(IRoomUser roomUser, string sessionId)
        {
            Mock<IUserSession> moqSession = new Mock<IUserSession>();
            moqSession.SetupGet(s => s.SessionId)
                      .Returns(sessionId);
            moqSession.SetupGet(s => s.Username)
                      .Returns(roomUser.Username);
            return moqSession;
        }

        public Mok Inject<T>(T obj)
        {
            using (var writer = System.IO.File.AppendText(@"d:/temp/test.log"))
            {
                writer.WriteLine(string.Format("Injecting object: {0}, of type: {1}", obj.GetHashCode(), typeof(T).Name));
                try
                {
                    ((IDependencyConfigurator)Dependency.Locator).SetupSingleton<T>(obj);
                }
                catch (Exception x)
                {
                    writer.WriteLine(x.Message);
                    //writer.WriteLine(x.StackTrace);
                    if (typeof(T) == typeof(IRoomsManager))
                    {
                        writer.WriteLine(new System.Diagnostics.StackTrace().ToString());
                    }
                    throw x;
                }

                if (typeof(T) == typeof(IRoomsManager))
                {
                    writer.WriteLine(new System.Diagnostics.StackTrace().ToString());
                }
            }
            return Helper;
        }

        public TObject Create<TObject>(params object[] args)
        {
            return Dependency.Locator.Create<TObject>(args);
        }

        public TObject Locate<TObject>(string key = null)
            where TObject : class
        {
            return Dependency.Locator.GetSingleton<TObject>(key);
        }

    }
}
