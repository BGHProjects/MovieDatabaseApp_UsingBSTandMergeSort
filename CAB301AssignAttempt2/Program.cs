using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Console;

namespace CAB301AssignAttempt2
{
    public class Program
    {
        /*
         * Initiates the Welcome message, the Main Menu, and any test data that is used
         */
        static void Main(string[] args)
        {
            MovieCollection MoC = new MovieCollection();
            MemberCollection MeC = new MemberCollection();

            //Test Data
            /*
            Movie movie1 = new Movie("Movie1","asdf","asdf",Genre.Drama, Classification.General, 123, 123, 123);
            movie1.timesBorrowed = 1;
            Movie movie4 = new Movie("Movie4", "asdf", "asdf", Genre.Drama, Classification.General, 123, 123, 0);
            movie4.timesBorrowed = 4;
            Movie movie3 = new Movie("Movie3", "asdf", "asdf", Genre.Drama, Classification.General, 123, 123, 123);
            movie3.timesBorrowed = 3;
            Movie movie2 = new Movie("Movie2", "asdf", "asdf", Genre.Drama, Classification.General, 123, 123, 123);
            movie2.timesBorrowed = 2;
            Movie movie5 = new Movie("Movie5", "asdf", "asdf", Genre.Drama, Classification.General, 123, 123, 123);
            movie5.timesBorrowed = 5;
            Movie movie6 = new Movie("Movie6", "asdf", "asdf", Genre.Drama, Classification.General, 123, 123, 123);
            movie6.timesBorrowed = 6;
            Movie movie7 = new Movie("Movie7", "asdf", "asdf", Genre.Drama, Classification.General, 123, 123, 123);
            movie7.timesBorrowed = 7;
            Movie movie8 = new Movie("Movie8", "asdf", "asdf", Genre.Drama, Classification.General, 123, 123, 123);
            movie8.timesBorrowed = 8;
            Movie movie9 = new Movie("Movie9", "asdf", "asdf", Genre.Drama, Classification.General, 123, 123, 123);
            movie9.timesBorrowed = 9;
            Movie movie10 = new Movie("Movie10", "asdf", "asdf", Genre.Drama, Classification.General, 123, 123, 123);
            movie10.timesBorrowed = 10;
            Movie movie11 = new Movie("Movie11", "asdf", "asdf", Genre.Drama, Classification.General, 123, 123, 123);
            movie11.timesBorrowed = 11;
            MoC.Insert(movie1);
            MoC.Insert(movie4);
            MoC.Insert(movie3);
            MoC.Insert(movie2);
            MoC.Insert(movie5);
            MoC.Insert(movie6);
            MoC.Insert(movie7);
            MoC.Insert(movie8);
            MoC.Insert(movie9);
            MoC.Insert(movie10);
            MoC.Insert(movie11);



            Member member1 = new Member("A", "A", "asdf", 1234, 1111);
            MeC.AddMember(member1);
            */

            DisplayWelcome();
            DisplayMainMenu(MoC, MeC);
        }

        static void DisplayWelcome()
        {
            WriteLine("Welcome to the Community Library");
        }
        
        /*
         * MAIN MENU - allows the user the following options:
         *  - Log in as Staff
         *  - Log in as a Member
         *  - Exit the program
         */
        static void DisplayMainMenu(MovieCollection movieC, MemberCollection memberC)
        {
            WriteLine("\n==========MAIN MENU==========");
            WriteLine(" 1. Staff Login");
            WriteLine(" 2. Member Login");
            WriteLine(" 0. Exit");
            WriteLine("==============================");

            char Input1 = '1';
            char Input2 = '2';
            char Input3 = '0';
            string UserInput;

            //Loops until actionable input is selected
            do
            {
                Write("\nPlease make a selection: (1-2, or 0 to exit): ");
                UserInput = Console.ReadLine();

                if (UserInput != Input1.ToString() && UserInput != Input2.ToString() && UserInput != Input3.ToString())
                {
                    WriteLine();
                    WriteLine("\nIncorrect Input. Please enter either 1,2 or 0");
                }

                    
            } while (UserInput != Input1.ToString() && UserInput != Input2.ToString() && UserInput != Input3.ToString());


                if (UserInput == Input1.ToString())
                {
                    StaffLogin(movieC, memberC);
                }
                else if (UserInput == Input2.ToString())
                {
                    MemberLogin(movieC, memberC);
                }
                else if (UserInput == Input3.ToString())
                {
                    return; // Closes the Program
                }
            
        }

