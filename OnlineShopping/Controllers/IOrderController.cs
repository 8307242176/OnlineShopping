using Microsoft.AspNetCore.Mvc;

namespace OnlineShopping.Controllers
{
    public interface IOrderController
    {
        IActionResult Index();
    }
}