using MVC_web.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Reflection.Emit;

namespace MVC_web.Services
{
    public class PlayersServices : IPlayersServices
    {
        private readonly IMongoCollection<Players> _Players;

        public PlayersServices(IAssessment2DatabaseSetting settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString/*"mongodb://localhost:27017"*/);
            var database = mongoClient.GetDatabase(settings.DatabaseName/*"Assessment2"*/);
            _Players = database.GetCollection<Players>(settings.PlayersCollectionName/*"Players"*/);
        }

        public Players Create(Players player)
        {

            _Players.InsertOne(player);
            return player;

        }

        public string Delete_with_ID(int id)
        {
            _Players.DeleteOne(player => player.Id == id);
            return "Deleted";
        }

        public List<Players> GetAll()
        {
            return _Players.Find(players => true).ToList();
            
        }

        public Players Get_with_ID(int id)
        {
            return _Players.Find(player => player.Id == id).FirstOrDefault();
        }

        public string Update_with_ID(int id, Players player)
        {
            _Players.ReplaceOne(player => player.Id == id, player);
            return ("Player with Id = "+player.Id+ " has been updated"); 
        }
        public string TopRank(Players player)
        {
            List<Players> players = _Players.Find(players => true).ToList(); ;
            foreach (var ranker in players)
            {
                if (ranker.Rank.Equals(1))
                {
                   return ("Player " + player + " is ranked number One");

                }
            }
            return null!;
        }
        public List<Players> Get_Player_Rank()
        {
            var player = _Players.Find(players => true).ToList();
            var sortedplayers = player.OrderBy(player => player.Rank).ToList();
            return sortedplayers;
        }
    //need fixing 
        public void PlayerRanker(Players players)
        {
            List<Players> existingPlayers = _Players.Find(players => true).ToList();
             var sortedExistingPlayers = existingPlayers.OrderBy(player => player.Rank).ToList();
            foreach (var existPlayer in sortedExistingPlayers)
            {
                if (existPlayer.Rank.Equals(players.Rank))
                {
                    existPlayer.Rank = int.Parse(existPlayer.Rank.ToString()) + 1;
                    players = existPlayer;
                }

            }
        }

        public string Update_Multiple(Players player)
        {       int id = player.Id;
            _Players.ReplaceOne(player => player.Id == id, player);
            return ("Player with Id = "+player.Id+ " has been updated");
        }
    }
}
