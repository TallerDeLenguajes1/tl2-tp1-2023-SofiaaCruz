using EspacioCadete;
using EspacioCliente;
using EmpresaDeCadeteria;
using EspacioPedidos;
using EspacioLecturaArchivos;
using System.Threading.Tasks.Dataflow;

string? op;
int i=1, numPedido = 0, opArchivo;
List<Cadete>? ListCadete;
List<Cadeteria>? ListCadeteria;

Console.Write("Elija el tipo de archivo que desea leer\n1-Csv\n2-Json\n>> ");
int.TryParse(Console.ReadLine(), out opArchivo);
while(opArchivo < 1 || opArchivo > 2)
{
    Console.Write("Debe elegir una opción valida\n>> ");
    int.TryParse(Console.ReadLine(), out opArchivo);
}
if(opArchivo == 1)
{
    ArchivosCsv Archivo = new ArchivosCsv();
    ListCadete = Archivo.ArchivoCadete("Cadetes.csv");
    ListCadeteria = Archivo.ArchivoCadeteria("Cadeteria.csv");
}
else
{
    ArchivosJson Archivo = new ArchivosJson();
    ListCadete = Archivo.ArchivoCadete("Cadetes.json");
    ListCadeteria = Archivo.ArchivoCadeteria("Cadeteria.json");
}
Cadeteria? Cadeteria = null;
if(ListCadeteria!= null && ListCadete != null)
{
    Cadeteria = ListCadeteria[0];
    Cadeteria.ListadoCadetes = ListCadete;
}

