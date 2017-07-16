using System.Collections.Specialized;
using UnityEngine;
using System.Net;
using System.IO;
using System;
using UnityEngine.Events;

namespace RankingServer {
	public class PostData : MonoBehaviour {

		[SerializeField]
		private string _access_game_key;
		[SerializeField]
		private string _access_token;

		private WebRequest request;

		/* POST URL */
		private string URL = "http://localhost:3000/rankings";
		[SerializeField]
		private UnityEvent OnPost;

		// Use this for initialization
		void Start () {
			Uri post_url = new Uri( URL + "?key=" + _access_game_key + "&access_token=" + _access_token );

			using (var wb = new WebClient ()) {
				string json = ToJson ("kazuma", "100");

				wb.Headers.Add (HttpRequestHeader.ContentType, "application/json");
				wb.UploadStringCompleted += (s, e) => {
					if (e.Error == null) {
						var result = e.Result;
						if(OnPost != null) {
							OnPost.Invoke();
						}
					} else {
						// error function
					}
				};

				wb.UploadStringAsync (post_url, "POST", json);
			}
		}

		private string ToJson(string nickname, string score) {
			return " { \"ranking\" : { \"nickname\" : \"" + nickname + "\", \"score\" : \""+ score + "\" } }";
		}

		public void SetCallback (UnityAction e) {
			OnPost.AddListener (e);
		}
	}
}