        /*
         * STAFF LOGIN
         * Handles incorrect input and passage through to Staff Menu
         */
        static void StaffLogin(MovieCollection movieC, MemberCollection memberC)
        {
            string staffUsername;
            string staffPassword;

            WriteLine("\nStaff Login");

            
            //Repeats Login Attempts until the user gets the details correct
            do
            {
                WriteLine("\nEnter Username: ");
                staffUsername = Console.ReadLine();
                WriteLine("Enter Password: ");
                staffPassword = Console.ReadLine();

                if (staffUsername != "staff" || staffPassword != "today123")
                {
                    WriteLine("\nLogin Details Incorrect, Please Try Again");
                }

            } while (staffUsername != "staff" || staffPassword != "today123");
            
            //Welcomes user if the details are correct
            if (staffUsername == "staff" && staffPassword =="today123")
            {
                WriteLine("\nLogin Details Correct, Welcome Staff\n");
                DisplayStaffMenu(movieC, memberC);
            }
        }

        /*
        * MEMBER LOGIN
        * Handles incorrect input and passage through to Staff Menu
        */
        static void MemberLogin(MovieCollection movieC, MemberCollection memberC)
        {
            string memberUsername;
            int memberPassword;

            WriteLine("\nMember Login");

            //Checks if Member exists in the MemberCollection
            do
            {
                WriteLine("\nEnter Member Username (LastnameFirstName): ");
                memberUsername = Console.ReadLine();

                bool checkOutcome = memberC.CheckUsername(memberUsername).Item1;
                
                if (checkOutcome == false)
                {
                    WriteLine("\nMember is not registered, Please Try Again");
                }

            } while (memberC.CheckUsername(memberUsername).Item1 == false);

            WriteLine("\nEnter Member Password: ");
            memberPassword = Convert.ToInt32(Console.ReadLine());

            //Checks the password for the user of the previously confirmed username
            bool passwordOutcome = memberC.CheckPassword(memberPassword, memberC.CheckUsername(memberUsername).Item2);

            if(passwordOutcome == false)
            {
                //If Password is incorrect, send alert and return to Main Menu
                WriteLine("\nIncorrect Password, Returning you to Main Menu");
                DisplayMainMenu(movieC, memberC);
            }
            else if (passwordOutcome == true)
            {
                //If Password is correct, take user to Member Menu
                WriteLine();
                WriteLine("Login Successful, Welcome " + memberC.CheckUsername(memberUsername).Item2.firstName);

                Member loggedInMember = memberC.CheckUsername(memberUsername).Item2;
                DisplayMemberMenu(movieC, memberC, loggedInMember);
            }

        }


        /*
        * STAFF MENU
        * Handles incorrect input and allows the user to do the following:
        *  - Add a New Movie to the Program
        *  - Remove an Existing Movie from the Program
        *  - Register a New Member to the Program
        *  - Find a Member's Phone Number
        *  - Return to the Main Menu
        */
        static void DisplayStaffMenu(MovieCollection movieC, MemberCollection memberC)
        {
            WriteLine("\n==========STAFF MENU==========");
            WriteLine(" 1. Add a New Movie DVD");
            WriteLine(" 2. Remove a Movie DVD");
            WriteLine(" 3. Register a New Member");
            WriteLine(" 4. Find a Registered Member's Phone Number");
            WriteLine(" 0. Return to Main Menu");
            WriteLine("==============================");
            
            char Input1 = '1';
            char Input2 = '2';
            char Input3 = '3';
            char Input4 = '4';
            char Input5 = '0';
            string UserInput;

            //Repeats prompt until a valid entry is received
            do
            {
                Write("\nPlease make a selection: (1-4, or 0 to Return to Main Menu): ");
                UserInput = Console.ReadLine();

                if (UserInput != Input1.ToString() && UserInput != Input2.ToString() && UserInput != Input3.ToString() && UserInput != Input4.ToString() && UserInput != Input5.ToString())
                {
                    WriteLine("\nIncorrect Input. Please enter either 1,2,3,4 or 0");
                }


            } while (UserInput != Input1.ToString() && UserInput != Input2.ToString() && UserInput != Input3.ToString() && UserInput != Input4.ToString() && UserInput != Input5.ToString());

            //Takes the user to the functionality depending on their selection
            if (UserInput == Input1.ToString())
            {
                EnterNewMovie(movieC, memberC);
            }
            else if (UserInput == Input2.ToString())
            {
                RemoveMovie(movieC, memberC);
            }
            else if (UserInput == Input3.ToString())
            {
                RegisterNewMember(movieC, memberC);
            }
            else if (UserInput == Input4.ToString())
            {
                FindPhoneNumber(movieC, memberC);
            }
            else if (UserInput == Input5.ToString())
            {
                DisplayMainMenu(movieC, memberC);
            }
        }

