using LabWork_Tracker.Interfaces;

namespace LabWork_Tracker.Services;

public class LaboratoryWorkService : ILaboratoryWorkService
{
    private readonly List<LaboratoryWork> _laboratoryWorks = new();
    
    public bool AddLaboratoryWork(LaboratoryWork laboratoryWork)
    {
        if (_laboratoryWorks.Any(x => x.Subject == laboratoryWork.Subject && x.Name == laboratoryWork.Name))
            return false; 
        
        _laboratoryWorks.Add(laboratoryWork);
        return true;
    }

    public bool EditLaboratoryWork(int id, Action<LaboratoryWork> editAction)
    {
        var lr = _laboratoryWorks.FirstOrDefault(x => x.Id == id);
        if (lr == null) 
            return false;
        
        editAction(lr);
        return true;
    }

    public bool RemoveLaboratoryWork(int id)
    {
        var lr = _laboratoryWorks.Find(x => x.Id == id);
        if (lr == null)
            return false;
        
        _laboratoryWorks.Remove(lr);
        return true;
    }

    public List<LaboratoryWork> GetAllLaboratoryWorks()
    {
        return _laboratoryWorks;
    }
}