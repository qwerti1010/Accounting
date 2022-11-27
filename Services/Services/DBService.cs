
using DBLibrary;
using DBLibrary.Interfaces;
using DBLibrary.Repositories;
using DBLibrary.Repositories.SQLRep;
using Services.Responses;

namespace Services.Services;

public class DBService
{
    private readonly DbContext _context;
    private readonly IDBRepository _repository;

    public DBService(DbContext context)
    {
        _context = context;
        _repository = new DBRep(_context);
    }

    public string Create()
    {
        _context.Open();
        var count = _repository.CountOfTables();
        if (count == 4)
        {
            _context.Close();
            return "База данных уже существует";
        }
        _repository.Create();
        _context.Close();
        return "База данных создана";
    }   
}
