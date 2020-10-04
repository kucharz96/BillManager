using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillManager.ModelsDTO;
using BillManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BillManager.Controllers
{
    public class BillController : Controller
    {
        private readonly IBillsService billService;
        private readonly ILogger logger;
        public BillController(IBillsService billService, ILogger<BillController> logger)
        {
            this.billService = billService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("/api/bill/add")]
        public IActionResult AddBill([FromBody]BillDTO billDTO)
        {
            logger.LogInformation("AddBill controller");
            return Ok(billService.AddBill(billDTO));
        }

        [HttpPut]
        [Route("/api/bill/edit")]
        public IActionResult EditBill([FromBody] BillDTO billDTO)
        {
            logger.LogInformation("EditBill controller");
            return Ok(billService.EditBill(billDTO));
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("/api/bill/delete/{mail}")]
        public IActionResult DeleteBill(string mail)
        {
            logger.LogInformation("DeleteBill controller");
            return Ok(billService.DeleteBill(mail));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/api/bill/getAll/{mail}")]
        public IActionResult GetAllBillByUser(string mail)
        {
            logger.LogInformation("GetAllBillByUser controller");
            return Ok(billService.GetAllBillByUser(mail));
        }

    }
}
