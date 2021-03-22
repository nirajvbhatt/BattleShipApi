using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BattleShipApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleShipController : ControllerBase
    {
        private readonly IBoard _battleShipBoard;

        private readonly ILogger<BattleShipController> _logger;

        public BattleShipController(ILogger<BattleShipController> logger, IBoard board)
        {
            _logger = logger;
            _battleShipBoard = board;
        }

        /// <summary>
        /// Get Battleship Board as Grid
        /// </summary>
        [HttpGet]
        public string GetBoard()
        {
            var rng = new Random();
            return JsonConvert.SerializeObject(_battleShipBoard.Grid);
        }

        /// <summary>
        /// Provide start and endpoint of the ship you want to add
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostAddShip([FromBody] AddShipRequest addShipRequest)
        {
            _battleShipBoard.AddShip(addShipRequest.StartPosition, addShipRequest.EndPosition);
            return CreatedAtAction(nameof(GetBoard), new Ship(addShipRequest.StartPosition, addShipRequest.EndPosition));
        }

        /// <summary>
        /// Provide x and y coordinates to hit a ship
        /// </summary>
        /// <returns>
        /// If you hit the ship, it returns true, else false
        /// </returns>
        [HttpPatch]
        public bool Attack(Point target)
        {
            return _battleShipBoard.Attack(target);
        }
    }

    public class AddShipRequest
    {
        public Point StartPosition { get; set; }
        public Point EndPosition { get; set; }
    }
}
