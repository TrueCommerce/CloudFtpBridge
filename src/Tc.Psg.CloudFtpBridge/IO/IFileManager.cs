using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tc.Psg.CloudFtpBridge.IO
{
    public interface IFileManager
    {
        Task ExecuteWorkflow(Workflow workflow);
    }
}
