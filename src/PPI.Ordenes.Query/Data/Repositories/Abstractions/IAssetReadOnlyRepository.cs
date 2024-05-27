using System.Collections.Generic;
using System.Threading.Tasks;
using PPI.Ordenes.Query.Abstractions;
using PPI.Ordenes.Query.QueriesModel;

namespace PPI.Ordenes.Query.Data.Repositories.Abstractions;
public interface IAssetReadOnlyRepository : IReadOnlyRepository<AssetQueryModel, int>
{
    Task<IEnumerable<AssetQueryModel>> GetAllAsync();

    Task<AssetQueryModel> GetByTickerAsync(string ticker);
}
