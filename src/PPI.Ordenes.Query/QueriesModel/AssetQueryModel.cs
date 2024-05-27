using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PPI.Ordenes.Query.Abstractions;

namespace PPI.Ordenes.Query.QueriesModel;
public class AssetQueryModel : IQueryModel<int>
{
    public AssetQueryModel(
        int id,        
        string ticker,
        string nombre,
        int tipoActivo,
        decimal precioUnitario)
    {
        Id = id;        
        Ticker = ticker;
        Nombre = nombre;
        TipoActivo = tipoActivo;
        PrecioUnitario = precioUnitario;
    }

    private AssetQueryModel()
    {
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ObjectId { get; set; }
    public int Id { get; private init; }    
    public string Ticker { get; private init; }
    public string Nombre { get; private init; }
    public int TipoActivo { get; private init; }
    public decimal PrecioUnitario { get; private init; }
}
