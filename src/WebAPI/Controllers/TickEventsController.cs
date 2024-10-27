using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QubicMicroservice.Controllers;

[ApiController]
[Route("[controller]")]
public class TickEventsController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public TickEventsController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetTickEvents([FromQuery] int tick)
    {
        //http://localhost:5226/TickEvents?tick=16317860 
        var request = new { tick = tick };
        var jsonContent = JsonConvert.SerializeObject(request);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://api.qubic.org/v1/events/getTickEvents", content);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            //Console.WriteLine("responseData: " + responseData);

            // Deserialize the response data to a dynamic object
            // var deserializedData = JsonConvert.DeserializeObject(responseData);

            // Return the deserialized data as JSON
            return Ok(responseData);
        }
        else
        {
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
    }
}
