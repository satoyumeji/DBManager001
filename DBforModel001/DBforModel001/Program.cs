using System;

namespace DBforModel001
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person(1, "佐藤夢路", 26);
            Country country = new Country(1, "日本");


            DBManager.GetInstance().insert(person);
            DBManager.GetInstance().insert(country);
        }
    }
}
