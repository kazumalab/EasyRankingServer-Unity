using System.Collections.Specialized;
using UnityEngine;
using System.Net;
using System.IO;
using System;
using UnityEngine.Events;
using RankingServer;

public class PostDataSample : MonoBehaviour {
	private RankingSystem ranking_system;
	[SerializeField]
	private string access_game_key;
	[SerializeField]
	private string access_token;

	void Start () {
		ranking_system = new RankingSystem (access_game_key, access_token);
		ranking_system.OnPost += () => print("完了");
		ranking_system.Send ("kazumalab", "300");
	}
}
