using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerHUD
    {
        public Player player;

        public TextMeshProUGUI rankText;
        public TextMeshProUGUI moneyText;
        public TextMeshProUGUI pointText;

    }
    public TextMeshProUGUI roundText;

    [SerializeField] public List<PlayerHUD> playerHUDs;

    private Dictionary<Player, (int points, int money)> previousStats = new();

    private void Start()
    {
        CacheCurrentStats();
        UpdateRankings();
        roundText.text = "0 / 3";
    }

    private void Update()
    {
        if (StatsChanged())
        {
            UpdateRankings();
            CacheCurrentStats();
        }
    }

    public void ChangeRound(int round, int maxRound)
    {
        roundText.text = round + "/" + maxRound;
    }

    private bool StatsChanged()
    {
        foreach (var hud in playerHUDs)
        {
            if (!previousStats.TryGetValue(hud.player, out var prev))
                return true;

            if (hud.player.point != prev.points || hud.player.money != prev.money)
                return true;
        }
        return false;
    }

    public void CacheCurrentStats()
    {
        previousStats.Clear();
        foreach (var hud in playerHUDs)
        {
            previousStats[hud.player] = (hud.player.point, hud.player.money);
        }
    }

    public void UpdateRankings()
    {
        var sortedPlayers = playerHUDs
            .OrderByDescending(p => p.player.point)
            .ThenByDescending(p => p.player.money)
            .ToList();

        Dictionary<Player, string> playerRanks = new Dictionary<Player, string>();
        int currentRank = 1;

        for (int i = 0; i < sortedPlayers.Count; i++)
        {
            var current = sortedPlayers[i];
            var prev = i > 0 ? sortedPlayers[i - 1] : null;

            if (prev != null &&
                current.player.point == prev.player.point &&
                current.player.money == prev.player.money)
            {
                playerRanks[current.player] = GetRankString(currentRank);
            }
            else
            {
                currentRank = i + 1;
                playerRanks[current.player] = GetRankString(currentRank);
            }
        }

        foreach (var hud in playerHUDs)
        {
            var player = hud.player;

            if (playerRanks.TryGetValue(player, out string rank))
            {
                if (hud.rankText) hud.rankText.text = rank;
            }

            if (hud.moneyText) hud.moneyText.text = $"{player.money}";
            if (hud.pointText) hud.pointText.text = $"{player.point}";
        }
    }

    public Player GetTopPlayer()
    {
        return playerHUDs
            .OrderByDescending(p => p.player.point)
            .ThenByDescending(p => p.player.money)
            .FirstOrDefault()?.player;
    }


    private string GetRankString(int rank)
    {
        //Important to order hirachy of players,so players can get an equal position for excitement effect :)
        return rank switch
        {
            1 => "1st",
            2 => "2nd",
            3 => "3rd",
            4 => "4th",
            _ => rank + "th"
        };
    }



}
