using Ardalis.Result;

namespace PPI.Ordenes.Domain.ValueObjects;
public sealed class MontoTotal
{
    public decimal Precio { get; }
    public int Cantidad { get; }
    public int TipoActivo { get; }
    public decimal Valor { get; }
    public decimal Comision { get; }
    public decimal Impuestos { get; }

    private MontoTotal(decimal precio, int cantidad, int tipoActivo, decimal valor, decimal comision, decimal impuestos)
    {
        Precio = precio;
        Cantidad = cantidad;
        TipoActivo = tipoActivo;
        Valor = valor;
        Comision = comision;
        Impuestos = impuestos;
    }

    public static Result<MontoTotal> Create (decimal precio, int cantidad, int tipoActivo)
    {
        var comision = 0m;
        var impuestos = 0m;
        decimal montoTotal;

        switch (tipoActivo)
        {
            case 1: //ACCION
                montoTotal = precio * cantidad;
                comision = montoTotal * 0.06m;
                impuestos = comision * 0.21m;
                break;                
            case 2: //BONO
                montoTotal = precio * cantidad;
                comision = montoTotal * 0.02m;
                impuestos = comision * 0.21m;
                break;
            case 3://FCI               
                montoTotal = precio * cantidad;
                break;
            default:
                return Result<MontoTotal>.Error("Tipo de activo no válido");
        }

        return Result<MontoTotal>.Success(new MontoTotal(precio, cantidad, tipoActivo, montoTotal, comision, impuestos));
    }
}
