using Microsoft.Extensions.Options;

namespace ReplicantDemo.Infrastructure.Configuration;

public class ConfigurationValidator : IValidateOptions<CacheSettings>
{
    public ValidateOptionsResult Validate(string? name, CacheSettings options)
    {
        var errors = new List<string>();

        if (options.MaxEntries <= 0)
        {
            errors.Add("MaxEntries must be greater than 0");
        }

        if (string.IsNullOrWhiteSpace(options.CacheDirectory))
        {
            errors.Add("CacheDirectory must not be empty");
        }

        return errors.Count > 0 
            ? ValidateOptionsResult.Fail(errors) 
            : ValidateOptionsResult.Success;
    }
}