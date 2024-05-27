INSERT INTO [dbo].[Accounts]
    ([Id], [IDCuenta], [PrimerNombre], [SegundoNombre], [TercerNombre], [Apellido], [CuentaComitente], [Estado], [FechaCreacion], [FechaActualizacion])
VALUES
    (NEWID(), 123, 'Juan', 'Carlos', 'Alberto', 'Perez', 456789, 1, GETDATE(), NULL);

