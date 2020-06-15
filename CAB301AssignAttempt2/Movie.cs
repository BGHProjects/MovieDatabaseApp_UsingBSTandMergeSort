using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace CAB301AssignAttempt2
{
    /*
     * Contains methods for the following:
     * - Inserting a Movie into the BST
     * - Finding if the Movie is in the BST
     * - Displaying all the Movies in the BST
     * - Adding all the Movies in the BST into an array
     */
    public enum Genre
    {
        Drama, Adventure, Family, Action, SciFi, Comedy, Thriller, Other
    }

    public enum Classification
    {
        General, ParentalGuidance, Mature, MatureAccompanied
    }

    public class Movie
    {

        public string title;
        public string actors;
        public string director;

        public Genre genre;
        public Classification classification;


        public int duration;
        public int releaseYear;
        public int copiesAvailable;
        public int timesBorrowed;

        public Movie nodeRight;
        public Movie nodeLeft;

        //Movie Constructor
        public Movie(string titleName, string actorsNames, string directorName, Genre genreName, Classification classificationName, int durationNumber, int yearOfRelease, int availableCopies)
        {
            title = titleName;
            actors = actorsNames;
            director = directorName;
            genre = genreName;
            classification = classificationName;
            duration = durationNumber;
            releaseYear = yearOfRelease;
            copiesAvailable = availableCopies;
        }

        //Alternative Constructor with Right and Left Nodes
        public Movie(string titleName, string actorsNames, string directorName, Genre genreName, Classification classificationName, int durationNumber, int yearOfRelease, int availableCopies, Movie rightNode, Movie leftNode, int borrowedNum)
        {
            title = titleName;
            actors = actorsNames;
            director = directorName;
            genre = genreName;
            classification = classificationName;
            duration = durationNumber;
            releaseYear = yearOfRelease;
            copiesAvailable = availableCopies;
            nodeRight = rightNode;
            nodeLeft = leftNode;
            timesBorrowed = borrowedNum;
        }

        //Finds spot in the tree for the movie
        public void Insert(Movie movie)
        {
            //If the first string (new Movie) is alphabetically AFTER
            //the original movie, it becomes the RIGHT node
            if (String.Compare(movie.title, this.title) == 1)
            {
                //If there is no right node, the new movie becomes the right node
                if (this.nodeRight == null)
                {
                    this.nodeRight = movie;
                }
                else
                //If there is a right node, call the function again
                //to go further down the tree
                {
                    this.nodeRight.Insert(movie);
                }
            }
            //If the new Movie is alphabetically BEFORE
            //the original movie, it becomes the LEFT Node
            else if (String.Compare(movie.title, this.title) == -1)
            {
                //If there is no left node, the new movie becomes the left node
                if (this.nodeLeft == null)
                {
                    this.nodeLeft = movie;
                }
                else
                //If there is a left node, call the function again
                //to go further down the tree
                {
                    this.nodeLeft.Insert(movie);
                }
            }
            else
            {
                Console.WriteLine("There is an error with Movie.Insert");
            }
        }

        public Movie Find(string movTitle)
        {
            //This value is used to start the search with
            Movie currentMovie = this;

            //Loop through all the movies until none are left
            while (currentMovie != null)
            {
                //If the values match, return the movie
                if (movTitle == currentMovie.title)
                {
                    return currentMovie;
                }
                //If inputted title is alphabetically AFTER currentMovie
                //the currentMovie becomes the right node
                //and the search starts again
                else if (String.Compare(movTitle, currentMovie.title) == 1)
                {
                    currentMovie = currentMovie.nodeRight;
                }
                //Otherwise, the inputted title must be BEFORE currentMovie
                //so currentMovie becomes the left node
                //and the search starts again
                else
                {
                    currentMovie = currentMovie.nodeLeft;
                }
            }

            //If the while loop is exited, that means there are
            //no more values to check, so return nothing
            return null;
        }

        //Goes through the MovieCollection Binary Search Tree
        //through In Order Traversal, recursively
        public void DisplayInOrder()
        {

            if (this.nodeLeft != null)
            {
                this.nodeLeft.DisplayInOrder();
            }

            Console.WriteLine();
            Console.WriteLine("Title: " + this.title);
            Console.WriteLine("Starring: " + this.actors);
            Console.WriteLine("Director: " + this.director);
            Console.WriteLine("Genre: " + this.genre);
            Console.WriteLine("Classification: " + this.classification);
            Console.WriteLine("Duration: " + this.duration);
            Console.WriteLine("Release Year: " + this.releaseYear);
            Console.WriteLine("Copies Available: " + this.copiesAvailable);
            Console.WriteLine("Times Rented: " + this.timesBorrowed);

            if (this.nodeRight != null)
            {
                this.nodeRight.DisplayInOrder();
            }
        }

        //Flattening Function for Top 10
        public void AllMoviesToArray(MovieCollection mCol, int startIndex)
        {

            if(this.nodeLeft != null)
            {
                this.nodeLeft.AllMoviesToArray(mCol, startIndex);
            }

            for(int i = 0; i < mCol.numOfMovies; i++)
            {
                if (mCol.movies[i] == null)
                {
                    mCol.movies[i] = this;
                    break;
                }
            }

            if(this.nodeRight != null)
            {
                this.nodeRight.AllMoviesToArray(mCol, startIndex);
            }
        }
                    
    

    }


}
