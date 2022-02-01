using WoodenGardenApp.Shared.Models.Database;

namespace WoodenGardenFrontServer.Services.IServices;

public interface IOrderService
{
    Task AddOrder(Order order);
}