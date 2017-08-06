namespace Kaplan.Interfaces
{
    public interface IProcess
    {
        void StartProcess();
        void FinalizeProcess(bool result);
    }
}