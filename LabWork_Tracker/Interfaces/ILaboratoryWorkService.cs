namespace LabWork_Tracker.Interfaces;

public interface ILaboratoryWorkService
{
    bool AddLaboratoryWork(LaboratoryWork laboratoryWork);
    bool EditLaboratoryWork(int id, Action<LaboratoryWork> editAction);
    bool RemoveLaboratoryWork(int id);
    List<LaboratoryWork> GetAllLaboratoryWorks();
}