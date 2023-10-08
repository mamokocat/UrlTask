using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TinyUrl.Common.Configuration.Interfaces;
using TinyUrl.Common.Constants;
using TinyUrl.Common.Interfaces;
using TinyUrl.Common.Models;
using TinyUrl.DAL;
using TinyUrl.DAL.Models;
using TinyUrl.Services.Interfaces;
using TinyUrl.Services.Mappers;

namespace TinyUrl.Services.Services
{
    public class UrlService : IUrlService
    {
        private readonly IDatabaseConfiguration databaseConfiguration;
        public UrlService(IDatabaseConfiguration databaseConfiguration) {
            this.databaseConfiguration = databaseConfiguration;
        }

        public async Task<ILookupModel<IUrlModel>> FilterAsync(int skip, int take)
        {
            using (var context = new TinyUrlDbContext(databaseConfiguration.TinyUrlDb))
            {
                var result = new LookupModel<IUrlModel>();

                var records = await context.Urls.Skip(skip).Take(take).ToListAsync();
                var totalRecords = context.Urls.Count();

                result.Records = records.Select(UrlMapper.Map).ToList();
                result.TotalRecords = totalRecords;

                return result;
            }
        }

        public async Task<Result<string>> GetRedirectUrlAsync(string tinyUrl)
        {
            using (var context = new TinyUrlDbContext(databaseConfiguration.TinyUrlDb))
            {
                var result = await context.Urls.FirstOrDefaultAsync(url => url.RelativePath == tinyUrl);

                if (result is not null)
                {
                    result.VisitCount++;
                    await context.SaveChangesAsync();
                    return new Result<string>(result.RedirectUrl, true);
                }

                return new Result<string>(string.Empty, false);
            }
        }

        public async Task<Result<IUrlModel>> GetUrlAsync(int id)
        {
            using (var context = new TinyUrlDbContext(databaseConfiguration.TinyUrlDb))
            {
                var result = await context.Urls.FirstOrDefaultAsync(url => url.Id == id);

                if (result is not null)
                {
                    return new Result<IUrlModel>(UrlMapper.Map(result), true);
                }

                return new Result<IUrlModel>(null, false);
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            using (var context = new TinyUrlDbContext(databaseConfiguration.TinyUrlDb))
            {
                var result = await context.Urls.FirstOrDefaultAsync(url => url.Id == id);
                if (result is null)
                {
                    return new Result<bool>(false, false);
                }

                context.Remove(result);
                await context.SaveChangesAsync();

                return new Result<bool>(true, true);
            }
        }

        public async Task<Result<List<string>>> ValidatePathAsync(string path)
        {
            using (var context = new TinyUrlDbContext(databaseConfiguration.TinyUrlDb))
            {
                var errors = new List<string>();
                var result = await context.Urls.FirstOrDefaultAsync(url => url.RelativePath == path);

                if (result is not null)
                {
                    errors.Add(ValidationErrors.DuplicatePath);
                }

                errors.AddRange(GetValidationErrors(path));

                return new Result<List<string>>(errors, errors.Count == 0);
            }
        }

        public async Task<Result<List<string>>> AddAsync(IAddEditViewModel model)
        {
            using (var context = new TinyUrlDbContext(databaseConfiguration.TinyUrlDb))
            {
                var errors = new List<string>();
                var result = await context.Urls.FirstOrDefaultAsync(url => url.RelativePath == model.RelativePath);
                if (result is not null)
                {
                    errors.Add(ValidationErrors.DuplicatePath);
                    return new Result<List<string>>(errors, false);
                }

                await context.AddAsync(new UrlDbModel
                {
                    RelativePath = model.RelativePath,
                    RedirectUrl = model.RedirectUrl,
                    CreatedDate = DateTime.Now,
                    VisitCount = 0,
                });
                await context.SaveChangesAsync();

                return new Result<List<string>>(errors, true);
            }
        }

        public async Task<Result<List<string>>> EditAsync(IAddEditViewModel model, int id)
        {
            using (var context = new TinyUrlDbContext(databaseConfiguration.TinyUrlDb))
            {
                var errors = new List<string>();
                var result = await context.Urls.FirstOrDefaultAsync(url => url.Id == id);
                if (result is null)
                {
                    errors.Add(ValidationErrors.UrlNotExist);
                    return new Result<List<string>>(errors, false);
                }

                result.RelativePath = model.RelativePath;
                result.RedirectUrl = model.RedirectUrl;

                await context.SaveChangesAsync();

                return new Result<List<string>>(errors, true);
            }
        }

        private List<string> GetValidationErrors(string path)
        {
            var errors = new List<string>();

            if (path.Length < 8 || path.Length > 16)
            {
                errors.Add(ValidationErrors.PathIsTooShort);
            }

            if (!path.Any(ch => Char.IsDigit(ch)))
            {
                errors.Add(ValidationErrors.PathNoDigits);
            }

            if (!path.Any(ch => Char.IsLetter(ch)))
            {
                errors.Add(ValidationErrors.PathNoLetters);
            }

            Regex letterSubsequences = new Regex(@"[a-zA-Z]{3,}");
            Regex digitSubsequences = new Regex(@"[0-9]{3,}");

            foreach (Match match in letterSubsequences.Matches(path).Concat(digitSubsequences.Matches(path)))
            {
                int subsequenceCount = 0;

                for (int index = 1; index < match.Value.Length; index++)
                {
                    int charDiff = match.Value[index] - match.Value[index - 1];
                    if (Math.Abs(charDiff) == 1)
                    {
                        subsequenceCount++;
                    }
                }

                if (subsequenceCount == match.Value.Length - 1)
                {
                    errors.Add(ValidationErrors.PathNoAlphaNumbericSubsequences);
                    return errors;
                }
            }

            return errors;
        }
    }
}
