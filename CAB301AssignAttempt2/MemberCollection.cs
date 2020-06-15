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
     * - Adding a Member to the Program
     * - Checking if an entered username matches a Member in the Program
     * - Checking if an entered password is valid
     * - Finding a Member in the Program
     * - Finding the Phone Number of a Member in the Program
     */
    public  class MemberCollection
    {
        public Member[] members = new Member[10];
        public bool isFull = false;

        public void AddMember(Member member)
        {
            if (!isFull) //if the array is not false
            {
                for (int i=0; i<10; i++)
                {
                    if (members[i] == null) 
                    {
                        members[i] = member; //If the spot in the array is empty, put current member in it
                        member.memberNumber = i + 1;
                        break;
                    }
                    else if (i == 9)
                    {
                        isFull = true; //If the array is full, set it to full and exit loop
                    }

                }
            }
        }

        
        public (bool,Member) CheckUsername(string enteredName)
        {
            bool outcome = false;
            Member actual = null;

            for (int i=0; i< members.Length; i++ )
            {
                if(members[i] == null)
                {
                    outcome = false;
                    actual = null;
                }
                else if(enteredName == members[i].memberLogin) //Checks the entered name to the member's login details
                {
                    outcome = true; //Confirms the login and exits the loop, so not all values are checked
                    actual = members[i];
                    break;
                }
                else
                {
                    outcome = false; //Keeps the outcome false if the login is not confirmed
                    actual = null;
                }
            }

            return (outcome, actual);
        }

        public bool CheckPassword(int enteredPassword, Member mem)
        {
            bool outcome = false;

            if(mem.password == enteredPassword)
            {
                outcome = true;
            }
            else
            {
                outcome = false;
            }

            return outcome;
        }

        public Member FindMember(string mFName, string mLName)
        {
            Member foundMember = null;

            for (int i = 0; i < members.Length; i++)
            {
                if (members[i] == null) //If there are no members, do not return a member
                {
                    foundMember = null;
                }
                else if (members[i].firstName == mFName && members[i].lastName == mLName)
                {
                    foundMember = members[i];
                    break;
                }
                else
                {
                    foundMember = null; //Do not return a member is there isn't one

                }
            }

               return foundMember;
        }

        public int FindPhoneNumber(string mFName, string mLName)
        {
            int foundNumber = 0;

            Member mem = FindMember(mFName, mLName);

            if (mem == null)
            {
                foundNumber = 0; //Return 0 if there is no member
            }
            else
            {
                foundNumber = mem.phoneNumber;
            }
            

            return foundNumber;
        }


        
    }
}
