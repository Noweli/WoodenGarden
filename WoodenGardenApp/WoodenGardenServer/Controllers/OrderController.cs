using Microsoft.AspNetCore.Mvc;
using WoodenGardenApp.Shared.Helpers;
using WoodenGardenApp.Shared.Models.Api;
using WoodenGardenApp.Shared.Models.Database;
using WoodenGardenServer.Data;
using WoodenGardenServer.Properties;

namespace WoodenGardenServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController
{
    private readonly ApplicationDbContext _dbContext;

    public OrderController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddOrder([FromBody] Order order)
    {
        if (order.Description.IsNullOrWhiteSpace())
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_Order_RequiredFieldMissing.Format("Description"));
        }

        if (order.RequesterEmail.IsNullOrWhiteSpace())
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_Order_RequiredFieldMissing.Format("Email"));
        }

        if (order.RequesterName.IsNullOrWhiteSpace())
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_Order_RequiredFieldMissing.Format("Name"));
        }

        try
        {
            _ = await _dbContext.Orders!.AddAsync(order);
            _ = await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new UnprocessableEntityObjectResult(new ErrorModel
            {
                ErrorMessage = ErrorMessages.ApiError_Order_FailedToAddOrder,
                ExceptionMessage = e.Message
            });
        }
        
        return new OkResult();
    }
}