using FocLab.Logic.Models;
using FocLab.Logic.Resources;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using FocLab.Model.Entities;
using NUnit.Framework;
using System.Threading.Tasks;

namespace FocLab.Tests.Services
{
    /// <summary>
    /// Тестирование методов сервиса <see cref="HouseService"/>
    /// </summary>
    [TestFixture]
    public class HouseServiceTests
    {
        [Test]
        public async Task ValidationTest()
        {
            var db = ApplicationDbContext.CreateForTesting();

            var houseService = new HouseService(db);

            //Не указываем в модели адрес 
            var resp = await houseService.CreateHouse(new CreateHouse());

            //Должна произойти ошибка валидации с нужным сообщением
            Assert.IsFalse(resp.IsSucceeded);
            Assert.AreEqual(resp.Message, MainResources.CreateHouseAddressRequiredErrorMessage);
        }

        /// <summary>
        /// В данном тесте произойдет попытка создать дом с адресом, который уже имеется в бд
        /// </summary>
        /// <returns></returns>
        [TestCase("address1")]
        public async Task CreateHouseWithAddressThatAlreadyExists(string adress)
        {
            var db = ApplicationDbContext.CreateForTesting();

            //Добавляем дом в базу данных
            db.Set<House>().Add(new House
            {
                Address = adress
            });
            //Сохраняем изменения
            db.SaveChanges();

            var houseService = new HouseService(db);

            var resp = await houseService.CreateHouse(new CreateHouse
            {
                Address = adress
            });

            //Дом не был добавлен и мы получаем текст с нужным сообщением
            Assert.IsFalse(resp.IsSucceeded);
            Assert.AreEqual(resp.Message, MainResources.HouseWithTheSameAdressAlreadyExists);
        }
    }
}