        /*
         * ENTER MOVIE
         * - Enables the user to add a new Movie to the program
         * - Filters for incorrect input
         * - Allows user to add more copies if Movie Title is already in Program
         */
        static void EnterNewMovie(MovieCollection movieC, MemberCollection memberC)
        {
            string movieTitle;
            string movieActors;
            string movieDirector;
            Genre movieGenre;
            Classification movieClassification;
            int movieDuration = 0;
            int movieReleaseYear = 0;
            int movieAvailableCopies = 0;

            string genreInput;
            string classInput;
            string durationInput;
            string rYearInput;
            string aCopiesInput;

            WriteLine();

            //Movie Title
            do
            {
                do
                {
                    WriteLine("Enter the Title of the Movie: ");
                    movieTitle = Console.ReadLine();

                    //Checks if input is valid
                    if (!Regex.IsMatch(movieTitle, @"^[a-zA-Z0-9\x20]+$"))
                    {
                        WriteLine("\nIncorrect Input, please enter a combination of letters and numbers\n");
                    }

                    if (Regex.IsMatch(movieTitle, @"^[\x20]+$"))
                    {
                        WriteLine("\nInput cannot be blank\n");
                    }

                    Movie foundMovie = movieC.Find(movieTitle);

                    //Checks whether movie already exists in database
                    if (foundMovie != null)
                    {
                        string copiesAdded;
                        do
                        {
                            WriteLine("\nPlease enter the number of copies you wish to add: ");
                            copiesAdded = Console.ReadLine();

                            if (!Regex.IsMatch(copiesAdded, @"^[0-9]+$"))
                            {
                                WriteLine("\nIncorrect input, please enter an integer");
                            }
                            else
                            {
                                int copiesToAdd = Convert.ToInt32(copiesAdded);
                                foundMovie.copiesAvailable += copiesToAdd;
                                WriteLine("\nAdded " + copiesToAdd + " copies to " + foundMovie.title + "\n");
                                WriteLine(foundMovie.title + " now has " + foundMovie.copiesAvailable + " copies available");
                                DisplayStaffMenu(movieC, memberC);
                                break;

                            }

                        } while (!Regex.IsMatch(copiesAdded, @"^[0-9]+$"));

                    }

                } while (!Regex.IsMatch(movieTitle, @"^[a-zA-Z0-9\x20]+$"));
            } while (Regex.IsMatch(movieTitle, @"^[\x20]+$"));

            //Actors/Actresses
            do
            {
                do
                {
                    do
                    {
                        WriteLine("Enter the Actors/Actresses in the Movie: ");
                        movieActors = Console.ReadLine();

                        if (!Regex.IsMatch(movieActors, @"^[a-zA-Z,\x20]+$"))
                        {
                            WriteLine("\nIncorrect Input, please enter a string\n");
                        }

                        if (Regex.IsMatch(movieActors, @"^[\x20]+$"))
                        {
                            WriteLine("\nInput cannot be blank\n");
                        }

                        if (Regex.IsMatch(movieActors, @"^[\,]+$"))
                        {
                            WriteLine("\nIncorrect Input, please enter a string\n");
                        }



                    } while (!Regex.IsMatch(movieActors, @"^[a-zA-Z,\x20]+$"));
                } while (Regex.IsMatch(movieActors, @"^[\x20]+$"));
            }while (Regex.IsMatch(movieActors, @"^[,]+$"));

            //Movie Director
            do
            {
                do
                {
                    WriteLine("Enter the Director of the Movie: ");
                    movieDirector = Console.ReadLine();

                    if (!Regex.IsMatch(movieDirector, @"^[a-zA-Z\x20]+$"))
                    {
                        WriteLine("\nIncorrect Input, please enter a string\n");
                    }

                    if (Regex.IsMatch(movieDirector, @"^[\x20]+$"))
                    {
                        WriteLine("\nInput cannot be blank\n");
                    }

                } while (!Regex.IsMatch(movieDirector, @"^[a-zA-Z\x20]+$"));
            } while (Regex.IsMatch(movieDirector, @"^[\x20]+$"));

            //Genre Selection
            do
            {
                WriteLine("\nThe available Genres are:");
                WriteLine("1. Drama");
                WriteLine("2. Adventure");
                WriteLine("3. Family");
                WriteLine("4. Action");
                WriteLine("5. Sci-Fi");
                WriteLine("6. Comedy");
                WriteLine("7. Thriller");
                WriteLine("8. Other");
                WriteLine("Please select the Genre of the Movie: ");
                genreInput = Console.ReadLine().ToString();

                if (genreInput == '1'.ToString()) { movieGenre = Genre.Drama; }
                else if (genreInput == '2'.ToString()) { movieGenre = Genre.Adventure; }
                else if (genreInput == '3'.ToString()) { movieGenre = Genre.Family; }
                else if (genreInput == '4'.ToString()) { movieGenre = Genre.Action; }
                else if (genreInput == '5'.ToString()) { movieGenre = Genre.SciFi; }
                else if (genreInput == '6'.ToString()) { movieGenre = Genre.Comedy; }
                else if (genreInput == '7'.ToString()) { movieGenre = Genre.Thriller; }
                else { movieGenre = Genre.Other; }

                if (genreInput != '1'.ToString() &&
                    genreInput != '2'.ToString() &&
                    genreInput != '3'.ToString() &&
                    genreInput != '4'.ToString() &&
                    genreInput != '5'.ToString() &&
                    genreInput != '6'.ToString() &&
                    genreInput != '7'.ToString() &&
                    genreInput != '8'.ToString()
                    )
                {
                    WriteLine("\nIncorrect Input, please enter either 1,2,3,4,5,6,7 or 8\n");
                }

            } while (genreInput != '1'.ToString() &&
                    genreInput != '2'.ToString() &&
                    genreInput != '3'.ToString() &&
                    genreInput != '4'.ToString() &&
                    genreInput != '5'.ToString() &&
                    genreInput != '6'.ToString() &&
                    genreInput != '7'.ToString() &&
                    genreInput != '8'.ToString());

            //Classification Selection
            do
            {
                WriteLine("\nThe available Classifications are:");
                WriteLine("1. General (G)");
                WriteLine("2. Parental Guidance (PG)");
                WriteLine("3. Mature (M)");
                WriteLine("4. Mature Accompanied (MA15+)");
                WriteLine("Please select the Classification of the Movie: ");
                classInput = Console.ReadLine().ToString();

                if (classInput == '1'.ToString()) { movieClassification = Classification.General; }
                else if (classInput == '2'.ToString()) { movieClassification = Classification.ParentalGuidance; }
                else if (classInput == '3'.ToString()) { movieClassification = Classification.Mature; }
                else { movieClassification = Classification.MatureAccompanied; }

                if(classInput != '1'.ToString() && classInput != '2'.ToString() && classInput != '3'.ToString() && classInput != '4'.ToString())
                {
                   WriteLine("\nIncorrect Input, please enter either 1,2,3 or 4\n");
                }

            } while (classInput != '1'.ToString() && classInput != '2'.ToString() && classInput != '3'.ToString() && classInput != '4'.ToString());

            //Duration in Minutes
            do
            {
                WriteLine("\nEnter the Movie's Duration in Minutes: ");
                durationInput = Console.ReadLine();

                if (!Regex.IsMatch(durationInput, @"^[0-9]+$"))
                {
                    WriteLine("\nIncorrect Input, please enter an integer\n");
                }
                else
                {
                    movieDuration = Convert.ToInt32(durationInput);
                }

            } while (!Regex.IsMatch(durationInput, @"^[0-9]+$"));

            //Year of Release
            do
            {
                WriteLine("\nEnter the Movie's Year of Release: ");
                rYearInput = Console.ReadLine();

                if (!Regex.IsMatch(rYearInput, @"^[0-9]+$"))
                {
                    WriteLine("\nIncorrect Input, please enter an integer\n");
                }
                else
                {
                    movieReleaseYear = Convert.ToInt32(rYearInput);
                }

            } while (!Regex.IsMatch(rYearInput, @"^[0-9]+$"));

            //Number of Available Copies
            do
            {
                WriteLine("\nEnter the Number of Available Copies: ");
                aCopiesInput = Console.ReadLine();

                if (!Regex.IsMatch(aCopiesInput, @"^[0-9]+$"))
                {
                    WriteLine("\nIncorrect Input, please enter an integer\n");
                }
                else
                {
                    movieAvailableCopies = Convert.ToInt32(aCopiesInput);
                }

            } while (!Regex.IsMatch(aCopiesInput, @"^[0-9]+$"));


            //Adds new movie to the Movie Collection and returns the user to the Staff Menu
            Movie newMovie = new Movie(movieTitle, movieActors, movieDirector, movieGenre, movieClassification, movieDuration, movieReleaseYear, movieAvailableCopies);
            movieC.Insert(newMovie);
            WriteLine("Successfully added " + newMovie.title + "\n");

            //Returns user to the Staff Menu
            DisplayStaffMenu(movieC, memberC);
        }

