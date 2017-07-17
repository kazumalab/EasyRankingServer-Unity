using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RankingServer {
	[Serializable]
	public struct Ranking {
		public string _nickname { get; }
		public string _score { get; }

		public Ranking(string nickname, string score) {
			_nickname = nickname;
			_score = score;
		}
	}
}
