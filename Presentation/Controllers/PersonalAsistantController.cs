using Azure.Core;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/ai")]
    [ApiController]
    [Consumes("multipart/form-data")]
    public class PersonalAsistantController:ControllerBase
    {
       
        private readonly ILoggerService _logger;


        public PersonalAsistantController (ILoggerService logger)
        {
           
            _logger = logger;
        }
        [HttpPost]
        [Authorize(Roles = "Admin,CompanyOwner,Manager")]

        public async Task<IActionResult> SendRequestMicroService([FromForm] PersonalAsistantDto aiDto)
        {
            await _logger.LogInfo($" [POST] AI Personal Asistant için istek gönderildi. User: {aiDto.UserId}");
            var client=new HttpClient();
            var request = new MultipartFormDataContent();

            request.Add(new StringContent(aiDto.Query), "text");
            request.Add(new StringContent(aiDto.UserId), "user_id");

            if (aiDto.File != null && aiDto.File.Length > 0)
            {
                var stream = aiDto.File.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(aiDto.File.ContentType);
                request.Add(fileContent, "file", aiDto.File.FileName);
            }
            

            var response = await client.PostAsync("http://127.0.0.1:8012/gemma-chat-bot",request);

            var result= await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
