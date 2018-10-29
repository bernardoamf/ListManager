using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListManager.ListManagerSQL;

namespace ListManager
{
    class FileOperations
    {

        public static List<int> ReadYears(string csv_file_path)
        {
            var lines = File.ReadAllLines(csv_file_path).Select(x => x.Split(','));
            //Considering each line contains same no. of elements
            string[] firstRow = lines.First();
            CSVColumns ColumIndex = SetCSVColumns(firstRow);
            //int lineLength = lines.First().Count();

            var CSVList = lines.Skip(1)
                            .Select(r => r)
                            .Take(10);

            List<int> YearList = new List<int>();
            string value;
            int year;
            string semesterName;
            foreach(var r in CSVList)
            {
                value = r[ColumIndex.semesterName].ToString().Replace(@"""", "");
                Debug.WriteLine("RowValue: " + value);
                semesterName = value.Split(' ')[0];
                Int32.TryParse(value.Split(' ')[1], out year);
                YearList.Add(year);
                //year = r[ColumIndex.semesterYear].ToString().Split(' ')[1]
                Debug.WriteLine("SemesterName: " + semesterName);
                Debug.WriteLine("SemesterYear: " + year.ToString());
            }
            //Remove the duplicates
            var YearListDistinct = YearList.Distinct();
            Debug.WriteLine("Number of items in the list: " + YearListDistinct.Count().ToString());
            /*
             var CSV = lines.Skip(1)
                       .SelectMany(x => x)
                       .Select((v, i) => new { Value = v, Index = i % lineLength })
                       .Where(x => x.Index == 2 || x.Index == 3)
                       .Select(x => x.Value);
            foreach (var data in CSV)
            {
                Console.WriteLine(data);
            }*/
            return new List<int>();
        }

        public static List<int> ReadYears2(string csv_file_path)
        {
            var lines = File.ReadAllLines(csv_file_path).Select(x => x.Split(','));
            //Considering each line contains same no. of elements
            string[] firstRow = lines.First();
            CSVColumns ColumIndex = SetCSVColumns(firstRow);
            //int lineLength = lines.First().Count();

            var CSVList = lines.Skip(1)
                            .Select(r => r)
                            .Take(10);

            List<SemesterYear> YearList = new List<SemesterYear>();
            string value;
            SemesterYear semesterYear = new SemesterYear();
            int year;
            string semesterName;
            foreach (var r in CSVList)
            {
                value = r[ColumIndex.semesterName].ToString().Replace(@"""", "");
                Debug.WriteLine("RowValue: " + value);
                semesterName = value.Split(' ')[0];
                Int32.TryParse(value.Split(' ')[1], out year);
                semesterYear = new SemesterYear();
                semesterYear.SemesterYearId = year;
                YearList.Add(semesterYear);
                //year = r[ColumIndex.semesterYear].ToString().Split(' ')[1]
                Debug.WriteLine("SemesterName: " + semesterName);
                Debug.WriteLine("SemesterYear: " + year.ToString());
            }
            //Remove the duplicates
            var YearListDistinct = YearList.Distinct(new SemesterYearComparer());
            
            //Get the list of years

            using (LisManagerADO db = new LisManagerADO())
            {
                foreach (SemesterYear sm in YearListDistinct)
                {
                    db.SemesterYears.AddIfNotExists(sm, x => x.SemesterYearId == sm.SemesterYearId);
                    //db.SemesterYears.Add(sm);
                }
                db.SaveChanges();
                
                Debug.WriteLine("Number of items in the list: " + YearListDistinct.Count().ToString());
                /*
                 var CSV = lines.Skip(1)
                           .SelectMany(x => x)
                           .Select((v, i) => new { Value = v, Index = i % lineLength })
                           .Where(x => x.Index == 2 || x.Index == 3)
                           .Select(x => x.Value);
                foreach (var data in CSV)
                {
                    Console.WriteLine(data);
                }*/
            }
            return new List<int>();
        }

        private static CSVColumns SetCSVColumns(string[] firstRow)
        {
            CSVColumns ColumnIndex = new CSVColumns();
            int i = 0;
            foreach(string s in firstRow)
            {
                switch(s)
                {
                    case ("enr_id"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.enr_id = i;
                        break;
                    case ("created"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.created = i;
                        break;
                    case ("student_id"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.student_id = i;
                        break;
                    case ("student_first_name"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.student_first_name = i;
                        break;
                    case ("student_last_name"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.student_last_name = i;
                        break;
                    case ("student_date_of_birth"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.student_date_of_birth = i;
                        break;
                    case ("account_id"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.account_id = i;
                        break;
                    case ("account_new_status"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.account_new_status = i;
                        break;
                    case ("semester"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.semesterName = i;
                        ColumnIndex.semesterYear = i;
                        //semesterArray = s.Split(' ');
                        //if (Int32.TryParse(semesterArray[1], out numValue) convert.ToInt32(semesterArray[1]))
                        //ColumnIndex.semesterName = semesterArray[0];
                        break;
                    case ("class_location"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.class_location = i;
                        break;
                    case ("class_teacher"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.class_teacher = i;
                        break;
                    case ("class_day_of_week"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.class_day_of_week = i;
                        break;
                    case ("class_time"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.class_time = i;
                        break;
                    case ("contact_first_name"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.contact_first_name = i;
                        break;
                    case ("contact_last_name"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.contact_last_name = i;
                        break;
                    case ("contact_email"):
                        Debug.WriteLine("Found:" + s);
                        ColumnIndex.contact_email = i;
                        break;
                }
                i++;
            }
            return ColumnIndex;
        }
    }
}
