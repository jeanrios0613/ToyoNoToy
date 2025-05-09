using managerelchenchenvuelve.Models;

namespace managerelchenchenvuelve.Services
{
    public interface IProcessService
    {
        Task<bool> UpdateProcessInstance(ProcessFormData formData, string userId, string username);
        Task<ConsultaSoloAmpymeCompleto> GetSolicitud(string id);
    }
} 