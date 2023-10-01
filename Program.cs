using EspacioCadete;
using EspacioCliente;
using EmpresaDeCadeteria;
using EspacioPedidos;
using EspacioLecturaArchivos;
using System.Threading.Tasks.Dataflow;

string? op;
int i=1, numPedido = 0, x = 0;

List<Pedido> ListPedido = new List<Pedido>();
LecturaArchivos Archivo = new LecturaArchivos();
List<Cadete> ListCadete = Archivo.ArchivoCadete("Cadetes.csv");
Cadeteria? Cadeteria = Archivo.ArchivoCadeteria("Cadeterias.csv", ListCadete);

if(Cadeteria != null){
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
                    Cadeteria.DarDeAltaPedido(numPedido, observacion, NuevoCliente, Idcad);
                    x = cadeteElegido.ListadoPedido.Count;
                    ListPedido.Add(cadeteElegido.ListadoPedido[x-1]);
                    eleccionCad = 0;
                    numPedido++;

                break;
                case "2": 
                    Console.WriteLine("--------Elija el pedido que desea reasignar--------");
                    int eleccionped = EleccionPedido();
                    Pedido PedidoElegido = ListPedido[eleccionped];
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
                    Cadeteria.ReasignarPedido(PedidoElegido, eleccionCad);
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
                    eleccionped = EleccionPedido();
                    PedidoElegido = ListPedido[eleccionped];
                    Console.WriteLine($"{Cadeteria.CambiarEstadoPedido(PedidoElegido, eleccionEstado)}");
                    ListPedido.Remove(PedidoElegido);
                    
                break;
                case "4": 
                    Console.WriteLine("\n--------Informe de cadeteria--------");
                    Cadeteria.MostrarInforme();
                break;
                case "0": 
                break;
            }
        }
    }while(op != "0");
}

int EleccionPedido()
{
    int j = 0;
    foreach(var ped in ListPedido)
    {
        Console.WriteLine($"----Pedido N° {ped.Numero}----");
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