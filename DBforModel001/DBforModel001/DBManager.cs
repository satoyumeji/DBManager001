using System;
using System.Collections.Generic;
using System.Text;

namespace DBforModel001
{
    /// <summary>
    /// DB接続用のシングルトンクラス（インスタンスが１つだけのクラス）
    /// </summary>
    class DBManager
    {
        /// <summary>
        /// モデルのクラス名とテーブル名を対応させた連想配列
        /// キー：モデルのクラス名、バリュー：テーブル名
        /// </summary>
        Dictionary<string, string> ModelToTableNameList = new Dictionary<string, string>()
        {
            {"Person", "PersonTable" },
            {"Country","CountryTable" }
        };

        // インスタンス、シングルトンなクラスの実体
        private static DBManager instance = new DBManager();
        
        /// <summary>
        /// コンストラクタ
        /// プライベートで宣言することで他のクラスからインスタンス化されない
        /// </summary>
        private DBManager()
        {
            // 接続の処理を書く
        }

        /// <summary>
        /// 他クラスからインスタンスにアクセスする時用のゲッター
        /// </summary>
        /// <returns>DBManagerのインスタンス</returns>
        public static DBManager GetInstance()
        {
            return DBManager.instance;
        }

        /// <summary>
        /// モデルの型からインサートするテーブルを指定、インサートを実行
        /// </summary>
        /// <param name="model">モデル</param>
        /// <returns>インサート成功、失敗の結果</returns>
        public bool insert(BaseModel model)
        {
            try
            {
                /*
                 * DB接続を記述
                */

                // モデルの型と対応するテーブルが登録されているかを確認
                if (!ExistTable(model))
                {
                    Console.WriteLine
                        (
                        "modelに対応するテーブルが存在しません。DBManagerのModelToTableNameListに記入して下さい。"
                        );
                    Console.WriteLine(model.GetType().Name );
                    return false;
                }

                // sqlの作成
                var sql_table = new StringBuilder("INSERT INTO " + ModelToTableNameList[model.GetType().Name]); // テーブル
                var sql_colum = new StringBuilder("(");         // 列名の部分のsql文
                var sql_value = new StringBuilder("VALUES(");   // 値の部分のsql文

                // モデルのインサート用のレコードを取得
                var record = model.GetInsertRecord();

                // レコードから列名と値を設定
                foreach (string colum in record.Keys)
                {
                    sql_colum.Append(colum + ",");
                    sql_value.Append(record[colum] + ",");
                }

                // それぞれの文の末尾のカンマを削除
                sql_colum.Remove(sql_colum.Length - 1, 1);
                sql_value.Remove(sql_value.Length - 1, 1);

                // それぞれのsql文を閉じる
                sql_colum.Append(")");
                sql_value.Append(");"); // もしかしたら";"がいらないかも

                // sql文の完成
                string sql = sql_table.ToString() + sql_colum.ToString() + sql_value.ToString();
                Console.WriteLine(sql.ToString());

                /*
                 * sql発行の処理を記述
                */

                // insert成功
                return true;
            }
            catch(Exception ex)
            {
                // 接続エラーの表示
                Console.WriteLine( ex.Message);
            }
            finally
            {
                // エラーの後処理、DBの接続を閉じる
            }

            // insert失敗、ここまで処理は来ないかも
            return false;
        }

        /// <summary>
        /// ModelToTableNameListにmodelの型が登録されているかを確認
        /// いらないかもしれない
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>true：登録済み、false：未登録</returns>
        private bool ExistTable(BaseModel model)
        {
            return ModelToTableNameList.ContainsKey(model.GetType().Name);
        } 


    }
}
