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
    public class InformationController : Controller
    {
        private readonly IInformationService informationService;
        private readonly ILogger logger;
        public InformationController(IInformationService informationService, ILogger<InformationController> logger)
        {
            this.informationService = informationService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("/api/information/add")]
        public IActionResult AddInformation([FromBody] InformationDTO InformationDTO)
        {
            logger.LogInformation("AddInformation controller");
            return Ok(informationService.AddInformation(InformationDTO));
        }

        [HttpPut]
        [Route("/api/information/edit")]
        public IActionResult EditInformation([FromBody] InformationDTO InformationDTO)
        {
            logger.LogInformation("EditInformation controller");
            return Ok(informationService.EditInformation(InformationDTO));
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("/api/information/delete/{informationId}")]
        public IActionResult DeleteInformation(int informationId)
        {
            logger.LogInformation("DeleteInformation controller");
            return Ok(informationService.DeleteInformation(informationId));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/api/information/getAll/{mail}")]
        public IActionResult GetAllInformationByUser(string mail)
        {
            logger.LogInformation("GetAllInformationByUser controller");
            return Ok(informationService.GetAllInformationByUser(mail));
        }
    }
}
