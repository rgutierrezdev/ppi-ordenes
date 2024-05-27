using MongoDB.Bson.Serialization;
using PPI.Ordenes.Query.Abstractions;
using PPI.Ordenes.Query.QueriesModel;

namespace PPI.Ordenes.Query.Data.Mappings;
public class AssetMap : IReadDbMapping
{
    public void Configure()
    {
        BsonClassMap.TryRegisterClassMap<AssetQueryModel>(classMap =>
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);

            classMap.MapMember(asset => asset.Id)
                .SetIsRequired(true);

            classMap.MapMember(asset => asset.Ticker)
                .SetIsRequired(true);

            classMap.MapMember(asset => asset.Nombre)
                .SetIsRequired(true);

            classMap.MapMember(asset => asset.TipoActivo)
                .SetIsRequired(true);

            classMap.MapMember(asset => asset.PrecioUnitario)
                .SetIsRequired(true);
        });
    }
}
