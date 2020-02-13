using FocLab.Logic.Models;
using FocLab.Logic.Resources;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using NUnit.Framework;
using System.Threading.Tasks;

namespace FocLab.Tests.Services
{
    /// <summary>
    /// Тестирование методов сервиса <see cref="WaterCounterService"/>
    /// </summary>
    [TestFixture]
    public class WaterCounterServiceTests
    {
        [Test]
        public async Task ValidationTest()
        {
            var db = ApplicationDbContext.CreateForTesting();

            var waterCounterService = new WaterCounterService(db);

            //Не указываем в модели заводской номер 
            var resp = await waterCounterService.CreateWaterCounter(new CreateWaterCounter());

            //Должна произойти ошибка валидации с нужным сообщением
            Assert.IsFalse(resp.IsSucceeded);
            Assert.AreEqual(resp.Message, MainResources.FactoryNumberIsRequired);
        }

        [Test]
        public async Task CreateWaterCounter_HouseNotFound()
        {
            var db = ApplicationDbContext.CreateForTesting();

            var waterCounterService = new WaterCounterService(db);

            //Не указываем в модели адрес 
            var resp = await waterCounterService.CreateWaterCounter(new CreateWaterCounter 
            {
                HouseId = 1,
                FactoryNumber = "random"
            });

            //Должна произойти ошибка с нужным сообщением
            Assert.IsFalse(resp.IsSucceeded);
            Assert.AreEqual(resp.Message, MainResources.HouseIsNotFoundByProvidedId);
        }
    }
}