using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DataSet1
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
                ColumnName = "FirstName",
                DataType = typeof(string),
                Caption = "Имя"
            };
            dataTable.Columns.Add(dataColumn);

            dataColumn = new DataColumn
            {
                ColumnName = "LastName",
                DataType = typeof(string),
                Caption = "Фамилия"
            };
            dataTable.Columns.Add(dataColumn);

            dataColumn = new DataColumn
            {
                ColumnName = "BirthDate",
                DataType = typeof(DateTime),
                Caption = "Днюха"
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
            dataRow["BirthDate"] = new DateTime(1996, 8, 22);
            dataTable.Rows.Add(dataRow);
            
            dataSet.Tables.Add(dataTable);


            dataTable.Rows[1]["LastName"] = "McCalister";


            //dataTable.Rows.RemoveAt(0);

            //DataRow row = dataTable.Rows.Cast<DataRow>().FirstOrDefault(r => r["LastName"].ToString() == "Doe");
            //if (row!=null)
            //{
            //    dataTable.Rows.Remove(row);
            //}

            //var result = from r in dataTable.AsEnumerable()
            //             where r.Field<DateTime>("BirthDate").Month > 6
            //             select new
            //             {
            //                 lName = r.Field<string>("LastName"),
            //                 fName = r.Field<string>("FirstName")
            //             };

            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);
            //}

            //var number = new
            //{
            //    Id = 23,
            //    Date = DateTime.Now
            //};

            /*dataSet.WriteXml("DataSetXml.xml");

            dataSet.Clear();

            dataSet.ReadXml("DataSetXml.xml");

            PrintDS(dataSet);*/

            dataSet.RemotingFormat = SerializationFormat.Binary;
            FileStream fileStream = new FileStream("DataSetBin.bin", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, dataSet);
            fileStream.Close();

            dataSet.Clear();

            using (fileStream = new FileStream("DataSetBin.bin", FileMode.Open))
            {
                dataSet = formatter.Deserialize(fileStream) as DataSet;
            }

            PrintDS(dataSet);

            //foreach (DataRow row in result)
            //{
            //    foreach (var item in row.ItemArray)
            //    {
            //        Console.Write($"{item}\t");
            //    }
            //    Console.WriteLine();
            //}

            //PrintTable(dataTable);
        }

        private static void PrintTable(DataTable dataTable)
        {
            DataTableReader tableReader = dataTable.CreateDataReader();

            while (tableReader.Read())
            {
                for (int i = 0; i < tableReader.FieldCount; i++)
                {
                    Console.Write($"{tableReader.GetValue(i),10}\t\t");
                }
                Console.WriteLine();
            }
            tableReader.Close();
        }

        private static void PrintDS(DataSet dataSet)
        {
            Console.WriteLine($"Name: {dataSet.DataSetName}");

            foreach (DictionaryEntry item in dataSet.ExtendedProperties)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }

            foreach (DataTable table in dataSet.Tables)
            {
                Console.WriteLine($"\t\t\t{table.TableName}");

                foreach (DataColumn column in table.Columns)
                {
                    Console.Write($"{column.Caption}\t\t");
                }
                Console.WriteLine("\n------------------------\n");
                
                foreach (DataRow row in table.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        Console.Write($"{item}\t\t");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
    }
}