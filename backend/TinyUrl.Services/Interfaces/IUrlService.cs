using TinyUrl.Common.Interfaces;
using TinyUrl.Common.Models;

namespace TinyUrl.Services.Interfaces
{
    public interface IUrlService
    {
        public Task<ILookupModel<IUrlModel>> FilterAsync(int skip, int take);

        public Task<Result<string>> GetRedirectUrlAsync(string tinyUrl);

        public Task<Result<IUrlModel>> GetUrlAsync(int id);

        public Task<Result<bool>> DeleteAsync(int id);

        public Task<Result<List<string>>> AddAsync(IAddEditViewModel model);

        public Task<Result<List<string>>> EditAsync(IAddEditViewModel model, int id);

        public Task<Result<List<string>>> ValidatePathAsync(string path, bool isEdit);
    }
}