        /*
         * REMOVE MOVIE
         * Searches BST for entered Movie
         * Deletes if found
         * Alerts user if not
         */
        static void RemoveMovie(MovieCollection movieC, MemberCollection memberC)
        {
            string mTitle;

            WriteLine("\nEnter the Title of the Movie you wish to remove: ");
            mTitle = Console.ReadLine();

            Movie outcome = movieC.Find(mTitle);

            if (outcome == null)
            {
                WriteLine("\n" + mTitle + " could not be found in the database, please check the details\n");
                DisplayStaffMenu(movieC, memberC);
            }
            else
            {
                movieC.Remove(outcome);
                WriteLine("\n" + mTitle + " has been removed from the database\n");
                DisplayStaffMenu(movieC, memberC);

            }
        }


        /*
         * REGISTER NEW MEMBER
         * Allows user to enter new Member into Program
         * Alerts user if entered name matches existing Member
         */
        static void RegisterNewMember(MovieCollection movieC, MemberCollection memberC)
        {
            string memberFirstName;
            string memberLastName;
            string memberAddress;
            int memberPhoneNumber = 0;
            int memberPassword = 0;

            string pNumberInput;
            string passwordInput;

            WriteLine();

            //Member First Name
            do
            {
                do
                {
                    WriteLine("Enter the Member's First Name: ");
                    memberFirstName = Console.ReadLine();

                    if (!Regex.IsMatch(memberFirstName, @"^[a-zA-Z\x20]+$"))
                    {
                        WriteLine("\nIncorrect Input, please enter a string\n");
                    }

                    if (Regex.IsMatch(memberFirstName, @"^[\x20]+$"))
                    {
                        WriteLine("\nInput cannot be blank");
                    }


                } while (!Regex.IsMatch(memberFirstName, @"^[a-zA-Z\x20]+$"));
            } while (Regex.IsMatch(memberFirstName, @"^[\x20]+$"));

            //Member Last Name
            do
            {
                do
                {
                    WriteLine("Enter the Member's Last Name: ");
                    memberLastName = Console.ReadLine();

                    if (!Regex.IsMatch(memberLastName, @"^[a-zA-Z\x20]+$"))
                    {
                        WriteLine("\nIncorrect Input, please enter a string\n");
                    }

                    if (Regex.IsMatch(memberLastName, @"^[\x20]+$"))
                    {
                        WriteLine("\nInput cannot be blank\n");
                    }

                } while (!Regex.IsMatch(memberLastName, @"^[a-zA-Z\x20]+$"));
            } while (Regex.IsMatch(memberLastName, @"^[\x20]+$"));

            //Checks to see if a Member of those names already exists in database
            Member checkedMem = memberC.FindMember(memberFirstName, memberLastName);

            if(checkedMem != null)
            {
                WriteLine("\n" + checkedMem.firstName + " " + checkedMem.lastName + " is already a registered member");
                DisplayStaffMenu(movieC, memberC);
                return;
            }

            //Member Address
            do
            {
                do
                {
                    WriteLine("Enter the Member's Address: ");
                    memberAddress = Console.ReadLine();

                    if (!Regex.IsMatch(memberAddress, @"^[a-zA-Z0-9\x20]+$"))
                    {
                        WriteLine("\nIncorrect Input, please do not use special characters\n");
                    }

                    if (Regex.IsMatch(memberFirstName, @"^[\x20]+$"))
                    {
                        WriteLine("\nInput cannot be blank\n");
                    }


                } while (!Regex.IsMatch(memberAddress, @"^[a-zA-Z0-9\x20]+$"));
            } while (Regex.IsMatch(memberLastName, @"^[\x20]+$"));

            //Member Phone Number
            do
            {
                WriteLine("Enter the Member's Phone Number: ");
                pNumberInput = Console.ReadLine();

                if (!Regex.IsMatch(pNumberInput, @"^[0-9]+$"))
                {
                    WriteLine("\nIncorrect Input, plese enter an integer\n");
                }
                else
                {
                    memberPhoneNumber = Convert.ToInt32(pNumberInput);
                }

            } while (!Regex.IsMatch(pNumberInput, @"^[0-9]+$"));

            //Member Password
            do
            {
                WriteLine("Enter a 4 Digit Number as the Member's Password");
                passwordInput = Console.ReadLine();

                if (!Regex.IsMatch(passwordInput, @"^[0-9]{4}$"))
                {
                    WriteLine("\nIncorrect Input, plese enter a four digit integer\n");
                }
                else
                {
                    memberPassword = Convert.ToInt32(passwordInput);
                }

            } while (!Regex.IsMatch(passwordInput, @"^[0-9]{4}$"));

            
            //Adds a new Member to the MemberCollection
            Member newMember = new Member(memberFirstName, memberLastName, memberAddress, memberPhoneNumber, memberPassword);
            memberC.AddMember(newMember);
            WriteLine("\nSuccessfully registered " + newMember.firstName + " " + newMember.lastName + " as Member No. " + newMember.memberNumber + "\n");

            //Returns user to the Staff Menu
            DisplayStaffMenu(movieC, memberC);

        }

