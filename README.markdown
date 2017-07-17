# Ranking Server for Unity

ランキングを簡単に実装できるアセットです。
Railsサーバーを別途用意しています。

RailsのプロジェクトをCloneしてください。
それ以降のやり方については以下のReadmeに記述しております。
https://github.com/kazumalab/EasyRankingServer-Ruby-on-Rails

# 使い方

- Unityにranking_server.unitypackageをImportします。
- Rails側でGameを作成し、Access_keyとAccess_tokenを取得し、Inspectorにそれぞれ入力してください。
- 対応しているのはnicknameとscoreのみ送信できます。
