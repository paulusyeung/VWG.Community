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



### Phonetic 內容
有幾隻 phonetic 嘅 library：

1. Levenshtein Distance
2. Metaphone
3. Soundex

