using System;
using System.Collections.Generic;
using System.Text;

namespace DBforModel001
{
    /// <summary>
    /// 人の情報のデータモデル
    /// テーブルのカラムとメンバ変数名が対応
    /// </summary>
    class Person : BaseModel
    {
        int id { get; set; }
        string name { get; set; }
        int age { get; set; }

        public Person(int id, string name, int age)
        {
            this.id = id;
            this.name = name;
            this.age = age;
        }


        /// <summary>
        /// レコードのインサート用の連想配列を返す
        /// 文字にシングルクォートを付けている　※ダサいので何か良い方法があれば求む
        /// 変数名をキー、値をバリュー
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetInsertRecord()
        {
            // 戻り値の連想配列、キー：カラム名　バリュー：値
            Dictionary<string, string> record = new Dictionary<string, string>();

            // メンバ変数名をキー、その値をバリュー
            record.Add(nameof(this.id),this.id.ToString());
            record.Add(nameof(this.name), "'" + name + "'");
            record.Add(nameof(age),age.ToString());

            return record;

        }
    }
}
