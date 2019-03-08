using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using TestTask.Models;

namespace TestTask
{
    public class CSVQuery
    {
        String path;
        public DataTable CSVTable { get; }
        List<Brand> brands;
        public CSVQuery(String p)
        {
            path = p;
            CSVTable = new DataTable();
            brands = new List<Brand>();
            loadData();
        }

        void loadData()
        {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    List<String> headers = new List<string>();
                    if((line = sr.ReadLine()) != null)
                    {
                        headers = (line.Split(';')).ToList();
                        foreach(var el in headers)
                        {
                            CSVTable.Columns.Add(el);
                        }
                    }
                    DataColumn[] keys = new DataColumn[1];
                    CSVTable.Columns[0].AutoIncrement = true;
                    CSVTable.Columns[0].AutoIncrementStep = 1;
                    CSVTable.Columns[0].DataType = typeof(int);
                    keys[0] = CSVTable.Columns[0];
                    CSVTable.PrimaryKey = keys;
                    while ((line = sr.ReadLine()) != null)
                    {
                        DataRow row = CSVTable.NewRow();
                        var rowEl = line.Split(';');
                        for(int i=0;i<headers.Count;i++)
                        {
                            row[headers[i]] = rowEl[i];
                        }
                         CSVTable.Rows.Add(row);
                    }
                }

        }
        public List<Brand> Get()
        {
            if(brands.Count<1)
            {
                try
                {
                    foreach(DataRow el in CSVTable.Rows)
                    {
                        brands.Add(new Brand() { IdBrand =Convert.ToInt32(el["IdBrand"]), Name = el["Name"].ToString(), Description = el["Description"].ToString() });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return brands;
        }

        public Brand Get(int index)
        {
           return brands.FirstOrDefault(x => x.IdBrand == index);
        }

        public void Insert(Brand b)
        {
            DataRow r = CSVTable.NewRow();
            r["Name"] = b.Name;
            r["Description"] = b.Description;
            CSVTable.Rows.Add(r);

            insertInFile(b);
        }

        public void Update(Brand b)
        {
            var res =  CSVTable.Rows.Find(b.IdBrand);
            int index = CSVTable.Rows.IndexOf(res);
            res["Name"] = b.Name;
            res["Description"] = b.Description;
            updateFile();
        }
        public void Delete(Brand b)
        {
            var res = CSVTable.Rows.Find(b.IdBrand);
            CSVTable.Rows.Remove(res);
            updateFile();
        }
        public void Delete(int IdBrand)
        {
            var res = CSVTable.Rows.Find(IdBrand);
            CSVTable.Rows.Remove(res);
            updateFile();
        }
        void updateFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    foreach (DataRow el in CSVTable.Rows)
                    {
                        String newLine = String.Format("{0};{1};{2}", el["IdBrand"], el["Name"], el["Description"]);
                        sw.WriteLine(newLine);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void insertInFile(Brand b)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                    String newLine = String.Format("{0};{1};{2}", CSVTable.Rows[CSVTable.Rows.Count - 1]["IdBrand"], b.Name, b.Description);
                    sw.WriteLine(newLine);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}