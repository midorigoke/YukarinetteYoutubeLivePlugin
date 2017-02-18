# Yukarinette YoutubeLive Plugin

ゆかりねっとからYoutubeLiveにコメントを投稿するプラグイン。

## Description

ゆかりねっとから任意のチャンネルの放送にコメントを投稿できます。

配信の遅延を考慮してコメントの投稿も任意の秒数遅延させることができます。

## Requirement

* [.NET Framework 4.6](https://www.microsoft.com/ja-jp/download/details.aspx?id=48137)
* [ゆかりねっと](http://www.okayulu.moe/)

## Installation

0. **Windows 8.1** 以前を利用している場合は[.NET Framework 4.6](https://www.microsoft.com/ja-jp/download/details.aspx?id=48137)をインストール
0. [releases](https://github.com/midorigoke/YukarinetteYoutubeLivePlugin/releases)からダウンロード
0. * `C:\Program Files (x86)\OKAYULU STYLE\ゆかりねっと\plugins`(64bit)
 * `C:\Program Files\OKAYULU STYLE\ゆかりねっと\plugins`(32bit)
   
にzipの中のdllを配置

## Usage

0. ゆかりねっとを起動する
0. プラグインページの*YotubeLive コメント投稿*の設定を開きチャンネルIDを指定する
0. *YoutubeLive コメント投稿*を有効にして音声認識を開始する
0. 初回起動時にブラウザが開きOAuth同意画面が表示されるので承認を押してください


* コメント投稿を有効にするには設定したチャンネルで放送が配信中、もしくは放送がスケジュールされている必要があります。
* 投稿するアカウントを変更したい場合はプラグインの設定画面から*トークンキャッシュ消去*をしてください。次回起動時にOAuth同意画面が表示されます。

## FAQ

### Q1

送信遅延ってどう使うの?
値はどれくらいにすればいいの?

### A1

放送が視聴者に届くまでの時間を設定することで放送と同じタイミングでコメントを表示させることができます。

環境によって違うと思うのでなんとも言えませんが、

* Windows10
* OBS Studio
* 720p 30fps
* 低遅延設定

私のこの環境でだと約7秒だったので初期値は7秒にしてあります。

時間差が気になる場合は調整してみてください。

### Q2

コメント読み上げを使うとこのプラグインで投稿したコメントも読んでしまって二回言うんだけど?

### A2

この前私が作った[これ](https://github.com/midorigoke/get_youtubelive_comments)はチャンネルオーナーのコメントは読み上げないので是非使ってみてください。(宣伝ノルマ達成)

### Q3

バグがあるんだけど

### A3

IssueかPull Requestお待ちしています。

## License

このソフトウェアは[MIT License](https://github.com/midorigoke/YukarinetteYoutubeLivePlugin/blob/master/LICENSE)のもとで公開されています。

