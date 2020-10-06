using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShip.BLL.GameLogic
{
    /// <summary>
    /// 
    /// </summary>
    public class Board
    {
        /// <summary>
        /// The battle area width
        /// </summary>
        private int _battleAreaWidth;
        /// <summary>
        /// The battle area height
        /// </summary>
        private int _battleAreaHeight;
        /// <summary>
        /// The current ship index
        /// </summary>
        private int _currentShipIndex;
        /// <summary>
        /// The shot history
        /// </summary>
        private Dictionary<Coordinate, ShotHistory> ShotHistory;

        /// <summary>
        /// Gets the ships.
        /// </summary>
        /// <value>
        /// The ships.
        /// </value>
        public Ship[] Ships { get; private set; }

        /// <summary>
        /// The battleship per player
        /// </summary>
        private int _battleshipPerPlayer;
        /// <summary>
        /// Initializes a new instance of the <see cref="Board" /> class.
        /// </summary>
        /// <param name="battleAreaWidth">Width of the battle area.</param>
        /// <param name="battleAreaHeight">Height of the battle area.</param>
        /// <param name="battleshipPerPlayer">The battleship per player.</param>
        public Board(int battleAreaWidth, int battleAreaHeight, int battleshipPerPlayer)
        {
            ShotHistory = new Dictionary<Coordinate, ShotHistory>();
            _battleAreaWidth = battleAreaWidth;
            _battleAreaHeight = battleAreaHeight;
            Ships = new Ship[battleshipPerPlayer];
            _currentShipIndex = 0;
            _battleshipPerPlayer = battleshipPerPlayer;
        }

        /// <summary>
        /// Fires the shot.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns></returns>
        public FireShotResponse FireShot(Coordinate coordinate)
        {
            var response = new FireShotResponse();
            if(coordinate == null || !IsValidCoordinate(coordinate))
            {
                response.ShotStatus = ShotStatus.Invalid;
                return response;
            }

            if (ShotHistory.ContainsKey(coordinate))
            {
                response.ShotStatus = ShotStatus.Duplicate;
                return response;
            }
            CheckShipsForHit(coordinate, response);
            CheckForVictory(response);
            return response;            
        }

        /// <summary>
        /// Places the ship.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">You can not add another ship," + _battleshipPerPlayer + " is the limit!</exception>
        /// <exception cref="Exception">You can not add another ship, 2 is the limit!</exception>
        public ShipPlacement PlaceShip(PlaceShipRequest request)
        {
            if (_currentShipIndex > _battleshipPerPlayer)
                throw new Exception("You can not add another ship," + _battleshipPerPlayer + " is the limit!");

            if (!IsValidCoordinate(request.Coordinate))
                return ShipPlacement.NotEnoughSpace;

            Ship newShip = ShipCreator.CreateShip(request);
            return PlaceShipOnBoard(request.Coordinate, newShip);
        }

        /// <summary>
        /// Checks for victory.
        /// </summary>
        /// <param name="response">The response.</param>
        private void CheckForVictory(FireShotResponse response)
        {
            if (response.ShotStatus == ShotStatus.HitAndSunk)
            {
                if (Ships.All(s => s.IsSunk))
                    response.ShotStatus = ShotStatus.Victory;
            }
        }

        /// <summary>
        /// Checks the ships for hit.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="response">The response.</param>
        private void CheckShipsForHit(Coordinate coordinate, FireShotResponse response)
        {
            response.ShotStatus = ShotStatus.Miss;

            foreach (var ship in Ships)
            {
                // no need to check sunk ships
                if (ship.IsSunk)
                    continue;

                ShotStatus status = ship.FireAtShip(coordinate);

                switch (status)
                {
                    case ShotStatus.HitAndSunk:
                        response.ShotStatus = ShotStatus.HitAndSunk;
                        ShotHistory.Add(coordinate, Responses.ShotHistory.Hit);
                        response.ShipImpacted = ship.ShipName;
                        break;
                    case ShotStatus.Hit:
                        response.ShotStatus = ShotStatus.Hit;
                        ShotHistory.Add(coordinate, Responses.ShotHistory.Hit);
                        response.ShipImpacted = ship.ShipName;
                        break;
                }

                if (status != ShotStatus.Miss)
                    break;
            }
            if (response.ShotStatus == ShotStatus.Miss)
            {
                ShotHistory.Add(coordinate, Responses.ShotHistory.Miss);
            }
        }

        /// <summary>
        /// Determines whether [is valid coordinate] [the specified coordinate].
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>
        ///   <c>true</c> if [is valid coordinate] [the specified coordinate]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValidCoordinate(Coordinate coordinate)
        {
            return coordinate.XCoordinate >= 1 && coordinate.XCoordinate <= _battleAreaWidth &&
            coordinate.YCoordinate >= 1 && coordinate.YCoordinate <= _battleAreaHeight;
        }

        /// <summary>
        /// Places the ship on board.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="newShip">The new ship.</param>
        /// <returns></returns>
        private ShipPlacement PlaceShipOnBoard(Coordinate coordinate, Ship newShip)
        {
            int positionIndex = 0;
            int maxX = coordinate.XCoordinate + newShip.Width;
            int maxY = coordinate.YCoordinate + newShip.Height;

            for (int i = coordinate.XCoordinate; i < maxX; i++)
            {
                for (int j = coordinate.YCoordinate; j < maxY; j++)
                {               
                    var currentCoordinate = new Coordinate(i, j);
                    if (!IsValidCoordinate(currentCoordinate))
                        return ShipPlacement.NotEnoughSpace;

                    if (OverlapsAnotherShip(currentCoordinate))
                        return ShipPlacement.Overlap;

                    newShip.BoardPositions[positionIndex] = currentCoordinate;
                    positionIndex++;
                }
            }
            AddShipToBoard(newShip);
            return ShipPlacement.Ok;
        }

        /// <summary>
        /// Adds the ship to board.
        /// </summary>
        /// <param name="newShip">The new ship.</param>
        private void AddShipToBoard(Ship newShip)
        {
            Ships[_currentShipIndex] = newShip;
            _currentShipIndex++;
        }

        /// <summary>
        /// Overlapses another ship.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns></returns>
        private bool OverlapsAnotherShip(Coordinate coordinate)
        {
            foreach (var ship in Ships)
            {
                if (ship != null)
                {
                    if (ship.BoardPositions.Contains(coordinate))
                        return true;
                }
            }

            return false;
        }
    }
}
