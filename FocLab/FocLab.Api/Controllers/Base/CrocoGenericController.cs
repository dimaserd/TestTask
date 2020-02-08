using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Api.Controllers.Base
{
    /// <summary>
    /// Обобщенный веб-контроллер с основной логикой
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    public class CrocoGenericController<TContext> : Controller where TContext : DbContext
    {
        /// <inheritdoc />
        public CrocoGenericController(TContext context, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            HttpContextAccessor = httpContextAccessor;
        }

        #region Свойства

        /// <summary>
        /// Контекст для работы с бд
        /// </summary>
        public TContext Context
        {
            get;
        }

        /// <summary>
        /// Контекст доступа к запросу
        /// </summary>
        public IHttpContextAccessor HttpContextAccessor { get; }

        public static string GetMimeMapping(string fileName)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType);

            return contentType ?? "application/octet-stream";
        }

        /// <summary>
        /// Возвращает физический файл по пути и имени файла, из имени файла берется Mime тип
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected PhysicalFileResult PhysicalFileWithMimeType(string filePath, string fileName)
        {
            return PhysicalFile(filePath, GetMimeMapping(fileName), fileName);
        }

        #endregion

    }
}