        /*
         * FIND REGISTERED MEMBER'S PHONE NUMBER
         * Displays phone number of Member that matches input
         * Alerts user if input does not match any Member in Program
         */
        static void FindPhoneNumber( MovieCollection movieC, MemberCollection memberC)
        {
            string memberFirstName;
            string memberLastName;
            int returnedNumber;

            WriteLine();
            WriteLine("Enter the Member's First Name: ");
            memberFirstName = Console.ReadLine();
            WriteLine("Enter the Member's Last Name: ");
            memberLastName = Console.ReadLine();

            returnedNumber = memberC.FindPhoneNumber(memberFirstName, memberLastName);

            if (returnedNumber == 0)
            {
                WriteLine("\nThere is no Member with that registered name\n");
            }
            else
            {
                WriteLine("\n" + memberFirstName + " " + memberLastName + "'s Phone number is: " + returnedNumber + "\n");
            }

            DisplayStaffMenu(movieC, memberC);

        }

        /*
         * MEMBER MENU
         * Filters for incorrect input and allows the user to do the following:
         * - Display all Movies in the Program
         * - Borrow a copy of a Movie from the Program
         * - Return a copy of a Movie to the Program
         * - Display all the Movies that the current Member is borrowing
         * - Display the Top 10 most borrowed Movies currently in the Program
         * - Return to the Main menu
         */
        static void DisplayMemberMenu(MovieCollection movieC, MemberCollection memberC, Member member)
        {
            WriteLine("\n==========MEMBER MENU==========");
            WriteLine("Member Currently Logged In: " + member.firstName + " " + member.lastName);
            WriteLine(" 1. Display all Movies");
            WriteLine(" 2. Borrow a Movie DVD");
            WriteLine(" 3. Return a Movie DVD");
            WriteLine(" 4. List current Borrowed Movie DVDs");
            WriteLine(" 5. Display Top 10 Most Popular Movies");
            WriteLine(" 0. Return to Main Menu");
            WriteLine("===============================\n");

            char Input1 = '1';
            char Input2 = '2';
            char Input3 = '3';
            char Input4 = '4';
            char Input5 = '5';
            char Input6 = '0';
            string UserInput;

            //Repeats prompt until a valid entry is received
            do
            {
                Write("\nPlease make a selection: (1-5, or 0 to Return to Main Menu): ");
                UserInput = Console.ReadLine();

                if (UserInput != Input1.ToString() && UserInput != Input2.ToString() && UserInput != Input3.ToString() && UserInput != Input4.ToString() && UserInput != Input5.ToString() && UserInput != Input6.ToString())
                {
                    WriteLine("\nIncorrect Input. Please enter either 1,2,3,4,5 or 0");
                }


            } while (UserInput != Input1.ToString() && UserInput != Input2.ToString() && UserInput != Input3.ToString() && UserInput != Input4.ToString() && UserInput != Input5.ToString() && UserInput != Input6.ToString());

            //Takes the user to the functionality depending on their selection
            if (UserInput == Input1.ToString())
            {
                DisplayAllMovies(movieC, memberC, member);
            }
            else if (UserInput == Input2.ToString())
            {
                MemberBorrowMovie(movieC, memberC, member);
            }
            else if (UserInput == Input3.ToString())
            {
                MemberReturnMovie(movieC, memberC, member);
            }
            else if (UserInput == Input4.ToString())
            {
                ListCurrentBorrowed(movieC, memberC, member);
            }
            else if (UserInput == Input5.ToString())
            {
                ShowTop10MoviesBorrowed(movieC, memberC, member);
            }
            else if (UserInput == Input6.ToString())
            {
                DisplayMainMenu(movieC, memberC);
            }
        }

