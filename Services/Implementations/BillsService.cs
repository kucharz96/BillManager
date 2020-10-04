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
    public class BillsService : IBillsService
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public BillsService(ApplicationDbContext context,ILogger<BillsService> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }
        public ResponseDTO AddBill(BillDTO billDTO)
        {
            logger.LogInformation("AddBill");
            try
            {
                var a = mapper.Map<Bill>(billDTO);
                context.Bills.Add(a);
                context.SaveChanges();
            }
            catch(Exception e)
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
                Message = "Added bill",
                Status = "Success"
            };
        }

        public ResponseDTO DeleteBill(string email)
        {
            logger.LogInformation("DeleteBill");
            var billToDelete = context.Bills.Where(i => i.ApplicationUser.Email == email).SingleOrDefault();
            if (billToDelete == null)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Bill not exist",
                    Status = "Error"
                };
            }
            try
            {
                context.Bills.Remove(billToDelete);
            }
            catch(Exception e)
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
                Message = "Deleted bill",
                Status = "Success"
            };
        }

        public ResponseDTO EditBill(BillDTO billDTO)
        {
            logger.LogInformation("EditBill");
            if(context.Bills.Where(b=>b.Name == billDTO.Name).Count() == 0)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Bill not exist",
                    Status = "Error"
                };
            }

            try
            {
                context.Bills.Update(mapper.Map<Bill>(billDTO));
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
                Message = "Updated bill",
                Status = "Success"
            };

        }

        public BillsDTO GetAllBillByUser(string email)
        {
            var result = context.Bills.Where(i => i.ApplicationUser.Email == email).ToList();
            BillsDTO billsDTO = new BillsDTO();
            billsDTO.BillList = new List<BillDTO>();
            foreach(var bill in result)
            {
                billsDTO.BillList.Add(mapper.Map<BillDTO>(bill));
            }
            billsDTO.BillList = billsDTO.BillList.OrderBy(i => i.Year).Reverse().ToList();
            return billsDTO;
        }
    }
}
