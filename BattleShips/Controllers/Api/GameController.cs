namespace BattleShips.Controllers.Api
{
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Http;
    using Logic;

    /// <summary>
    /// WARNING: This WebAPI controller is not stateless
    ///
    /// I know this is not a good practice at all but the exercise required to be fully in .NET
    /// and the user interface will be in Angular.JS so I needed a way without a repository to store
    /// the game in the session status variable.
    ///
    /// Another option would be pass the game object in and out from Javascript all the time what I found is even worst.
    ///
    /// Also is not even RESTfull, as I'm naming the two functions like if it where a SOA web service (this is quite odd also).
    /// </summary>
    public class GameController : ApiController
    {
        // GET api/game/InitGame
        [HttpGet]
        public IHttpActionResult InitGame()
        {
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                var playerShips = new List<Ship> { new Battleship(), new Destroyer(), new Destroyer() };
                var computerShips = new List<Ship> { new Battleship(), new Destroyer(), new Destroyer() };
                var game = session["Game"] = new Game(playerShips, computerShips);
                return this.Ok(game);
            }

            return this.NotFound();
        }

        // GET api/game/UserShoot
        [HttpGet]
        public IHttpActionResult UserShoot(int x, int y)
        {
            var session = HttpContext.Current.Session;
            var game = session?["Game"] as Game;
            if (game != null)
            {
                game.UserShoot(x, y);
                if (!game.Computer.AllShipsSunk)
                {
                    game.ComputerShoot();
                }

                return this.Ok(game);
            }

            return this.NotFound();
        }
    }
}