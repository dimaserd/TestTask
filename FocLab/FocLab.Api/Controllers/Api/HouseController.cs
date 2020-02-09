using FocLab.Logic.Models;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FocLab.Api.Controllers.Api
{
    [Route("Api/House")]
    public class HouseController : Controller
    {
        HouseService HouseService => new HouseService(Context);

        WaterCounterService WaterCounterService => new WaterCounterService(Context);

        ApplicationDbContext Context { get; }

        public HouseController(ApplicationDbContext context)
        {
            Context = context;
        }

        [HttpPost("Get")]
        public Task<List<HouseModel>> GetHouses()
        {
            return HouseService.GetHouses();
        }

        [HttpPost("Create")]
        public Task<BaseApiResponse> CreateHouse(CreateHouse model)
        {
            return HouseService.CreateHouse(model);
        }

        [HttpPost("AddWaterCounter")]
        public Task<BaseApiResponse> AddWaterCounter(CreateWaterCounter model)
        {
            return WaterCounterService.CreateWaterCounter(model);
        }
    }
}