        /*
         * DISPLAY ALL MOVIES
         * Searchs through the BST for all Movies currently in the Program
         * And displays them to the console
         */
        static void DisplayAllMovies(MovieCollection movieC, MemberCollection memberC, Member member)
        {
            movieC.DisplayInOrder();
            WriteLine();
            DisplayMemberMenu(movieC, memberC, member);

        }

        /*
         * BORROW MOVIE
         * Enables the user to borrow a copy of a Movie from the Program
         * Alerts the user if their input does not match the title of a Movie in the Program
         * Alerts the user if they are already borrowing the Movie they have inputted
         * Alerts the user if there are no more copies of the Movie in the Program
         * Alerts the user if they are borrowing the maximum number of Movies at one time
         */
        static void MemberBorrowMovie(MovieCollection movieC, MemberCollection memberC, Member member)
        {
            string movieTitle;

            WriteLine("\nPlease enter the movie you wish to borrow: ");
            movieTitle = Console.ReadLine();

            Movie borrowedMovie = movieC.Find(movieTitle);

            if (borrowedMovie == null)
            {
                WriteLine("\n" + movieTitle + " could not be found in the database, please check the details");
                DisplayMemberMenu(movieC, memberC, member);
            }

            //Checks to see if the movie is already borrowed
            bool alreadyBorrowed = false;

            for (int i = 0; i < member.moviesBorrowed.Length; i++)
            {
                if(member.moviesBorrowed[i] == null)
                {
                    alreadyBorrowed = false;
                }
                else if (member.moviesBorrowed[i].title == borrowedMovie.title)
                {
                    alreadyBorrowed = true;
                    break;
                }
                else
                {
                    alreadyBorrowed = false;
                }
            }

            //Determines next course based on if the movie is already borrowed or not
            if (alreadyBorrowed == false)
            {
                
                if (borrowedMovie.copiesAvailable < 1)
                {
                    WriteLine("\nThere are no more copies of " + movieTitle + " remaining\n");
                    DisplayMemberMenu(movieC, memberC, member);
                }
                else
                {
                    int currentlyBorrowedMovies = 0;

                    //Counts number of Movies the user is already borrowing
                    for (int i = 0; i < member.moviesBorrowed.Length; i++)
                    {
                        if(member.moviesBorrowed[i] != null)
                        {
                            currentlyBorrowedMovies++;
                        }
                    }
                    
                    //Kicks user if they have reached the maximum number of Borrowed Movies
                    if(currentlyBorrowedMovies == 10)
                    {
                        WriteLine("\nYou are already borrowing the maximum amount of Movies possible\n");
                        DisplayMemberMenu(movieC, memberC, member);
                    }
                    else
                    {
                        //Finds empty position in Movies Borrowed array
                        //And adds the Movie to it
                        for (int i = 0; i < member.moviesBorrowed.Length; i++)
                        {
                            if (member.moviesBorrowed[i] == null)
                            {
                                member.moviesBorrowed[i] = borrowedMovie;
                                break;
                            }
                        }
                        //Decreases number of available copies
                        borrowedMovie.copiesAvailable -= 1;
                        //Increases number of times borrowed
                        borrowedMovie.timesBorrowed += 1;

                        //Calculates how many movies the user can borrow
                        int nullMovies = 0;
                        for (int i = 0; i < member.moviesBorrowed.Length; i++)
                        {
                            if (member.moviesBorrowed[i] == null)
                            {
                                nullMovies++;
                            }
                        }

                        WriteLine("\nYou have now borrowed " + movieTitle);
                        WriteLine("You have borrowed " + (member.moviesBorrowed.Length - nullMovies) + " of your total 10 Movies you can borrow at one time\n");
                        DisplayMemberMenu(movieC, memberC, member);
                    }
                }

            }
            else
            {
                WriteLine("\nYou are already borrowing: " + movieTitle + "\n");
                DisplayMemberMenu(movieC, memberC, member);
            }

        }

