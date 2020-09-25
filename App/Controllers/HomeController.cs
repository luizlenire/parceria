using App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.Controllers
{
    /* --> † 25/09/2020 - Luiz Lenire. <-- */

    public sealed class HomeController : Controller
    {
        #region --> Private methods. <--

        private IRepository repository { get; set; }

        #endregion --> Private methods. <--

        #region --> Constructors. <--

        public HomeController(IRepository irepository) => repository = irepository;

        #endregion --> Constructors. <--

        #region --> Public methods. <--

        public IActionResult Index() => View(repository.listControleParceria);

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        #endregion --> Public methods. <--
    }
}
