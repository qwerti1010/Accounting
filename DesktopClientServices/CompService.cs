
using DBLibrary.Entities.DTOs;
using DesktopClientServices.Responses;
using System.Net.Http.Json;

namespace DesktopClientServices;

public class CompService
{
    private readonly HttpClient _client;

    public CompService(HttpClient httpClient)
    {
        _client = httpClient;
    }

    public async Task<IList<ComputerDTO>?> GetAllAsync()
    {
        return await _client.GetFromJsonAsync<IList<ComputerDTO>>
            ("https://localhost:7049/api/Computer/GetAll");
    }

    public async Task<IList<ComputerDTO>?> GetNextAsync()
    {
        return await _client.GetFromJsonAsync<IList<ComputerDTO>>
            ("https://localhost:7049/api/Computer/Next");
    }

    public async Task<IList<ComputerDTO>?> GetPreviousAsync()
    {
        return await _client.GetFromJsonAsync<IList<ComputerDTO>>
            ("https://localhost:7049/api/Computer/Previous");
    }

    public async Task<Response<ComputerDTO>> GetByIDAsync(uint id)
    {
        using var response = await _client.GetAsync($"https://localhost:7049/api/Computer/GetByID/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return new Response<ComputerDTO>
            {
                Message = $"Компьютер с id:{id} не найден"
            };
        }

        var result = await response.Content.ReadFromJsonAsync<ComputerDTO>();
        return new Response<ComputerDTO>
        {
            IsSuccess = true,
            Message = $"Компьютер с id:{result!.ID} найден",
            Value = result
        };
    } 

    public async Task<string> DeleteAsync(uint id)
    {
        using var response = await _client.DeleteAsync($"https://localhost:7049/api/Computer/Delete?id={id}");         
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<IList<ComputerDTO>?> GetFilteredAsync(string name, string price, string status, string empId)
    {
        var filter = new ComputerFilterDTO{ NameFilter = name, StatusFilter = status};
        if (decimal.TryParse(price, out decimal p))
        {
            filter.PriceFilter = p;
        }
        if(uint.TryParse(empId, out uint id))
        {
            filter.EmplID = id;
        }

        using var response = await _client.PostAsJsonAsync("https://localhost:7049/api/Computer/Filters", filter);
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<IList<ComputerDTO>>();
    }

    public async Task<Response<ComputerDTO>> CreateAsync(string name, DateTime regDate,
        string price, string status, uint empID, DateTime expSt, IList<PropertyDTO> properties)
    {
        if (!decimal.TryParse(price, out decimal p))
        {
            return new Response<ComputerDTO>
            {
                Message = "Неверный формат цены"
            };
        }
        var computer = new ComputerCreateDTO
        {
            Name = name,
            RegistrationDate = regDate,
            Price = p,
            Status = status,
            EmployeeID = empID,
            ExploitationStart = expSt,
            Properties = properties
        };

        using var response = await _client.PostAsJsonAsync("https://localhost:7049/api/Computer/Create", computer);
        var result = await response.Content.ReadAsStringAsync();
        return new Response<ComputerDTO>
        {
            IsSuccess = response.IsSuccessStatusCode,
            Message = result
        };
    }

    public async Task<Response<ComputerDTO>> UpdateAsync(ComputerDTO dto)
    {
        using var response = await _client.PutAsJsonAsync("https://localhost:7049/api/Computer/Change", dto);
        var result = await response.Content.ReadAsStringAsync();
        return new Response<ComputerDTO>
        {
            IsSuccess = response.IsSuccessStatusCode,
            Message = result
        };
    }


}
