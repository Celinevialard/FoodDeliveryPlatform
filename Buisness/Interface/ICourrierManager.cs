using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface ICourrierManager
    {
        void AddCourrier(Courrier courrier);
        List<int> GetLocationsByCourrierId(int courrierId);
    }
}