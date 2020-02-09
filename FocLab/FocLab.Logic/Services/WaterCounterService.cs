using FocLab.Logic.Models;
using FocLab.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FocLab.Logic.Services
{
    public class WaterCounterService : BaseService
    {
        public WaterCounterService(DbContext context) : base(context)
        {
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
                return new BaseApiResponse(false, "Дом не найден по указанному идентификатору");
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