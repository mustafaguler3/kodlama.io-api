using log4net;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Core.CrossCuttingConserns.Logging.Log4Net
{
    public class LoggerServiceBase
    {
        private ILog _log;

        public LoggerServiceBase(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(File.OpenRead("log4Net.config"));

            ILoggerRepository logger = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(Hierarchy));
            log4net.Config.XmlConfigurator.Configure(logger, xmlDocument["log4net"]);

            _log = LogManager.GetLogger(logger.Name, name);

        }
    }
}
