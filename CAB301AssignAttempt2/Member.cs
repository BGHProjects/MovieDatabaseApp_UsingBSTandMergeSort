using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace CAB301AssignAttempt2
{
    /*
     * Contains methods for the following:
     * - Finding a Movie based on the Member
     */
    public class Member
    {
        public string firstName;
        public string lastName;
        public string address;
        public int phoneNumber;
        public int password;

        //Extra fields for functionality
        public int memberNumber;
        public string memberLogin;
        public Movie[] moviesBorrowed;

        //Default Constructor with just the fields required for a Member
        public Member(string fName, string lName, string add, int pNumber, int pWord)
        {
            firstName = fName;
            lastName = lName;
            address = add;
            phoneNumber = pNumber;
            password = pWord;
            memberLogin = lName + fName;
            moviesBorrowed = new Movie[10];
        }

        

        //Another Constructor, containing additional fields for functionality
        public Member(string fName, string lName, string add, int pNumber, int pWord, int mNumber, Movie[] mBorrowed)
        {
            firstName = fName;
            lastName = lName;
            address = add;
            phoneNumber = pNumber;
            password = pWord;
            memberNumber = mNumber;
            moviesBorrowed = mBorrowed;
        }

        public Movie FindMovie(string movTitle)
        {
            Movie foundMovie = null;

            for (int i = 0; i < moviesBorrowed.Length; i++)
            {
                if(moviesBorrowed[i] == null)
                {
                    foundMovie = null;
                }
                else if (moviesBorrowed[i].title == movTitle)
                {
                    foundMovie = moviesBorrowed[i];
                    break;
                }
            }

            return foundMovie;
        }


    }
}
