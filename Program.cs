// https://epimient.github.io/Profundizaci-n_en_NET/#/clase-05/doc
/*
 * To do: 2. fix ShowMenu function, it should not do validations.
*/

List<object[]> products = new(5)
{
    new object[] { "Arroz", 2500m, 20 },
    new object[] { "Leche", 4500m, 15 },
    new object[] { "Pan", 3000m, 5 }
};

do
{
    int opcion = ShowMenu();

    switch (opcion)
    {
        case 1:
            RegisterProduct(products);
            Console.Clear();

            break;
        case 2:
            ShowInventory(products);
            Console.Clear();

            break;
        case 3:
            RegisterSell(products);

            break;
        case 5:
            Console.WriteLine("Gracias por usar este programa!");
            Environment.Exit(0);

            break;
        default:
            Console.Clear();
            ShowError("Opcion no valida.");

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
    
    do
    {
        Console.WriteLine(message);
        ShowInput();

        value = Console.ReadLine();

        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
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
    int i;

    for (i = 0; i < list.Count; i++)
    {
        if (list[i][0].ToString() == name)
        {
            return false;
        }
    }

    return true;
}
static void RegisterProduct(List<object[]> list)
{
    string name;
    decimal price;
    int quantity;

    do
    {
        name = ReadString("Ingrese el nombre del producto.");

        if (!IsNameValid(name, list))
        {
            ShowError("El nombre del producto ya existe en el inventario.");
        }
        else
        {
            break;
        }
    }
    while (true);

    price = ReadDecimal("Ingrese el precio del producto.", 0);
    quantity = ReadIntegrer("Ingrese la cantidad de productos (cantidad disponible).", 1, 1000);

    list.Add([ name, price, quantity ]);
}
static void ShowInventory(List<object[]> list, bool stopUser = true)
{
    if (list.Count == 0)
    {
        ShowError("No hay productos registrados en el inventario para mostrar.");
        AskContinue();
        return;
    }

    int i;
    string info;

    Console.WriteLine("=======================================================");
    Console.WriteLine(" INVENTARIO DE PRODUCTOS                               ");
    Console.WriteLine("=======================================================");
    Console.WriteLine("ID | Nombre del producto | Precio | Cantidad disponible");
    Console.WriteLine("-------------------------------------------------------");

    for (i = 0; i < list.Count; i++)
    {
        info = $"{i + 1} | {list[i][0]} | {list[i][1]:C2} | {list[i][2]}";
        
        if (Convert.ToInt32(list[i][2]) <= 5)
        {
            info += " | ¡Poco stock!";
        }

        Console.WriteLine(info);
    }

    Console.WriteLine("=======================================================");
    
    if (stopUser)
    {
        AskContinue();
    }
}
static void AskContinue()
{
    Console.WriteLine("Presione cualquier tecla para continuar.");
    ShowInput();

    Console.ReadKey();
}
static void RegisterSell(List<object[]> list)
{
    int product, quantity;
    bool discount;
    decimal total, ivaAmount, discountAmount, subtotal;

    ShowInventory(list, false);

    if (list.Count == 0)
    {
        return;
    }

    product = ReadIntegrer("Seleccione el ID del producto a vender.", 1, list.Count) - 1;
    quantity = ReadIntegrer("Ingrese la cantidad a vender.", 1, Convert.ToInt32(list[product][2]));
    discount = AskYesNo("Aplica descuento de cliente frecuente (10%)? (S/N).");
    
    decimal productPrice = Convert.ToDecimal(list[product][1]);
    total = CalculateInvoice(productPrice, quantity, discount, out ivaAmount, out discountAmount, out subtotal);
    list[product][1] = Convert.ToInt32(list[product][1]) - quantity;

    string productName = Convert.ToString(list[product][0]) ?? "";
    ShowInvoice(productName, quantity, subtotal, discountAmount, ivaAmount, total);
}
static char ReadChar(string message)
{
    while (true)
    {
        Console.WriteLine(message);
        ShowInput();
        string input = Console.ReadLine() ?? "";

        if (input.Length == 1)
        {
            return input[0];
        }

        ShowError("Ingrese un solo carácter.");
    }
}
static bool AskYesNo(string message)
{
    bool yes;

    do
    {
        char response = ReadChar(message);

        if (char.ToUpper(response) == 'S')
        {
            yes = true;
            break;
        }
        else if (char.ToUpper(response) == 'S')
        {
            yes = false;
            break;
        }
        else
        {
            ShowError("Debe responder S o N (Si o No).");
        }
    }
    while (true);

    return yes;
}
static decimal CalculateInvoice(decimal price, int quantity, bool discount, out decimal ivaAmount, out decimal discountAmount, out decimal subtotal)
{
    decimal relativeTotal = price * quantity;
    subtotal = price * quantity;
    discountAmount = 0;

    if (discount)
    {
        discountAmount = relativeTotal * 0.1m;
        relativeTotal *= 0.9m;
    }

    ivaAmount = relativeTotal * 0.19m;

    return relativeTotal + ivaAmount;
}
static void ShowInvoice(string productName, int quantity, decimal subtotal, decimal discount, decimal iva, decimal total)
{
    Console.WriteLine("============================================");
    Console.WriteLine("               Ticket de venta              ");
    Console.WriteLine("============================================");
    Console.WriteLine($"Producto:         {productName} (x{quantity})");
    Console.WriteLine($"Subtotal:         {subtotal:C2}");
    Console.WriteLine($"Descuento (10%):  -{discount:C2}");
    Console.WriteLine($"IVA (19%):        {iva:C2}");
    Console.WriteLine("--------------------------------------------");
    Console.WriteLine($" Total a pagar:   {total:C2}");
    Console.WriteLine("============================================");

    AskContinue();
}