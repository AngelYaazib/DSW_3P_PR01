using DSW_3P_PR01.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace DSW_3P_PR01.Repositories;

public class MisionJsonRepository : IMisionRepository
{
    //Crear Ruta
    private readonly string _ruta;
    private readonly JsonSerializerOptions _opciones = new()
    {
        WriteIndented = true
    };

    public MisionJsonRepository(IWebHostEnvironment environment)
    {
        _ruta = Path.Combine(environment.ContentRootPath, "Data","Mision.json");

        Directory.CreateDirectory(Path.GetDirectoryName(_ruta)!);

        if (!File.Exists(_ruta))
        {
            File.WriteAllText(_ruta, "[]");
        }
    }

     private List<Mision> Leer()
    {
        string Json = File.ReadAllText(_ruta);

        return JsonSerializer.Deserialize<List<Mision>>(Json, _opciones) ?? new List<Mision>();
    }

    private void Guardar(List<Mision> datos)
    {
        string json = JsonSerializer.Serialize(datos, _opciones);
        File.WriteAllText(_ruta, json);
    }

    public List<Mision> ObtenerTodas()
    {
        return Leer();
    }

    public Mision? ObtenerPorId(int id)
    {
        return Leer().FirstOrDefault(m => m.Id == id)!;
    }

    public Mision Agregar(Mision nueva)
    {
        var datos = Leer();

        nueva.Id = datos.Count > 0 ? datos.Max(m => m.Id) + 1 : 1;
        datos.Add(nueva);
        Guardar(datos);
        return nueva;
    }

    public bool Actualizar(int id, Mision misionActualizada)
{
    var datos = Leer();
    var misionExistente = datos.FirstOrDefault(m => m.Id == id);

    if (misionExistente == null)
        return false;

    misionExistente.Nombre = misionActualizada.Nombre;
    misionExistente.Dificultad = misionActualizada.Dificultad;
    misionExistente.ExperienciaOtorgada = misionActualizada.ExperienciaOtorgada;
    misionExistente.Completada = misionActualizada.Completada;

    Guardar(datos);
    return true;
}

    public bool Eliminar(int id)
    {
        var datos = Leer();
        var misionExistente = datos.FirstOrDefault(m => m.Id == id);

        if (misionExistente == null)
            return false;

        datos.Remove(misionExistente);
        Guardar(datos);
        return true;
    }
}