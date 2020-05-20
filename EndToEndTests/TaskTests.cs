using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace EndToEndTests
{
    public class TaskTests
    {
        //private readonly Uri _path = new Uri("https://taktask.azurewebsites.net");
        private readonly Uri _path = new Uri("http://localhost:6000");
        
        [Fact]
        public async Task GetTasks()
        {
            var httpClient = new HttpClient() { BaseAddress = _path };

            var res = await httpClient.GetAsync($"Task/House/{Guid.Empty}");

            Assert.True(res.IsSuccessStatusCode);

            var body = await res.Content.ReadAsStringAsync();

            var profile = JsonConvert.DeserializeObject<List<Domain.Task>>(body);

            Assert.True(profile[0].TaskId == Guid.Empty);
        }
    }
}
