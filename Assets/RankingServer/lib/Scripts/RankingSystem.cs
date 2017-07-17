using System.Collections.Specialized;
using UnityEngine;
using System.Net;
using System.IO;
using System;
using UnityEngine.Events;
using System.Collections.Generic;

namespace RankingServer {
	public class RankingSystem {

		/* POST URL */
		private readonly string URL = "http://localhost:3000/rankings";
		private Uri post_uri;

		private string _access_game_key;
		private string _access_token;

		public event Action OnPost;
		private WebClient wb;

		public RankingSystem (string key, string token) {
			_access_game_key = key;
			_access_token    = token;
			Init ();
		}

		private void Init() {
			/* Web Client Initialize */
			post_uri = new Uri( URL + "?key=" + _access_game_key + "&access_token=" + _access_token );
			wb = new WebClient ();
			wb.Headers.Add (HttpRequestHeader.ContentType, "application/json");
			wb.UploadStringCompleted += Result;

			OnPost += Get;
		}

		/* send data to ranking server. */
		public void Send (string nickname, string score) {
			string data = ToJson(nickname, score);
			wb.UploadStringAsync (post_uri, "POST", data);
		}

		/* get rankings from server. */
		private void Get() {
			/* initialize */
			Uri get_uri = new Uri( URL + "?key=" + _access_game_key + "&access_token=" + _access_token );
			wb = new WebClient ();
			wb.Headers.Add (HttpRequestHeader.ContentType, "application/json");
			wb.DownloadStringCompleted += GetResult;

			/* get json */
			wb.DownloadStringAsync (get_uri);
		}

		private string ToJson(string nickname, string score) {
			return " { \"ranking\" : { \"nickname\" : \"" + nickname + "\", \"score\" : \""+ score + "\" } }";
		}

//		private List<Ranking> FromJson(string json) {
//
//		}

		/* result callback of post ranking data. */
		private void Result (object s, UploadStringCompletedEventArgs e) {
			if (e.Error == null) {
				var result = e.Result;
				if (OnPost != null) {
					OnPost.Invoke ();
				}
			} else {
				// error function
				Debug.Log("Error! : " + e.Result);
			}
		}

		/* result callback of get rankings. */
		private void GetResult (object s, DownloadStringCompletedEventArgs e) {
			if (e.Error == null) {
				string result_json = e.Result;
			} else {
				Debug.Log("Error! : " + e.Result);
			}
		}
	}
}
