using Gjognumskygni.API.Model;
using Gjognumskygni.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gjognumskygni.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartiesController : ControllerBase
    {
        private readonly ILogger<PartiesController> Logger;

        private readonly IList<PartyViewModel> Parties;

        private readonly ApplicationDbContext DbContext;

        public PartiesController(ILogger<PartiesController> logger, ApplicationDbContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;

            Parties = new List<PartyViewModel>();
            Parties.Add(new PartyViewModel
            {
                Id = 1,
                Letter = "A",
                Name = "Fólkaflokkurin",
                StartDate = new DateTime(2015, 09, 01),
                EndDate = null
            });
            Parties.Add(new PartyViewModel
            {
                Id = 2,
                Letter = "B",
                Name = "Sambandsflokkurin",
                StartDate = new DateTime(2015, 09, 01),
                EndDate = null
            });
            Parties.Add(new PartyViewModel
            {
                Id = 3,
                Letter = "C",
                Name = "Javnaðarflokkurin",
                StartDate = new DateTime(2015, 09, 01),
                EndDate = null
            });
            Parties.Add(new PartyViewModel
            {
                Id = 4,
                Letter = "D",
                Name = "Sjálvstýri",
                StartDate = new DateTime(2015, 09, 01),
                EndDate = null
            });
            Parties.Add(new PartyViewModel
            {
                Id = 5,
                Letter = "E",
                Name = "Tjóðveldi",
                StartDate = new DateTime(2015, 09, 01),
                EndDate = null
            });
            Parties.Add(new PartyViewModel
            {
                Id = 6,
                Letter = "F",
                Name = "Framsókn",
                StartDate = new DateTime(2015, 09, 01),
                EndDate = null
            });
            Parties.Add(new PartyViewModel
            {
                Id = 7,
                Letter = "H",
                Name = "Miðflokkurin",
                StartDate = new DateTime(2015, 09, 01),
                EndDate = null
            });
        }

        [Route("GetCurrentParties")]
        public IList<PartyViewModel> GetCurrentParties()
        {
            //return Parties;
            return DbContext.Parties.Where(x => true).Select(x => new PartyViewModel
            {
                Id = x.Id,
                EndDate = x.EndDate,
                Letter = x.Letter,
                Name = x.Name,
                StartDate = x.StartDate
            }).ToList();
        }

        [Route("GetCurrentParty/{id}")]
        public PartyViewModel GetCurrentParty(int id)
        {
            return Parties.FirstOrDefault(x => x.Id == id);
        }
    }
}
