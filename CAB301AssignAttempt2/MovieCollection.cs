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
     * - Removing a Movie from the BST
     * - Finding a Movie's Successor in the BST
     * - Displaying all the Movies in the BST in order
     * - Adding all the Movies from the BST into an Array
     * - Mergesorting the Movies from the BST
     * - Merging sorted arrays of Movies into a single sorted array
     */
    public class MovieCollection
    {
        public Movie rootMovie;
        public int numOfMovies;
        public Movie[] movies;

        
        //Function to Insert a new Movie into MovieCollection
        public void Insert(Movie movie)
        {
            //If there is a root, the new movie gets inserted down the tree
            if (rootMovie != null)
            {
                rootMovie.Insert(movie);

            }
            //If there is no root, the new movie becomes the root
            else
            {
                rootMovie = movie;
            }

            numOfMovies++;
        }

        public Movie Find(string movTitle)
        {
            //If a rootMovie exists, there is something to find
            if (rootMovie != null)
            {
                return rootMovie.Find(movTitle);
            }
            else //If there is no rootMovie, there is nothing to find
            {
                return null;
            }

        }

        //Function to Remove a Movie from MovieCollection
        public void Remove(Movie movie)
        {
            //Set current and parent to the root, so we can remove the parent's reference
            Movie current = rootMovie;
            Movie parent = rootMovie;
            bool isLeftChild = false; //Determines which child of parent is to be removed

            if (current == null) //If the tree is empty, there is nothing to remove, so just return
            {
                return;
            }

            //Find the Movie in the collection
            //Loop through the collection until the Movie is found, or until it is not
            while (current != null && current != movie)
            {
                //Set the parent reference to the current Movie, so we can check its children
                parent = current;

                //If the current Movie is the left node (alphabetically BEFORE the inputted Movie)
                if (String.Compare(movie.title, current.title) == -1)
                {
                    current = current.nodeLeft;
                    isLeftChild = true; //Let's us know that the child is a LEFT child
                }
                else
                {
                    current = current.nodeRight;
                    isLeftChild = false; // Let's us know that the child is a RIGHT child
                }
            }

            //After we have changed the current Movie, if it is now null
            //exit function, because there is nothing to remove
            if (current == null) 
            {
                return;
            }

            //This part filters for whether the Movie is a Leaf, if it has a Left Node Movie, or a Right Node Movie, or Both

            //If the current Movie is a Leaf
            if (current.nodeRight == null && current.nodeLeft == null)
            {
                //If we are at the rootMovie, there is not parent Movie, so just remove it
                if (current == rootMovie)
                {
                    rootMovie = null;
                    numOfMovies--;
                }
                else
                {
                    //Use boolean from earlier to determine child is to be removed from reference
                    if (isLeftChild)
                    {
                        parent.nodeLeft = null;
                        numOfMovies--;
                    }
                    else
                    {
                        parent.nodeRight = null;
                        numOfMovies--;
                    }
                }

            }
            //If the current Movie only has a Left Node Movie
            else if (current.nodeRight == null)
            {
                //If we are at the rootMovie, rootMovie becomes left child
                if (current == rootMovie)
                {
                    rootMovie = current.nodeLeft;
                }
                else
                {
                    //Here we determine which child is to be removed (dereferenced)
                    if (isLeftChild)
                    {
                        //Current is left, so parent left node becomes current left node
                        parent.nodeLeft = current.nodeLeft;
                    }
                    else
                    {
                        //Current is right, so parent right node becomes current left node
                        parent.nodeRight = current.nodeLeft;
                    }
                }

            }
            //If the current Movie only has a Right Node Movie
            else if (current.nodeLeft == null)
            {
                //This section is the same as above, except in this case
                //The right child is being dereferenced

                //If we are at the rootMovie, rootMovie becomes right child
                if (current == rootMovie)
                {
                    rootMovie = current.nodeRight;
                }
                else
                {
                    //Here we determine which child is to be removed (dereferenced)
                    if (isLeftChild)
                    {
                        //Current is left, so parent left node becomes current right node
                        parent.nodeLeft = current.nodeRight;
                    }
                    else
                    {
                        //Current is right, so parent right node becomes current right node
                        parent.nodeRight = current.nodeRight;
                    }
                }
            }
            //If the current Movie has Both a Left Node Movie and a Right Node Movie
            else
            {
                //If Movie has Both child nodes,
                //Go through right nodes and find leaf of the left nodes (earliest alphabetically BEFORE)
                //and then the right node would be left node of parent, which is the successor Movie

                //Find successor to the current (earliest alphabetically BEFORE)
                Movie successor = FindSuccessor(current);

                //If we are at the rootMovie, rootMovie becomes successor Movie
                if(current == rootMovie)
                {
                    rootMovie = successor;
                }
                else if (isLeftChild)
                {
                    //Left child of parent becomes successor 
                    parent.nodeLeft = successor;
                }
                else
                {
                    //Right child of parent becomes successor
                    parent.nodeRight = successor;
                }
            }
        }

        //Finds the earliest alphabetically AFTER Movie
        public Movie FindSuccessor(Movie movie)
        {
            Movie parentSuccessor = movie;
            Movie successor = movie;
            Movie current = movie.nodeRight;

            //Begin at right node and search all the left nodes
            while (current != null)
            {
                //Shifts values leftwards to go to next left node
                parentSuccessor = successor;
                successor = current;
                current = current.nodeLeft;
            }

            //If the next Movie isn't just the right child
            if (successor != movie.nodeRight)
            {
                parentSuccessor.nodeLeft = successor.nodeRight;
                //Make right node of Movie to be deleted to be the Successor's right node
                successor.nodeRight = movie.nodeRight;
            }

            //Make the left node of the Movie to be deleted to be the Successor's left node
            successor.nodeLeft = movie.nodeLeft;

            return successor;
        }

        //Uses In Order Traversal to sort the MovieCollection
        public void DisplayInOrder()
        {

            if (rootMovie != null)
            {
                rootMovie.DisplayInOrder();
            }
        }

        public Movie[] GetTop10()
        {
            this.movies = new Movie[numOfMovies];

            rootMovie.AllMoviesToArray(this, 0);

            return this.movies;
        }

        public Movie[] mergeSort(Movie[] movies)
        {
            Movie[] left;
            Movie[] right;
            Movie[] result = new Movie[movies.Length];
            //Base case to avoid infinite recursion

            if(movies.Length <= 1)
            {
                return movies;
            }

            //Midpoint of Movie array to split with
            int midPoint = movies.Length / 2;

            left = new Movie[midPoint];

            //If the Movies array is even, right has the same values as left
            //Otherwise right will have one more than left
            if(movies.Length % 2 == 0)
            {
                right = new Movie[midPoint];
            }
            else
            {
                right = new Movie[midPoint + 1];
            }

            //Populate left
            for (int i = 0; i < midPoint; i++)
            {
                left[i] = movies[i];
            }

            //Populate right, by starting where 'Populate left' finished
            int x = 0;

            for(int i = midPoint; i < movies.Length; i++)
            {
                right[x] = movies[i];
                x++;
                //Increments right independent of incrementing the for loop
            }

            //Recusrively sort both sides
            left = mergeSort(left);
            right = mergeSort(right);

            //Merge both sides
            result = merge(left, right);

            return result;

        }

        public Movie[] merge(Movie[] left, Movie[] right)
        {
            int resultLength = left.Length + right.Length;
            Movie[] result = new Movie[resultLength];

            int indexLeft = 0, indexRight = 0, indexResult = 0;

            //Continue loop while there is an element in either array
            while(indexLeft < left.Length || indexRight < right.Length)
            {
                //If both sides have elements
                if (indexLeft < left.Length && indexRight < right.Length)
                {
                    //If left array Movie is borrowed less times than right array Movie
                    //then this is added to the result array
                    if(right[indexRight] == null || left[indexLeft].timesBorrowed <= right[indexRight].timesBorrowed)
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    //Otherwise the right movie is added to the result array
                    else
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                //If only the left array has elements
                //add these to the result array
                else if(indexLeft < left.Length)
                {
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                //If only the right array has elements
                //add these to the result array
                else if (indexRight < right.Length)
                {
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }
            }

            return result;
        }

    }
}
