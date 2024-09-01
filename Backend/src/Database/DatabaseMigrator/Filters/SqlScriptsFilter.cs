using DbUp.Engine;
using DbUp.Support;

namespace DatabaseMigrator.Filters
{
    public class SqlScriptsFilter : IScriptFilter
    {
        public IEnumerable<SqlScript> Filter(
            IEnumerable<SqlScript> sorted, 
            HashSet<string> executedScriptNames, 
            ScriptNameComparer comparer)
        {
            return sorted.OrderBy(s => s.Name.Split('-')[1]);
        }
    }
}