        /*
         * RETURN MOVIE
         * Enables the user to return a Movie that they are borrowing
         * Alerts the user if their input does not match a Movie they are borrowing
         */
        static void MemberReturnMovie(MovieCollection movieC, MemberCollection memberC, Member member)
        {
            string movieTitle;

            WriteLine("\nPlease enter the movie you wish to return: ");
            movieTitle = Console.ReadLine();

            Movie borrowedMovie = member.FindMovie(movieTitle);

            if (borrowedMovie == null)
            {
                WriteLine();
                WriteLine("\nYou have not borrowed " + movieTitle);
                WriteLine();
                DisplayMemberMenu(movieC, memberC, member);
            }
            else
            {
                for (int i = 0; i < member.moviesBorrowed.Length; i++)
                {
                    if (member.moviesBorrowed[i] == borrowedMovie)
                    {
                        member.moviesBorrowed[i] = null;
                    }
                }


                //Increases number of available copies
                borrowedMovie.copiesAvailable += 1;

                //Calculates how many movies the user can borrow
                int nullMovies = 0;
                for (int i = 0; i < member.moviesBorrowed.Length; i++)
                {
                    if (member.moviesBorrowed[i] == null)
                    {
                        nullMovies++;
                    }
                }


                WriteLine();
                    WriteLine("You have now returned " + movieTitle);
                    WriteLine("You have borrowed " + (member.moviesBorrowed.Length - nullMovies) + " of your total 10 Movies you can borrow at one time");
                    WriteLine();
                    DisplayMemberMenu(movieC, memberC, member);
             }


            
        }

