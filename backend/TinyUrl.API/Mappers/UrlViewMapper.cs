
using TinyUrl.API.Models;
using TinyUrl.API.Recources;
using TinyUrl.Common.Interfaces;

namespace TinyUrl.API.Mappers
{
    public class UrlViewMapper : IUrlViewMapper
    {
        public LookupViewModel<UrlViewModel> MapUrlLookup(ILookupModel<IUrlModel> model)
        {
            return new LookupViewModel<UrlViewModel>
            {
                TotalRecords = model.TotalRecords,
                Records = model.Records.Select(r => new UrlViewModel
                {
                    Id = r.Id,
                    RedirectUrl = r.RedirectUrl,
                    RelativePath = r.RelativePath,
                    CreatedDate = r.CreatedDate,
                    VisitCount = r.VisitCount
                }).ToList()
            };
        }

        public List<string> MapRecources(List<string> errors)
        {
            return errors.Select(error => ResponseMessages.ResourceManager.GetString(error)).ToList();
        }

        public ValidationResultViewModel MapValidationResult(IResult<List<string>> validationResult)
        {
            return new ValidationResultViewModel
            {
                IsValid = validationResult.IsSuccess,
                ValidationErrors = this.MapRecources(validationResult.Value)
            };
        }
    }
}
