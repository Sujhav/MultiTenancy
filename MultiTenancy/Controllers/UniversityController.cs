using Microsoft.AspNetCore.Mvc;

namespace MultiTenancy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UniversityController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("v1{country}")]
        public async Task<IActionResult> getlistv1([FromRoute] string country)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://universities.hipolabs.com/");
            var response = await client.GetAsync($"search?country={country}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("v2{country}")]
        public async Task<IActionResult> getlistv2([FromRoute] string country)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"http://universities.hipolabs.com/search?country={country}");
            var httpClient = _httpClientFactory.CreateClient("universities");

            var response = await httpClient.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                return Ok(result);
            }
            return BadRequest();
        }


        [HttpGet("v3{country}")]
        public async Task<IActionResult> getlistv3([FromRoute] string country)
        {
            var httpClient = _httpClientFactory.CreateClient("universities");
            var response = await httpClient.GetAsync($"search?country={country}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
