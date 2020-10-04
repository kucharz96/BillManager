using AutoMapper;
using BillManager.Data;
using BillManager.Models;
using BillManager.ModelsDTO;
using BillManager.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Services.Implementations
{
    public class InformationsService : IInformationService
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        public InformationsService(ApplicationDbContext context, ILogger<InformationsService> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }
        public ResponseDTO AddInformation(InformationDTO InformationDTO)
        {
            logger.LogInformation("AddInformation");
            try
            {
                var a = mapper.Map<Information>(InformationDTO);
                context.Informations.Add(a);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = e.Message,
                    Status = "Error"
                };
            }

            return new ResponseDTO()
            {
                Code = 200,
                Message = "Added information",
                Status = "Success"
            };

        }

        public ResponseDTO DeleteInformation(int informationId)
        {
            logger.LogInformation("DeleteInformation");
            var informationToDelete = context.Informations.Where(i => i.Id == informationId).SingleOrDefault();
            if (informationToDelete == null)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Information not exist",
                    Status = "Error"
                };
            }
            try
            {
                context.Informations.Remove(informationToDelete);
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = e.Message,
                    Status = "Error"
                };
            }
            return new ResponseDTO()
            {
                Code = 200,
                Message = "Deleted information",
                Status = "Success"
            };
        }

        public ResponseDTO EditInformation(InformationDTO InformationDTO)
        {
            logger.LogInformation("EditInformation");
            if (context.Informations.Where(b => b.Name == InformationDTO.Name).Count() == 0)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Information not exist",
                    Status = "Error"
                };
            }

            try
            {
                context.Informations.Update(mapper.Map<Information>(InformationDTO));
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = e.Message,
                    Status = "Error"
                };
            }
            return new ResponseDTO()
            {
                Code = 200,
                Message = "Updated information",
                Status = "Success"
            };
        }

        public InformationsDTO GetAllInformationByUser(string email)
        {
            var result = context.Informations.Where(i => i.ApplicationUser.Email == email).ToList();
            InformationsDTO billsDTO = new InformationsDTO();
            billsDTO.InformationList = new List<InformationDTO>();
            foreach (var bill in result)
            {
                billsDTO.InformationList.Add(mapper.Map<InformationDTO>(bill));
            }
            return billsDTO;
        }
    }
}
