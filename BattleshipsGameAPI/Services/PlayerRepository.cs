using BattleshipsGameAPI.Services.Options;
using BattleshipsGameDotNET.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BattleshipsGameAPI.Services
{
    public interface IPlayerRepository
    {
        Task<Player> LoadPlayerAsync(string playerId, CancellationToken cancellationToken);
        Task SavePlayerAsync(Player player, CancellationToken cancellationToken);
    }

    internal class JsonPlayerRepository : IPlayerRepository
    {
        private readonly JsonPlayerRepositoryOptions options;

        public JsonPlayerRepository(IOptions<JsonPlayerRepositoryOptions> options)
        {
            this.options = options.Value;
        }

        public async Task<Player> LoadPlayerAsync(string playerId, CancellationToken cancellationToken)
        {
            var playerFilePath = Path.Combine(options.StorageBasePath, $"{playerId}.json");

            if (!File.Exists(playerFilePath))
                return null;

            return JsonConvert.DeserializeObject<Player>(await File.ReadAllTextAsync(playerFilePath, cancellationToken));
        }
        public async Task SavePlayerAsync(Player player, CancellationToken cancellationToken) =>
            await File.WriteAllTextAsync(Path.Combine(options.StorageBasePath, $"{player.Id}.json"), JsonConvert.SerializeObject(player, Formatting.Indented), cancellationToken);
    }
}