if(Cadeteria != null && ListCadete != null){
    do
    {
        
        Console.WriteLine("\n\nIngrese una opcion");
        Console.WriteLine("1- Dar de alta un pedido");
        Console.WriteLine("2- Reasignar un pedido");
        Console.WriteLine("3- Cambiar estado o cancelar pedido");
        Console.WriteLine("4- Mostrar informe");
        Console.Write("0- Salir\n>> ");
        
        op = Console.ReadLine();
        if(op != "0")
        {
            switch (op)
            {
                case "1": 
                    Console.WriteLine("► Ingrese el nombre del cliente");
                    string? nombreCliente = Console.ReadLine();
                    Console.WriteLine("► Ingrese la dirección del cliente");
                    string? direCliente = Console.ReadLine();
                    Console.WriteLine("► Ingrese el teléfono del cliente");
                    string? telCliente = Console.ReadLine();
                    Console.WriteLine("► Ingrese datos de referencia de la direccion del cliente");
                    string? datosDeRef = Console.ReadLine();

                    while(string.IsNullOrEmpty(nombreCliente) || string.IsNullOrEmpty(direCliente) || string.IsNullOrEmpty(telCliente) || string.IsNullOrEmpty(datosDeRef))
                    {
                        Console.WriteLine("Error, ingrese valores validos");
                        Console.WriteLine("► Ingrese el nombre del cliente");
                        nombreCliente = Console.ReadLine();
                        Console.WriteLine("► Ingrese la dirección del cliente");
                        direCliente = Console.ReadLine();
                        Console.WriteLine("► Ingrese el teléfono del cliente");
                        telCliente = Console.ReadLine();
                        Console.WriteLine("► Ingrese datos de referencia de la direccion del cliente");
                        datosDeRef = Console.ReadLine();
                    }
                    Cliente NuevoCliente = new Cliente(nombreCliente, direCliente, telCliente, datosDeRef);

                    Console.WriteLine($"------Pedido N° {numPedido}------\n► Ingrese una observacion");
                    string? observacion = Console.ReadLine();
                    while(string.IsNullOrEmpty(observacion))
                    {
                        Console.WriteLine("Debe ingresar una observación");
                        observacion = Console.ReadLine();
                    }

                    Console.WriteLine("---------Elija un cadete--------");
                    int eleccionCad = 0;
                    while(eleccionCad < 1 || eleccionCad > i)
                    {
                        foreach(var cad in ListCadete)
                        {
                            Console.WriteLine($"► {cad.Id} {cad.Nombre}");
                            i++;
                        }
                        int.TryParse(Console.ReadLine(), out eleccionCad);
                        if(eleccionCad < 1 || eleccionCad > i)
                        {
                            Console.WriteLine("Error, debe elegir una opción valida");
                            i=1;
                        }
                    }
                    i=1;
                    Cadete cadeteElegido = ListCadete[eleccionCad-1];
                    int Idcad = cadeteElegido.Id;
                    Cadeteria.DarDeAltaPedido(numPedido, observacion, NuevoCliente, cadeteElegido.Id);
                    eleccionCad = 0;
                    numPedido++;

                break;
                case "2": 
                    Console.WriteLine("--------Elija el pedido que desea reasignar--------");
                    int eleccionped = EleccionPedido(Cadeteria);
                    Pedido PedidoElegido = Cadeteria.ListadoPedidos[eleccionped];
                    int j=0;
                    Console.WriteLine("--------Ahora elija al cadete que le asignara el pedido---------");
                    foreach(var cad in ListCadete)
                    {
                        Console.WriteLine($"{cad.Id}-{cad.Nombre}");
                        j++;
                    }
                    int.TryParse(Console.ReadLine(), out eleccionCad);
                    while(eleccionCad < 1 || eleccionCad > j )
                    {
                        Console.WriteLine("Error, debe elegir una opcion valida");
                        int.TryParse(Console.ReadLine(), out eleccionCad);
                    }
                    cadeteElegido = ListCadete[eleccionCad-1];
                    Cadeteria.AsignarCadeteAPedido(cadeteElegido.Id, PedidoElegido.Numero);
                break;
                case "3": 
                    Console.Write("Que desea relizar?\n1-Cambiar Estado\n2-Cancelar\n>> ");
                    int eleccionEstado;
                    int.TryParse(Console.ReadLine(), out eleccionEstado);
                    while(eleccionEstado != 1 && eleccionEstado != 2)
                    {
                        Console.WriteLine("Error, debe ingresar una opcion valida");
                        int.TryParse(Console.ReadLine(), out eleccionEstado);
                    }
                    string aux;
                    if(eleccionEstado == 1)
                    {
                        aux = "cambiar de estado";
                    }
                    else
                    {
                        aux = "cancelar";
                    }
                    Console.WriteLine($"--------Elija el pedido que desea {aux}---------");
                    eleccionped = EleccionPedido(Cadeteria);
                    PedidoElegido = Cadeteria.ListadoPedidos[eleccionped];
                    Console.WriteLine($"{Cadeteria.CambiarEstadoPedido(PedidoElegido.Numero, eleccionEstado)}");
                    
                break;
                case "4": 
                    Console.WriteLine("\n--------Informe de cadeteria--------");
                    foreach(var cad in Cadeteria.ListadoCadetes)
                    {
                        Console.WriteLine($"\n{Cadeteria.MostrarInforme(cad.Id)}");
                    }
                    Console.WriteLine($"\n---------------Nomina Total:{Cadeteria.NominaTotal()}");
                break;
                case "0": 
                break;
            }
        }
    }while(op != "0");
}

int EleccionPedido(Cadeteria cadeteria)
{
    int j = 0;
    foreach(var ped in cadeteria.ListadoPedidos)
    {
        Console.WriteLine($"\n----Pedido N° {ped.Numero}----");
        Console.WriteLine($"{ped.MostrarDatosDePedido()}");
        j++;
    }
    int eleccionped;
    int.TryParse(Console.ReadLine(), out eleccionped);
    while(eleccionped < 0 || eleccionped > j)
    {
        Console.WriteLine("Error, debe elegir una opcion valida");
        int.TryParse(Console.ReadLine(), out eleccionped);
    }
    return eleccionped;
}