static void ShowInput()
{
    Console.Write("> ");
}
static void ShowError(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ResetColor();
}
static int ShowMenu()
{
    int opcion;

    Console.WriteLine("==================================================");
    Console.WriteLine(" SISTEMA GESTOR DE VENTAS E INVENTARIO (MINI-POS) ");
    Console.WriteLine("==================================================");
    Console.WriteLine(" Seleccione una opcion:                           ");
    Console.WriteLine(" 1. Registrar nuevo producto en inventario.       ");
    Console.WriteLine(" 2. Consultar inventario.                         ");
    Console.WriteLine(" 3. Registrar una venta.                          ");
    Console.WriteLine(" 4. Ver reporte de caja y estadisticas diarias.   ");
    Console.WriteLine(" 5. Salir.                                        ");
    Console.WriteLine("==================================================");
    ShowInput();

    try
    {
        opcion = Convert.ToInt16(Console.ReadLine());
    }
    catch
    {
        //ShowError(e.ToString());
        ShowError("No ingreso una opcion valida.");
        opcion = 0;
    }

    return opcion;
}


do
{
    int opcion = ShowMenu();

    switch (opcion)
    {
        case 1:

            break;
        default:

            break;
    }
}
while (true);