using EmpresaDeCadeteria;
using EspacioCadete;
using EspacioPedidos;

namespace EspacioLecturaArchivos;

public class LecturaArchivos
{
    public List<Cadete> ArchivoCadete(string nombreArchivo)
    {
        List<Cadete> listaCadete = new List<Cadete>();
        StreamReader Cad = new StreamReader(nombreArchivo); 
        string? linea;
        int a;

        while((linea = Cad.ReadLine()) != null)
        {
            string[] aux = linea.Split(",").ToArray();
            List<Pedido> ped = new List<Pedido>();
            int.TryParse(aux[0], out a);
            Cadete nuevCadete = new Cadete(a, aux[1], aux[2], aux[3], ped);
            listaCadete.Add(nuevCadete);
        }

        return listaCadete;
    }

    public Cadeteria? ArchivoCadeteria(string nombreArchivo, List<Cadete> ListCadete)
    {
        StreamReader Cadeteria = new StreamReader(nombreArchivo);
        string? linea;
        string[]? aux = null;
        while ((linea = Cadeteria.ReadLine()) != null)
        {
            aux = linea.Split(",");
        }
        if(aux != null)
        {
            Cadeteria cad = new Cadeteria(aux[0], aux[1], ListCadete);
            return cad;
        }
        else
        {
            return null;
        }


    }
}

