public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        /*
        Initialize an empty list of doubles.
        Using a for loop, go through integers 1 up to and including the length parameter.
        Then for that integer in the for loop, multiply it by the number parameter given and set this as the 
            value for the i-1 index
        After the for loop is over, return the list of doubles.
        */

        double[] multiples = [];
        for (int i = 1; i <= length; i++) {
            multiples[i-1] = number * i;
        }

        return multiples; // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        /*
        Find the starting index for the last amount of items
        Using the GetRange method, get the last number of items defined by the amount parameter starting at the starting index
        Using the RemoveRange method, remove that same range from the orifgical data list.
        Using the InsertRange method, insert the list from the GetRange method into the data list starting at index 0
        */

        int i_startOfRange = data.Count - amount + 1;
        List<int> lastItems = data.GetRange(i_startOfRange, amount);
        data.RemoveRange(i_startOfRange, amount);
        data.InsertRange(0, lastItems);
    }
}
