using System;
using System.Collections.Generic;
using System.Text;

namespace DBforModel001
{
    /// <summary>
    /// DBデータ抽象クラス
    /// </summary>
    abstract class BaseModel
    {
        /// <summary>
        /// カラムを連想配列で返す抽象メソッド
        /// キー：カラム名、バリュー：値
        /// </summary>
        /// <returns>テーブルのレコードの連想配列</returns>
        abstract public Dictionary<string, string> GetInsertRecord();
    }
}