        /*
         * LIST CURRENTLY BORROWED MOVIES
         * Displays all the Movies that the user is borrowing
         * Alerts the user if they are not currently borrowing any movies
         */
        static void ListCurrentBorrowed(MovieCollection movieC, MemberCollection memberC, Member member)
        {
            WriteLine();

            bool isThereAMovie = false;

            for (int i = 0; i < member.moviesBorrowed.Length; i++)
            {
                if (member.moviesBorrowed[i] != null)
                {
                    isThereAMovie = true;
                    break;
                }
            }


            if (isThereAMovie == false)
            {
                WriteLine(member.firstName + ", you are not currently borrowing any movies");
                WriteLine();
                DisplayMemberMenu(movieC, memberC, member);

            }
            else
            {
                WriteLine("You are currently borrowing: ");

                for (int i = 0; i < member.moviesBorrowed.Length; i++)
                {
                    if(member.moviesBorrowed[i] != null)
                    {
                        WriteLine(member.moviesBorrowed[i].title);
                    }
                    
                }

                WriteLine();
                DisplayMemberMenu(movieC, memberC, member);
            }
        }

        /*
         * DISPLAY TOP 10 BORROWED MOVIES
         * Navigates the BST to find the 10 most borrowed Movies in the Program
         * Displays less than 10 is there are less than 10 movies in the Program
         */
        static void ShowTop10MoviesBorrowed(MovieCollection movieC, MemberCollection memberC, Member member)
        {
            WriteLine();

            Movie[] sortedResults = movieC.mergeSort(movieC.GetTop10());

            WriteLine("The Top 10 Most Borrowed Movies are: ");
            WriteLine("-------------------------------------");

            int x = 1;

            int forLoopIndex;

            if (sortedResults.Length < 10)
            {
                forLoopIndex = sortedResults.Length - 1;
            }
            else
            {
                forLoopIndex = 10;
            }

            for (int i = forLoopIndex-1; i > -1; i--)
            {
                if(sortedResults[i] == null)
                {
                    continue;
                }
                WriteLine(x + ". " + sortedResults[i].title + "         (borrowed " + sortedResults[i].timesBorrowed + " times)");
                x++;
            }
            
            WriteLine();

            DisplayMemberMenu(movieC, memberC, member);

        }
    }
}
