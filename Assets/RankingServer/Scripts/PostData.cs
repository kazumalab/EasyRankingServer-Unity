using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

namespace RankingServer {
	public class PostData : MonoBehaviour {

		[SerializeField]
		public string _access_game_key;
		[SerializeField]
		public string _access_token;

		private HttpWebRequest req;

		// Use this for initialization
		void Start () {
			// req = new HttpWebRequest();
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}
