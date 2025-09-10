using System.Collections.Generic;

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
        // Plan (step-by-step):
        // 1. Validate the input length. If length <= 0 return an empty array (defensive).
        // 2. Allocate a double[] of size 'length'.
        // 3. For each index i from 0 to length-1 compute the (i+1)-th multiple: number * (i + 1).
        //    (i+1 because the first multiple should be 'number' itself.)
        // 4. Store the computed value in result[i].
        // 5. Return the populated result array.

        if (length <= 0)
        {
            // Defensive: handle invalid input gracefully.
            return new double[0];
        }

        double[] result = new double[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
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
        // Plan (step-by-step):
        // 1. If data is null or empty, do nothing and return.
        // 2. Let n = data.Count.
        // 3. Normalize the amount with modulo: amount = amount % n (this handles amount == n).
        // 4. If normalized amount is 0, no rotation is needed — return.
        // 5. Perform an in-place rotation using the reversal trick:
        //    a) Reverse the entire list.
        //    b) Reverse the first 'amount' elements (these correspond to the original last 'amount' items).
        //    c) Reverse the remaining 'n - amount' elements.
        //    This sequence results in a right rotation by 'amount' without allocating a new list.
        // 6. Return (the list is modified in-place).

        if (data == null) return;      // nothing to do for null
        int n = data.Count;
        if (n == 0) return;            // nothing to do for empty list

        // Normalize amount to handle edge cases
        amount %= n;
        if (amount == 0) return;       // full rotation -> same list

        // In-place reversal algorithm (O(n) time, O(1) extra space)
        data.Reverse();                // reverse entire list
        data.Reverse(0, amount);       // reverse first 'amount' elements
        data.Reverse(amount, n - amount); // reverse remaining elements
    }
}
