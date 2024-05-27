using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using PPI.Ordenes.Query.Abstractions;
using PPI.Ordenes.Query.Data.Repositories.Abstractions;
using PPI.Ordenes.Query.QueriesModel;

namespace PPI.Ordenes.Query.Data.Repositories;
internal class AssetReadOnlyRepository(IReadDbContext readDbContext)
     : BaseReadOnlyRepository<AssetQueryModel, int>(readDbContext), IAssetReadOnlyRepository
{
    public async Task<IEnumerable<AssetQueryModel>> GetAllAsync()
    {
        var sort = Builders<AssetQueryModel>.Sort
            .Ascending(asset => asset.Ticker);            

        var findOptions = new FindOptions<AssetQueryModel>
        {
            Sort = sort
        };

        using var asyncCursor = await Collection.FindAsync(Builders<AssetQueryModel>.Filter.Empty, findOptions);
        return await asyncCursor.ToListAsync();
    }

    public async Task<AssetQueryModel> GetByTickerAsync(string ticker)
    {
        using var asyncCursor = await Collection.FindAsync(queryModel => queryModel.Ticker.Equals(ticker));
        return await asyncCursor.FirstOrDefaultAsync();
    }
}
