### [Country Codes](https://github.com/datasets/country-codes/blob/master/data/country-codes.csv)
我想做一個效果:

隨便打幾隻字，然後個軟件可以估到你係想揾邊個 country，用嚟自動填 country 或者 country phone code。

我將個 csv 檔案 embedded 咗喺個 library 度，整咗幾款 phonetic 嘅 utilities，我覺得 Levenshtein Distance 比較理想，輸入嘅時候如果打錯字，錯得唔係太離譜嘅話，佢會估到。

隻 csv 有齊 幾種唔同嘅 standard，我會用：

1. [FIFA](https://en.wikipedia.org/wiki/List_of_FIFA_country_codes)
2. Dial（國家電話字段）
3. ISO-3166-1 Alpha 3
4. [FIPS](https://www.geodatasource.com/resources/tutorials/international-country-code-fips-versus-iso-3166/#:~:text=International%20Country%20Code%20is%20a,10%2D4%20and%20many%20more.)
5. TLD (domain name 國家編號)
6. 英文名稱

### [OpenCC](https://github.com/BYVoid/OpenCC)
OpenCC v1.1.1：中文 <=> 中文 轉換

> 中文簡繁轉換開源項目，支持詞彙級別的轉換、異體字轉換和地區習慣用詞轉換（中國大陸、臺灣、香港、日本新字體）。不提供普通話與粵語的轉換。
>
> 特點
>
>* 嚴格區分「一簡對多繁」和「一簡對多異」。
>* 完全兼容異體字，可以實現動態替換。
>* 嚴格審校一簡對多繁詞條，原則爲「能分則不合」。
>* 支持中國大陸、臺灣、香港異體字和地區習慣用詞轉換，如「裏」「裡」、「鼠標」「滑鼠」。
>* 詞庫和函數庫完全分離，可以自由修改、導入、擴展。

##### 參考

1. [OpenCC 官網](https://github.com/BYVoid/OpenCC) 下載 source codes
2. [CMake](https://cmake.org/download/) 用嚟 compile OpenCC
3. [網上文章 .1](https://blog.darkthread.net/blog/opencc-notes-1)
4. [網上文章 .2](https://blog.darkthread.net/blog/call-opencc-with-csharp/)
5. 我要 compile 為 32 bits，所以要改少少：

```bash
cmake -H. -Bbuild -G"Visual Studio 14" -DCMAKE_INSTALL_PREFIX="./install" 
cmake --build build --config Release --target install
```
因為 opencc.dll 係 unmanaged library，OpenCC.cs PInvoke 嘅時候又要改改（[參考](https://stackoverflow.com/a/32686446)）：
```csharp
[DllImport("opencc.dll", EntryPoint = "opencc_open", CallingConvention = CallingConvention.Cdecl)]
[DllImport("opencc.dll", EntryPoint = "opencc_convert_utf8", CallingConvention = CallingConvention.Cdecl)]
[DllImport("opencc.dll", CallingConvention = CallingConvention.Cdecl)]
[DllImport("opencc.dll", CallingConvention = CallingConvention.Cdecl)]
[DllImport("opencc.dll", CallingConvention = CallingConvention.Cdecl)]
```

##### Usage
```csharp
var opencc = new OpenCC();
opencc.Load();
txtResult.Text = opencc.Convert(txtSource.Text, _ConfigFileName);
```

### Phonetic 內容
有幾隻 phonetic 嘅 library：

1. Levenshtein Distance
2. Metaphone
3. Soundex

