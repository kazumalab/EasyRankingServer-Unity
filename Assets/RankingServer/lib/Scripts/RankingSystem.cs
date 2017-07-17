using System.Collections.Specialized;
using UnityEngine;
using System.Net;
using System.IO;
using System;
using UnityEngine.Events;
using System.Collections.Generic;

namespace RankingServer {
	public class RankingSystem {

		/* callback method (args List<Ranking>) */
		public event Action<List<Ranking>> OnPost;

		/* POST URL */
		private readonly string URL = "http://localhost:3000/api/v1/rankings";
		private Uri _post_uri;

		private string _access_game_key;
		private string _access_token;

		private WebClient _wb;

		private List<string> _rankings_text = new List<string>();
		public List<Ranking> _rankings = new List<Ranking> ();

		public RankingSystem (string key, string token) {
			_access_game_key = key;
			_access_token    = token;
			Init ();
		}

		private void Init() {
			/* Web Client Initialize */
			_post_uri = new Uri( URL + "?key=" + _access_game_key + "&access_token=" + _access_token );
			_wb = new WebClient ();
			_wb.Headers.Add (HttpRequestHeader.ContentType, "application/json");
			_wb.UploadStringCompleted += Result;
		}

		/* send data to ranking server. */
		public void Send (string nickname, string score) {
			string data = ToJson(nickname, score);
			_wb.UploadStringAsync (_post_uri, "POST", data);
		}

		/* get rankings from server. */
		private void Get() {
			/* initialize */
			Uri get_uri = new Uri( URL + "?key=" + _access_game_key + "&access_token=" + _access_token );
			_wb = new WebClient ();
			_wb.Headers.Add (HttpRequestHeader.ContentType, "application/json");
			_wb.DownloadStringCompleted += GetResult;

			/* get json */
			_wb.DownloadStringAsync (get_uri);
		}

		private string ToJson(string nickname, string score) {
			return " { \"ranking\" : { \"nickname\" : \"" + nickname + "\", \"score\" : \""+ score + "\" } }";
		}

		/* Create Ranking List from Json List */
		private List<Ranking> FromJson(List<string> rankings_text) {
			List<Ranking> rankings = new List<Ranking> ();
			foreach (var rank_json in rankings_text) {
				Debug.Log (rank_json);
				var ranking = JsonUtility.FromJson<Ranking> (rank_json);
				rankings.Add (ranking);
			}
			return rankings;
		}

		/* result callback of post ranking data. */
		private void Result (object s, UploadStringCompletedEventArgs e) {
			if (e.Error == null) {
				Get ();
			} else {
				// error function
				Debug.Log("Error! : " + e.Result);
			}
		}

		/* result callback of get rankings. */
		private void GetResult (object s, DownloadStringCompletedEventArgs e) {
			if (e.Error == null) {
				string result_json = e.Result.Replace("[", "").Replace("]", "");
				string[] decs = { "}," };
				foreach (var text in result_json.Split(decs, StringSplitOptions.None)) {
					if (text.Contains ("}")) {
						_rankings_text.Add (text);
					} else {
						_rankings_text.Add (text + "}");
					}
				}

				if (OnPost != null) {
					OnPost.Invoke (FromJson (_rankings_text));
				}
			} else {
				Debug.Log("Error! : " + e.Result);
			}
		}
	}
}
