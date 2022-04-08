using Dapper;

namespace Library.Database
{
    public class DynamicParametersBuilder
    {
        private readonly DynamicParameters _dynamicParameters = new DynamicParameters();

        public DynamicParametersBuilder Append(string key, object value)
        {
            _dynamicParameters.Add(key, value);

            return this;
        }

        public DynamicParameters Build() => _dynamicParameters;
    }
}