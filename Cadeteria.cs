using EspacioCadete;
using EspacioCliente;
using EspacioPedidos;
namespace EmpresaDeCadeteria;

public class Cadeteria
{
    private string? nombre;
    private string? telefono;
    private List<Cadete> listadoCadetes;
    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Telefono { get => telefono; set => telefono = value; }
    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }

    public Cadeteria(string nombre, string telefono, List<Cadete> listadoCadetes)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.listadoCadetes = listadoCadetes;
    }

    public void DarDeAltaPedido(int numero, string obs, Cliente cliente, int idCadete)
    {
        Pedido nuevoPedido = new (numero, obs, cliente.Nombre, cliente.Direccion, cliente.Telefono, cliente.DatosReferenciaDireccion);

        AsinarPedidoACadete(nuevoPedido, idCadete);
    }
    public void AsinarPedidoACadete(Pedido pedido, int idCad)
    {
        if(listadoCadetes != null)
        {
            foreach(var cad in listadoCadetes)
            {
                if(cad.Id == idCad)
                {
                    cad.TomarPedido(pedido);
                }
            }
        }
    }

    public string CambiarEstadoPedido (Pedido pedido, int estado)
    {
        if(estado == 1)
        {
            pedido.Estado = estadoPedido.entregado;
            return "Pedido entregado";
        }
        else
        {
            pedido.Estado = estadoPedido.cancelado;
            foreach(var cad in listadoCadetes)
            {
                foreach(var ped in cad.ListadoPedido)
                {
                    if(ped.Numero == pedido.Numero){
                        cad.EliminarPedido(ped);
                    }
                }
            }
            return "Pedido cancelado";
        }
    }

    public void ReasignarPedido(Pedido pedido, int nuevCad)
    {
        if(listadoCadetes!=null)
        {
            foreach(var cad in listadoCadetes)
            {
                if(cad.ListadoPedido !=null)
                {
                    foreach(var ped in cad.ListadoPedido)
                    {
                        if(ped.Numero == pedido.Numero)
                        {
                            cad.EliminarPedido(ped);
                        }
                    }

                    AsinarPedidoACadete(pedido, nuevCad);
                }
            }
        }
    }

    public void MostrarInforme()
    {
        float total = 0;
        foreach(var cad in listadoCadetes)
        {
            Console.WriteLine($"--Cadete N° {cad.Id}--\nNombre: {cad.Nombre}\nPedidos entregados: {cad.JornalACobrar()/500}\nPedidos pendientes: {cad.pedidosPendientes()}\nJornal a cobrar: ${cad.JornalACobrar()}\n\n");
            total+= cad.JornalACobrar();
        }
        Console.WriteLine($"-----------------Nomina total: {total}");
    }

    
}