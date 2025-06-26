namespace LabWork_Tracker.Interfaces;

public interface ILaboratoryWorkService
{
    void AddLaboratoryWork(LaboratoryWork laboratoryWork);
    void EditLaboratoryWork(int id, Action<LaboratoryWork> editAction);
    void RemoveLaboratoryWork(int id);
    List<LaboratoryWork> ShowAllLaboratoryWorks();
}