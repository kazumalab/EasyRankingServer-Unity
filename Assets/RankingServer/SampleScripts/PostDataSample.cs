using System.Collections.Specialized;
using UnityEngine;
using System.Collections.Generic;
using RankingServer;

public class PostDataSample : MonoBehaviour {
	private RankingSystem _ranking_system;
	[SerializeField]
	private string _access_game_key;
	[SerializeField]
	private string _access_token;

	[SerializeField]
	private List<Ranking> _rankings;

	void Start () {
		_ranking_system = new RankingSystem (_access_game_key, _access_token);
		_ranking_system.OnPost += SetRankings;
		_ranking_system.Send ("kazumalab", "300");
	}

	public void SetRankings(List<Ranking> ranks) {
		_rankings = ranks;
	}
}
