using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLFeedImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            var ciXmlIntegration = new CiXmlIntegration();

            ciXmlIntegration.StartProcessing();            
        }        
    }
}
