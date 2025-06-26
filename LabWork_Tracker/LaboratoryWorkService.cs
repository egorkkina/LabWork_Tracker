using LabWork_Tracker.Interfaces;

namespace LabWork_Tracker;

public class LaboratoryWorkService : ILaboratoryWorkService
{
    private List<LaboratoryWork> _laboratoryWorks = new List<LaboratoryWork>();
    
    public void AddLaboratoryWork(LaboratoryWork laboratoryWork)
    {
        _laboratoryWorks.Add(laboratoryWork);
    }

    public void EditLaboratoryWork(int id, Action<LaboratoryWork> editAction)
    {
        var lr = _laboratoryWorks.FirstOrDefault(x => x.Id == id);
        if (lr != null) editAction(lr);
    }

    public void RemoveLaboratoryWork(int id)
    {
        _laboratoryWorks.Remove(_laboratoryWorks.Find(x => x.Id == id));
    }

    public List<LaboratoryWork> ShowAllLaboratoryWorks()
    {
        return _laboratoryWorks;
    }
}