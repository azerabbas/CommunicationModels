using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Events;
using UserService.Datas;
using UserService.Models;
using UserService.ViewModels;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserApiDbContext context, IPublishEndpoint publish) : ControllerBase
    {
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUserVM createUserVM)
        {

            // Yeni user yaradilir
            User user = new()
            {
                Id = Guid.NewGuid(),
                Name = createUserVM.Name,
                Surname = createUserVM.Surname,
                Username = createUserVM.Username,
                Password = createUserVM.Password
            };


            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            //Userin datalari evente add olunur
            UserCreatedEvent userCreatedEvent = new()
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Surname = user.Surname,
                Username = user.Username,
            };

            // event butun queue'lara gonderilir. 
            await publish.Publish(userCreatedEvent);

            return Ok();
        }
    }
}
