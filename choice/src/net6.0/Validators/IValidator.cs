using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.Validators
{
    public interface IValidator
    {
        Task<bool> Validate();

        Dictionary<string, string> Fails { get; }
    }
}
