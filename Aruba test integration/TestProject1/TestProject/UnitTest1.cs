using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using FluentAssertions;
using Domain.Models.DB;
using System.Text.Json;

namespace TestProject
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public UnitTest1(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task getAllItemAsync()
        {
            var response = await _client.GetAsync("/api/Attivita/getAllItem");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<Attivita[]>(responseContent);
            items.Should().BeOfType<Attivita[]>();

        }
    }
}