using System.Threading.Tasks;

namespace Tc.Psg.CloudFtpBridge.IO
{
    public interface IFileManager
    {
        Task ExecuteWorkflow(Workflow workflow);
        Task ProcessStagedWorkflowFiles(Workflow workflow);
        Task StageWorkflowFiles(Workflow workflow);
    }
}
