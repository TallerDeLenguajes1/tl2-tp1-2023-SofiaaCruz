using EspacioPedidos;

namespace EspacioCadete
{    
    public class Cadete
    {
        const int valorPedido = 500;
        private int id;
        private string? nombre;
        private string? direccion;
        private string? telefono;
        private List<Pedido> listadoPedido;

        public int Id { get => id; set => id = value; }
        public string? Nombre { get => nombre; set => nombre = value; }
        public string? Direccion { get => direccion; set => direccion = value; }
        public string? Telefono { get => telefono; set => telefono = value; }
        public List<Pedido> ListadoPedido { get => listadoPedido; set => listadoPedido = value; }

        //constructor
        public Cadete(int id, string nombre, string direccion, string telefono, List<Pedido> listadoPedido)
        {
            this.id = id;
            this.nombre = nombre; 
            this.direccion = direccion;
            this.telefono = telefono;
            this.listadoPedido = listadoPedido;
        }

        public float JornalACobrar()
        {
            float total = 0;
            if(listadoPedido != null){
                foreach(var pedido in listadoPedido)
                {
                    if(pedido.Estado == estadoPedido.entregado)
                    {
                        total += valorPedido;
                    }
                }
            }

            return total;
        }

        public void TomarPedido(Pedido pedido)
        {
            listadoPedido.Add(pedido);
        }

        public bool EliminarPedido(Pedido ped)
        {
            if(listadoPedido != null)
            {
                return listadoPedido.Remove(ped);            
            }

            return true;
        }

        public int pedidosPendientes()
        {
            int contador = 0;
            foreach(var ped in ListadoPedido)
            {
                if(ped.Estado == estadoPedido.pendiente)
                {
                    contador++;
                }
            }
            return contador;
        }
    }
}