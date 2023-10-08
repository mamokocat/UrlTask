using TinyUrl.API.Models;
using TinyUrl.Common.Interfaces;

namespace TinyUrl.API.Mappers
{
    public interface IUrlViewMapper
    {
        LookupViewModel<UrlViewModel> MapUrlLookup(ILookupModel<IUrlModel> model);
        List<string> MapRecources(List<string> errors);

        ValidationResultViewModel MapValidationResult(IResult<List<string>> validationResult);
    }
}
