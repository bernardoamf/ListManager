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
            List<ClassHelper> Classes = new List<ClassHelper>();
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
            ClassHelper classHelper;
            Class classDBHelper;
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
                //Debug.WriteLine("RowValue: " + semesterHelper);
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
                classHelper = new ClassHelper();
                classHelper.Time = r[ColumIndex.class_time].ToString();
                classHelper.DayOfWeek = r[ColumIndex.class_day_of_week];
                classHelper.LocationName = r[ColumIndex.class_location];
                classHelper.TeacherName = r[ColumIndex.class_teacher].Replace(@"""", "");
                classHelper.SemesterId = semesterIdHelper;
                Classes.Add(classHelper);

                //Add to Teachers
                teacher = new Teacher();
                teacher.Name = r[ColumIndex.class_teacher].Replace(@"""", ""); ;
                Teachers.Add(teacher);

              /*  semesterYear = new SemesterYear();
                semesterYear.SemesterYearId = year;
                YearList.Add(semesterYear);
                //year = r[ColumIndex.semesterYear].ToString().Split(' ')[1]
                Debug.WriteLine("SemesterName: " + semesterName);
                Debug.WriteLine("SemesterYear: " + year.ToString());*/
            }
            //Remove the duplicates
            var SemesterYearsDistinct = SemesterYears.Distinct(new SemesterYearComparer());
            var SemesterNamesDistinct = SemesterNames.Distinct(new SemesterNameComparer());
            var SemestersDistinct = Semesters.Distinct(new SemesterComparer());
            var EmailsDistinct = Emails.Distinct(new EmailComparer());
            var ContactsDistinct = Contacts.Distinct(new ContactComparer());
            var StudentsDistinct = Students.Distinct(new StudentComparer());
            var AccountsDistinct = Accounts.Distinct(new AccountComparer());
            var EnrollmentsDistinct = Enrollments.Distinct(new EnrollmentComparer());
            var LocationsDistinct = Locations.Distinct(new LocationComparer());
            var ClassesDistinct = Classes.Distinct(new ClassHelperComparer());
            var TeachersDistinct = Teachers.Distinct(new TeacherComparer());

            //Get the list of years

            using (LisManagerADO db = new LisManagerADO())
            {
                //Process SemesterYear
                Debug.WriteLine("Number of items in the list: " + SemesterYearsDistinct.Count().ToString());
                foreach (SemesterYear e in SemesterYearsDistinct)
                {
                    Debug.WriteLine("itemValue: " + e.SemesterYearId);
                    db.SemesterYears.AddIfNotExists(e, x => x.SemesterYearId == e.SemesterYearId);
                    //db.SemesterYears.Add(e);
                }
               db.SaveChanges();

                //Process SemesterName
                Debug.WriteLine("Number of items in the list: " + SemesterNamesDistinct.Count().ToString());
                foreach (SemesterName e in SemesterNamesDistinct)
                {
                    Debug.WriteLine("itemValue: " + e.SemesterNameId);
                    db.SemesterNames.AddIfNotExists(e, x => x.SemesterNameId == e.SemesterNameId);
                    //db.SemesterNames.Add(e);
                }
                db.SaveChanges();

                //Process Semesters
                Debug.WriteLine("Number of items in the list: " + SemestersDistinct.Count().ToString());
                foreach (Semester e in SemestersDistinct)
                {
                    Debug.WriteLine("itemValue: " + e.SemesterId);
                    db.Semesters.AddIfNotExists(e, x => x.SemesterId == e.SemesterId);
                    //db.Semesters.Add(e);
                }
                db.SaveChanges();

                //Process Locations
                Debug.WriteLine("Number of items in the list: " + LocationsDistinct.Count().ToString());
                foreach (Location e in LocationsDistinct)
                {
                    Debug.WriteLine(String.Format("Classes itemValue: {0} {1}", e.LocationId, e.Name));
                    db.Locations.AddIfNotExists(e, x => x.Name == e.Name);
                   // db.Locations.Add(e);
                }
                db.SaveChanges();

                //Process Teachers
                Debug.WriteLine("Number of items in the list: " + TeachersDistinct.Count().ToString());
                foreach (Teacher e in TeachersDistinct)
                {
                    Debug.WriteLine(String.Format("Teacher itemValue: {0} {1}", e.TeacherId, e.Name));
                    db.Teachers.AddIfNotExists(e, x => x.Name == e.Name);
                   // db.Teachers.Add(e);
                }
                db.SaveChanges();

                //Process Classes
                Debug.WriteLine("Number of items in the list: " + ClassesDistinct.Count().ToString());
                foreach (ClassHelper e in ClassesDistinct)
                {
                    classDBHelper = new Class();
                    classDBHelper.DayOfWeek = e.DayOfWeek;
                    classDBHelper.Time = e.Time;
                    classDBHelper.SemesterId = e.SemesterId;
                    classDBHelper.LocationId = Convert.ToInt32((from l in LocationsDistinct where l.Name == e.LocationName select l.LocationId).First());
                    classDBHelper.TeacherId = Convert.ToInt32((from t in TeachersDistinct where t.Name == e.TeacherName select t.TeacherId).First());
                    Debug.WriteLine(String.Format("Classes itemValue: {0} {1} {2} {3} {4} {5}", classDBHelper.ClassId, classDBHelper.LocationId, classDBHelper.SemesterId
                                                                                                    , classDBHelper.Time, classDBHelper.DayOfWeek, classDBHelper.TeacherId));
                    db.Classes.AddIfNotExists(classDBHelper, x => classDBHelper.ClassId == classDBHelper.ClassId);
                    //db.SemesterYears.Add(sm);
                }
                db.SaveChanges();

                //Process Email
                Debug.WriteLine("Number of items in the list: " + EmailsDistinct.Count().ToString());
                foreach (Email e in EmailsDistinct)
                {
                    Debug.WriteLine(String.Format("Teacher itemValue: {0} {1}", e.EmailId, e.Email1));
                    //db.SemesterYears.AddIfNotExists(sm, x => x.SemesterYearId == sm.SemesterYearId);
                    //db.SemesterYears.Add(sm);
                }
                //db.SaveChanges();

                //Process Contact
                Debug.WriteLine("Number of items in the list: " + ContactsDistinct.Count().ToString());
                foreach (Contact e in ContactsDistinct)
                {
                    Debug.WriteLine(String.Format("Contact itemValue: {0} {1} {2} {3}", e.ContactId, e.EmailId, e.FirstName, e.LastName));
                    //db.SemesterYears.AddIfNotExists(sm, x => x.SemesterYearId == sm.SemesterYearId);
                    //db.SemesterYears.Add(sm);
                }
                //db.SaveChanges();

                //Process Account
                Debug.WriteLine("Number of items in the list: " + AccountsDistinct.Count().ToString());
                foreach (Account e in AccountsDistinct)
                {
                    Debug.WriteLine(String.Format("Account itemValue: {0} {1} {2}", e.AccountId, e.ContactId, e.CreatedSemesterId));
                    //db.SemesterYears.AddIfNotExists(sm, x => x.SemesterYearId == sm.SemesterYearId);
                    //db.SemesterYears.Add(sm);
                }
                //db.SaveChanges();

                //Process Student
                Debug.WriteLine("Number of items in the list: " + StudentsDistinct.Count().ToString());
                foreach (Student e in StudentsDistinct)
                {
                    Debug.WriteLine(String.Format("Student itemValue: {0} {1} {2} {3} {4}", e.StudentId, e.FirstName, e.LastName, e.ContactId, e.BirthDate));
                    //db.SemesterYears.AddIfNotExists(sm, x => x.SemesterYearId == sm.SemesterYearId);
                    //db.SemesterYears.Add(sm);
                }
                //db.SaveChanges();

                //Process Enrollment
                Debug.WriteLine("Number of items in the list: " + EnrollmentsDistinct.Count().ToString());
                foreach (Enrollment e in EnrollmentsDistinct)
                {
                    Debug.WriteLine(String.Format("Enrollment itemValue: {0} {1} {2} {3} {4}", e.EnrollmentId, e.StudentId, e.SemesterId, e.AccountId, e.CreateDate));
                    //db.SemesterYears.AddIfNotExists(sm, x => x.SemesterYearId == sm.SemesterYearId);
                    //db.SemesterYears.Add(sm);
                }
                //db.SaveChanges();

                Debug.WriteLine("Number of items in the list: " + SemesterYearsDistinct.Count().ToString());
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
