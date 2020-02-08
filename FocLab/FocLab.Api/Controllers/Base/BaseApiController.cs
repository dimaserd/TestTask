using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FocLab.Api.Controllers.Base
{
    /// <inheritdoc />
    /// <summary>
    /// Базовый абстрактный контроллер в котором собраны общие методы и свойства
    /// </summary>
    public class BaseApiController : CrocoGenericController<ApplicationDbContext>
    {
        /// <inheritdoc />
        public BaseApiController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        #region Поля

        //TODO Impelement RoleManager
        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public RoleManager<IdentityRole> RoleManager = null;
        #endregion

    } 
}
