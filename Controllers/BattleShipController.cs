using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BattleShipApi.Model;
using BattleShipApi.Services;

namespace BattleShipApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleShipController : ControllerBase
    {
        private readonly IBattleShipService _battleShipService;

        private readonly ILogger<BattleShipController> _logger;

        public BattleShipController(ILogger<BattleShipController> logger, IBattleShipService battleShipService)
        {
            _logger = logger;
            _battleShipService = battleShipService;
        }

        /// <summary>
        /// Get Battleship Board as Grid
        /// </summary>
        [HttpGet]
        public string GetBoard()
        {
            return _battleShipService.GetBoard();
        }

        /// <summary>
        /// Provide start and endpoint of the ship you want to add
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostAddShip([FromBody] AddShipRequest addShipRequest)
        {
            _battleShipService.AddShip(addShipRequest.StartPosition, addShipRequest.EndPosition);
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
            return _battleShipService.Attack(target);
        }
    }

    public class AddShipRequest
    {
        public Point StartPosition { get; set; }
        public Point EndPosition { get; set; }
    }
}
