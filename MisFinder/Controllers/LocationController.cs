using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;

namespace MisFinder.Controllers
{
    public class StateController : Controller
    {
        private readonly IStateRepository repository;

        public StateController(IStateRepository repository)
        {
            this.repository = repository;
        }
        public Task<IEnumerable<State>> GetAllStates()
        {
           return repository.GetAllStates();
        }
    }
}
