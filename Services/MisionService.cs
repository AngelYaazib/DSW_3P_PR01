using DSW_3P_PR01.Models;
using DSW_3P_PR01.Repositories;

namespace DSW_3P_PR01.Services;

public class MisionService : IMisionService
{
    private readonly IMisionRepository _repository;

    public MisionService(IMisionRepository repository)
    {
        _repository = repository;
    }

    public List<Mision> ObtenerTodas()
    {
        return _repository.ObtenerTodas();
    }

    public Mision? ObtenerPorId(int id)
    {
        if (id <= 0)
            return null;

        return _repository.ObtenerPorId(id);
    }

    public Mision Agregar(Mision mision)
    {
        ValidarMision(mision);

        mision.Id = 0;
        mision.Completada = false;

        return _repository.Agregar(mision);
    }

    public bool Actualizar(int id, Mision mision)
    {
        if (id <= 0)
            return false;

        ValidarMision(mision);

        var existente = _repository.ObtenerPorId(id);

        if (existente == null)
            return false;

        mision.Id = id;

        return _repository.Actualizar(id, mision);
    }

    public bool Eliminar(int id)
    {
        if (id <= 0)
            return false;

        return _repository.Eliminar(id);
    }

    private static void ValidarMision(Mision mision)
    {
        if (mision == null)
            throw new ArgumentNullException(nameof(mision));

        if (string.IsNullOrWhiteSpace(mision.Nombre))
            throw new ArgumentException("El nombre de la misión es obligatorio.");

        string[] dificultadesValidas = {"Fácil", "Media", "Difícil" };

        if (!dificultadesValidas.Contains(mision.Dificultad, StringComparer.OrdinalIgnoreCase))
            throw new ArgumentException("La dificultad debe ser Fácil, Media o Difícil.");

        if (mision.ExperienciaOtorgada < 0)
            throw new ArgumentException("La experiencia otorgada no puede ser negativa.");
    }
}