using EspacioCliente;
using EspacioCadete;
namespace EspacioPedidos;

enum estadoPedido{
    pendiente, entregado, cancelado
}

public class Pedido{
    private int numero;
    private string? obs;
    private Cliente? cliente;
    private estadoPedido estado;
    private Cadete cadete;


    public int Numero { get => numero; set => numero = value; }
    public string? Obs { get => obs; set => obs = value; }
    public Cliente? Cliente { get => cliente; set => cliente = value; }
    internal estadoPedido Estado { get => estado; set => estado = value; }
    public Cadete Cadete { get => cadete; set => cadete = value; }

    public Pedido(int numero, string obs, string nomCliennte, string direCliente, string telCliente, string datosRefCliente) //constructor
    {
        this.numero = numero;
        this.obs = obs;
        cliente = new Cliente(nomCliennte, direCliente, telCliente, datosRefCliente);
        estado = estadoPedido.pendiente;
        cadete = new Cadete();
    }
    public string? VerDireccionCliente(Cliente cliente)
    {
        return cliente.Direccion;
    }

    public string? VerDatosCliente(Cliente cliente)
    {
        return cliente.Datos();
    }

    public string MostrarDatosDePedido()
    {
        return $"Número: {numero}\nObservación: {obs}\nEstado: {estado}";
    }

}