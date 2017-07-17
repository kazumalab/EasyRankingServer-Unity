using System.Collections.Specialized;
using UnityEngine;
using System.Net;
using System.IO;
using System;
using UnityEngine.Events;

namespace RankingServer {
	[Serializable]
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
		}

		public void Send (string nickname, string score) {
			string data = " { \"ranking\" : { \"nickname\" : \"" + nickname + "\", \"score\" : \""+ score + "\" } }";
			wb.UploadStringAsync (post_uri, "POST", data);
		}

		private string ToJson(string nickname, string score) {
			return " { \"ranking\" : { \"nickname\" : \"" + nickname + "\", \"score\" : \""+ score + "\" } }";
		}

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
	}
}
