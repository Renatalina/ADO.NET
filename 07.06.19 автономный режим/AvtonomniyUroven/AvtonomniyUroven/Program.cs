using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtonomniyUroven
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet dataSet = new DataSet("Ex1");
            dataSet.ExtendedProperties["Date"] = DateTime.Now;
            dataSet.ExtendedProperties["Company"] = "ITStep";

            DataTable dataTable = new DataTable("People");
            DataColumn dataColumn = new DataColumn
            {
                //это описание колонки в таблице 
                ColumnName = "Id",
                DataType = typeof(int),
                AllowDBNull = false,
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1
            };
            dataTable.Columns.Add(dataColumn);
            dataTable.PrimaryKey = new DataColumn[] { dataColumn };

            dataColumn = new DataColumn
            {
                //это описание колонки в таблице 
                ColumnName = "FirstName",
                DataType = typeof(string),
                Caption="Имя"
            };
            dataTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn
            {
                //это описание колонки в таблице 
                ColumnName = "LastName",
                DataType = typeof(string),
                Caption = "Фамилия"
            };
            dataTable.Columns.Add(dataColumn);
            dataColumn = new DataColumn
            {
                //это описание колонки в таблице 
                ColumnName = "BirthDate",
                DataType = typeof(DateTime),
                Caption = "ДР"
            };
            dataTable.Columns.Add(dataColumn);

            DataRow dataRow = dataTable.NewRow();
            dataRow[1] = "John";
            dataRow[2] = "Doe";
            dataRow[3] = new DateTime(1997, 3, 14);
            dataTable.Rows.Add(dataRow);

            dataRow = dataTable.NewRow();
            dataRow["FirstName"] = "Jeam";
            dataRow["LastName"] = "Beam";
            dataRow["BirthDate"] = new DateTime(1999, 4, 14);
            dataTable.Rows.Add(dataRow);

            dataSet.Tables.Add(dataTable);

            //PrintDS(dataSet);

            dataTable.Rows[1]["LastName"] = "McCalister";

            //PrintTable(dataTable);


            //эта фуркция удалит строку из таблици
            //dataTable.Rows.RemoveAt(0);
            DataRow row = dataTable.Rows.Cast<DataRow>().FirstOrDefault
                  (r => r["LastName"].ToString() == "Doe");
            if(row !=null)
            {
               // dataTable.Rows.Remove(row);
            }

            ///это запрос для базы данных на языке линкью
            /*var result = from r in dataTable.AsEnumerable()
                         where ((DateTime)r["BirthDate"]).Month > 6
                         select r;
                     */


            ///это запрос ЛИНКЬЮ
            /*
            var result = from r in dataTable.AsEnumerable()
                         where r.Field<DateTime>("BirthDate").Month > 3
                         select new
                         {
                             lName = r.Field<string>("LastName"),
                             fName=r.Field<string>("FirstName")
                         };
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            //вот это вот называется АНОНИМНЫЙ ТИП !!!!!!!!!!!!!
            var number = new {
                Id = 23,
                Date = DateTime.Now
            };
            */

            dataSet.WriteXml("DataSetXml.xml");
            dataSet.Clear();
            dataSet.ReadXml("DataSetXML.xml");
            PrintDS(dataSet);

            /*
            foreach (DataRow row1 in result)
            {
                foreach(var item in row1.ItemArray)
                {
                     Console.Write($"{item}\t");
                }
                Console.WriteLine();
            }*/
               // PrintTable(dataTable);

        }

        private static void PrintTable(DataTable dataTable)
        {
            DataTableReader tableReader = dataTable.CreateDataReader();
            while (tableReader.Read())
            {
                for (int i=0; i<tableReader.FieldCount; i++)
                {
                    Console.Write($"{tableReader.GetValue(i)}\t\t");
                }
                Console.WriteLine();
            }
            tableReader.Close();
        }

        private static void PrintDS(DataSet dataSet)
        {
            ///этот метод выведет на экран всю таблицу
            Console.WriteLine($"Name: {dataSet.DataSetName}");
            foreach (DictionaryEntry item in dataSet.ExtendedProperties)
            {
                Console.WriteLine($"{item.Key}:{item.Value}");
            }
            foreach (DataTable table in dataSet.Tables)
            {
                Console.WriteLine($"\t\t{table.TableName}");
                foreach(DataColumn column in table.Columns)
                {
                    Console.Write($"{column.Caption}\t\t");
                }
                Console.WriteLine("\n----------------------------\n");
                foreach (DataRow row in table.Rows)
                {
                    foreach(var item in row.ItemArray)
                    {
                        Console.Write($"{item}\t\t");
                    }
                    Console.WriteLine($"{row}");
                }
                Console.WriteLine("\n----------------------------\n");
            }
            Console.WriteLine();
        }
    }
}
