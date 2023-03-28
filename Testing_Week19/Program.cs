using System.Runtime.Intrinsics.X86;

namespace Test_Plan_Source_Code

{

    class User

    {

        private string m_fName;

        private string m_lName;

        private int m_age;

        private string m_userName;

        private string m_password;

        public string FirstName { get { return m_fName; } set { m_fName = value; } }

        public string LastName { get { return m_lName; } set { m_lName = value; } }

        public int Age { get { return m_age; } set { m_age = value; } }

        public string Username { get { return m_userName; } set { m_userName = value; } }

        public string Password { get { return m_password; } set { m_password = value; } }

        public User(string fName, string lName, int age, string userName, string password)

        {

            m_fName = fName;

            m_lName = lName;

            m_age = age;

            m_userName = userName;

            m_password = password;

        }

        public string GetFullName()

        {

            return m_fName + " " + m_lName;

        }

    }

    internal class Program

    {

        static void RunMenu(ref List<string> menuList, ref int menuChoice)

        {

            bool isValid = true;

            do

            {

                Console.WriteLine(menuList[0] + "\n");

                for (int i = 1; i < menuList.Count; i++)

                {

                    Console.WriteLine(i + ": " + menuList[i]);

                }

                try

                {

                    Console.WriteLine("\nPlease make a selection from the list:");


                    menuChoice = Convert.ToInt16(Console.ReadLine());

                    if (menuChoice < 0 || menuChoice > menuList.Count - 0)

                    {

                        throw new IndexOutOfRangeException();

                    }

                    isValid = true;

                }

                catch (IndexOutOfRangeException)

                {

                    Console.WriteLine("Sorry, you can only make selections from the list provided...\n[Press Enter to Try Again]");

                    Console.ReadLine();

                    isValid = false;

                    Console.Clear();

                }

                catch (Exception)

                {

                    Console.WriteLine("Sorry, an unknown error has occurred...\n[Press Enter to Try Again]");

                    Console.ReadLine();

                    isValid = false;

                    Console.Clear();

                }

            } while (isValid == false);

        }

        static void LoadUsers(ref List<User> userList)

        {

            try

            {

                FileStream userFile = new FileStream("../My_Users.txt", FileMode.Open);

                StreamReader myReader = new StreamReader(userFile);

                int dataUserCount = Convert.ToInt16(myReader.ReadLine());

                if (dataUserCount == 0)

                {

                    throw new FileNotFoundException();

                }

                for (int i = 0; i < dataUserCount; i++)

                {

                    string tmpFName = myReader.ReadLine();

                    string tmpLName = myReader.ReadLine();

                    int tempAge = Convert.ToInt16(myReader.ReadLine());

                    string tmpUserName = myReader.ReadLine();

                    string tmpPassword = myReader.ReadLine();

                    userList.Add(new User(tmpFName, tmpLName, tempAge, tmpUserName, tmpPassword));

                }

                myReader.Close();

                userFile.Close();

            }

            catch (FileNotFoundException)

            {

                Console.WriteLine("There are currently no known users in this system...");

            }

        }

        static void SaveUsers(ref List<User> userList)

        {

            try

            {

                FileStream checkFile = new FileStream("../My_Users.txt", FileMode.OpenOrCreate);

                checkFile.Close();

            }

            catch (FileNotFoundException)

            {

                FileStream createFile = new FileStream("../My_Users.txt", FileMode.CreateNew);

                createFile.Close();

            }

            catch (IOException)

            {

                Console.WriteLine("An Error has been detected...\n[Press Enter to Return to Menu]");

                Console.ReadLine();

                Console.Clear();

            }

            try

            {

                FileStream userFile = new FileStream("../My_Users.txt", FileMode.Open);

                StreamWriter userWriter = new StreamWriter(userFile);

                userWriter.WriteLine(userList.Count);

                for (int i = 0; i < userList.Count; i++)

                {

                    userWriter.WriteLine(userList[i].FirstName);

                    userWriter.WriteLine(userList[i].LastName);

                    userWriter.WriteLine(userList[i].Age);

                    userWriter.WriteLine(userList[i].Username);

                    userWriter.WriteLine(userList[i].Password);

                }

                userWriter.Close();

                userFile.Close();

                Console.Clear();

            }

            catch (Exception)

            {

                Console.WriteLine("An Error has been detected...\n[Press Enter to Return to Menu]");

                Console.ReadLine();

                Console.Clear();

            }

        }

        static string GetString(string msg)

        {

            string myString = "";

            Console.Clear();

            Console.WriteLine(msg);

            myString = Console.ReadLine();

            return myString;

        }

        static int GetInt(string msg)

        {

            int myInt = 0;

            Console.Clear();

            Console.WriteLine(msg);

            myInt = Convert.ToInt16(Console.ReadLine());

            return myInt;

        }

        static void EnterNewUser(ref List<User> userList, ref int menuChoice)

        {

            Console.Clear();

            Console.WriteLine("--------------------------Add User--------------------------\n");

            do

            {

                List<string> menuList = new List<string> { "Would you like to:", "Enter Details", "Return to Menu" };

                RunMenu(ref menuList, ref menuChoice);

                switch (menuChoice)

                {

                    case 1:

                        string newFName = GetString("Please Enter the User's First Name:");

                        string newLName = GetString("Please Enter the User's Last Name:");

                        int newAge = GetInt("Please Enter the User's Age:");

                        string newUsername = GetString("Please Enter the User's Username:");

                        string newLPassword = GetString("Please Enter the User's password:");

                        int secondMenuChoice = 0;

                        Console.Clear();

                        menuList = new List<string> { "Are you happy with these details?\n---------------\nName: " + newFName + " " + newLName + "\nAge: " + newAge + "\nUsername: " + newUsername + "\nPassword: " + newLPassword, "Yes", "No" };

                        RunMenu(ref menuList, ref secondMenuChoice);

                        switch (secondMenuChoice)

                        {
                            case 1:

                                userList.Add(new User(newFName, newLName, newAge, newUsername, newLPassword));

                                SaveUsers(ref userList);

                                Console.WriteLine("You have successfully added a new user!\n[Press Enter to Continue]");

                                Console.ReadLine();

                                Console.Clear();
                                
                                break;

                            case 2:
                                Console.Clear();
                                break;
                        }

                        break;

                    case 2:

                        Console.Clear();

                        break;

                }

            } while (menuChoice != 2);

        }

        static void ViewAllUsers(ref List<User> userList)

        {

            Console.Clear();

            Console.WriteLine("--------------------------All Users--------------------------");

            for (int i = 1; i < userList.Count(); i++)

            {

                Console.WriteLine("\nUser number " + i + "\n------------------\nName: " + userList[i].GetFullName() + "\nAge: " + userList[i].Age + "\nUsername: " + userList[i].Username + "\nPassword: " + userList[i].Password);

            }

            Console.WriteLine("\n[Press Enter to Return to Menu]");

            Console.ReadLine();

            Console.Clear();

        }

        static void Main(string[] args)

        {

            int menuChoice = 0;

            do

            {

                List<string> menuList = new List<string> { "-------------------Menu-------------------", "Enter Details", "View All Details", "Exit" };

                List<User> userList = new List<User>();

                RunMenu(ref menuList, ref menuChoice);

                LoadUsers(ref userList);

                switch (menuChoice)

                {

                    case 1:

                        EnterNewUser(ref userList, ref menuChoice);
                        break;
                    case 2:

                        ViewAllUsers(ref userList);

                        break;

                }

            } while (menuChoice != 3);

        }

    }

}














