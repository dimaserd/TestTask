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

        /// <summary>
        /// Получить дома упорядоченные по убыванию показаний счетчиков
        /// </summary>
        /// <returns></returns>
        public async Task<List<HouseModel>> GetHouses()
        {
            var result = await Context.Set<House>().Select(HouseSelectExpression).ToListAsync();

            return result.OrderByDescending(x => x.WaterCounter != null? x.WaterCounter.CurrentIndication : 0).ToList();
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