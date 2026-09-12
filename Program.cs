// https://epimient.github.io/Profundizaci-n_en_NET/#/clase-05/doc
/*
 * To do: 1. Clear screen 2. fix ShowMenu function, it should not do validations.
*/
//products.Add(new object[] { "Test", 3m, 5 });

List<object[]> products = new(5);

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
static int ReadIntegrer(string message, int min, int max)
{
    int value;

    do
    {
        Console.WriteLine(message);
        Console.WriteLine($"Rango permitdo: {min} - {max} ");
        ShowInput();

        if (!int.TryParse(Console.ReadLine(), out value))
        {
            ShowError("Ingrese solo numeros enteros.");
        }
        else if (value > max || value < min)
        {
            ShowError($"Ingrese un valor dentro del rango establecido ({min} - {max}).");
        }
        else
        {
            break;
        }
    }
    while (true);

    return value;
}
static decimal ReadDecimal(string message, decimal min)
{
    decimal value;
    do
    {
        Console.WriteLine(message);
        Console.WriteLine($"El valor no puede ser menor a {min}");
        ShowInput();

        if (!decimal.TryParse(Console.ReadLine(), out value))
        {
            ShowError("Ingrese solo numeros.");
        }
        else if (value < min)
        {
            ShowError($"El valor no fue mayor ni igual al establecido ({min})");
        }
        else
        {
            break;
        }
    }
    while (true);

    return value;
}
static string ReadString(string message)
{
    string? value;

    Console.WriteLine(message);
    ShowInput();

    do
    {
        value = Console.ReadLine();

        if (string.IsNullOrEmpty(value))
        {
            ShowError("Este campo no puede estar vacío.");
        }
        else
        {
            break;
        }
    }
    while (true);

    return value;
}
static bool IsNameValid(string name, List<object[]> list)//Validates the name of the product, it should not exist two products with the same name
{
    int i = 0;

    for (i = 0; i < list.Count; i++)
    {
        if (list[i][0].ToString() == name)
        {
            return false;
        }
    }

    return true;
}