using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
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
            
            //Initialiaze all the lists
            List<Account> Accounts = new List<Account>();
            List<Class> Classes = new List<Class>();
            List<Contact> Contacts = new List<Contact>();
            List<Email> Emails = new List<Email>();
            List<Enrollment> Enrollments = new List<Enrollment>();
            List<Location> Locations = new List<Location>();
            List<Semester> Semesters = new List<Semester>();
            List<SemesterYear> SemesterYears = new List<SemesterYear>();
            List<SemesterName> SemesterNames = new List<SemesterName>();
            List<Student> Students = new List<Student>();
            List<Teacher> Teachers = new List<Teacher>();

            //Initialize all the helpers
            Email email;
            Contact contact;
            Student student;
            Account account;
            Enrollment enrollment;
            Location location;
            Semester semester;
            SemesterName semesterName;
            SemesterYear semesterYear;
            Class classDBElement;
            Teacher teacher;

            int semesterYearHelper;
            string semesterNameHelper;
            string semesterHelper;
            string semesterIdHelper;
            int semesterSpaceIndex;
            DateTime _dateTime;


            //Iterate through the list
            foreach (var r in CSVList)
            {
                //Add to SemesterName and SemesterYear and Semester
                //Clean the semester data
                semesterHelper = r[ColumIndex.semesterName].ToString().Replace(@"""", "");
                semesterSpaceIndex = semesterHelper.IndexOf(" ");
                if (semesterHelper.IndexOf(" ",semesterSpaceIndex + 1) != -1)
                    semesterHelper = semesterHelper.Substring(0, semesterHelper.IndexOf(" ", semesterSpaceIndex + 1)).Trim();
                Debug.WriteLine("RowValue: " + semesterHelper);
                if (Int32.TryParse(semesterHelper.Split(' ')[1], out semesterYearHelper))
                {
                    semesterNameHelper = semesterHelper.Split(' ')[0];
                    semesterIdHelper = semesterHelper.Replace(' ', '_');
                }
                else 
                {
                    semesterNameHelper = semesterHelper.Split(' ')[0];
                    semesterIdHelper = semesterHelper.Split(' ')[0];
                }
                //Add to SemesterName
                semesterName = new SemesterName();
                semesterName.SemesterNameId = semesterNameHelper;
                SemesterNames.Add(semesterName);
                //Add to SemesterYear
                semesterYear = new SemesterYear();
                semesterYear.SemesterYearId = semesterYearHelper;
                SemesterYears.Add(semesterYear);
                //Add to Semester
                semester = new Semester();
                semester.SemesterId = semesterIdHelper;
                Semesters.Add(semester);

                //Add to email List
                email = new Email();
                email.Email1 = r[ColumIndex.contact_email];
                Emails.Add(email);

                //Add to Contact List
                contact = new Contact();
                contact.FirstName = r[ColumIndex.contact_first_name];
                contact.LastName = r[ColumIndex.contact_last_name];
                Contacts.Add(contact);

                //Add to Student
                student = new Student();
                student.StudentId = Convert.ToInt32(r[ColumIndex.student_id]);
                student.FirstName = r[ColumIndex.student_first_name];
                student.LastName = r[ColumIndex.student_last_name];
                if (DateTime.TryParse(r[ColumIndex.student_date_of_birth], out _dateTime))
                    student.BirthDate = _dateTime;
                Students.Add(student);

                //Add to Accounts
                account = new Account();
                account.AccountId = Convert.ToInt32(r[ColumIndex.account_id]);
                if (r[ColumIndex.account_new_status].ToString() == "Yes")
                    account.CreatedSemesterId = r[ColumIndex.account_new_status].ToString().Replace(' ', '_');
                Accounts.Add(account);

                //Add to Enrollment
                enrollment = new Enrollment();
                enrollment.EnrollmentId = Convert.ToInt32(r[ColumIndex.enr_id]);
                if (DateTime.TryParse(r[ColumIndex.created], out _dateTime))
                    enrollment.CreateDate = _dateTime;
                enrollment.SemesterId = account.CreatedSemesterId = r[ColumIndex.account_new_status].ToString().Replace(' ', '_');
                Enrollments.Add(enrollment);

                //Add to Location
                location = new Location();
                location.Name = r[ColumIndex.class_location];
                Locations.Add(location);

                //Add to Class
                classDBElement = new Class();
                classDBElement.Time = DateTime.ParseExact(r[ColumIndex.class_time], "HH:mm:ss", CultureInfo.InvariantCulture).ToString("hh:mm:ss");
                classDBElement.DayOfWeek = r[ColumIndex.class_day_of_week];
                Classes.Add(classDBElement);

                //Add to Teachers
                teacher = new Teacher();
                teacher.Name = r[ColumIndex.class_teacher];
                Teachers.Add(teacher);

              /*  semesterYear = new SemesterYear();
                semesterYear.SemesterYearId = year;
                YearList.Add(semesterYear);
                //year = r[ColumIndex.semesterYear].ToString().Split(' ')[1]
                Debug.WriteLine("SemesterName: " + semesterName);
                Debug.WriteLine("SemesterYear: " + year.ToString());*/
            }

            //Remove the duplicates
            var YearListDistinct = SemesterYears.Distinct(new SemesterYearComparer());
            
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
