using EspacioCadete;
using EspacioCliente;
using EspacioPedidos;
namespace EmpresaDeCadeteria;

public class Cadeteria
{
    private string? nombre;
    private string? telefono;
    private List<Cadete> listadoCadetes;
    private List<Pedido> listadoPedidos;

    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Telefono { get => telefono; set => telefono = value; }
    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
    public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        listadoCadetes = new List<Cadete>();
        listadoPedidos = new List<Pedido>();
    }

    public void DarDeAltaPedido(int numero, string obs, Cliente cliente, int idCadete)
    {
        Pedido nuevoPedido = new (numero, obs, cliente.Nombre, cliente.Direccion, cliente.Telefono, cliente.DatosReferenciaDireccion);
        listadoPedidos.Add(nuevoPedido);
        AsignarCadeteAPedido(idCadete, numero);
    }

    public void EliminarPedido(int idPed)
    {   
        if(listadoPedidos != null)
        {
            Pedido? PedidoAEliminar = listadoPedidos.Find(ped => ped.Numero == idPed);
            if(PedidoAEliminar != null)
            {
                listadoPedidos.Remove(PedidoAEliminar);
            }
        }
    }

    public string CambiarEstadoPedido (int numPedido, int estado)
    {
        Pedido? PedidoACambiarEstado = listadoPedidos.Find(ped => ped.Numero == numPedido);
        if(PedidoACambiarEstado != null)
        {
            if(estado == 1)
            {
                PedidoACambiarEstado.Estado = estadoPedido.entregado;
                return "Pedido entregado";
            }
            else 
            {
                PedidoACambiarEstado.Estado = estadoPedido.cancelado;
                return "Pedido cancelado";
            }
        }
        else{
            return "";
        }
        
    }
    public void AsignarCadeteAPedido(int idCadete, int idPedido)
    {
        Cadete? cadeteElegido = listadoCadetes.Find(cad => cad.Id == idCadete);
        Pedido? pedidoElegido = listadoPedidos.Find(ped => ped.Numero == idPedido);
        if(cadeteElegido != null && pedidoElegido != null)
        {
            pedidoElegido.Cadete = cadeteElegido;
        }
    }

    public float JornalACobrar(int idCad)
    {   
        float total = 0;
        foreach(var ped in listadoPedidos)
        {
            if(ped.Cadete.Id == idCad)
            {
                if(ped.Estado == estadoPedido.entregado)
                {
                    total+=500;
                }
            }
        }
        return total;
    }

    public int pedidosPendientes(int idCad)
    {
        int total = 0;
        foreach(var ped in listadoPedidos)
        {
            if(ped.Cadete.Id == idCad)
            {
                if(ped.Estado == estadoPedido.pendiente)
                {
                    total++;
                }
            }
        }
        return total;
    }
    public void MostrarInforme()
    {
        float total = 0;
        foreach(var cad in listadoCadetes)
        {
            Console.WriteLine($"--Cadete N° {cad.Id}--\nNombre: {cad.Nombre}\nPedidos entregados: {JornalACobrar(cad.Id)/500}\nPedidos pendientes: {pedidosPendientes(cad.Id)}\nJornal a cobrar: ${JornalACobrar(cad.Id)}\n\n");
            total+= JornalACobrar(cad.Id);
        }
        Console.WriteLine($"-----------------Nomina total: {total}");
    } 

    public void agregarCadete(List<Cadete> listadoCadetes)
    {
        this.listadoCadetes = listadoCadetes;
    }
}