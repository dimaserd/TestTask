using FocLab.Logic.Models;
using FocLab.Logic.Resources;
using FocLab.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FocLab.Logic.Services
{
    public class HouseService : BaseService
    {
        public HouseService(DbContext context) : base(context)
        {
        }

        public static Expression<Func<House, HouseModel>> HouseSelectExpression = x => new HouseModel
        {
            Id = x.Id,
            Address = x.Address,
            WaterCounter = x.WaterCounterId.HasValue ? new WaterCounterModel
            {
                Id = x.WaterCounter.Id,
                CurrentIndication = x.WaterCounter.CurrentIndication,
                FactoryNumber = x.WaterCounter.FactoryNumber
            } : null
        };

        public Task<List<HouseModel>> GetHouses()
        {
            return Context.Set<House>().Select(HouseSelectExpression).ToListAsync();
        }

        public async Task<BaseApiResponse> CreateHouse(CreateHouse model)
        {
            var validation = Validate(model);

            if(!validation.IsSucceeded)
            {
                return validation;
            }

            var set = Context.Set<House>();

            if (await set.AnyAsync(x => x.Address == model.Address))
            {
                return new BaseApiResponse(false, MainResources.HouseWithTheSameAdressAlreadyExists);
            }

            set.Add(new House
            {
                Address = model.Address
            });

            return await TrySaveChangesAndReturnResultAsync(MainResources.HouseCreated);
        }
    }
}