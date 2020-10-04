using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ViewModel;
using System.Linq;
using System.Collections.Generic;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartiesController : ControllerBase
    {
        private readonly ILogger<PartiesController> _logger;

        private readonly IList<PartyViewModel> Parties;

        public PartiesController(ILogger<PartiesController> logger)
        {
            _logger = logger;
            Parties = new List<PartyViewModel>();
            Parties.Add(new PartyViewModel
            {
                Id = 3,
                Letter = "A",
                Name = "Fólkaflokkurin",
                StartDate = new DateTime(2015, 09, 01),
                EndDate = null
            });
            Parties.Add(new PartyViewModel
            {
                Id = 4,
                Letter = "B",
                Name = "Sambandsflokkurin",
                StartDate = new DateTime(2015, 09, 01),
                EndDate = null
            });
            Parties.Add(new PartyViewModel
            {
                Id = 1,
                Letter = "C",
                Name = "Javnaðarflokkurin",
                StartDate = new DateTime(2015, 09, 01),
                EndDate = null
            });
            Parties.Add(new PartyViewModel
            {
                Id = 6,
                Letter = "D",
                Name = "Sjálvstýri",
                StartDate = new DateTime(2015, 09, 01),
                EndDate = null
            });
            Parties.Add(new PartyViewModel
            {
                Id = 2,
                Letter = "E",
                Name = "Tjóðveldi",
                StartDate = new DateTime(2015, 09, 01),
                EndDate = null
            });
            Parties.Add(new PartyViewModel
            {
                Id = 5,
                Letter = "F",
                Name = "Framsókn",
                StartDate = new DateTime(2015, 09, 01),
                EndDate = null
            });
            Parties.Add(new PartyViewModel
            {
                Id = 6,
                Letter = "H",
                Name = "Miðflokkurin",
                StartDate = new DateTime(2015, 09, 01),
                EndDate = null
            });
        }

        [Route("GetCurrentParties")]
        public IList<PartyViewModel> GetCurrentParties()
        {
            return Parties;
        }

        [Route("GetCurrentParty/{id}")]
        public PartyViewModel GetCurrentParty(int id)
        {
            return Parties.FirstOrDefault(x => x.Id == id);
        }
    }
}
