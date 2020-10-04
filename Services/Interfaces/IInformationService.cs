using BillManager.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Services.Interfaces
{
    public interface IInformationService
    {
        ResponseDTO AddInformation(InformationDTO InformationDTO);
        ResponseDTO EditInformation(InformationDTO InformationDTO);
        ResponseDTO DeleteInformation(string email);
        InformationsDTO GetAllInformationByUser(string userId);

    }
}
