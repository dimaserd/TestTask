using FocLab.Logic.Models;
using FocLab.Logic.Resources;
using FocLab.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FocLab.Logic.Services
{
    public class WaterCounterService : BaseService
    {
        public WaterCounterService(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Рандомно с небольшими преференциями для более маленьких показаний добавлеяет показаний к счетчикам
        /// </summary>
        /// <returns></returns>
        public async Task<BaseApiResponse> AddIndications()
        {
            var set = Context.Set<WaterCounter>();

            var list = await set.OrderByDescending(x => x.CurrentIndication).ToListAsync();

            var random = new Random();

            for (int i = 0; i < list.Count; i++)
            {
                list[i].CurrentIndication += random.Next(i, 10);
            }

            set.UpdateRange(list);

            return await TrySaveChangesAndReturnResultAsync("Ok");
        }

        public async Task<BaseApiResponse> CreateWaterCounter(CreateWaterCounter model)
        {
            var validation = Validate(model);
            
            if(!validation.IsSucceeded)
            {
                return validation;
            }

            var houseSet = Context.Set<House>();

            var house = await houseSet.FirstOrDefaultAsync(x => x.Id == model.HouseId);

            if(house == null)
            {
                return new BaseApiResponse(false, MainResources.HouseIsNotFoundByProvidedId);
            }

            //Если у дома узе есть привязанный счетчик воды
            if (house.WaterCounterId.HasValue)
            {
                return new BaseApiResponse(false, MainResources.HouseAlreadyHasAWaterCounter);
            }

            var waterCounter = new WaterCounter
            {
                FactoryNumber = model.FactoryNumber,
                CurrentIndication = 0
            };

            Context.Set<WaterCounter>().Add(waterCounter);

            var createWaterCounterResult = await TrySaveChangesAndReturnResultAsync("Ok");

            if(!createWaterCounterResult.IsSucceeded)
            {
                return new BaseApiResponse(false, "Произошла ошибка при создании счетчика воды");
            }

            //Устанавиливаю идентифкатор счетчика для дома
            house.WaterCounterId = waterCounter.Id;

            houseSet.Update(house);

            return await TrySaveChangesAndReturnResultAsync("Счетчик воды создан и привязан к дому");
        }
    }
}