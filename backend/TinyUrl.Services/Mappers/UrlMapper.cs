using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyUrl.Common.Interfaces;
using TinyUrl.DAL.Models;
using TinyUrl.Services.Models;

namespace TinyUrl.Services.Mappers
{
    public static class UrlMapper
    {
        public static IUrlModel Map(UrlDbModel entity)
        {
            return new UrlModel
            {
                Id = (int)entity.Id,
                RelativePath = entity.RelativePath,
                RedirectUrl = entity.RedirectUrl,
                CreatedDate = entity.CreatedDate,
                VisitCount = entity.VisitCount,
            };
        }
    }
}
