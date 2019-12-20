using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace DJ.Targets
{
    [Target(nameof(CacheTarget))]
    public class CacheTarget : Target
    {
        /// <summary>
        /// If there is no target in nlog.config defined a new one is registered with the default maxcount
        /// </summary>
        /// <param name="defaultMaxCount"></param>
        /// <returns></returns>
        public static CacheTarget GetInstance(int defaultMaxCount = 0)
        {
            if(LogManager.Configuration == null)
                LogManager.Configuration = new LoggingConfiguration();
            var target = (CacheTarget)LogManager.Configuration.AllTargets.FirstOrDefault(t => t is CacheTarget);
            if (target == null)
            {
                target = new CacheTarget { MaxCount = defaultMaxCount, Name = nameof(CacheTarget)};
                LogManager.Configuration.AddTarget(target.Name, target);
                LogManager.Configuration.LoggingRules.Insert(0, new LoggingRule("*", LogLevel.FromString("Trace"), target));
                LogManager.ReconfigExistingLoggers();
            }
            return target;
        }

        // ##############################################################################################################################
        // Properties
        // ##############################################################################################################################

        #region Properties

        // ##########################################################################################
        // Public Properties
        // ##########################################################################################

        /// <summary>
        /// The maximum amount of entries held in buffer/cache
        /// </summary>
        public int MaxCount { get; set; } = 100;
        
        public IObservable<LogEventInfo> Cache => _CacheSubject.AsObservable();
        private readonly ReplaySubject<LogEventInfo> _CacheSubject;

        // ##########################################################################################
        // Private Properties
        // ##########################################################################################

        #endregion

        // ##############################################################################################################################
        // Constructor
        // ##############################################################################################################################

        #region Constructor

        public CacheTarget()
        {
            _CacheSubject = new ReplaySubject<LogEventInfo>(MaxCount);
        }

        #endregion

        // ##############################################################################################################################
        // override
        // ##############################################################################################################################

        #region override

        protected override void Write(LogEventInfo logEvent)
        {
            _CacheSubject.OnNext(logEvent);
        }

        #endregion
    }
}