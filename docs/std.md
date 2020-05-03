# 標準ライブラリ

## グローバル

グローバル空間にある関数群です。

### version
この Ebister 言語のバージョンを返します。

### print(value)
`value` を出力します。

### printLine(value?)
`value` を出力して改行します。valueが未指定の場合は改行のみします。

### inputLine(prompt?)
`prompt` を出力し、1行の入力を受け付け、値を返します。`prompt` が未指定の場合は何も出力しません。

### inputNumber(prompt?)
`prompt` を出力し、1行の入力を受け付け、それを数値に変換して返します。`prompt` が未指定の場合は何も出力しません。

### isDefined(name)
`name` という名前の変数、定数、関数が定義されていれば `true` 、いなければ `false` を返します。

### isDefined(objectOrGroup, name)

`objectOrGroup` にオブジェクト、またはグループを指定し、 `name` という名前の変数、定数、関数が定義されていれば `true` 、いなければ `false` を返します。

### typeof(value)
`value` の型を文字列形式で返します。取りうる値は以下のとおりです
- `"string"`
- `"number"`
- `"boolean"`
- `"array"`
- `"function"`
- `"group"`
- `"object"`
- `"null"`

### toNumber(value)
`value` を数値に変換します。変換できる型は以下の通りです

- 文字列型
    - Ebister の数値フォーマットとして正しく解釈できる文字列でない場合、NaNを返します
- boolean 型
    - true → 1, false → 0
- null → 0
- その他 → NaN

### toString(value)
`value` を文字列に変換します。

### isNullOrEmpty(value)
valueがnull, 空文字列, 0, 要素数0の配列, false のいずれかであれば `true`、でなければ `false` を返します。

### len(value)
value が文字列の場合は文字列の長さ、配列の場合は配列の長さ、それ以外の場合は0を返します。

## Math グループ
算術演算の関数群を提供するグループです。

### floor(num)
num の小数部分を切り捨てて返します。

### round(num)
num の小数部分を四捨五入して返します。

### ceil(num)
num の小数部分を切り上げて返します。

### abs(num)
num の絶対値を返します。

### sign(num)
num が負数であれば-1, 正数であれば1、0であれば0を返します。

### min(value...)
与えられた値のうち小さい方を返します。

### max(value...)
与えられた値のうち最も大きいものを返します。

### sin(rad)
サインを求めます。単位はラジアンです。

### cos(rad)
コサインを求めます。単位はラジアンです。

### tan(rad)
タンジェントを求めます。単位はラジアンです。

### asin(num)
アークサインを求めます。

### acos(num)
アークコサインを求めます。

### atan(num)
アークタンジェントを求めます。

### atan(x, y)
x, y座標からアークタンジェントを求めます。

### sinh(num)
ハイパボリックサインを求めます。

### cosh(num)
ハイパボリックコサインを求めます。

### tanh(num)
ハイパボリックタンジェントを求めます。

### randomize(seed)
乱数を初期化します。seedを指定した場合その値で初期化しますが、指定しない場合現在日時で初期化します。

### rnd(max)
`max` を最大値とする乱数を返します。

### sqrt(n)
√nを求めます。

### pow(n, r)
nのr乗を求めます。

### pi
円周率です。

### e
自然対数です。


## String
文字列操作を提供するグループです。

### from(value)
`toString(value)` と等価です。

### indexOf(haystack, needle)
文字列 haystack の中に文字列 needle が含まれるかどうか調べ、見つかった場合は最前にある needle の位置を返します。含まれていない場合は-1を返します。

### lastIndexOf(haystack, needle)
文字列 haystack の中に文字列 needle が含まれるかどうか調べ、見つかった場合は最後にある needle の位置を返します。含まれていない場合は-1を返します。

### concat(array)
array の中身を全て `toString()` した上で結合して返します。

### join(array, separator)
array の中身を全て `toString()` し、 separator で繋げて返します。

### split(text, separator?)
text を separator で分割し、文字列の配列として返します。separatorを指定しない場合は1文字ずつ分割します。

### replace(text, from, to)
text にふくまれる from を全て to に置換して返します。

### trim(text)
text の左右にある空白文字を削除して返します。

### trimLeft(text)
text の左にある空白文字を削除します。

### trimRight(text)
text の右にある空白文字を削除します。

### contains(haystack, needle)
haystack に needle が含まれるなら true, でなければ false を返します。

### startsWith(text1, text2)
text1 が text2 から始まるなら true, 違えば false を返します。

### endsWith(text1, text2)
text1 が text2 で終わるなら true, 違えば false を返します。

### subString(text, startIndex, length?)
text の startIndex 番目から始まり、lengthが指定されていれば length 分、そうでなければ最後まで文字列を切り出して返します。

### format(text, params...)
書式文字列 text を用いて params を整形した文字列を返します。
書式は C# に準じます。

### toArray(text)
文字列を1文字ずつの配列に変換します。 `split(text)` はこの関数を呼びます

### Array グループ
配列操作関数を提供するグループです。

### concat(arr1...)
複数の配列を結合し、一つの配列にします。

### contains(haystack, needle)
配列 haystack に needle という値が存在すれば true, 存在しなければ false を返します。

### indexOf(haystack, needle)
配列 haystack に needle という値が存在する場合、その最前のインデックスを返します。なければ-1を返します。

### lastIndexOf(haystack, needle)
配列 haystack に needle という値が存在する場合、その最後のインデックスを返します。なければ-1を返します。

### push(arr, value)
配列 arr の末尾に value を挿入します。

### pop(arr)
配列 arr の末尾要素を arr から削除し、それを返します。

### unshift(arr, value)
配列 arr の先頭に value を挿入します。

### shift(arr)
配列 arr の先頭要素を arr から削除し、それを返します。

### map(arr, callback)
配列 arr の各要素を callback を用いてマッピングします。callbackは値、あるいは値およびインデックスを引数に取る関数で、この関数の返り値が入った配列を返り値とします。

#### 例
```js
// [ "0: Apple", "1: Banana", "2: Chocolate" ]
Array.map([ "Apple", "Banana", "Chocolate" ], (v, i) => i + ": " + v)
```

### filter(arr, callback)
配列 arr の各要素を callback を用いてフィルタリングします。
callbackは値を引数に取り、真偽値を返す関数です。この関数の返り値を条件として要素をフィルタリングします。

#### 例
```js
// [ "Banana", "Chocolate" ]
Array.filter([ "Apple", "Banana", "Chocolate" ], (v) => String.contains(v, "a"))
```

### all(arr, callback)

配列 arr の各要素について順番に callback を呼び出し、callback が全て true を返せば true, そうでなければ false を返します。
callbackは値を引数に取り、真偽値を返す関数です。

### any(arr, callback)

配列 arr の各要素について順番に callback を呼び出し、callback が一つでも true を返せば true, 一つも返さなければ false を返します。
callbackは値を引数に取り、真偽値を返す関数です。

### distinct(arr)
配列 arr の重複する要素を取り除いた物を返します。

## JSON グループ

JSON グループは JSON との相互運用をサポートします。

### stringify(obj, pretty?)

オブジェクトをJSON文字列に変換し、文字列形式で返します。pretty = true の場合、綺麗に整形します。

### parse(str)

JSON 文字列をオブジェクトにパースし返します。正しいJSON文字列でなければエラーとなります。

[トップへ戻る](index.md)
